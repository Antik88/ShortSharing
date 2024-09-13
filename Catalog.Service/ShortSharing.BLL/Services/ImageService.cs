using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Services;

public class ImageService(IImageRepository imageRepository, IMapper mapper) : IImageService
{
    const string BucketName = "sharing";

    public async Task<(MemoryStream, string, string)> GetImage(string name)
    {
        var memoryStream = new MemoryStream();

        await minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(BucketName)
            .WithObject(name)
            .WithCallbackStream((stream) =>
            {
                stream.CopyToAsync(memoryStream);
            }));

        memoryStream.Position = 0;

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(name, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return (memoryStream, contentType, name);
    }

    public async Task<ImageModel> PutImage(IFormFile formFile, Guid thingId)
    {
        var objectName = $"{thingId}-{formFile.FileName}";

        using var stream = formFile.OpenReadStream();
        await minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(BucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(formFile.Length)
            .WithContentType(formFile.ContentType));

        var imageModel = new ImageModel
        {
            Name = formFile.FileName,
        };

        await imageRepository.PutImage(mapper.Map<ImageEntity>(imageModel));

        return imageModel;
    }
}
