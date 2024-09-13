using AutoMapper;
using ShortSharing.API.Dtos.CategoryDtos;
using ShortSharing.API.Dtos.ImageDtos;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.API.Dtos.TypeDtos;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;

namespace ShortSharing.API.Mappers;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ThingModel, CreateThingDto>().ReverseMap();
        CreateMap<ThingDto, ThingModel>().ReverseMap();

        CreateMap<TypeModel, TypeDto>().ReverseMap();
        CreateMap<TypeModel, CreateTypeDto>().ReverseMap();

        CreateMap<TypeEntity, TypeModel>().ReverseMap();

        CreateMap<CategoryModel, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, CategoryModel>().ReverseMap();

        CreateMap<PutImageDto, ImageModel>().ReverseMap();
        CreateMap<ImageDto, ImageModel>().ReverseMap();
    }
}
