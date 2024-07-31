using AutoMapper;
using Rent.Service.API.Dtos;
using Rent.Service.Application.Model;

namespace Rent.Service.API.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<RentDto, RentModel>().ReverseMap();
    }
}
