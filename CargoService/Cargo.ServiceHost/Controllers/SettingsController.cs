using Cargo.Application.CommandHandlers.Settings;
using Cargo.Contract.Commands.Settings;
using Cargo.Contract.Commands.Settings.CommercialPayload;
using Cargo.Contract.Commands.Settings.CommPayload;
using Cargo.Contract.Queries;
using Cargo.Contract.Queries.CommPayloads;
using Cargo.Contract.Queries.Settings;
using Cargo.Contract.Queries.Settings.CommercialPayload;
using Directories.Contract.Commands.Settings.BookingRules;
using Directories.Contract.Queries.Settings.BookingRules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers
{

    [Authorize]
    [Route("Api/[controller]/V1")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<ReportController> logger;
        private readonly IMediator mediator;

        public SettingsController(IMediator mediator, ILogger<ReportController> logger)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        /// <summary>
        /// Получить список воздушных судов
        /// </summary>
        /// <param name="query"></param>
        [HttpGet(nameof(AircraftTypes))]
        public async Task<IActionResult> AircraftTypes([FromQuery] AircraftTypesQuery query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }

        /// <summary>
        /// Получить ПКЗ
        /// </summary>
        /// <param name="query"></param>
        [HttpGet(nameof(FindPkz))]
        public async Task<IActionResult> FindPkz([FromQuery] FindPkzQuery query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }

        #region CarrierSettinParam
        /// <summary>
        /// Получение списка настроек для перевозчика
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Список настроек перевозчика</returns>
        [HttpGet(nameof(CarrierSettinParam))]
        [MessageInjector]
        public async Task<IActionResult> CarrierSettinParam([FromQuery] CarrierSettinParamQuery query)
        {
            return base.Ok(await this.mediator.Send(query));
        }

        /// <summary>
        /// Изменение настройки для перевозчика
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(CarrierSettinParamSave))]
        [MessageInjector]
        public async Task<IActionResult> CarrierSettinParamSave([FromBody] SaveCarrierSettinParamCommand command)
        {
            var resultResponse = await this.mediator.Send(command);
            if (resultResponse.IsSuccess)
            {
                return base.Ok();
            }

            return base.BadRequest(resultResponse.Errors);
        }

        /// <summary>
        /// Сброс настройки для перевозчика
        /// </summary>
        /// <returns></returns>
        [HttpDelete(nameof(CarrierSettinParamRemove))]
        [MessageInjector]
        public async Task<IActionResult> CarrierSettinParamRemove([FromBody] ResetCarrierSettinParamCommand command)
        {
            return base.Ok(await this.mediator.Send(command));
        }
        #endregion

        #region MailLimits
        /// <summary>
        /// Удалить ПКЗ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteCommPayload))]
        public async Task<IActionResult> DeleteCommPayload([FromQuery] DeleteCommPayloadCommand query)
        {
            var res = await this.mediator.Send(query);
            if (res.IsSuccess)
            {
                return base.Ok();
            }
            return base.BadRequest(res.Errors);
        }
        /// <summary>
        /// Сохранить ПКЗ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost(nameof(SaveCommPayload))]
        public async Task<IActionResult> SaveCommPayload([FromBody] SaveCommPayloadCommand query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        /// <summary>
        /// Получить список ПКЗ
        /// </summary>
        /// <param name="query"></param>
        [HttpGet(nameof(CommPayload))]
        public async Task<IActionResult> CommPayload([FromQuery] CommPayloadsQuery query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        #endregion

        #region CommPayload
        /// <summary>
        /// Удалить ПКЗ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteMailLimits))]
        public async Task<IActionResult> DeleteMailLimits([FromQuery] DeleteMailLimitsCommand query)
        {
            var res = await this.mediator.Send(query);
            if (res.IsSuccess)
            {
                return base.Ok();
            }
            return base.BadRequest(res.Errors);
        }
        /// <summary>
        /// Сохранить ПКЗ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost(nameof(SaveMailLimits))]
        public async Task<IActionResult> SaveMailLimits([FromBody] SaveMailLimitsCommand query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        /// <summary>
        /// Получить список загрузки почты
        /// </summary>
        /// <param name="query"></param>
        [HttpGet(nameof(MailLimits))]
        public async Task<IActionResult> MailLimits([FromQuery] MailLimitsQuery query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        #endregion

        #region Aircraft
        /// <summary>
        /// Удалить борт
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteAircraft))]
        public async Task<IActionResult> DeleteAircraft([FromQuery] DeleteAircraftCommand query)
        {
            var res = await this.mediator.Send(query);
            if (res.IsSuccess)
            {
                return base.Ok();
            }
            return base.BadRequest(res.Errors);
        }
        /// <summary>
        /// Сохранить борт
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost(nameof(SaveAircrafts))]
        public async Task<IActionResult> SaveAircrafts([FromBody] SaveAircraftsCommand query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        /// <summary>
        /// Получить список бортов
        /// </summary>
        /// <param name="query"></param>
        [HttpGet(nameof(Aircrafts))]
        public async Task<IActionResult> Aircrafts([FromQuery] AircraftsQuery query)
        {
            var res = await this.mediator.Send(query);
            return base.Ok(res);
        }
        #endregion

    }
}