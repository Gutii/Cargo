using AutoMapper;
using Cargo.Application.QueryHandlers;
using Directories.Application.Services.Settings;
using Directories.Contract.DTOs.Settings.BookingRules;
using Directories.Contract.Queries.Settings.BookingRules;
using Directories.Infrastructure.Data;
using Directories.Infrastructure.Data.Model.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Directories.Application.QueryHandlers.Settings.BookingRules
{
    public class CarrierSettingsParametersQueryHandler : IQueryHandler<CarrierSettinParamQuery, ICollection<CarrierParametersSettingsDto>>
    {
        CarrierSettingsParamService CarrierSettingsParamService;

        public CarrierSettingsParametersQueryHandler(CarrierSettingsParamService carrierSettingsParamService)
        {
            CarrierSettingsParamService = carrierSettingsParamService;
        }

        public async Task<ICollection<CarrierParametersSettingsDto>> Handle(CarrierSettinParamQuery request, CancellationToken cancellationToken)
        {
            var task = CarrierSettingsParamService.CarrierSettingsParamAsync(request.CustomerId);
            return await task;
        }
    }
}
