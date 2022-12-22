using AutoMapper;
using Cargo.Contract.Commands.Settings;
using Cargo.Contract.DTOs.Settings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Settings;
using IDeal.Common.Components.Paginator;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services.Settings
{
    public class CommPayloadsService
    {
        CargoContext dbContext;
        IMapper mapper;
        public CommPayloadsService(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<CommPayloadAt>> GetPayloads(PageInfo pageinfo, string IataCode)
        {
            var task = Task.Run(() =>
            {
                var сommPayload = dbContext
                .CommPayloads
                .Include(pn => pn.InIataLocation)
                .Include(pn => pn.FromIataLocation)
                .Include(pn => pn.AircraftType)
                .Include(pn => pn.Airline)
                .Where(pn => pn.IataCode == IataCode)
                .AsNoTracking()
                .Page(pageinfo);

                return сommPayload;
            });
            return await task;
        }

        public async Task<CommPayloadDto> SavePayloads(SaveCommPayloadCommand saveCommPayloadCommand)
        {
            var task = Task.Run(() =>
            {
                var commPayDto = saveCommPayloadCommand.CommPayload;
                if (commPayDto.Volume == null || commPayDto.Volume == 0 || commPayDto.Weight == null || commPayDto.Weight == 0)
                {
                    var AircraftTypes = dbContext.AircraftTypes.Where(a => a.Id == commPayDto.AircraftTypeId).FirstOrDefault();
                    if (commPayDto.Weight == null || commPayDto.Weight == 0)
                    {
                        commPayDto.Weight = AircraftTypes.MaxPayloadWeight;
                    }

                    if (commPayDto.Volume == null || commPayDto.Volume == 0)
                    {
                        commPayDto.Volume = AircraftTypes.MaxPayloadVolume;
                    }
                }
                commPayDto.IataCode = saveCommPayloadCommand.IataCode;

                if (commPayDto.Id == 0)
                {
                    var a = mapper.Map<CommPayloadAt>(commPayDto);
                    dbContext.CommPayloads.Add(a);
                }
                else
                {
                    dbContext.CommPayloads.Update(mapper.Map<CommPayloadAt>(commPayDto));
                }

                dbContext.SaveChanges();

                return commPayDto;
            });
            return await task;
        }

        public async Task<Result> DeleteCommPayload(int CommPayloadId)
        {
            var task = Task.Run(() =>
            {
                var commPayloadId = dbContext.CommPayloads.Where(s => s.Id == CommPayloadId).FirstOrDefault();

                if (commPayloadId == null)
                {
                    return Result.Fail("Null reference");
                }

                dbContext.CommPayloads.Remove(commPayloadId);
                dbContext.SaveChanges();

                return Result.Ok();
            });
            return await task;
        }

    }
}
