using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.EntityFrameworkCore;

namespace EarthDrive.Seeding
{
    public static class SeedingCars
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new EarthDriveContext(
                serviceProvider.GetRequiredService<DbContextOptions<EarthDriveContext>>());

            
            if (context.Cars.Any())
            {
                return;
            }

           
            context.Cars.AddRange(
                new Car
                {
                    RegistrationNumber = "ABC123",
                    Model = "Corolla",
                    Capacity = 4,
                    RatePerDay = 50,
                    Status = "Good",
                    Available = true
                },
                new Car
                {
                    RegistrationNumber = "XYZ789",
                    Model = "Civic",
                    Capacity = 5,
                    RatePerDay = 70,
                    Status = "Fair",
                    Available = true
                },
                new Car
                {
                    RegistrationNumber = "LMN456",
                    Model = "Camry",
                    Capacity = 4,
                    RatePerDay = 80,
                    Status = "Poor",
                    Available = false
                }
            );

            context.SaveChanges();
        }
    }
}
