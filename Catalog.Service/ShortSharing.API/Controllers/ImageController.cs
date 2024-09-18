using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortSharing.API.Dtos.ImageDtos;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Services;

namespace ShortSharing.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController(IImageService imageService, IMapper mapper) : ControllerBase
{
    [HttpPut]
    public async Task<PutImageDto> PutImage(IFormFile formFile, Guid thingId)
    {
        var result = await imageService.PutImage(formFile, thingId);

        return mapper.Map<PutImageDto>(result);
    }

    [HttpGet]
    public async Task<File> GetImage(string name)
    {
        var (stream, contentType, fileName) = await imageService.GetImage(name);

        return File(stream, contentType, fileName);
    }
}
