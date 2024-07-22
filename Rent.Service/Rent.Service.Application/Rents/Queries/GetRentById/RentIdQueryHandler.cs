using AutoMapper;
using MediatR;
using Rent.Service.Application.Rents.Queries.GetRents;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.Queries.GetRentById;

public class RentIdQueryHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<GetRentById, RentModel>
{
    public async Task<RentModel> Handle(GetRentById request, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByIdAsync(request.RentId);

        return mapper.Map<RentModel>(rent);
    }
}
