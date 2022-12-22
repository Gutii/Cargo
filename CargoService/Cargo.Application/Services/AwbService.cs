using AutoMapper;
using Cargo.Application.Services.CommercialPayload;
using Cargo.Contract.DTOs;
using Cargo.Contract.DTOs.Settings;
using Cargo.Contract.Queries.Quotas;
using Cargo.Contract.Queries.Settings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model;
using Cargo.Infrastructure.Data.Model.Settings;
using Cargo.Infrastructure.Data.Model.Tariffs;
using Directories.Application.Services.Settings;
using Directories.Contract.DTOs.Settings.BookingRules;
using IDeal.Common.Components;
using IDeal.Common.Components.Messages.ObjectStructures.Fsas;
using IDeal.Common.Components.Messages.ObjectStructures.Fwbs.Ver17;
using IDeal.Common.Messaging.Histories;
using IDeal.Common.Messaging.Messages;
using IdealResults;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cargo.Contract.DTOs.Bookings;
using Microsoft.Extensions.Options;

namespace Cargo.Application.Services
{
    public class AwbService
    {
        Regex awbPattern;
        CargoContext dbContext;
        IMapper mapper;
        IPublishEndpoint endpoint;
        ILogger<AwbService> logger;
        SettingsService commPayloadService;
        ChainSearchPkz сhainSearchPkz;
        SearchQuotas searchQuotas;
        private TelegrammService _telegrammService;
        CarrierSettingsParamService carrierSettingsParamService;
        IOptions<RatesServiceConfig> _ratesSettings;

        public AwbService(CargoContext CargoContext,
            IMapper mapper,
            IPublishEndpoint endpoint,
            ILogger<AwbService> logger,
            SettingsService commPayloaderService,
            ChainSearchPkz сhainSearchPkz,
            SearchQuotas searchQuotas,
            TelegrammService telegrammService,
            CarrierSettingsParamService carrierSettingsParamService, IOptions<RatesServiceConfig> ratesSettings)
        {
            awbPattern = new Regex(@"\b(?<acPrefix>\d{3})-(?<serialNumber>\d{8})\b");

            dbContext = CargoContext;
            this.mapper = mapper;
            this.endpoint = endpoint;
            this.logger = logger;
            commPayloadService = commPayloaderService;
            this.сhainSearchPkz = сhainSearchPkz;
            this.searchQuotas = searchQuotas;
            _telegrammService = telegrammService;
            this.carrierSettingsParamService = carrierSettingsParamService;
            _ratesSettings = ratesSettings;
        }

        public async Task<Customer> CarrierInfo(int id) => await Task.FromResult(dbContext.Carriers.AsNoTracking().FirstOrDefault(x => x.Id == id));

        public async Task<Result<Awb>> Awb(int? awbId = null, string awbIdentifier = null)
        {
            if (!string.IsNullOrEmpty(awbIdentifier))
            {
                Match m = AwbIdentifierParse(awbIdentifier);
                if (m.Success)
                {
                    var id = dbContext.Awbs
                        .AsNoTracking()
                        .Where(il => il.AcPrefix == m.Groups["acPrefix"].Value && il.SerialNumber == m.Groups["serialNumber"].Value)
                        .Select(awb => awb.Id)
                        .FirstOrDefault();
                    if (id == 0)
                        return Result.Invalid($"Накладная не найдена: awbIdentifier = {awbIdentifier}");
                    awbId ??= id;
                }
            }

            Awb awb = dbContext.Awbs
             .AsNoTracking()
             .Include(a => a.Bookings)
             .ThenInclude(b => b.FlightSchedule)
             .Include(a => a.BookingRcs)
             //.ThenInclude(b => b.FlightSchedule)
             .Include(a => a.Agent)
             .ThenInclude(c => c.SalesAgent)
             .Include(a => a.Consignee)
             .Include(a => a.Consignor)
             .Include(a => a.SizeGroups)
             .Include(a => a.Prepaid)
             .Include(a => a.Collect)
             .Include(a => a.Messages)
             .Include(a => a.OtherCharges)
             //.OrderByDescending(m=>m.)
             .FirstOrDefault(il => awbId.HasValue ? il.Id == awbId : true);
            ;
            if (awb == null)
            {
                return Result.Invalid($"Накладная не найдена: awbId = {awbId}");
            }
            awb.Messages = awb.Messages.OrderByDescending(m => m.DateCreated).ToList();

            return await Task.FromResult(Result.Ok(awb));
        }

