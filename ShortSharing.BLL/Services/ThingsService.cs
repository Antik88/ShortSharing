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

    public async Task<ThingModel> CreateAsync(ThingModel thingModel)
    {
        var thingEntity = _mapper.Map<ThingEntity>(thingModel);

        var thing = await _repository.CreateAsync(thingEntity);

        return thingModel;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<List<ThingModel>> GetAllAsync()
    {
        var things = await _repository.GetAllAsync();

        return _mapper.Map<List<ThingModel>>(things);
    }

    public async Task<ThingModel?> GetByIdAsync(Guid id)
    {
        var things = await _repository.GetByIdAsync(id);

        return _mapper.Map<ThingModel>(things);
    }

    public async Task<ThingModel?> UpdateAsync(Guid id, ThingEntity thingModel)
    {
        var thing = await _repository.UpdateAsync(id, thingModel);

        return _mapper.Map<ThingModel>(thing);
    }
}
