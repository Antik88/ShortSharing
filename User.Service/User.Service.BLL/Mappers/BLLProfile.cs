using AutoMapper;
using User.Service.BLL.Models;
using User.Service.DLL.Entities;

namespace User.Service.BLL.Mappers;

public class BLLProfile : Profile
{
    public BLLProfile()
    {
        CreateMap<UserModel, UserEntity>().ReverseMap();
    }
}
