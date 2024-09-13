using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Repositories;

public class ImageRepository(ApplicationDbContext context) : IImageRepository
{
    public async Task<ImageEntity> PutImage(ImageEntity image)
    {
        context.Images.Add(image);

        await context.SaveChangesAsync();

        return image;
    } 
    public Task<(Stream, string, string)> GetImage(string imageName)
    {
        throw new NotImplementedException();
    }
}
