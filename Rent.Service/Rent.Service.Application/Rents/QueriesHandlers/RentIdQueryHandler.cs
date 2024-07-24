using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Queries;

namespace Rent.Service.Application.Rents.QueriesHandlers;

public class RentIdQueryHandler(
    IRentQueryRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentByIdQuery, RentModel>
{
    public async Task<RentModel> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByIdAsync(request.RentId);

        return mapper.Map<RentModel>(rent);
    }
}
