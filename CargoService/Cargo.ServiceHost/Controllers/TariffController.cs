using Cargo.Contract.Commands.Tariffs;
using Cargo.Contract.DTOs.Tariffs;
using Cargo.Contract.Queries.Tariffs;
using Cargo.Infrastructure.Data;
using Cargo.ServiceHost.Transmittable.Outgoing;
using Cargo.ServiceHost.Transmittable.Payloads;
using Cargo.ServiceHost.Transmittable.Results;
using Cargo.ServiceHost.TransportBrokers;
using IDeal.Common.Components.Paginator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers
{
    /// <summary> 
    /// Контроллер для работы с тарифами
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("Api/[controller]/V1")]
    public class TariffController : ControllerBase
    {
        private ILogger<TariffController> mLogger = null;
        private IMediator mMediator = null;
        private CargoContext mContext = null;

        public TariffController(IMediator med, ILogger<TariffController> lgr, CargoContext ctx)
        {
            this.mediator = med;
            this.logger = lgr;
            this.context = ctx;
        }

        protected ILogger<TariffController> logger
        {
            get => mLogger;
            private set => mLogger = value ?? throw new NullReferenceException("Internal error. Null reference assignment.");
        }

        protected IMediator mediator
        {
            get => mMediator;
            private set => mMediator = value ?? throw new NullReferenceException("Internal error. Null reference assignment.");
        }

        protected CargoContext context
        {
            get => mContext;
            private set => mContext = value ?? throw new NullReferenceException("Internal error. Null reference assignment.");
        }

        #region Tariff Scales        

        #region RateGridDto

        [HttpPost("addRate")]
        [ActionName("addRate")]
        [MessageInjector]
        public XmtActionResultRateGrid addRate([FromBody] RateGridDto pck)
        {
            XmtActionResultRateGrid ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                RateGridDto grd = svc.addRate(pck);

                ret = new XmtActionResultRateGrid(true, new XmtPayloadRateGrid(grd));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGrid(x);
            }

            return ret;
        }

        [HttpPost("updateRate")]
        [ActionName("updateRate")]
        [MessageInjector]
        public XmtActionResultRateGrid updateRate([FromBody] RateGridDto pck)
        {
            XmtActionResultRateGrid ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                RateGridDto grd = svc.updateRate(pck);

                ret = new XmtActionResultRateGrid(true, new XmtPayloadRateGrid(grd));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGrid(x);
            }

            return ret;
        }

        [HttpPost("deleteRate")]
        [ActionName("deleteRate")]
        [MessageInjector]
        public XmtActionResult deleteRate([FromBody] RateGridDto pck)
        {
            XmtActionResult ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                ret = new XmtActionResult(svc.deleteRate(pck));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResult(x);
            }

            return ret;
        }

        [HttpPost("getRate")]
        [ActionName("getRate")]
        [MessageInjector]
        public XmtActionResultRateGrid getRate(RateGridDto pck)
        {
            XmtActionResultRateGrid ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                RateGridDto grd = svc.getRate(pck);

                ret = new XmtActionResultRateGrid(true, new XmtPayloadRateGrid(grd));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGrid(x);
            }

            return ret;
        }

        #endregion


        [HttpPost("deleteRateById")]
        [ActionName("deleteRateById")]
        public XmtActionResult deleteRateById([FromBody] ulong gridId)
        {
            XmtActionResult ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                ret = new XmtActionResult(svc.deleteRate(gridId));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResult(x);
            }

            return ret;
        }

        [HttpPost("deleteAllRates")]
        [ActionName("deleteAllRates")]
        public XmtActionResult deleteAllRates()
        {
            XmtActionResult ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                ret = new XmtActionResult(svc.deleteAllRates());
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResult(x);
            }

            return ret;
        }

        [HttpPost("getRates")]
        [ActionName("getRates")]
        [MessageInjector]
        public XmtActionResultRateGrids getRates(RateGridQuery query)
        {
            XmtActionResultRateGrids ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                RateGridDto[] grds = svc.getRates(query);

                ret = new XmtActionResultRateGrids(true, new XmtPayloadRateGrids(grds));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGrids(x);
            }

            return ret;
        }

        [HttpPost("getRatesPage")]
        [ActionName("getRatesPage")]
        [MessageInjector]
        public XmtActionResultRateGridPage getRatesPage(RateGridPagedQuery query)
        {
            XmtActionResultRateGridPage ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                PagedResult<RateGridDto> page = svc.getRatesPage(query);

                ret = new XmtActionResultRateGridPage(true, new XmtPayloadRateGridPage(page));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGridPage(x);
            }

            return ret;
        }

        [HttpPost("getRateById")]
        [ActionName("getRateById")]
        public XmtActionResultRateGrid getRateById([FromBody] ulong gridId)
        {
            XmtActionResultRateGrid ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                RateGridDto grd = svc.getRate(gridId);

                ret = new XmtActionResultRateGrid(true, new XmtPayloadRateGrid(grd));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultRateGrid(x);
            }

            return ret;
        }

        [HttpPost("deserializeRanks")]
        [ActionName("deserializeRanks")]
        public XmtActionResultUintArray deserializeRanks([FromBody] string sText)
        {
            XmtActionResultUintArray ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                List<uint> a = svc.deserializeRanks(sText);

                ret = new XmtActionResultUintArray(true, new XmtPayloadUintArray(a));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultUintArray(x);
            }

            return ret;
        }

        [HttpPost("serializeRanks")]
        [ActionName("serializeRanks")]
        public XmtActionResultString serializeRanks([FromBody] uint[] ranks)
        {
            XmtActionResultString ret = null;

            try
            {
                RatesBroker svc = new RatesBroker(this.context);
                string spec = svc.serializeRanks(ranks);

                ret = new XmtActionResultString(true, new XmtPayloadString(spec));
            }
            catch (Exception x)
            {
                this.logger.LogError(x, null, null);
                ret = new XmtActionResultString(x);
            }

            return ret;
        }
        #endregion

        #region TariffGroup

        /// <summary>
        /// Создание или изменение Тарифных групп
        /// </summary>
        /// <param command="query">"Запрос на создание или изменение Тарифных групп"</param>
        /// <returns></returns>
        [HttpPost(nameof(TariffGroupInsertOrUpdate))]
        [MessageInjector]
        public async Task<ActionResult> TariffGroupInsertOrUpdate([FromBody] CreateOrUpdateTariffGroupCommand query)
        {
            return base.Ok(await this.mediator.Send(query));
        }

        /// <summary>
        /// Получение списка Тарифных групп
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(TariffGroups))]
        [MessageInjector]
        public async Task<ActionResult> TariffGroups([FromQuery] TariffGroupsQuery request)
        {
            return base.Ok(await this.mediator.Send(request));
        }

        /// <summary>
        /// Получение Тарифнной группы по Id
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        [HttpGet(nameof(TariffGroupById))]
        public async Task<ActionResult> TariffGroupById([FromQuery] TariffGroupByIdQuery searchQuery)
        {
            return base.Ok(await this.mediator.Send(searchQuery));
        }
        #endregion

        #region Tariff Solutions
        /// <summary>
        /// Получение списка тарифных решений
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(TariffSolutions))]
        [MessageInjector]
        public async Task<IActionResult> TariffSolutions([FromQuery] TariffSolutionsQuery request)
        {
            return base.Ok(await this.mediator.Send(request));
        }

        /// <summary>
        /// Получение тарифного решения по Id
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        [HttpGet(nameof(TariffSolutionById))]
        public async Task<IActionResult> TariffSolutionById([FromQuery] TariffSolutionByIdQuery searchQuery)
        {
            return base.Ok(await this.mediator.Send(searchQuery));
        }

        /// <summary>
        /// Создание или изменение тарифного решения
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(TariffSolutionInsertOrUpdate))]
        [MessageInjector]
        public async Task<ActionResult> TariffSolutionInsertOrUpdate([FromBody] TariffSolutionDto tariffSolutionDto)
        {
            return base.Ok(await this.mediator.Send(new CreateOrUpdateTariffSolutionCommand { TariffSolution = tariffSolutionDto }));
        }
        #endregion

        #region Carrier Charges      
        /// <summary>
        /// Получение списка сборов перевозчика
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(CarrierCharges))]
        [MessageInjector]
        public async Task<IActionResult> CarrierCharges([FromQuery] CarrierChargesQuery request)
        {
            return base.Ok(await this.mediator.Send(request));
        }

        /// <summary>
        /// Получение тарифного решения по Id
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        [HttpGet(nameof(CarrierChargeById))]
        public async Task<ActionResult<CarrierChargeDto>> CarrierChargeById([FromQuery] CarrierChargeByIdQuery searchQuery)
        {
            return await this.mediator.Send(searchQuery);
        }

        /// <summary>
        /// Создание или изменение тарифного решения
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(CarrierChargeInsertOrUpdate))]
        [MessageInjector]
        public async Task<ActionResult<CarrierChargeDto>> CarrierChargeInsertOrUpdate([FromBody] CarrierChargeDto carrierChargeDto)
        {
            return await this.mediator.Send(new CreateOrUpdateCarrierChargeCommand { CarrierCharge = carrierChargeDto });
        }
        #endregion

        /// <summary>
        /// Получение списка продуктов перевозки
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(TransportProducts))]
        [MessageInjector]
        public async Task<IActionResult> TransportProducts([FromQuery] TransportProductsQuery request)
        {
            return base.Ok(await this.mediator.Send(request));
        }

        /// <summary>
        /// Получение списка сборов ИАТА
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(IataCharges))]
        public async Task<IActionResult> IataCharges([FromQuery] IataChargesQuery request)
        {
            return base.Ok(await this.mediator.Send(request));
        }
    }
}