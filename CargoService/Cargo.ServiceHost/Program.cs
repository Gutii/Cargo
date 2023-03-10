using AutoMapper.EquivalencyExpression;
using Cargo.Application.Automapper;
using Cargo.Application.CommandHandlers;
using Cargo.Application.Consumers;
using Cargo.Application.Parsers;
using Cargo.Application.Services;
using Cargo.Application.Services.CommercialPayload;
using Cargo.Application.Services.CommPayload;
using Cargo.Application.Services.PoolAwbNum;
using Cargo.Application.Services.Reports;
using Cargo.Application.Services.Settings;
using Cargo.Application.Validation;
using Cargo.Contract.Commands;
using Cargo.Infrastructure.Data;
using Cargo.ServiceHost.PipelineBehaviors;
using Directories.Application.Services.Settings;
using FluentValidation.AspNetCore;
using IdealResults;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cargo.ServiceHost
{
    class Program
    {
        public static async Task<Int32> Main(string[] args)
        {
            try
            {
                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
                builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((HostContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false);
                    if (HostContext.HostingEnvironment.EnvironmentName == "Development")
                    {
                        config.AddJsonFile($"appsettings.{HostContext.HostingEnvironment.EnvironmentName}.json");
                    }
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSystemdConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.TimestampFormat = "|yyyy.MM.dd HH:mm:ss.fffff|";
                    });
                })
                .ConfigureServices((builderContext, services) =>
                {
                    JwtSettings bindJwtSettings = builderContext.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                    services.AddCors(options =>
                    {
                        options.AddPolicy("EnableCORS", builder =>
                        {
                            builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true)
                           .AllowCredentials().Build();
                        });
                    });

                    services.AddMvcCore();
                    services.AddSwaggerGen(c =>
                    {
                        c.ResolveConflictingActions((enumerable => enumerable.First()));
                        c.CustomSchemaIds(t => t.FullName);
                        //c.DescribeAllEnumsAsStrings();
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cargo", Version = "v1" });
                        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Cargo.ServiceHost.xml"));
                        c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                        {
                            BearerFormat = "JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                            Description = "Доступ в сервис Cargo"
                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
                    })
                        .AddControllers().AddJsonOptions(x =>
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                            ValidateIssuer = bindJwtSettings.ValidateIssuer,
                            ValidIssuer = bindJwtSettings.ValidIssuer,
                            ValidateAudience = bindJwtSettings.ValidateAudience,
                            ValidAudience = bindJwtSettings.ValidAudience,
                            RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                            ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                            ClockSkew = TimeSpan.FromDays(1),

                        };
                    });


                    //.AddControllersAsServices();
                    //.AddNewtonsoftJson();

                    //Зависимости:
                    services.Configure<RabbitMqHost>(builderContext.Configuration.GetSection(nameof(RabbitMqHost)));
                    services.Configure<JwtSettings>(builderContext.Configuration.GetSection(nameof(JwtSettings)));
                    services.Configure<YandexSettings>(builderContext.Configuration.GetSection(nameof(YandexSettings)));
                    services.Configure<TarantoolSettings>(
                        builderContext.Configuration.GetSection(nameof(TarantoolSettings)));
                    services.Configure<RatesServiceConfig>(
                        builderContext.Configuration.GetSection(nameof(RatesServiceConfig)));
                    
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageChangedConsumer>();
                        x.AddConsumer<FlightScheduleChangedConsumer>();
                        x.AddConsumer<ProcessCgoConsumer>();
                        x.AddConsumer<ProcessFfrConsumer>();
                        x.AddConsumer<ProcessFsuConsumer>();
                        x.AddConsumer<ProcessFbrConsumer>();
                        x.AddConsumer<ProcessFblEventConsumer>();
                        x.AddConsumer<ProcessFsrConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            var host = context.GetService<IOptions<RabbitMqHost>>().Value;
                            cfg.Host(host.Host, host.Port, host.VirtualHost, conf =>
                            {
                                conf.Username(host.Username);
                                conf.Password(host.Password);
                            });
                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    services.AddTransient<IResultLogger, FluentResultLogger>();
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                    services
                        .AddMediatR(typeof(SaveAwbCommand).Assembly)                // Конманды и запросы из проекта Contract
                        .AddMediatR(typeof(SaveAwbCommandHandler).Assembly);  // Обработчики команд и запросов из проекта Application
                    services
                        .AddAutoMapper(typeof(BookingProfile).Assembly)
                        .AddAutoMapper(cfg => cfg.AddCollectionMappers());

                    services.AddDbContext<CargoContext>(options =>
                    {
                        options.UseMySql(builderContext.Configuration.GetConnectionString("MySqlConnection"),
                                MySqlServerVersion.LatestSupportedServerVersion, optionsBuilder =>
                                {
                                    optionsBuilder.EnableRetryOnFailure();
                                });
                    });

                    //    services.AddControllers().AddJsonOptions(x =>
                    //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                    services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SaveAwbCommandValidator>());
                    // services.AddQuartz(q =>
                    // {
                    //     q.UseMicrosoftDependencyInjectionJobFactory();
                    //     q.AddJobAndTrigger<SendFblsJob>(builderContext.Configuration);
                    // });
                    //services.AddQuartzHostedService( q=> q.WaitForJobsToComplete = true);ReportService
                    services.AddTransient<ReportService, ReportService>();
                    services.AddTransient<AircraftsService, AircraftsService>();
                    services.AddTransient<ChainSearchPkz, ChainSearchPkz>();
                    services.AddTransient<SearchQuotas, SearchQuotas>();                    
                    services.AddTransient<CommPayloadsService, CommPayloadsService>();
                    services.AddTransient<ScheduleService, ScheduleService>();
                    services.AddTransient<SettingsService, SettingsService>();
                    services.AddTransient<AwbService, AwbService>();
                    services.AddTransient<FblService, FblService>();
                    services.AddTransient<FsaService, FsaService>();
                    services.AddTransient<CarrierSettingsParamService, CarrierSettingsParamService>();                    
                    services.AddTransient<TelegrammService, TelegrammService>();
                    services.AddTransient<MyFlightsService, MyFlightsService>();
                    services.AddTransient<MailLimitsService, MailLimitsService>();
                    services.AddSingleton<YandexApiService, YandexApiService>();
                    services.AddSingleton<PoolAwbService, PoolAwbService>();

                    Result.Setup(cfg =>
                    {
                        var logger = services.BuildServiceProvider().GetService<IResultLogger>();
                        cfg.Logger = logger;
                    });
                });

                WebApplication app = builder.Build();

                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseCors("EnableCORS");

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JwtAuth v1");
                    //c.RoutePrefix = string.Empty;
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.Run();

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                return 1;
            }
        }
    }

    class RabbitMqHost
    {
        public string Host { get; set; }

        public ushort Port { get; set; }

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }



    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}
