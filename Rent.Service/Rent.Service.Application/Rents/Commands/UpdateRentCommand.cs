using MediatR;

namespace Rent.Service.Application.Rents.Commands;

public class UpdateRentCommand : IRequest<int>
{
    public Guid id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid UserId { get; set; }
}
