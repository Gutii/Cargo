using AutoMapper;
using Cargo.Contract.Commands.Settings.CommPayload;
using Cargo.Contract.DTOs.Settings.CommercialPayload;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Settings;
using IDeal.Common.Components.Paginator;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services.CommercialPayload
{
    public class MailLimitsService
    {
        CargoContext dbContext;
        IMapper mapper;
        public MailLimitsService(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<MailLimits>> GetMailLimits(PageInfo pageInfo, string IataCode)
        {
            var task = Task.Run(() =>
            {
                var сommPayload = dbContext
                .MailLimits
                .Include(pn => pn.InIataLocation)
                .Include(pn => pn.FromIataLocation)
                .Include(pn => pn.AircraftType)
                .Include(pn => pn.Airline)
                .Where(pn => pn.IataCode == IataCode)
                .AsNoTracking()
                .Page(pageInfo);

                return сommPayload;
            });
            return await task;
        }

        public async Task<MailLimitsDto> SaveMailLimits(SaveMailLimitsCommand saveMailLimitsCommand)
        {
            var task = Task.Run(() =>
            {
                var mailLimitsDto = saveMailLimitsCommand.MailLimits;
                if (mailLimitsDto.Volume == null || mailLimitsDto.Volume == 0 || mailLimitsDto.Weight == null || mailLimitsDto.Weight == 0)
                {
                    var AircraftTypes = dbContext.AircraftTypes.Where(a => a.Id == mailLimitsDto.AircraftTypeId).FirstOrDefault();
                    if (mailLimitsDto.Weight == null || mailLimitsDto.Weight == 0)
                    {
                        mailLimitsDto.Weight = AircraftTypes.MaxPayloadWeight;
                    }

                    if (mailLimitsDto.Volume == null || mailLimitsDto.Volume == 0)
                    {
                        mailLimitsDto.Volume = AircraftTypes.MaxPayloadVolume;
                    }
                }
                mailLimitsDto.IataCode = saveMailLimitsCommand.IataCode;

                if (mailLimitsDto.Id == 0)
                {
                    var a = mapper.Map<MailLimits>(mailLimitsDto);
                    dbContext.MailLimits.Add(a);
                }
                else
                {
                    dbContext.MailLimits.Update(mapper.Map<MailLimits>(mailLimitsDto));
                }

                dbContext.SaveChanges();
                return mailLimitsDto;
            });
            return await task;
        }

        public async Task<Result> DeleteMailLimits(int MailLimitsId)
        {
            var task = Task.Run(() =>
            {
                var mailLimitsId = dbContext.MailLimits.Where(s => s.Id == MailLimitsId).FirstOrDefault();

                if (mailLimitsId == null)
                {
                    return Result.Fail("Null reference");
                }

                dbContext.MailLimits.Remove(mailLimitsId);
                dbContext.SaveChanges();

                return Result.Ok();
            });
            return await task;
        }
    }
}
