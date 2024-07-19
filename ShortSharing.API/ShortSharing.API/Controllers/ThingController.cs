using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShortSharing.API.Constants;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.Shared;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace ShortSharing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThingController : ControllerBase
{
    private readonly IThingsService _thingsService;
    private readonly IMapper _mapper;

    public ThingController(IThingsService thingsService, IMapper mapper)
    {
        _thingsService = thingsService;
        _mapper = mapper;
    }

    [HttpGet(ApiConstants.Id)]
    [ProducesResponseType(Status200OK, Type = typeof(ThingDto))]
    public async Task<ThingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var thing = await _thingsService.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<ThingDto>(thing);
    }

    [HttpGet(ApiConstants.All)]
    [ProducesResponseType(Status200OK, Type = typeof(PagedResult<ThingDto>))]
    public async Task<ActionResult<PagedResult<ThingDto>>> GetAllAsync(
        [FromQuery] QueryParameters query, 
        CancellationToken token)
    {
        var result = await _thingsService.GetAllAsync(query, token);

        var items = _mapper.Map<List<ThingDto>>(result.Items);

        return new PagedResult<ThingDto> { 
            Items = items,
            TotalCount = result.TotalCount,
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize
        };

    }

    [HttpPost]
    [ProducesResponseType(Status200OK, Type = typeof(CreateThingDto))]
    public async Task<CreateThingDto> CreateThingAsync(
        [FromBody] CreateThingDto thingDto,
        CancellationToken token)
    {
        var thingModel = _mapper.Map<ThingModel>(thingDto);
        await _thingsService.CreateAsync(thingModel, token);

        return thingDto;
    }

    [HttpDelete(ApiConstants.Id)]
    [ProducesResponseType(Status200OK)]
    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        await _thingsService.DeleteAsync(id, token);
    }
}
