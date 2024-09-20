using Xunit;
using Shouldly;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.Tests;

public class CategoryServiceTests
{
    private readonly ICategoryService _categoryService;
    private readonly IGenericRepository<CategoryEntity> _categoryRepository;
    private readonly ICategoryRepository _specificCategoryRepository;
    private readonly IMapper _mapper;

    public CategoryServiceTests()
    {
        _categoryRepository = Substitute.For<IGenericRepository<CategoryEntity>>();
        _specificCategoryRepository = Substitute.For<ICategoryRepository>();
        _mapper = Substitute.For<IMapper>();

        _categoryService = new CategoryService(_mapper, _categoryRepository, _specificCategoryRepository);
    }

    [Theory, AutoMoqData]
    public async Task GetById_InvalidId_ReturnsNull(Guid id)
    {
        // Arrange
        _categoryRepository.GetByIdAsync(id, default).ReturnsNull();

        // Act
        var result = await _categoryService.GetById(id, default);

        // Assert
        result.ShouldBeNull();
    }

    [Theory, AutoMoqData]
    public async Task GetById_ValidId_ReturnsNotNull(CategoryEntity categoryEntity, CategoryModel categoryModel, Guid id)
    {
        // Arrange
        _categoryRepository.GetByIdAsync(id, default).Returns(categoryEntity);
        _mapper.Map<CategoryModel>(categoryEntity).Returns(categoryModel);

        // Act
        var result = await _categoryService.GetById(id, default);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<CategoryModel>();
    }

    [Theory, AutoMoqData]
    public async Task Delete_ValidId_ReturnsDeletedModel(CategoryEntity categoryEntity, CategoryModel categoryModel, Guid id)
    {
        // Arrange
        _categoryRepository.GetByIdAsync(id, default).Returns(categoryEntity);
        _mapper.Map<CategoryModel>(categoryEntity).Returns(categoryModel);

        // Act
        var result = await _categoryService.Delete(id, default);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<CategoryModel>();
    }

    [Theory, AutoMoqData]
    public async Task Create_ShouldReturnCreatedCategoryModel(CategoryModel categoryModel, CategoryEntity categoryEntity)
    {
        // Arrange
        _mapper.Map<CategoryEntity>(categoryModel).Returns(categoryEntity);
        _categoryRepository.CreateAsync(categoryEntity, default).Returns(categoryEntity);
        _mapper.Map<CategoryModel>(categoryEntity).Returns(categoryModel);

        // Act
        var result = await _categoryService.Create(categoryModel, default);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<CategoryModel>();
    }

    [Theory, AutoMoqData]
    public async Task GetAll_ShouldReturnListOfCategories(List<CategoryEntity> categoryEntities, List<CategoryModel> categoryModels)
    {
        // Arrange
        _specificCategoryRepository.GetAllCategories(default).Returns(categoryEntities);
        _mapper.Map<List<CategoryModel>>(categoryEntities).Returns(categoryModels);

        // Act
        var result = await _categoryService.GetAll(default);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<CategoryModel>>();
    }
}
