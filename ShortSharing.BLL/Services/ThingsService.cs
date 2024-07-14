using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.BLL.Services;

public class ThingsService : IThingsService
{
    private readonly IGenericRepository<ThingEntity> _repository;
    private readonly IThingRepository _thingRepository;
    private readonly IMapper _mapper;

    public ThingsService(IGenericRepository<ThingEntity> repository, IThingRepository thingRepository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _thingRepository = thingRepository;
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

        return new PagedResult<ThingModel>(
            items,
            result.TotalCount,
            result.CurrentPage,
            result.PageSize
        );
    }

    public async Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var things = await _repository.GetByIdAsync(id, token);

        return _mapper.Map<ThingModel>(things);
    }

    public async Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity, CancellationToken token)
    {
        var thing = await _repository.UpdateAsync(id, entity, token);

        return _mapper.Map<ThingModel>(thing);
    }
}
