using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Abstractions;

public interface IImageRepository
{
    Task<ImageEntity> PutImage(ImageEntity image);
    Task<(Stream, string, string)> GetImage(string imageName);
}
