using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EarthDrive.Seeding
{
    public class SeedAuthorizationTable
    {
        public static void Initilize(IServiceProvider serviceProvider)
        {
            using var context = new EarthDriveContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EarthDriveContext>>());

            if (context.AuthorizationTable.Any())
            {
                return;
            }
            var hasher = new PasswordHasher<UserAuthorization>();

            context.AuthorizationTable.AddRange(
                new UserAuthorization
                {
                    Username = "Admin",
                    Password = hasher.HashPassword(null, "Admin123"),
                    Role = "Admin"
                }, 
                new UserAuthorization
                {
                    Username = "User1",
                    Password = hasher.HashPassword(null, "User1Password"),
                    Role = "User"
                }, 
                new UserAuthorization
                {
                    Username = "User2",
                    Password = hasher.HashPassword(null, "User2Password"),
                    Role = "User"
                }
                );
            context.SaveChanges();
        }
    }
}
