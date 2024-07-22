using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
}
