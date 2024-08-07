using AutoMapper;
using Email.Service.BLL.Models;
using Email.Service.DAL.Entities;

namespace Email.Service.BLL.Mappers;

public class BLLProfile : Profile
{
    public BLLProfile()
    {
        CreateMap<TemplateModel, TemplateEntity>().ReverseMap();
    }
}
