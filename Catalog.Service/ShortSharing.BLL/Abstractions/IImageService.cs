using ShortSharing.BLL.Models;

namespace ShortSharing.BLL.Abstractions;

public interface IImageService
{
    Task<ImageModel> PutImage(ImageModel image);
    Task<(Stream, string, string)> GetImage(string imageName);
}
