using Xunit;
using Shouldly;
using AutoMapper;
using NSubstitute;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.Tests;

public class TypeServiceTests
{
    private readonly ITypeService _typeService;
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public TypeServiceTests()
    {
        _typeRepository = Substitute.For<ITypeRepository>();
        _mapper = Substitute.For<IMapper>();

        _typeService = new TypeService(_mapper, _typeRepository);
    }

    [Theory, AutoMoqData]
    public async Task Create_ShouldReturnCreatedTypeModel(TypeModel typeModel, TypeEntity typeEntity)
    {
        // Arrange
        _mapper.Map<TypeEntity>(typeModel).Returns(typeEntity);
        _typeRepository.CreateAsync(typeEntity, default).Returns(typeEntity);
        _mapper.Map<TypeModel>(typeEntity).Returns(typeModel);

        // Act
        var result = await _typeService.Create(typeModel, default);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<TypeModel>();
        result.ShouldBe(typeModel);
    }

    [Theory, AutoMoqData]
    public async Task Create_ShouldCallRepositoryAndMapper(TypeModel typeModel, TypeEntity typeEntity)
    {
        // Arrange
        _mapper.Map<TypeEntity>(typeModel).Returns(typeEntity);
        _typeRepository.CreateAsync(typeEntity, default).Returns(typeEntity);
        _mapper.Map<TypeModel>(typeEntity).Returns(typeModel);

        // Act
        var result = await _typeService.Create(typeModel, default);

        // Assert
        await _typeRepository.Received(1).CreateAsync(typeEntity, default);
        _mapper.Received(1).Map<TypeEntity>(typeModel);
        _mapper.Received(1).Map<TypeModel>(typeEntity);
    }
}
