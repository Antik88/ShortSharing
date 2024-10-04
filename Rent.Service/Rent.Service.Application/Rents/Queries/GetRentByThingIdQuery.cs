using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Queries;

public record GetRentByThingIdQuery(Guid RentId) : IRequest<List<RentModel>>;

public class GetRentByThingIdHandler(
    IRentQueryRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentByThingIdQuery, List<RentModel>>
{
    public async Task<List<RentModel>> Handle(GetRentByThingIdQuery request,
        CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByThingId(request.RentId);

        return mapper.Map<List<RentModel>>(rent);
    }
}
