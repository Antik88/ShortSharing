using MediatR;
using Rent.Service.Application.Rents.Queries.GetRents;

namespace Rent.Service.Application.Rents.Queries.GetRentById;

public class GetRentById : IRequest<RentModel>
{
   public Guid RentId { get; set; }  
}
