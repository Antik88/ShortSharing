using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShortSharing.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    [HttpPut] 
    public async Task<> GetImage(string name)
    {
        
    }
}
