using Microsoft.AspNetCore.Http;
using ShortSharing.BLL.Models;

namespace ShortSharing.BLL.Abstractions;

public interface IImageService
{
    Task<ImageModel> PutImage(IFormFile formFile, Guid thingId);
    Task<(Stream, string, string)> GetImage(string name);
}
