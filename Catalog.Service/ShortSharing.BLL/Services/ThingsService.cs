using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;
using System.Formats.Asn1;

namespace ShortSharing.BLL.Services;

public class ThingsService : IThingsService
{
    private readonly IGenericRepository<ThingEntity> _repository;
    private readonly IThingRepository _thingRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public ThingsService(IGenericRepository<ThingEntity> repository,
        IThingRepository thingRepository, IMapper mapper,
        ICacheService cache)
    {
        _repository = repository;
        _mapper = mapper;
        _thingRepository = thingRepository;
        _cacheService = cache;
    }

    public async Task<ThingModel> CreateAsync(ThingModel entity, CancellationToken token)
    {
        var thingEntity = _mapper.Map<ThingEntity>(entity);

        var thing = await _thingRepository.CreateAsync(thingEntity, token);

        return _mapper.Map<ThingModel>(thing);
    }

    public Task DeleteAsync(Guid id, CancellationToken token)
    {
        return _repository.DeleteAsync(id, token);
    }

    public async Task<PagedResult<ThingModel>> GetAllAsync(QueryParameters queryParameters, CancellationToken token)
    {
        var result = await _thingRepository.GetAllAsync(queryParameters, token);

        var items = _mapper.Map<List<ThingModel>>(result.Items);

        return new PagedResult<ThingModel> {
            Items = items,
            TotalCount = result.TotalCount,
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize
        };
    }

    public async Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var things = await _thingRepository.GetById(id, token);

        return _mapper.Map<ThingModel>(things);
    }

    public async Task<List<ThingModel>?> GetByOwnerId(Guid ownerId, CancellationToken token)
    {
        var things = await _thingRepository.GetByOwnerId(ownerId, token);

        return _mapper.Map<List<ThingModel>>(things);
    }

    public async Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity, CancellationToken token)
    {
        var thing = await _repository.UpdateAsync(id, entity, token);

        return _mapper.Map<ThingModel>(thing);
    }
    public async Task<ThingModel> GetShortThing(Guid id, CancellationToken token)
    {
        var thing = await _cacheService.GetData<ThingEntity>($"thing-{id}");

        if (thing != null)
            return _mapper.Map<ThingModel>(thing);

        thing = await _thingRepository.GetShortThing(id, token);

        return _mapper.Map<ThingModel>(thing);
    }
}
