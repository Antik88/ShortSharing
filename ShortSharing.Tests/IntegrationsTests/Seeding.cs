using MassTransit.Futures.Contracts;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;

namespace ShortSharing.Tests.IntegrationsTests;

public class Seeding
{
    public static readonly string iPhoneName = "iPhone";
    public static readonly Guid MacBookId = Guid.NewGuid();
    public static readonly Guid iPhoneId = Guid.NewGuid();

    public static readonly Guid CategoryId = Guid.NewGuid();
    public static readonly Guid TypeId = Guid.NewGuid();
    public static readonly Guid OwnerId = Guid.NewGuid();

    public static void InitializeTestDatabase(ApplicationDbContext db)
    {
        var categories = GetCategories();
        db.Categories.AddRange(categories);

        var types = GetTypes(categories);
        db.Types.AddRange(types);

        var users = GetUsers();
        db.Users.AddRange(users);

        var things = GetThings(categories, types, users);
        db.Things.AddRange(things);

        db.SaveChanges();
    }

    private static List<CategoryEntity> GetCategories()
    {
        return new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id = CategoryId,
                    Name = "Electronics"
                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Books"
                }
            };
    }

    private static List<TypeEntity> GetTypes(List<CategoryEntity> categories)
    {
        return new List<TypeEntity>
            {
                new TypeEntity
                {
                    Id = TypeId,
                    Name = "Smartphone",
                    Category = categories[0]
                },
                new TypeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Laptop",
                    Category = categories[0]
                },
                new TypeEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Fiction",
                    Category = categories[1]
                }
            };
    }

    private static List<UserEntity> GetUsers()
    {
        return new List<UserEntity>
            {
                new UserEntity
                {
                    Id = OwnerId,
                    Name = "john_doe",
                    Email = "john_doe@example.com"
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "jane_doe",
                    Email = "jane_doe@example.com"
                }
            };
    }

    private static List<ThingEntity> GetThings(List<CategoryEntity> categories, List<TypeEntity> types, List<UserEntity> users)
    {
        return new List<ThingEntity>
            {
                new ThingEntity
                {
                    Id = iPhoneId,
                    Name = iPhoneName,
                    Description = "Latest iPhone model",
                    Price = 999.99,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Category = categories[0],
                    Type = types[0],
                    Owner = users[0]
                },
                new ThingEntity
                {
                    Id = MacBookId,
                    Name = "MacBook Pro",
                    Description = "Latest MacBook Pro with M1 chip",
                    Price = 1999.99,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Category = categories[0],
                    Type = types[1],
                    Owner = users[1]
                }
            };
    }
}