        public Result<Awb> TrackedAwb(int? awbId = null)
        {
            try
            {
                if (awbId == null)
                {
                    Awb awb = new Awb();
                    dbContext.Add(awb);
                    return Result.Ok(awb);
                }
                else
                {
                    Awb awb = dbContext.Awbs
                     .Include(a => a.Bookings)
                     .Include(a => a.Consignee)
                     .Include(a => a.Consignor)
                     .Include(a => a.SizeGroups)
                     .Include(a => a.Prepaid)
                     .Include(a => a.Collect)
                     .FirstOrDefault(a => a.Id == awbId);

                    if (awb == null)
                    {
                        return Result.Invalid($"Накладная не найдена awbId = {awbId}");
                    }

                    return Result.Ok(awb);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Не удалось извлечь накладную для отслеживания").CausedBy(ex));
            }

        }

        public async Task<Result<AgentContractPoolAwbNums>> ReserveAsync(int? contragentId, string awbIdentifier = null, int customerId = 0)
        {
            try
            {
                //string carrierCode = "SU";

                int serialNumber = 0;
                string acPrefix = "555"; //TODO: Обязательно бросать префикс
                var m = AwbIdentifierParse(awbIdentifier);

                if (m.Success)
                {
                    serialNumber = Convert.ToInt32(m.Groups["serialNumber"].Value);
                    acPrefix = m.Groups["acPrefix"].Value;
                }

                #region Проверки

                if (customerId == 0)
                    return Result.Invalid("Не заполнен перевозчик");

                if (!string.IsNullOrEmpty(awbIdentifier) && !m.Success)
                    return Result.Invalid($"Некорректный формат накладной: {awbIdentifier}");

                if (!string.IsNullOrEmpty(awbIdentifier) && serialNumber % 10 != CheckDigit(serialNumber / 10))
                    return Result.Invalid($"Неверная контрольная цифра накладной: {awbIdentifier}");

                if (!contragentId.HasValue && !m.Success)
                    return Result.Invalid("Не заполнен код контрагента и корректный номер накладной");

                var carrier = await dbContext.Carriers.FirstOrDefaultAsync(x => x.Id == customerId && x.AcPrefix == acPrefix);
                if (carrier == null)
                    return Result.Invalid($"Не найден перевозчик с Id = {customerId} для префикса {acPrefix}");

                // if (!string.IsNullOrEmpty(acPrefix) && !dbContext.Carriers.AsNoTracking().Any(c => c.Id == customerId && c.AcPrefix == acPrefix))
                //     return Result.Invalid($"Префикс накладной {acPrefix} не соответствует перевозчику {carrier.IataCode}");

                #endregion

                serialNumber /= 10;

                var poolsQuery = dbContext
                    .PoolAwbs
                    .Include(p => p.UsedAwbNumbers)
                    .Include(p => p.Contract)
                    .ThenInclude(c => c.SaleAgent)
                    .ThenInclude(sa => sa.Carrier)
                    .Include(p => p.Contract)
                    .ThenInclude(c => c.SaleAgent)
                    .ThenInclude(sa => sa.Contragent)
                    .Where(p => p.Contract.SaleAgent.Carrier.Id == customerId)
                    .Where(p => !contragentId.HasValue || p.Contract.SaleAgent.ContragentId == contragentId)
                    .Where(p => p.Contract.DateAt <= DateTime.UtcNow.Date && (!p.Contract.DateTo.HasValue || p.Contract.DateTo.Value >= DateTime.UtcNow.Date))
                    //.AsNoTracking()
                    .AsEnumerable();

                var poolWithFreeNumber = poolsQuery.Where(p =>
                    {
                        var query = Enumerable.Range(p.StartNumber, p.PoolLenght)
                            .Except(p.UsedAwbNumbers.Select(num => num.SerialNumber));
                        if (serialNumber > 0)
                            return query.Any(num => num == serialNumber);

                        serialNumber = query.FirstOrDefault();
                        return serialNumber > 0;
                    })
                    .FirstOrDefault();

                if (poolWithFreeNumber == null)
                    return Result.Invalid("Не удалось подобрать подходящий пул номеров накладных");

                //dbContext.Attach(poolWithFreeNumber);
                var poolAwbNum = new AgentContractPoolAwbNums
                {
                    SerialNumber = serialNumber,
                    AwbPool = poolWithFreeNumber,
                    AwbPoolId = poolWithFreeNumber.Id
                };

                poolWithFreeNumber.UsedAwbNumbers.Add(poolAwbNum);
                await dbContext.SaveChangesAsync();

                return Result.Ok(poolAwbNum);

            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                return Result.Fail(new Error("Ошибка резервирования номера для накладной").CausedBy(ex));
            }
        }

        private Result<Awb> BlankAwb(AgentContractPoolAwbNums poolAwbNum = null, string awbIdentifier = null)
        {
            try
            {
                string acPrefix = null;
                string serialNumber = null;

                if (poolAwbNum != null)
                {
                    acPrefix = poolAwbNum.AwbPool?.Contract?.SaleAgent?.Carrier?.AcPrefix;
                    serialNumber = (poolAwbNum.SerialNumber * 10 + CheckDigit(poolAwbNum.SerialNumber)).ToString("D8");
                }

                if ((string.IsNullOrEmpty(acPrefix) || string.IsNullOrEmpty(serialNumber)) && !string.IsNullOrEmpty(awbIdentifier))
                {
                    Match m = AwbIdentifierParse(awbIdentifier);
                    if (m.Success)
                    {
                        acPrefix = m.Groups["acPrefix"].Value;
                        serialNumber = m.Groups["serialNumber"].Value;
                    }
                }

                if (string.IsNullOrEmpty(acPrefix) || string.IsNullOrEmpty(serialNumber))
                {
                    return Result.Fail("Некорректный формат номера накладной");
                }

                Awb newAwb = new Awb
                {
                    AcPrefix = acPrefix,
                    SerialNumber = serialNumber,
                    CarrierId = 55,
                    Destination = string.Empty,
                    Origin = string.Empty,

                    AgentId = poolAwbNum?.AwbPool?.Contract?.SaleAgentId,
                    VolumeCode = "MC",
                    VolumeAmount = 0,
                    WeightCode = "K",
                    Weight = 0,
                    ManifestDescriptionOfGoods = string.Empty,
                    ManifestDescriptionOfGoodsRu = string.Empty,
                    QuanDetShipmentDescriptionCode = "T",
                    NumberOfPieces = 0,
                    PoolAwbId = poolAwbNum?.AwbPoolId,
                    CreatedDate = DateTime.UtcNow
                };

                dbContext.Add(newAwb);
                return Result.Ok(newAwb);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("не удалось получить бланк накладной по номеру").CausedBy(ex));
            }
        }

