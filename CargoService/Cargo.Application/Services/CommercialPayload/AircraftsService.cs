using AutoMapper;
using Cargo.Application.CommandHandlers.Settings;
using Cargo.Contract.DTOs.Settings;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Data.Model.Settings;
using IDeal.Common.Components.Paginator;
using IdealResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Application.Services.CommPayload
{


    public class AircraftsService
    {
        CargoContext dbContext;
        IMapper mapper;
        public AircraftsService(CargoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<Aircraft>> GetAircrafts(PageInfo pageInfo, string IataCode)
        {
            var task = Task.Run(() =>
            {
                var aircrafts = dbContext
                .Aircraft
                .Include(pn => pn.AircraftType)
                .Where(pn => pn.IataCode == IataCode)
                .AsNoTracking()
                .Page(pageInfo);

                return aircrafts;
            });
            return await task;
        }

        public async Task<AircraftsDto> SaveAircrafts(SaveAircraftsCommand saveAircraftsCommand)
        {
            var task = Task.Run(() =>
            {
                var commPayDto = saveAircraftsCommand.AircraftsDto;
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
                commPayDto.IataCode = saveAircraftsCommand.IataCode;

                if (commPayDto.Id == 0)
                {
                    dbContext.Aircraft.Add(mapper.Map<Aircraft>(commPayDto));
                }
                else
                {
                    dbContext.Aircraft.Update(mapper.Map<Aircraft>(commPayDto));
                }

                dbContext.SaveChanges();

                return commPayDto;
            });
            return await task;
        }

        public async Task<Result> DeleteAircrafts(int AircraftId)
        {
            var task = Task.Run(() =>
            {
                var aircraft = dbContext.Aircraft.Where(s => s.Id == AircraftId).FirstOrDefault();

                if (aircraft == null)
                {
                    return Result.Fail("Null reference");
                }

                dbContext.Aircraft.Remove(aircraft);
                dbContext.SaveChanges();

                return Result.Ok();
            });
            return await task;
        }

    }
}
