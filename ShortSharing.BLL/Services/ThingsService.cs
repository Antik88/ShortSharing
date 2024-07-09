using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Services;

public class ThingsService : IThingsService
{
    private readonly IGenericRepository<ThingEntity> _repository;
    private readonly IMapper _mapper;

    public ThingsService(IGenericRepository<ThingEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ThingModel> CreateAsync(ThingModel thingModel, CancellationToken token)
    {
        var thingEntity = _mapper.Map<ThingEntity>(thingModel);

        var thing = await _repository.CreateAsync(thingEntity, token);

        return thingModel;
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        await _repository.DeleteAsync(id, token);
    }

    public async Task<List<ThingModel>> GetAllAsync(CancellationToken token)
    {
        var things = await _repository.GetAllAsync(token);

        return _mapper.Map<List<ThingModel>>(things);
    }

    public async Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var things = await _repository.GetByIdAsync(id, token);

        return _mapper.Map<ThingModel>(things);
    }

    public async Task<ThingModel?> UpdateAsync(Guid id, ThingEntity thingModel, CancellationToken token)
    {
        var thing = await _repository.UpdateAsync(id, thingModel, token);

        return _mapper.Map<ThingModel>(thing);
    }
}
