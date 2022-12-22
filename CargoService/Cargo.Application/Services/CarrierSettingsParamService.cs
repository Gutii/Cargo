using Directories.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdealResults;
using Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps;
using Directories.Contract.DTOs.Settings.BookingRules;
using Directories.Contract.Commands.Settings.BookingRules;
using Cargo.Infrastructure.Data;
using IDeal.Common.Components;
using Cargo.Contract.DTOs;

namespace Directories.Application.Services.Settings
{
    public enum IdPatametersSettings
    {
        weightForMinCheck = 1,
        volumeForMinCheck,
        weightFromOriginal,
        volumeFromOriginal,
        weightFromOrigNotRemain,
        volumeFromOrigNotRemain,
        weightFromOrigPercent,
        volumeFromOrigPercent
    }


    public class CarrierSettingsParamService
    {
        CargoContext dbContext;
        RequestClient requestClient;
        public CarrierSettingsParamService(CargoContext dbContext)
        {
            this.dbContext = dbContext;
            requestClient = new RequestClient("http://185.105.226.107:49500");
        }

        public async Task<CarrierParametersSettingsDto> ResetCarrierSettinParam(ResetCarrierSettinParamCommand request)
        {

            var carrier = await requestClient.GetRequest<CarrierDto>($"/Api/WithoutAuthorization/V1/Carrier?CarrierId={request.CarrierId}");

            var task = Task.Run(() =>
            {
                var CarrierSettings = dbContext.CarrierSettings
                .Include(e => e.ParametersSettings)
                .Where(c => c.CarrierId == carrier.Id &&
                request.CarrierParametersSettingsCharge.ParametersSettingsId == c.ParametersSettingsId)
                .FirstOrDefault();
                if (CarrierSettings == null)
                {
                    return request.CarrierParametersSettingsCharge;
                }
                request.CarrierParametersSettingsCharge.Value = CarrierSettings.ParametersSettings.Value;
                request.CarrierParametersSettingsCharge.CarrierId = null;
                request.CarrierParametersSettingsCharge.IsDefault = "Default";
                dbContext.Remove(CarrierSettings);
                dbContext.SaveChanges();

                return request.CarrierParametersSettingsCharge;
            });
            return await task;
        }

        public Result<CarrierParametersSettingsDto> SaveCarrierSettingsParam(SaveCarrierSettinParamCommand request)
        {
            var result = new Result<CarrierParametersSettingsDto>();
            result = request.CarrierParametersSettingsCharge;
            var carrier = dbContext.Carriers
                .Where(c => c.Id == request.CustomerId)
                .FirstOrDefault();
            var CarrierSettings = dbContext.CarrierSettings
                .Where(c => c.CarrierId == carrier.Id &&
                request.CarrierParametersSettingsCharge.ParametersSettingsId == c.ParametersSettingsId)
                .FirstOrDefault();

            var paramsett = dbContext.ParametersSettings
                .Where(c => request.CarrierParametersSettingsCharge.ParametersSettingsId == c.Id)
                .FirstOrDefault();

            if (request.CarrierParametersSettingsCharge.Value == paramsett.Value)
            {
                return result;
            }

            try
            {
                Result<Type> a = Type.GetType(paramsett.ValueType);
                var obj = Convert.ChangeType(request.CarrierParametersSettingsCharge.Value.Replace('.', ','), a.Value);
                result.Value.Value = obj.ToString();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message).CausedBy(ex));
            }

            if (CarrierSettings == null)
            {
                CarrierSettings = new CarrierSettings();
                CarrierSettingsUpdate(CarrierSettings, request, carrier.Id);
                result.Value.IsDefault = "c";//carrier.IataCode;
                dbContext.CarrierSettings.Add(CarrierSettings);
            }
            else
            {
                CarrierSettingsUpdate(CarrierSettings, request, carrier.Id);
                dbContext.CarrierSettings.Update(CarrierSettings);
            }

            dbContext.SaveChanges();
            
