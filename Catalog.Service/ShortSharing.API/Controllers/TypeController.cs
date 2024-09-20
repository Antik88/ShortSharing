using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortSharing.API.Constants;
using ShortSharing.API.Dtos.TypeDtos;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;

namespace ShortSharing.API.Controllers;

[Authorize(Roles = Roles.Admin)]
[Route("api/[controller]")]
[ApiController]
public class TypeController(IMapper mapper, ITypeService typeService) : ControllerBase
{
    [HttpPost]
    public async Task<TypeDto> Create(CreateTypeDto typeDto, CancellationToken token)
    {
        var typeModel = mapper.Map<TypeModel>(typeDto);

        var result = await typeService.Create(typeModel, token);

        return mapper.Map<TypeDto>(result);
    }
}
