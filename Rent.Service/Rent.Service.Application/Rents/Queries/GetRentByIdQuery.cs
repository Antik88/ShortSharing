using MediatR;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Queries;

public class GetRentByIdQuery : IRequest<RentModel>
{
    public Guid RentId { get; set; }
}
