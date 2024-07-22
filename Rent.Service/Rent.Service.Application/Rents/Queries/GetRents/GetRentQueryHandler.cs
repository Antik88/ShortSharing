using AutoMapper;
using MediatR;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.Queries.GetRents;

public class GetRentQueryHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentQuery, List<RentModel>>
{
    public async Task<List<RentModel>> Handle(GetRentQuery request, CancellationToken cancellationToken)
    {
        var rents = await rentRepository.GetAllRentsAsync();

        mapper.Map<List<RentModel>>(rents);

        var rentList = mapper.Map<List<RentModel>>(rents);

        return rentList;
    }
}
