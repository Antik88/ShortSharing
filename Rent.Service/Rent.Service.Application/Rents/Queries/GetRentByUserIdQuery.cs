using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Queries;

public record GetRentByUserIdQuery(Guid UserId) : IRequest<List<RentModel>>;

public class GetRentByUserIdQueryHandler(
    IRentQueryRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentByUserIdQuery, List<RentModel>>
{
    public async Task<List<RentModel>> Handle(GetRentByUserIdQuery request, CancellationToken cancellationToken)
    {
        var rents = await rentRepository.GetByUserId(request.UserId);

        return mapper.Map<List<RentModel>>(rents);
    }
}
