using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.BLL.Services;

public class ThingsService(IGenericRepository<ThingEntity> repository,
        IThingRepository thingRepository, IMapper mapper,
        ICacheService cache) : IThingsService
{

    public async Task<ThingModel> CreateAsync(ThingModel entity, CancellationToken token)
    {
        var thingEntity = mapper.Map<ThingEntity>(entity);

        var thing = await thingRepository.CreateAsync(thingEntity, token);

        return mapper.Map<ThingModel>(thing);
    }

    public Task DeleteAsync(Guid id, CancellationToken token)
    {
        return repository.DeleteAsync(id, token);
    }

    public async Task<PagedResult<ThingModel>> GetAllAsync(QueryParameters queryParameters, CancellationToken token)
    {
        var result = await thingRepository.GetAllAsync(queryParameters, token);

        var items = mapper.Map<List<ThingModel>>(result.Items);

        return new PagedResult<ThingModel> {
            Items = items,
            TotalCount = result.TotalCount,
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize
        };
    }

    public async Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var things = await thingRepository.GetById(id, token);

        return mapper.Map<ThingModel>(things);
    }

    public async Task<List<ThingModel>?> GetByOwnerId(Guid ownerId, CancellationToken token)
    {
        var things = await thingRepository.GetByOwnerId(ownerId, token);

        return mapper.Map<List<ThingModel>>(things);
    }

    public async Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity, CancellationToken token)
    {
        var thing = await repository.UpdateAsync(id, entity, token);

        return mapper.Map<ThingModel>(thing);
    }
    public async Task<ThingModel> GetShortThing(Guid id, CancellationToken token)
    {
        var thing = await cache.GetData<ThingEntity>($"thing-{id}");

        if (thing != null)
            return mapper.Map<ThingModel>(thing);

        thing = await thingRepository.GetShortThing(id, token);

        return mapper.Map<ThingModel>(thing);
    }
}
