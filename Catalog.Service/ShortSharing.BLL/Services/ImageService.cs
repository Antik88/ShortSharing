using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;

namespace ShortSharing.BLL.Services;

public class ImageService(IImageRepository imageRepository, IMapper mapper) : IImageService
{
    public Task<(Stream, string, string)> GetImage(string imageName)
    {
        throw new NotImplementedException();
    }

    public Task<ImageModel> PutImage(ImageModel image)
    {
        throw new NotImplementedException();
    }
}
