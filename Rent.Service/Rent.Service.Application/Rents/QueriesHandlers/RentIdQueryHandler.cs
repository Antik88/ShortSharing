using AutoMapper;
using MediatR;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Queries;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.QueriesHandlers;

public class RentIdQueryHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentByIdQuery, RentModel>
{
    public async Task<RentModel> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByIdAsync(request.RentId);

        return mapper.Map<RentModel>(rent);
    }
}
