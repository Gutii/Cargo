using Cargo.Infrastructure.Data.Configurations;
using Cargo.Infrastructure.Data.Configurations.Dictionary;
using Cargo.Infrastructure.Data.Configurations.MyFlights;
using Cargo.Infrastructure.Data.Configurations.Quotas;
using Cargo.Infrastructure.Data.Configurations.Rates;
using Cargo.Infrastructure.Data.Configurations.Schedule;
using Cargo.Infrastructure.Data.Configurations.Settings;
using Cargo.Infrastructure.Data.Configurations.Settings.BookingRules;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Data.Model.Dictionary;
using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Cargo.Infrastructure.Data.Model.MessageSettings;
using Cargo.Infrastructure.Data.Model.Quotas;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using Cargo.Infrastructure.Data.Model.Tariffs;
using Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Infrastructure.Data
{
    public class CargoContext : DbContext
    {
        public CargoContext(DbContextOptions<CargoContext> options) : base(options)
        {
        }
        public virtual DbSet<TelexSetting> TelexSettings { get; set; }
        public virtual DbSet<MessageIdentifier> MessageIdentifiers { get; set; }
        public virtual DbSet<MessageProperty> MessagesProperties { get; set; }

        public List<ChangeTrack> ChangeLog { get; set; }

        public virtual DbSet<Awb> Awbs { get; set; }
        public DbSet<AwbContragent> AwbContragents { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRcs> BookingRcs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ConsignmentStatus> ConsignmentStatuses { get; set; }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<IataLocation> IataLocations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Shr> Shrs { get; set; }
        public DbSet<Contragent> Contragents { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentContract> AgentContracts { get; set; }
        public virtual DbSet<AgentContractPoolAwb> PoolAwbs { get; set; }
        public virtual DbSet<Customer> Carriers { get; set; }

        #region Flights schedule
        public virtual DbSet<FlightShedule> FlightShedules { get; set; }

        #endregion
        public DbSet<ParametersSettings> ParametersSettings { get; set; }
        public DbSet<CarrierSettings> CarrierSettings { get; set; }
        public DbSet<RateGridHeader> RateGrids { get; set; }
        public DbSet<RateGridRank> RateGridRanks { get; set; } //may be excluded from this root in the future
        public DbSet<TransportProduct> TransportProducts { get; set; }

        public CargoContext() : base() { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<TariffGroup> TariffGroups { get; set; }

        public DbSet<TariffSolution> TariffSolutions { get; set; }
        public DbSet<RateGridRankValue> RateGridRankValues { get; set; }
        public DbSet<TariffAddon> TariffAddons { get; set; }
        public DbSet<IataCharge> IataCharges { get; set; }

        public DbSet<CarrierCharge> CarrierCharges { get; set; }
        public DbSet<CarrierChargeBinding> CarrierChargeBindings { get; set; }

        #region Commercial payload
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<CommPayloadAt> CommPayloads { get; set; }
        public DbSet<MailLimits> MailLimits { get; set; }
        public DbSet<CommPayload> CommercialPayloads { get; set; }
        public DbSet<CommPayloadNode> PayloadNode { get; set; }
        public DbSet<CommPayloadRule4AicraftType> CommPayloadRule4AicraftTypes { get; set; }
        public DbSet<CommPayloadRule4Carrier> CommPayloadRule4Carriers { get; set; }
        public DbSet<CommPayloadRule4Route> CommPayloadRule4Routes { get; set; }
        public DbSet<CommPayloadRule4Flight> CommPayloadRule4Flights { get; set; }
        #endregion

        public DbSet<AircraftSize> AircraftSizes { get; set; }
        #region Quotas
        public DbSet<QuotasCorrect> QuotasCorrect { get; set; }
        public DbSet<QuotasOperative> QuotasOperative { get; set; }
        public DbSet<QuotasDirectory> QuotasDirectory { get; set; }
        #endregion
        #region MyFlight
        public DbSet<MyFlight> MyFlights { get; set; }
        public DbSet<MyFlightNumbers> MyFlightNumbers { get; set; }
        public DbSet<MyFlightRoute> MyFlightRoutes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AwbConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new BookingRcsConfiguration());
            modelBuilder.ApplyConfiguration(new AwbContragentConfiguration());
            modelBuilder.ApplyConfiguration(new SizeGroupConfigutaion());
            modelBuilder.ApplyConfiguration(new OtherChargeConfiguration());
            modelBuilder.ApplyConfiguration(new TaxChargeConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfigutaion());
            modelBuilder.ApplyConfiguration(new ConsignmentStatusConfiguration());


            modelBuilder.ApplyConfiguration(new IataLocationConfigutaion());
            modelBuilder.ApplyConfiguration(new CountryConfigutaion());
            modelBuilder.ApplyConfiguration(new AirlineConfigutaion());
            modelBuilder.ApplyConfiguration(new ShrConfigutaion());
            modelBuilder.ApplyConfiguration(new AgentConfigutaion());
            modelBuilder.ApplyConfiguration(new ContragentConfigutaion());
            modelBuilder.ApplyConfiguration(new ContactDetailConfigutaion());
            modelBuilder.ApplyConfiguration(new ContactInformationConfigutaion());
            modelBuilder.ApplyConfiguration(new IataLocationExtInfoConfigutaion());
            modelBuilder.ApplyConfiguration(new SlaProhibitionConfigutaion());
            modelBuilder.ApplyConfiguration(new SlaTimeLimitationConfigutaion());
            modelBuilder.ApplyConfiguration(new TelexSettingConfigutaion());
            modelBuilder.ApplyConfiguration(new AirportContactInformationConfigutaion());
            modelBuilder.ApplyConfiguration(new CurrencyConfigutaion());
            modelBuilder.ApplyConfiguration(new TariffGroupConfigutaion());
            modelBuilder.ApplyConfiguration(new CarrierConfiguration());

            modelBuilder.ApplyConfiguration(new TariffSolutionConfiguration());
            modelBuilder.ApplyConfiguration(new RateGridRankValueConfiguration());
            modelBuilder.ApplyConfiguration(new TariffAddonConfiguration());
            modelBuilder.ApplyConfiguration(new IataChargeConfiguration());
            modelBuilder.ApplyConfiguration(new CarrierChargeConfiguration());
            modelBuilder.ApplyConfiguration(new CarrierChargeBindingConfiguration());

            modelBuilder.ApplyConfiguration(new FlightSheduleConfiguration());

            #region Agents contracts
            modelBuilder.ApplyConfiguration(new AgentContractConfiguration());
            modelBuilder.ApplyConfiguration(new AgentContractPoolAwbConfiguration());
            modelBuilder.ApplyConfiguration(new AgentContractPoolAwbNumsConfiguration());
            #endregion

            #region Commercial payload
            modelBuilder.ApplyConfiguration(new AircraftTypeConfigutaion());
            modelBuilder.ApplyConfiguration(new MailLimitsConfiguration());
            modelBuilder.ApplyConfiguration(new AircraftConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadAtConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadNodeConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadRule4AicraftTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadRule4CarrierConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadRule4RouteConfiguration());
            modelBuilder.ApplyConfiguration(new CommPayloadRule4FlightConfiguration());
            #endregion

            modelBuilder.ApplyConfiguration(new AircraftSizeConfiguration());

            #region MyFlight
            modelBuilder.ApplyConfiguration(new MyFlightConfigutaion());
            modelBuilder.ApplyConfiguration(new MyFlightNumbersConfigutaion());
            modelBuilder.ApplyConfiguration(new MyFlightRouteConfigutaion());
            #endregion


            modelBuilder.ApplyConfiguration(new RateGridHeaderConfiguration());
            modelBuilder.ApplyConfiguration(new RateGridRankConfiguration());

            modelBuilder.ApplyConfiguration(new QuotasOperativeConfiguration());
            modelBuilder.ApplyConfiguration(new QuotasDirectoryConfiguration());
            modelBuilder.ApplyConfiguration(new QuotasCorrectConfiguration());

            modelBuilder.ApplyConfiguration(new CarrierSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new ParametersSettingsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.InitListChangeTracks();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            this.InitListChangeTracks();

            return base.SaveChanges();
        }

        private void InitListChangeTracks()
        {
            this.ChangeLog = new List<ChangeTrack>();
            var changeTrack = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
            foreach (var entry in changeTrack)
            {
                if (entry.Entity != null)
                {
                    string entityName = entry.Entity.GetType().Name;
                    string state = entry.State.ToString();
                    var props = entry.Properties;
                    if (entry.State == EntityState.Added)
                    {
                        ChangeLog.AddRange(props.Where(p => !p.Metadata.IsPrimaryKey())
                        .Where(p => !Equals(p.CurrentValue, default))
                        .Select(p => new ChangeTrack { State = state, ObjectName = entityName, PropertyName = p.Metadata.Name, OldValue = null, NewValue = p.CurrentValue?.ToString() }));
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var pk = props.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                        ChangeLog.AddRange(props.Where(p => !p.Metadata.IsPrimaryKey())
                        .Where(p => !Equals(p.CurrentValue, p.OriginalValue))
                        .Select(p => new ChangeTrack { State = state, ObjectName = entityName, ObjectId = pk.CurrentValue.ToString(), PropertyName = p.Metadata.Name, OldValue = p.OriginalValue?.ToString(), NewValue = p.CurrentValue?.ToString() }));
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        var pk = props.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                        ChangeLog.Add(new ChangeTrack { State = state, ObjectName = entityName, ObjectId = pk.CurrentValue.ToString(), PropertyName = pk.Metadata.Name });
                    }
                }
            }
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CargoContext>
    {
        public CargoContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Cargo.ServiceHost/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<CargoContext>();
            var connectionString = configuration.GetConnectionString("MySqlConnection");
            builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion);
            return new CargoContext(builder.Options);
        }
    }
}
