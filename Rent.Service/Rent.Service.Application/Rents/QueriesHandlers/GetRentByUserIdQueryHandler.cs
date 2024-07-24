using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Queries;

namespace Rent.Service.Application.Rents.QueriesHandlers;

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
