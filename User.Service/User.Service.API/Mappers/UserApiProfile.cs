using AutoMapper;
using User.Service.API.Dtos;
using User.Service.BLL.Models;

namespace User.Service.API.Mappers;

public class UserApiProfile : Profile
{
    public UserApiProfile()
    {
        CreateMap<UserModel, UserDto> ().ReverseMap();
        CreateMap<UserResponseDto, UserModel> ().ReverseMap();
        CreateMap<UserModel, CreateUserDto>().ReverseMap();
    }
}
