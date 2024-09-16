﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
public class ThingController(IThingsService thingsService, IMapper mapper) : ControllerBase
{
    [HttpGet(ApiConstants.Id)]
    [ProducesResponseType(Status200OK, Type = typeof(ThingDto))]
    public async Task<ThingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var thing = await thingsService.GetByIdAsync(id, cancellationToken);
        return mapper.Map<ThingDto>(thing);
    }

    [HttpGet(ApiConstants.All)]
    [ProducesResponseType(Status200OK, Type = typeof(PagedResult<ThingDto>))]
    public async Task<ActionResult<PagedResult<ThingDto>>> GetAllAsync([FromQuery] QueryParameters query, CancellationToken token)
    {
        var result = await thingsService.GetAllAsync(query, token);

        var items = mapper.Map<List<ThingDto>>(result.Items);

        return new PagedResult<ThingDto> { 
            Items = items,
            TotalCount = result.TotalCount,
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize
        };
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(Status200OK, Type = typeof(CreateThingDto))]
    public async Task<CreateThingDto> CreateThingAsync([FromBody] CreateThingDto thingDto, CancellationToken token)
    {
        var thingModel = mapper.Map<ThingModel>(thingDto);
        await thingsService.CreateAsync(thingModel, token);

        return thingDto;
    }

    [Authorize]
    [HttpDelete(ApiConstants.Id)]
    [ProducesResponseType(Status200OK)]
    public Task DeleteAsync(Guid id, CancellationToken token)
    {
        return thingsService.DeleteAsync(id, token);
    }
}