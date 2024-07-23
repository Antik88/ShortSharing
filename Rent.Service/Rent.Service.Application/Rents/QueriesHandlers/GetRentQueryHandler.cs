using AutoMapper;
using MediatR;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Queries;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.QueriesHandlers;

public class GetRentQueryHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentQuery, List<RentModel>>
{
    public async Task<List<RentModel>> Handle(GetRentQuery request, CancellationToken cancellationToken)
    {
        var rents = await rentRepository.GetAllRentsAsync();

        return mapper.Map<List<RentModel>>(rents);
    }
}
