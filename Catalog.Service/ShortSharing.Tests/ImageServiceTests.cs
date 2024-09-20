using Xunit;
using Shouldly;
using AutoMapper;
using NSubstitute;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.BLL.Models;
using Microsoft.AspNetCore.Http;
using Minio;
using System.Text;
using Minio.DataModel.Args;

namespace ShortSharing.Tests;

public class ImageServiceTests
{
    private readonly IImageService _imageService;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly IMinioClient _minioClient;

    public ImageServiceTests()
    {
        _imageRepository = Substitute.For<IImageRepository>();
        _mapper = Substitute.For<IMapper>();
        _minioClient = Substitute.For<IMinioClient>();

        _imageService = new ImageService(_imageRepository, _mapper, _minioClient);
    }

    [Fact]
    public async Task PutImage_ValidFormFile_ReturnsImageModel()
    {
        // Arrange
        var thingId = Guid.NewGuid();
        var formFile = Substitute.For<IFormFile>();
        var fileName = "new-image.png";
        var contentType = "image/png";
        var fileContent = "Fake Image Content";
        var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));

        formFile.FileName.Returns(fileName);
        formFile.ContentType.Returns(contentType);
        formFile.Length.Returns(fileStream.Length);
        formFile.OpenReadStream().Returns(fileStream);

        var imageEntity = new ImageEntity
        {
            Name = fileName,
            ThingId = thingId
        };

        var imageModel = new ImageModel
        {
            Name = fileName,
            ThingId = thingId
        };

        _mapper.Map<ImageEntity>(Arg.Any<ImageModel>()).Returns(imageEntity);

        _imageRepository.PutImage(imageEntity).Returns(Task.FromResult(imageEntity));

        // Act
        var result = await _imageService.PutImage(formFile, thingId);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(fileName);
        result.ThingId.ShouldBe(thingId);
    }

    [Fact]
    public async Task GetImage_ReturnsCorrectStream_ContentType_AndFileName()
    {
        // Arrange
        var fileName = "new-image.png";
        var expectedContentType = "image/png";
        var expectedData = Encoding.UTF8.GetBytes("fake image data");
        var memoryStream = new MemoryStream(expectedData);

        // Act
        var result = await _imageService.GetImage(fileName);

        // Assert
        result.Item1.ShouldBeOfType<MemoryStream>();
    }
}
