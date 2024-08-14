using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rent.Service.API.Constants;
using Rent.Service.API.Dtos;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Application.Rents.Queries;

namespace Rent.Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController(ISender mediator, IMapper mapper) : ApiControllerBase(mediator)
{
    [HttpGet]
    public async Task<List<RentDto>> GetAllAsync()
    {
        var rents = await Mediator.Send(new GetRentQuery());

        return mapper.Map<List<RentDto>>(rents);
    }

    [HttpGet(Routes.GetRentByUserId)]
    public async Task<List<RentDto>> GetByUserIdAsync(Guid userId)
    {
        var rents = await Mediator.Send(new GetRentByUserIdQuery(userId));

        return mapper.Map<List<RentDto>>(rents);
    }

    [HttpGet(Routes.ById)]
    public async Task<RentDto> GetByIdAsync(Guid id)
    {
        var rent = await Mediator.Send(new GetRentByIdQuery(id));

        return mapper.Map<RentDto>(rent);
    }

    [HttpPost]
    public async Task<RentDto> CreateRent(CreateRentCommand createRentCommand)
    {
        var createRent = await Mediator.Send(createRentCommand);

        return mapper.Map<RentDto>(createRent);
    }

    [HttpPut]
    public async Task<RentDto> UpdateRent(UpdateRentCommand updateRentCommand)
    {
        var result = await Mediator.Send(updateRentCommand);

        return mapper.Map<RentDto>(result);  
    }

    [HttpPatch(Routes.ExtendRentById)]
    public async Task<RentDto> ExtendRent(ExtendRentCommand extendRentCommand)
    {
        var result = await Mediator.Send(extendRentCommand);

        return mapper.Map<RentDto>(result);
    }

    [HttpPatch(Routes.CancelRent)]
    public async Task<RentDto> CancelRent(CancelRentCommand cancelCommand)
    {
        var result = await Mediator.Send(cancelCommand);

        return mapper.Map<RentDto>(result);
    }

    [HttpDelete(Routes.ById)]
    public async Task DeleteByIdAsync(Guid id)
    {
        await Mediator.Send(new DeleteRentCommand() { Id = id });
    }
}
