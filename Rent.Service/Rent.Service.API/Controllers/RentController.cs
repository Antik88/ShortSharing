using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rent.Service.BLL.Models;
using Rent.Service.BLL.Services.Interfaces;

namespace Rent.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController(IRentService service) : ControllerBase
    {

        [HttpPost("")]
        public async Task<RentModel> CreateRent(CancellationToken cancellationToken)
        {
            var model = new RentModel();

            var result = await service.AddAsync(model, cancellationToken);

            return result;
        }
    }
}
