using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Commands;

public class UpdateRentCommand : IRequest<RentModel>
{
    public Guid Id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
}

public class UpdateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper) : IRequestHandler<UpdateRentCommand, RentModel>
{
    public async Task<RentModel> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var result = await rentRepository.UpdateAsync(
            request.Id, request.StartRentDate, request.EndRentDate);

        return mapper.Map<RentModel>(result);
    }
}
