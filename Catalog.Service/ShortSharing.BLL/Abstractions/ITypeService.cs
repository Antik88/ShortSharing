using ShortSharing.BLL.Models;

namespace ShortSharing.BLL.Abstractions;

public interface ITypeService
{
    Task<TypeModel> Create(TypeModel model, CancellationToken token);
}