            return result;
        }

        public void CarrierSettingsUpdate(CarrierSettings CarrierSettings, SaveCarrierSettinParamCommand request, int Id)
        {
            CarrierSettings.CarrierId = Id;
            CarrierSettings.ParametersSettingsId = request.CarrierParametersSettingsCharge.ParametersSettingsId;
            CarrierSettings.Value = request.CarrierParametersSettingsCharge.Value;

            request.CarrierParametersSettingsCharge.CarrierId = Id;
        }

        public async Task<ICollection<CarrierParametersSettingsDto>> CarrierSettingsParamAsync(int carrierId)
        {
            var carrier = await requestClient.GetRequest<CarrierDto>($"/Api/WithoutAuthorization/V1/Carrier?CarrierId={carrierId}");
            var task = Task.Run(() =>
            {
                
                var paramset = this.dbContext.ParametersSettings.AsNoTracking().ToList();
                var carrParamSett = this.dbContext.CarrierSettings
                .Where(c => (c.CarrierId == carrierId))
                .Select(p => new { p.CarrierId, p.ParametersSettingsId, p.Value, IataCode = "sd"/*sp.Carrier.IataCode*/ })
                .AsNoTracking()
                .ToList();

                var carrParSettDTO = new List<CarrierParametersSettingsDto>();
                foreach (var paramsetitem in paramset)
                {
                    var a = carrParamSett.Where(p => p.ParametersSettingsId == paramsetitem.Id).FirstOrDefault();
                    var obcarsetDTO = new CarrierParametersSettingsDto()
                    {
                        CarrierId = a == null ? null : a.CarrierId,
                        ParametersSettingsId = paramsetitem.Id,
                        Abbreviation = paramsetitem.FunctionalSection + paramsetitem.NumberGroupParameters + "-" + paramsetitem.NumberParameterOnGroup,
                        DescriptionRu = paramsetitem.DescriptionRu,
                        DescriptionEn = paramsetitem.DescriptionEn,
                        Value = a == null ? paramsetitem.Value : a.Value,
                        UnitMeasurement = paramsetitem.UnitMeasurement,
                        IsDefault = a == null ? "Default" : carrier.IataCode

                    };
                    carrParSettDTO.Add(obcarsetDTO);

                }
                return carrParSettDTO;
            }

            );
            return await task;
        }
        
        public bool RulesFollowedPKZ(ICollection<CarrierParametersSettingsDto> carrierParametersSettings, decimal weightInitial, decimal volumeInitial, decimal weight, decimal volume)
        {
            if(carrierParametersSettings == null)
                return true;

            decimal weightDifferenc = weight - weightInitial;
            decimal volumeDifferenc = volume - volumeInitial;

            decimal weightExceedingPercent = 0;
            decimal volumeExceedingPercent = 0;

            if (weightDifferenc > 0)
            {
                weightExceedingPercent = (weightDifferenc / weight) * 100;
            }

            if(volumeDifferenc > 0)
            {
                volumeExceedingPercent = (volumeDifferenc / volume) * 100;
            }

            var weightForMinCheck       = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.weightForMinCheck        ).FirstOrDefault().Value);
            var volumeForMinCheck       = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.volumeForMinCheck        ).FirstOrDefault().Value);
                                                                                                                                                                                                  
            var weightFromOriginal      = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.weightFromOriginal       ).FirstOrDefault().Value);
            var volumeFromOriginal      = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.volumeFromOriginal       ).FirstOrDefault().Value);
                                                                                                                                                                                                  
            var weightFromOrigNotRemain = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.weightFromOrigNotRemain  ).FirstOrDefault().Value);
            var volumeFromOrigNotRemain = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.volumeFromOrigNotRemain  ).FirstOrDefault().Value);
                                                                                                                                                                                                  
            var weightFromOrigPercent   = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.weightFromOrigPercent    ).FirstOrDefault().Value);
            var volumeFromOrigPercent   = decimal.Parse(carrierParametersSettings.Where(c => c.ParametersSettingsId == (int)IdPatametersSettings.volumeFromOrigPercent    ).FirstOrDefault().Value);


            if (weightForMinCheck >= weight && volumeForMinCheck >= volume)
            {
                return true;
            }
                

            if (weightFromOriginal >= weightDifferenc && volumeFromOriginal >= volumeDifferenc)
            {
                return true;
            }            

            if (weightFromOrigPercent >= weightExceedingPercent && volumeFromOrigPercent >= volumeExceedingPercent)
            {
                return true;
            }

            if (weightFromOrigNotRemain >= weightDifferenc && volumeFromOrigNotRemain >= volumeDifferenc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
