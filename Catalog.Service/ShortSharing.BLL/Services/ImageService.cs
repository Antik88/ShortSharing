using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Minio;
using Minio.DataModel.Args;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;
using static ShortSharing.BLL.Constants.BLLConst;

namespace ShortSharing.BLL.Services;

public class ImageService(IImageRepository imageRepository,
    IMapper mapper,
    IMinioClient minioClient) : IImageService
{
    public async Task<(Stream, string, string)> GetImage(string name)
    {
        var memoryStream = new MemoryStream();

        await minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(BucketName)
            .WithObject(name)
            .WithCallbackStream((stream) =>
            {
                stream.CopyToAsync(memoryStream);
                stream.FlushAsync();
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
        var objectName = $"{formFile.FileName}";

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
            ThingId = thingId
        };

        await imageRepository.PutImage(mapper.Map<ImageEntity>(imageModel));

        return imageModel;
    }
}