        public async Task<Result<int>> SaveAwbAsync(AgentContractPoolAwbNums poolAwbNumber)
        {
            Result<Awb> blankAwb = BlankAwb(poolAwbNumber);
            if (blankAwb.IsSuccess)
            {
                return await SaveAwb(blankAwb.Value, StatusAwb.Draft.Value, false);
            }

            return blankAwb.ToResult<int>();
        }

        public async Task<Result<int>> SaveAwb(Awb awb, string status, bool isCarrier)
        {
            try
            {
                //CalculateTariff(awb);
                await CalculateTariffV2(awb);
            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                logger.LogError(ex, "Ошибка при расчете тарифа");
            }

            if (!awb.CarrierId.HasValue)
            {
                //Определяем перевозчика по префиксу
                var carrier =
                    await dbContext.Carriers.FirstOrDefaultAsync(
                        x => x.AcPrefix == awb.AcPrefix || x.AcMailPrefix == awb.AcPrefix);
                awb.CarrierId = carrier?.Id ?? 55;
            }

            if (status == StatusAwb.Draft.Value || string.IsNullOrEmpty(status))
                return await SaveAwbDraft(awb);

            if (status == StatusAwb.Booking.Value)
                return await SaveAwbBooked(awb, isCarrier);

            if (status == StatusAwb.Cargo.Value)
                return await SaveAwbRcs(awb);

            if (status == StatusAwb.Verified.Value)
                return await SaveAwbVerified(awb);

            return Result.Fail(new Error($"Неподдерживаемый статус накладной: {status}").CausedBy(new NotSupportedException()));
        }

        private async Task CalculateTariffV2(Awb awb)
        {
            var dto = mapper.Map<AwbDto>(awb);

            var origin = dbContext.IataLocations.FirstOrDefault(x => x.Code == awb.Origin);
            var dest = dbContext.IataLocations.FirstOrDefault(x => x.Code == awb.Destination);
            var tg = dbContext.TariffGroups
                .Include(x=>x.Airports)
                .Where(x => x.Airports.Contains(origin))
                .Select(x=>x.Code)
                .ToList();
            tg.AddRange(dbContext.TariffGroups
                .Include(x=>x.Airports)
                .Where(x => x.Airports.Contains(dest))
                .Select(x=>x.Code)
                .ToList());

            dto.TariffGroups = tg;
            //https://localhost:5201/Api/RatesCalc/V1/CalcCostTransportation
            var ratesClient = new RequestClient(_ratesSettings.Value.Url);
            var res = await ratesClient.PostRequest<AwbDto, AwbDto>("CalcCostTransportation", dto);
            //mapper.Map(awb, res);
        }

