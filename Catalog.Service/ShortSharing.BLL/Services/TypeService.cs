using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Services;

public class TypeService(IMapper mapper, ITypeRepository typeRepository) : ITypeService
{
    public async Task<TypeModel> Create(TypeModel model, CancellationToken token)
    {
        var entity = mapper.Map<TypeEntity>(model);

        var result = await typeRepository.CreateAsync(entity, token);

        return mapper.Map<TypeModel>(result);
    }
}
