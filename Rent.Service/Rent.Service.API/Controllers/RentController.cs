using Microsoft.AspNetCore.Mvc;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Application.Rents.Queries.GetRentById;
using Rent.Service.Application.Rents.Queries.GetRents;

namespace Rent.Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentController : ApiControllerBase 
{
    [HttpGet]
    public async Task<List<RentModel>> GetAllAsync()
    {
        var rents = await Mediator.Send(new GetRentQuery());
        return rents;
    }

    [HttpGet("{id}")]
    public async Task<RentModel> GetByIdAsync(Guid id)
    {
        var rent = await Mediator.Send(new GetRentById() { RentId =  id});
        return rent;
    }

    [HttpPost]
    public async Task<RentModel> CreateRent(CreateRentCommand createRentCommand)
    {
        var createRent = await Mediator.Send(createRentCommand);

        return createRent;
    }

    [HttpPut("{id}")]
    public Task<int> UpdateRent(UpdateRentCommand updateRentCommand)
    {
        return  Mediator.Send(updateRentCommand);
    }

    [HttpDelete("{id}")]
    public async Task<int> DeleteByIdAsync(Guid id)
    {
        return await Mediator.Send(new DeleteRentCommand() { Id = id });
    }
}
