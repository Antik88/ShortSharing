using Rent.Service.BLL.Helpers;
using Rent.Service.BLL.Models;
using Rent.Service.BLL.Services.Interfaces;
using System.Net.Http.Json;

namespace Rent.Service.BLL.Services.Implementations;

public class RentService(ServiceRequest serviceRequest) : IRentService
{
    public async Task<RentModel> AddAsync(RentModel rentModel, CancellationToken cancellationToken)
    {
        var thing = await serviceRequest.GetModelFromServiceAsync<ThingModel>("https://localhost:7191/api/Thing/4933279e-b4b9-4a0d-a6fe-affa892562a8", cancellationToken);

        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RentModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}
