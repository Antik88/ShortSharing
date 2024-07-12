using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

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

    public async Task<ThingModel> CreateAsync(ThingModel thingModel, CancellationToken token)
    {
        var thingEntity = _mapper.Map<ThingEntity>(thingModel);

        var thing = await _thingRepository.CreateAsync(thingEntity, token);

        return _mapper.Map<ThingModel>(thing);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        await _repository.DeleteAsync(id, token);
    }

    public async Task<List<ThingModel>> GetAllAsync(CancellationToken token, int pageNumber, int pageSize, Guid? categoryId, Guid? typeId)
    {
        var things = await _thingRepository.GetAllAsync(token, pageNumber, pageSize, categoryId, typeId);
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
