using Cargo.Contract.DTOs;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using Directories.Contract.DTOs.Settings.BookingRules;
using IDeal.Common.Components;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services
{
    public class SettingsService
    {

        CargoContext dbContext;
        RequestClient requestClient;
        public SettingsService(CargoContext dbContext)
        {
            this.dbContext = dbContext;
            requestClient = new RequestClient("http://185.105.226.107:49500");
        }

        public async Task<Result<ICollection<MyFlight>>> MyFlights(int carrierId)
        {
            try
            {
                ICollection<MyFlight> mfs = await this.dbContext.MyFlights
                .AsNoTracking()
                .Include(mf => mf.MyFlightsRoutes)
                .Include(mf => mf.MyFlightsNumbers)
                .Where(mf => mf.CarrierId == carrierId)
                .ToListAsync();

                return Result.Ok(mfs);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Произошла ошибка при выборе рейсов в управлении для carrierId = {carrierId}").CausedBy(ex));
            }
        }

        public async Task<Result<MyFlight>> MyFlight(int myFlightId)
        {
            try
            {
                MyFlight mf = await this.dbContext.MyFlights
                .AsNoTracking()
                .Include(mf => mf.MyFlightsRoutes)
                .Include(mf => mf.MyFlightsNumbers)
                .FirstOrDefaultAsync(mf => mf.Id == myFlightId);

                if (mf != null)
                {
                    return Result.Ok(mf);
                }

                return Result.Fail(new Error("Validation Failure").CausedBy("Рейс в управлении не найден"));
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Произошла ошибка при поиске рейса в управлении myFlightId = {myFlightId}").CausedBy(ex));
            }
        }

        public async Task<Result<IEnumerable<CommPayloadFlatDetail>>> Payloads()
        {
            var nodes = dbContext
                .PayloadNode
                .Include(pn => pn.CommercialPayload)
                .ThenInclude(cp => cp.CommPayloadRule4AicraftType)
                .Include(pn => pn.CommercialPayload)
                .ThenInclude(cp => cp.CommPayloadRule4Carrier)
                .Include(pn => pn.CommercialPayload)
                .ThenInclude(cp => cp.CommPayloadRule4Route)
                .Include(pn => pn.CommercialPayload)
                .ThenInclude(cp => cp.CommPayloadRule4Flight)
                .Include(pn => pn.Childs)
                .Include(pn => pn.Childs)
                .Include(pn => pn.Childs)
                .Include(pn => pn.Childs)
                .Include(pn => pn.Childs)
                .ToList();

            var res = nodes
            .Where(p => !p.ParentId.HasValue)
            .SelectMany(pn => pn.Descendants())
            .Select(l => l.Ancestors().Select(n => n.CommercialPayload)).Select(anc => Aggregate(anc));
            return Result.Ok(res);
        }

        public async Task<Result<CommPayloadFlatDetail>> FindPayload(string aircraftType, DateTime? flightDate)
        {
            var nodes = dbContext
            .PayloadNode
            .Include(pn => pn.CommercialPayload)
            .ThenInclude(cp => cp.CommPayloadRule4AicraftType)
            .Include(pn => pn.CommercialPayload)
            .ThenInclude(cp => cp.CommPayloadRule4Carrier)
            .Include(pn => pn.CommercialPayload)
            .ThenInclude(cp => cp.CommPayloadRule4Route)
            .Include(pn => pn.CommercialPayload)
            .ThenInclude(cp => cp.CommPayloadRule4Flight)
            .Include(pn => pn.Childs)
            .Include(pn => pn.Childs)
            .Include(pn => pn.Childs)
            .Include(pn => pn.Childs)
            .Include(pn => pn.Childs)
            .ToList();

            var res = nodes
            .Where(p => !p.ParentId.HasValue)
            .SelectMany(pn => pn.Descendants())
            .Select(l => l.Ancestors().Select(n => n.CommercialPayload)).Select(anc => Aggregate(anc)).FirstOrDefault(cp =>
            {
                return
                (string.IsNullOrEmpty(aircraftType) ? true : (cp.AircraftType ?? string.Empty) == (aircraftType ?? string.Empty))
                /*&& (string.IsNullOrEmpty(request.Carrier) ? true : (cp.Carrier ?? string.Empty) == (request.Carrier ?? string.Empty))
                && (string.IsNullOrEmpty(request.Origin) ? true : (cp.Origin ?? string.Empty) == (request.Origin ?? string.Empty))
                && (string.IsNullOrEmpty(request.Destination) ? true : (cp.Destination ?? string.Empty) == (request.Destination ?? string.Empty))
                && (string.IsNullOrEmpty(request.FlightNumber) ? true : (cp.FlightNumber ?? string.Empty) == (request.FlightNumber ?? string.Empty))*/
                && (!flightDate.HasValue ? true : (cp.DateAt ?? DateTime.MinValue) < flightDate && (cp.DateTo ?? DateTime.MaxValue) > flightDate);
            }) ?? new CommPayloadFlatDetail();

            return Result.Ok(res);
        }

        private CommPayloadFlatDetail Aggregate(IEnumerable<Cargo.Infrastructure.Data.Model.Settings.CommPayloads.CommPayload> commPayloads)
        {
            CommPayloadFlatDetail commPayloadFlat = new CommPayloadFlatDetail();
            bool first = true;
            foreach (var item in commPayloads)
            {
                if (first)
                {
                    commPayloadFlat.CommPayloadId = item.Id;
                    commPayloadFlat.Weight = item.Weight;
                    commPayloadFlat.Volume = item.Volume;
                    first = false;
                }
                if (item.CommPayloadRule4AicraftType != null)
                {
                    commPayloadFlat.AircraftType = item.CommPayloadRule4AicraftType.AircraftType;
                }
                if (item.CommPayloadRule4Carrier != null)
                {
                    commPayloadFlat.Carrier = item.CommPayloadRule4Carrier.Carrier;
                }
                if (item.CommPayloadRule4Route != null)
                {
                    commPayloadFlat.Origin = item.CommPayloadRule4Route.Origin;
                    commPayloadFlat.Destination = item.CommPayloadRule4Route.Destination;
                }
                if (item.CommPayloadRule4Flight != null)
                {
                    commPayloadFlat.FlightNumber = $"{item.CommPayloadRule4Flight.FlightCarrier}{item.CommPayloadRule4Flight.FlightNumber}";
                    commPayloadFlat.DateAt = item.CommPayloadRule4Flight.DateAt;
                    commPayloadFlat.DateTo = item.CommPayloadRule4Flight.DateTo;
                }
            }
            return commPayloadFlat;
        }

        public async Task<ICollection<CarrierParametersSettingsDto>> CarrierSettingsParamAsync(int? carrierId, string carrierCode)
        {
            var carrier = await requestClient.GetRequest<CarrierDto>($"/Api/WithoutAuthorization/V1/Carrier?CarrierId={carrierId}");
            var task = Task.Run(() =>
            {
                var paramset = this.dbContext.ParametersSettings.AsNoTracking().ToList();
                var carrParamSett = this.dbContext.CarrierSettings
                .Where(c => (c.CarrierId == carrierId))
                .Select(p => new { p.CarrierId, p.ParametersSettingsId, p.Value })
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
    }

    public class CommPayloadFlatDetail
    {
        /// <summary>
        /// Идентификатор типа ПКЗ
        /// </summary>
        public int CommPayloadId { get; set; }

        /// <summary>
        /// Тип воздушного судна
        /// </summary>
        public string AircraftType { get; set; }

        /// <summary>
        /// Перевозчик
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// Пункт отправления
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Номер рейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Начало действия
        /// </summary>
        public DateTime? DateAt { get; set; }

        /// <summary>
        /// Окончание действия
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
    }
}
