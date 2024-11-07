using Email.Service.BLL.Service;
using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Email.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(ITemplateService templateService) : ControllerBase
    {
        [HttpGet]
        public async Task<List<TemplateEntity>> GetTemplates()
        {
            return await templateService.GetAll();
        }
    }
}
