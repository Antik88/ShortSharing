using User.Service.DLL.Context;
using User.Service.DLL.Entities;

namespace User.Service.Test.IntegrationTests
{
    public static class Seed
    {
        public static async Task InitializeTestDatabase(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AuthId = "AuthId1",
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "Password123"
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AuthId = "AuthId2",
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Password = "Password456"
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AuthId = "AuthId3",
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Password = "Password789"
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
