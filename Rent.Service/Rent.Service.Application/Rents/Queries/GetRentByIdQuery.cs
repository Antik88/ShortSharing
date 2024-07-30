using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Queries;

public record GetRentByIdQuery(Guid RentId) : IRequest<RentModel>;

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