        private async Task<Result<int>> SaveAwbDraft(Awb awb)
        {
            try
            {
                awb.Status = StatusAwb.Draft.Value;
                if (awb.Bookings != null)
                    foreach (var booking in awb.Bookings)
                        booking.SpaceAllocationCode = "--";
                await dbContext.SaveChangesAsync();
                await ToHistory(awb.Id);

                return Result.Ok(awb.Id);
            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                return Result.Fail(new Error($"Не удалось сохранить накладную в статусе 'Проект' awb = {awb?.Id}").CausedBy(ex));
            }
        }

        private async Task<Result<int>> SaveAwbBooked(Awb awb, bool isCarrier)
        {
            try
            {
                ulong[] fids = awb.Bookings.Select(b => b.FlightScheduleId).ToArray();
                var flts = dbContext.FlightShedules
                    .AsNoTracking()
                    .Where(f => fids.Contains(f.Id))
                    .ToList();
                var pageDto = mapper.Map<List<FlightSheduleDto>>(flts);

                List<PkzDto> pkzs = await pkzDtos(pageDto);

                var quotaPkz = await searchQuotas.FindQuotas(mapper.Map<FindQuotasQuery>(pageDto.FirstOrDefault()));                

                if(quotaPkz != null)
                {
                    bool awbContains = quotaPkz.AwbsId.Contains(awb.Id);
                    var weight = awbContains ? quotaPkz.BusyWeight : quotaPkz.BusyWeight + awb.Weight;
                    var volume = awbContains ? quotaPkz.BusyVolume : quotaPkz.BusyVolume + awb.VolumeAmount;
                    if (quotaPkz.QuotaVolume > 0 && quotaPkz.QuotaWeight > 0)
                    {
                        if ((quotaPkz.QuotaVolume <= volume || quotaPkz.QuotaWeight <= weight)
                            && quotaPkz.IsHardAllotment && awb.AgentId == quotaPkz.AgentId)
                        {
                            return Result.Fail(new Error($"Не удалось сохранить накладную - лимит загрузки квоты превышен awb = {awb?.Id}").CausedBy(new NotSupportedException()));
                        }
                    }
                }


                //Допустимые размеры
                // var types = awb.Bookings.Select(x => x.FlightSchedule.AircraftType).ToArray();
                // var aircraftSizes = dbContext.AircraftSizes.Where(x =>
                //     types.Contains(x.AircraftType)).OrderBy(x=>x.H).ToList();

                var contragent = dbContext.Contragents
                    .AsNoTracking()
                    .Include(c => c.Carrier)
                    .Select(c => new { c.Id, IsCarrier = c.Carrier != null }).FirstOrDefault(c => c.Id == awb.AgentId);

                Awb origAwb = dbContext.Awbs.AsNoTracking()
                    .Include(a => a.Bookings)
                    .FirstOrDefault(a => a.Id == awb.Id) ?? new Awb();

                bool isChanged = origAwb.Bookings.FullJoin(awb.Bookings,
                    b => b.Id,
                    b => false,
                    b => false,
                    (b1, b2)
                        => Math.Abs(b1.Weight - b2.Weight) < 0.001M && Math.Abs(b1.VolumeAmount - b2.VolumeAmount) < 0.001M && b1.NumberOfPieces == b2.NumberOfPieces && b1.SpaceAllocationCode == b2.SpaceAllocationCode)
                    .Any(it => !it);
                awb.Status = StatusAwb.Booking.Value;

                if (!contragent.IsCarrier)
                {
                    dbContext
                       .FlightShedules
                       .AsNoTracking()
                       .Include(f => f.Bookings)
                       .ThenInclude(f => f.FlightSchedule)
                       .Select(f => new
                       {
                           f.Id,
                           f.AircraftType,
                           f.SaleState,
                           Weigth = f.Bookings.Where(b => awb.Id == b.AwbId).Sum(b => b.Weight),
                           Volume = f.Bookings.Where(b => awb.Id == b.AwbId).Sum(b => b.VolumeAmount),
                           Sac = f.Bookings.Where(b => awb.Id == b.AwbId).Select(b => b.SpaceAllocationCode).FirstOrDefault(),
                           FlightWeigth = f.Bookings.Where(b => b.SpaceAllocationCode == "KK" && awb.Id != b.AwbId).Sum(b => b.Weight),
                           FlightVolume = f.Bookings.Where(b => b.SpaceAllocationCode == "KK" && awb.Id != b.AwbId).Sum(b => b.VolumeAmount)
                       })
                       .Where(f => awb.Bookings.Select(b => b.FlightScheduleId).Contains(f.Id))
                       .AsEnumerable()
                       .Join(awb.Bookings, f => f.Id, b => b.FlightScheduleId, async (old, b) =>
                       {
                           if (isCarrier)
                               return Task.FromResult(b);

                           if (old.Sac == "KK" && awb.Weight <= old.Weigth && awb.VolumeAmount <= old.Volume)
                               return Task.FromResult(b);

                           ICollection<CarrierParametersSettingsDto> carrierSettingsParam = null;

                           bool rulesFollowedPKZ = true;
                           if (awb.CarrierId != null)
                           {
                               carrierSettingsParam = await carrierSettingsParamService.CarrierSettingsParamAsync((int)awb.CarrierId);
                               rulesFollowedPKZ = carrierSettingsParamService.RulesFollowedPKZ(carrierSettingsParam, old.Weigth, old.Volume, awb.Weight, awb.VolumeAmount);
                           }
                            

                           decimal weight = old.FlightWeigth + b.Weight;
                           decimal volume = old.FlightVolume + b.VolumeAmount;

                           //var pkzs = await сhainSearchPkz.FindPkz(new FindPkzQuery{AircraftType = old.AircraftType});
                           var pkz = pkzs.FirstOrDefault(p => p.AircraftType == old.AircraftType);

                           if (weight <= pkz?.CommPayloadWeight
                               && volume <= pkz?.CommPayloadVolume
                               && old.SaleState != FlightSaleState.Closed
                               && rulesFollowedPKZ
                              )
                           {
                               b.SpaceAllocationCode = "KK";
                           }
                           else
                           {
                               b.SpaceAllocationCode = "NN";
                           }

                           //Проверка размеров
                           // var sizes = aircraftSizes
                           //     .Where(x => x.AircraftType == old.AircraftType)
                           //     .OrderBy(x=>x.H)
                           //     .ThenBy(x=>x.W)
                           //     .ToList();
                           // foreach (var sizeGroup in awb.SizeGroups)
                           // {
                           //     var size = sizes.FirstOrDefault(x => x.H > sizeGroup.Height && x.W > sizeGroup.Width);
                           //     if (size == null || size.L < sizeGroup.Lenght)
                           //         b.SpaceAllocationCode = "NN";
                           // }

                           return Task.FromResult(b);
                       })
                       .ToList();
                }

                await dbContext.SaveChangesAsync();
                await ToHistory(awb.Id);
                if (dbContext.ChangeLog.Any() && awb.Bookings.All(b => b.SpaceAllocationCode == "KK"))
                //await fsaService.SendFsaBkd(awb.Id);
                {
                    awb = dbContext.Awbs
                        .AsNoTracking()
                        .Include(a => a.Bookings)
                        .ThenInclude(b => b.FlightSchedule)
                        .Single(a => a.Id == awb.Id);

                    // Добавление email аэропорта из TelexSettings
                    //var email = string.Empty;
                    //var il = dbContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == awb.Origin);
                    //var ts = dbContext.TelexSettings.AsNoTracking().Where(s => s.IataLocationId == il.Id && s.Type == "FSA (BKD)").ToList();
                    //ts.ForEach(s => { email = string.Concat(email, ";", s.Emails); });

                    var il = dbContext.IataLocations.AsNoTracking().FirstOrDefault(s => s.Code == awb.Origin);
                    var fsa = mapper.Map<Fsa>(awb);
                    await endpoint.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Fsa = fsa, LinkedObjectId = awb.Id, CustomerId = 55, IataLocationId = il?.Id });
                }

