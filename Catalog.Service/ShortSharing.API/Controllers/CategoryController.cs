using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortSharing.API.Constants;
using ShortSharing.API.Dtos.CategoryDtos;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;

namespace ShortSharing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]
public class CategoryController(IMapper mapper, ICategoryService categoryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet(ApiConstants.All)]
    public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        var category = await categoryService.GetAll(cancellationToken);

        return mapper.Map<List<CategoryDto>>(category);
    }
        
    [AllowAnonymous]
    [HttpGet(ApiConstants.Id)]
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetById(id, cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }

    [HttpPost]
    public async Task<CreateCategoryDto> Create(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryModel = mapper.Map<CategoryModel>(categoryDto);

        var category = await categoryService.Create(categoryModel, cancellationToken);

        return mapper.Map<CreateCategoryDto>(category);
    }

    [HttpDelete]
    public async Task<CategoryDto> Delete(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await categoryService.Delete(categoryId, cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }

    [HttpPut]
    public async Task<CategoryDto> Update(Guid categoryId, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryModel = mapper.Map<CategoryModel>(categoryDto);

        var category = await categoryService.Update(categoryId, categoryModel, cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }
}
