using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.EntityFrameworkCore;

namespace EarthDrive.Seeding
{
    public class CustomerSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EarthDriveContext(
                serviceProvider.GetRequiredService<DbContextOptions<EarthDriveContext>>()))
            {
                //look for customers
                if (context.Customer.Any())
                {
                    return;
                }
                context.Customer.AddRange(
                    new Customer
                    {
                        cName = "Anna Williams",
                        cDob = DateTime.Parse("1983-3-21"),
                        cPhone = "1234567890",
                        cEmail = "anna.williams21@gmail.com"
                    },
                    new Customer
                    {
                        cName = "Jin Kazama",
                        cDob = DateTime.Parse("2003-8-5"),
                        cPhone = "0987654321",
                        cEmail = "jin.kazama5@gmail.com"
                    },
                    new Customer
                    {
                        cName = "Emilie De Rochefort",
                        cDob = DateTime.Parse("2006-11-11"),
                        cPhone = "7890123456",
                        cEmail = "lili.rochefort11@gmail.com"
                    },
                    new Customer
                    {
                        cName = "Clive Rosfield",
                        cDob = DateTime.Parse("1992-10-1"),
                        cPhone = "4321567890",
                        cEmail = "clive.rosfield1@gmail.com"
                    },
                    new Customer
                    {
                        cName = "Asuka Kazama",
                        cDob = DateTime.Parse("2003-12-5"),
                        cPhone = "0987123456",
                        cEmail = "asuka.kazama5@gmail.com"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