                return Result.Ok(awb.Id);
            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                return Result.Fail(new Error($"Не удалось сохранить накладную в статусе 'Бронирование' awb = {awb?.Id}").CausedBy(ex));
            }
        }

        #region Pkz
        /// <summary>
        /// Получение pkz с проверкой есть ли CGO
        /// </summary>
        /// <returns></returns>
        public async Task<List<PkzDto>> pkzDtos(List<FlightSheduleDto> pageDto)
        {
            List<PkzDto> pkzs = new List<PkzDto>();
            foreach (var item in pageDto)
            {
                PkzDto pkzDto;
                if (item.PayloadVolume == 0 && item.PayloadWeight == 0)
                {
                    pkzDto = await сhainSearchPkz.FindPkz(mapper.Map<FindPkzQuery>(item));
                }
                else
                {
                    pkzDto = new PkzDto()
                    {
                        AircraftType = item.AircraftType,
                        CommPayloadVolume = Convert.ToDecimal(item.PayloadVolume), //decimal.Parse(item.PayloadVolume.ToString()),
                        CommPayloadWeight = Convert.ToDecimal(item.PayloadWeight) //decimal.Parse(item.PayloadWeight.ToString())
                    };
                }
                pkzs.Add(pkzDto);
            }
            return pkzs;
        }
        #endregion


        private async Task<Result<int>> SaveAwbRcs(Awb awb)
        {
            try
            {
                if (awb.Status == StatusAwb.Cargo.Value)
                    return Result.Ok(awb.Id);

                if (awb.Bookings.Any(booking => booking.SpaceAllocationCode != "KK" || booking.SpaceAllocationCode != "CA"))
                    return Result.Fail(
                        new Error($"Не удалось сохранить накладную в статусе 'Груз сдан' awb = {awb.Id}"));

                awb.Status = StatusAwb.Cargo.Value;
                dbContext.BookingRcs.AddRange(mapper.Map<ICollection<BookingRcs>>(awb.Bookings));

                await dbContext.SaveChangesAsync();
                await ToHistory(awb.Id);


                return Result.Ok(awb.Id);
            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                return Result.Fail(new Error($"Не удалось сохранить накладную в статусе 'Груз сдан' awb = {awb?.Id}").CausedBy(ex));
            }
        }

        private async Task<Result<int>> SaveAwbVerified(Awb awb)
        {
            try
            {
                if (awb.Status != StatusAwb.Verified.Value)
                {
                    awb.Status = StatusAwb.Verified.Value;
                    await dbContext.SaveChangesAsync();
                    await ToHistory(awb.Id);

                    awb = dbContext.Awbs
                        .AsNoTracking()
                        .Include(a => a.Bookings)
                        .ThenInclude(b => b.FlightSchedule)
                        .Include(a => a.Agent)
                        .ThenInclude(c => c.SalesAgent)
                        .FirstOrDefault(w => w.Id == awb.Id);

                    if (awb.Agent?.SalesAgent != null)
                    {
                        awb.Agent.SalesAgent = awb.Agent.SalesAgent.Where(sa => sa.CarrierId == awb.CarrierId).ToList();
                    }

                    var fwb = mapper.Map<Fwb>(awb);

                    await endpoint.Publish<BuildMessage>(new { __CorrelationId = Guid.NewGuid(), Fwb = fwb, LinkedObjectId = awb.Id, CustomerId = 55 });
                }
                else
                {
                    await dbContext.SaveChangesAsync();
                    await ToHistory(awb.Id);
                }

                return Result.Ok(awb.Id);
            }
            catch (Exception ex)
            {
                await _telegrammService.SendError(ex);
                return Result.Fail(new Error($"Не удалось сохранить накладную в статусе 'Валидация' awb = {awb?.Id}").CausedBy(ex));
            }
        }

        private async Task ToHistory(int awbId)
        {
            try
            {
                await endpoint.Publish<SendHistory>(new
                {
                    __CorrelationId = Guid.NewGuid(),
                    HistoryCode = "SAVE_AWB",
                    DateModifed = DateTime.UtcNow,
                    Description = "Сохранение накладной",
                    DescriptionEng = "Save awb",
                    LinkedAwbId = awbId,
                    ChangeLog = mapper.Map<List<Change>>(dbContext.ChangeLog),
                    CustomerId = 55
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Не удалось отправить изменения в History");
            }
        }

        private int CheckDigit(int awbSerialNumber)
        {
            int step1 = awbSerialNumber / 7;
            int step2 = step1 * 7;
            int expectedCheckDigit = awbSerialNumber - step2;
            return expectedCheckDigit;
        }

        Match AwbIdentifierParse(string awbIdentifier)
        {
            return awbPattern.Match(awbIdentifier ?? string.Empty);
        }

        #region Tariffs
        private async void CalculateTariff(Awb awb)
        {
            var origin = awb.Origin;
            var destination = awb.Destination;
            var product = awb.Product;

            var tariffSolution = dbContext.TariffSolutions
                .AsNoTracking()
                .Include(x => x.AwbOriginAirport)
                .Include(x => x.AwbDestinationAirport)
                .Include(x => x.AwbOriginTariffGroup)
                .Include(x => x.AwbDestinationTariffGroup)
                .Include(x => x.TransitAirport)
                .Include(x => x.RateGridRankValues)
                .Include(x => x.Addons)
                .ThenInclude(x => x.ShrCodes)
                .FirstOrDefault(x =>
                    x.ContragentId == awb.CarrierId &&
                    (x.AwbOriginAirport.Code == origin || x.AwbOriginTariffGroup.Airports.Any(y => y.Code == origin))
                    && (x.AwbDestinationAirport.Code == destination || x.AwbDestinationTariffGroup.Airports.Any(y => y.Code == destination)));

            if (tariffSolution == null)
            {
                return;
            }

            awb.TariffsSolutionCode = tariffSolution.Code;
            awb.AllIn = tariffSolution.IsAllIn;
            awb.PaymentProcedure = tariffSolution.PaymentTerms;
            awb.WeightCharge = tariffSolution.WeightCharge;
            awb.SalesChannel = tariffSolution.SalesChannel;

            decimal weightToCharge = 0;

            if (tariffSolution.WeightCharge == "CHARGEABLE")   //  CHARGEABLE / ACTUAL
                weightToCharge = awb.ChargeWeight;
            else
                weightToCharge = awb.Weight;

            var total = 0m;
            var baseTariffRate = 0m;

            var tariffNet = tariffSolution.RateGridRankValues.OrderBy(x => x.Rank).ToArray();
            if (tariffNet.Length > 0)
            {
                RateGridRankValue selectedRank = null;

                for (int i = 0; i < tariffNet.Length; i++)
                {
                    if (tariffNet[i].Rank <= weightToCharge)
                        selectedRank = tariffNet[i];
                    else
                        break;
                }

                if (selectedRank != null)
                {
                    baseTariffRate = selectedRank.Value;
                    total = weightToCharge * baseTariffRate;
                }
            }

            var minTariff = tariffSolution.MinTariff;
            var tariffClass = "Q";
            string addOnCode = null;

            bool isMinimum = false;
            if (total < minTariff)
            {
                isMinimum = true;
                tariffClass = "M";
                baseTariffRate = minTariff;
                total = minTariff;
            }

            var finalTariffRate = baseTariffRate;

            TariffAddon addon = FindAddon(awb, tariffSolution, isMinimum);

            if (addon != null)
            {
                decimal addonValue = 0m;

                if (isMinimum)
                {
                    addonValue = addon.MinimumAddon;
                    addOnCode = $"M{addonValue}";
                }
                else
                {
                    addonValue = addon.WeightAddon;
                    addOnCode = $"Q{addonValue}";
                }

                if (addonValue > 100)
                {
                    tariffClass = "S";
                }
                else
                {
                    tariffClass = "R";
                }

                finalTariffRate = finalTariffRate * addonValue / 100;
                total = total * addonValue / 100;
            }

            awb.TariffClass = tariffClass;
            awb.BaseTariffRate = baseTariffRate;
            awb.AddOn = addOnCode;
            awb.TariffRate = finalTariffRate;
            awb.Total = total;

            FillPrepaidCollectFields(awb, tariffSolution, total);

            if (awb.OtherCharges == null)
                awb.OtherCharges = new List<OtherCharge>();

            var charges = FindCarrierCharges(awb);

            if (charges != null && charges.Count > 0)
            {

                foreach (var charge in charges)
                {
                    var binding = charge.CarrierChargeBindings.FirstOrDefault(x => x.Airports.Any(a => a.Code == origin));

                    if (binding != null)
                    {
                        var value = 0.0M;

                        switch (binding.Parameter)
                        {
                            case "AWB":
                                value = binding.Value;
                                break;
                            case "ACTUAL_WEIGHT":
                                value = awb.Weight * binding.Value;
                                break;
                            case "CHARGE_WEIGHT":
                                value = awb.ChargeWeight * binding.Value;
                                break;
                            case "PERCENT":
                                value = total * binding.Value / 100;
                                break;
                        }

                        var otherCharge = awb.OtherCharges.FirstOrDefault(x => x.TypeCharge == charge.Code && x.CA == charge.Recepient);

                        if (otherCharge == null)
                        {
                            otherCharge = new OtherCharge
                            {
                                TypeCharge = charge.Code,
                                CA = charge.Recepient,
                            };

                            awb.OtherCharges.Add(otherCharge);
                        }

                        otherCharge.Prepaid = value;
                    }
                }
            }
        }

        private List<CarrierCharge> FindCarrierCharges(Awb awb)
        {
            var origin = awb.Origin;

            var allChargedForOriginAirport = dbContext.CarrierCharges
                .AsNoTracking()
                .Include(x => x.IncludedShrCodes)
                .Include(x => x.ExcludedShrCodes)
                .Include(x => x.IncludedProducts)
                .Include(x => x.ExcludedProducts)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Currency)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Country)
                .Include(x => x.CarrierChargeBindings)
                .ThenInclude(x => x.Airports)
                .Where(x => x.CarrierChargeBindings.Any(b => b.Airports.Any(a => a.Code == origin)))
                .ToList();

            // Если доп сборов нет
            if (allChargedForOriginAirport.Count == 0)
            {
                return allChargedForOriginAirport;
            }

            // Если все доп сборы обязательные
            if (allChargedForOriginAirport.All(x => x.ApplicationType == "M"))
            {
                return allChargedForOriginAirport;
            }

            List<CarrierCharge> matchedCharges = new List<CarrierCharge>(allChargedForOriginAirport.Count);

            // Все обязательные доп сборы
            matchedCharges.AddRange(allChargedForOriginAirport
                .Where(x => x.ApplicationType == "M"));

            /*
            matchedCharges.AddRange(allChargedForOriginAirport
                .Where(x => x.ApplicationType == "СM" 
                        && x.IncludedProducts.Any(p => p.Trigger == awb.Product) 
                        && x.ExcludedProducts.All(p => p.Trigger != awb.Product)
                        && x.ExcludedProducts.All(p => p.Trigger != awb.Product)));
            */

            return matchedCharges;
        }

        private static void FillPrepaidCollectFields(Awb awb, TariffSolution tariffSolution, decimal total)
        {
            if (tariffSolution.PaymentTerms == "PREPAID")
            {
                // Prepaid
                if (awb.Prepaid == null)
                    awb.Prepaid = new TaxCharge();

                awb.Prepaid.Charge = total;
                awb.Prepaid.ValuationCharge = 0;
                awb.Prepaid.Tax = 0;
                awb.Prepaid.TotalOtherChargesDueAgent = 0;
                awb.Prepaid.TotalOtherChargesDueCarrier = 0;
                awb.Prepaid.Total = total;

                // Collect = 0
                if (awb.Collect != null)
                {
                    awb.Collect.Charge = 0;
                    awb.Collect.ValuationCharge = 0;
                    awb.Collect.Tax = 0;
                    awb.Collect.TotalOtherChargesDueAgent = 0;
                    awb.Collect.TotalOtherChargesDueCarrier = 0;
                    awb.Collect.Total = 0;
                }
            }
            else
            {
                // Collect
                if (awb.Collect == null)
                    awb.Collect = new TaxCharge();

                awb.Collect.Charge = total;
                awb.Collect.ValuationCharge = 0;
                awb.Collect.Tax = 0;
                awb.Collect.TotalOtherChargesDueAgent = 0;
                awb.Collect.TotalOtherChargesDueCarrier = 0;
                awb.Collect.Total = total;

                // Prepaid = 0
                if (awb.Prepaid != null)
                {
                    awb.Prepaid.Charge = 0;
                    awb.Prepaid.ValuationCharge = 0;
                    awb.Prepaid.Tax = 0;
                    awb.Prepaid.TotalOtherChargesDueAgent = 0;
                    awb.Prepaid.TotalOtherChargesDueCarrier = 0;
                    awb.Prepaid.Total = 0;
                }
            }
        }

        private TariffAddon FindAddon(Awb awb, TariffSolution tariffSolution, bool isMinimum)
        {
            TariffAddon result = null;

            // Если есть надбавки в тарифе и SHR в накладной
            if (tariffSolution.Addons != null && tariffSolution.Addons.Any() && !string.IsNullOrEmpty(awb.SpecialHandlingRequirements))
            {

                var awbShrs = awb.SpecialHandlingRequirements.Split("/", StringSplitOptions.RemoveEmptyEntries);
                if (awbShrs.Length > 0)
                {
                    var addons = tariffSolution.Addons
                        .Where(x => x.ShrCodes.Any(y => awbShrs.Contains(y.Code)))
                        .ToList();

                    if (addons.Any())
                    {
                        if (addons.Count == 1)
                        {
                            result = addons[0];
                        }
                        else
                        {
                            result = addons.OrderByDescending(x => isMinimum ? x.MinimumAddon : x.WeightAddon).First();
                        }
                    }
                }
            }

            return result;
        }
        #endregion
    }

    public class RatesServiceConfig
    {
        public string Url { get; set; }
    }
}
