using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.API.Dtos;
using User.Service.BLL.Models;
using User.Service.BLL.Service.Interfaces;
using User.Service.Shared;

namespace User.Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(
    IMapper mapper,
    IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<UserDto> CreateUserAsync(
        [FromBody] UserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var userModel = mapper.Map<UserModel>(createUserDto);

        var newUserModel = await userService.AddAsync(userModel, cancellationToken);

        var newUserDto = mapper.Map<UserDto>(newUserModel);

        return newUserDto;
    }

    [Authorize (Roles = "Admin")]
    [HttpGet("/getRange")]
    public async Task<PageResult<UserDto>> GetRangeAsync(
      [FromQuery] Query query,
      CancellationToken cancellationToken)
    {
        var users = await userService.GetRangeAsync(query, cancellationToken);

        return new PageResult<UserDto>
        {
            Items = mapper.Map<List<UserDto>>(users.Items),
            PageSize = query.PageSize,
            CurrentPage = query.Page
        };
    }

    [Authorize (Roles = "User")]
    [HttpGet("{id}")]
    public async Task<UserDto> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(id, cancellationToken);

        return mapper.Map<UserDto>(user);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public Task DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return userService.Delete(id, cancellationToken);
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<CreateUserDto> PatchById(
        [FromRoute] Guid id,
        [FromBody] CreateUserDto userDto,
        CancellationToken cancellationToken)
    {
        var result = await userService.UpdateAsync(id,
            mapper.Map<UserModel>(userDto), cancellationToken);

        return mapper.Map<CreateUserDto>(result);
    }
}
