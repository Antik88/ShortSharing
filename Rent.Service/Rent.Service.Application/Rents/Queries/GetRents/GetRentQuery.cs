using MediatR;

namespace Rent.Service.Application.Rents.Queries.GetRents;

public record GetRentQuery : IRequest<List<RentModel>>;
