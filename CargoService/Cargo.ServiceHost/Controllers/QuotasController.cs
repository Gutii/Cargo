using AutoMapper;
using Cargo.Application.Services;
using Cargo.Contract.Commands.Quotas;
using Cargo.Contract.Queries.Quotas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers
{
    [Authorize]
    [Route("Api/[controller]/V1")]
    [ApiController]
    public class QuotasController : ControllerBase
    {
        private readonly AwbService _awbService;
        private readonly IMediator mediator;
        IMapper mapper;

        /// <summary>
        /// Работа с бронированием
        /// </summary>
        /// <param name="mediator"></param>
        public QuotasController(IMediator mediator, IMapper mapper, AwbService awbService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _awbService = awbService;
        }
        #region QuotasOperative
        /// <summary>
        /// Корректировки квот
        /// </summary>
        [HttpGet(nameof(QuotasCorrect))]
        public async Task<IActionResult> QuotasCorrect([FromQuery] QuotasCorrectQuery query)
        {
            return base.Ok(await mediator.Send(query));
        }
        /// <summary>
        /// Сохранение корректировки квоты
        /// </summary>
        [HttpPost(nameof(SaveQuotasCorrect))]
        [MessageInjector]
        public async Task<IActionResult> SaveQuotasCorrect([FromBody] SaveQuotasCorrectCommand query)
        {
            var result = await mediator.Send(query);
            if (!result.IsValid)
            {
                return base.BadRequest(result.Errors);
            }
            return base.Ok(result.Value);
        }
        #endregion
        #region QuotasDirectory
        /// <summary>
        /// Справочник квот
        /// </summary>
        [HttpGet(nameof(QuotasDirectory))]
        public async Task<IActionResult> QuotasDirectory([FromQuery] QuotasDirectoryQuery query)
        {
            return base.Ok(await mediator.Send(query));
        }
        /// <summary>
        /// Сохранение справочника квоты
        /// </summary>
        [HttpPost(nameof(SaveQuotasDirectory))]
        [MessageInjector]
        public async Task<IActionResult> SaveQuotasDirectory([FromBody] SaveQuotasDirectoryCommand query)
        {
            var result = await mediator.Send(query);
            if (!result.IsValid)
            {
                return base.BadRequest(result.Errors);
            }
            return base.Ok(result.Value);
        }
        #endregion
        #region QuotasOperative
        /// <summary>
        /// Оперативные квоты
        /// </summary>
        [HttpGet(nameof(QuotasOperative))]
        public async Task<IActionResult> QuotasOperative([FromQuery] QuotasOperativeQuery query)
        {
            return base.Ok(await mediator.Send(query));
        }
        /// <summary>
        /// Сохранение оперативной квоты
        /// </summary>
        [HttpPost(nameof(SaveQuotasOperative))]
        [MessageInjector]
        public async Task<IActionResult> SaveQuotasOperative([FromBody] SaveQuotasOperativeCommand query)
        {
            var result = await mediator.Send(query);
            if (!result.IsValid)
            {
                return base.BadRequest(result.Errors);
            }
            return base.Ok(result.Value);
        }

        /// <summary>
        /// Сохранение оперативных квот из файла CVS
        /// </summary>
        [HttpPost(nameof(QuotasOperParseCvs))]
        public async Task<IActionResult> QuotasOperParseCvs([FromBody] CvsSaveQuotasOperativeCommand query)
        {
            var result = await mediator.Send(query);
            if (!result.IsValid)
            {
                return base.BadRequest(result.Errors);
            }
            return base.Ok();
        }

        #endregion
    }
}
