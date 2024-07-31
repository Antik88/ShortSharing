using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Rent.Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender Mediator;

    public ApiControllerBase(ISender mediator)
    {
        Mediator = mediator;
    }
}
