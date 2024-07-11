using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShortSharing.API.Constants;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Common.Exceptions;
using ShortSharing.BLL.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace ShortSharing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThingController : ControllerBase
{
    private readonly IThingsService _thingsService;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateThingDto> _validator;

    public ThingController(IThingsService thingsService,
        IValidator<CreateThingDto> validator, IMapper mapper)
    {
        _thingsService = thingsService;
        _validator = validator; 
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
    [ProducesResponseType(Status200OK, Type = typeof(List<ThingDto>))]
    public async Task<List<ThingDto>> GetAllAsync(CancellationToken token)
    {
        var things = await _thingsService.GetAllAsync(token);

        return _mapper.Map<List<ThingDto>>(things);
    }

    [HttpPost]
    [ProducesResponseType(Status200OK, Type = typeof(CreateThingDto))]
    public async Task<CreateThingDto> CreateThingAsync(
        [FromBody] CreateThingDto thingDto,
        CancellationToken token)
    {
        _validator.ValidateAndThrow(thingDto);

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
