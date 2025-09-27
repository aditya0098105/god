using EarthDrive.Data;
using EarthDrive.Models;
using Microsoft.EntityFrameworkCore;

namespace EarthDrive.Seeding
{
    public class SeedTranscriptTable
    {
        public static void Initilize(IServiceProvider serviceProvider)
        {
            using var context = new EarthDriveContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EarthDriveContext>>());

            if (context.TransactionModel.Any())
            {
                return;
            }

            context.TransactionModel.AddRange(
                new TransactionModel
                {
                    CustomerID = 101,
                    TransactionDate = DateTime.Parse("2025 - 09 - 01"),

                    CarID = 501,
                    CarPlateNumber = "1234AB",
                    DurationOfRental = 5,
                    PickupLocation = "Downtown",
                    PickUpTime = DateTime.Parse("2025 - 09 - 01 10:00 AM"),
                    DropoffLocation = "Airport",
                    ReturnTime = DateTime.Parse("2025 - 09 - 06 12:00 PM"),
                    VehicalType = "SUV",

                    DailyRate = 50.00,
                    LateFees = 0.00,
                    TotalDue = 250.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true,
                    ExtraNotes = "may be late for pick up."

                },
                new TransactionModel
                {
                    CustomerID = 102,
                    TransactionDate = DateTime.Parse("2025 - 09 - 03"),

                    CarID = 502,
                    CarPlateNumber = "5678CD",
                    DurationOfRental = 3,
                    PickupLocation = "Airport",
                    PickUpTime = DateTime.Parse("2025 - 09 - 03 8:00 AM"),
                    DropoffLocation = "Downtown",
                    ReturnTime = DateTime.Parse("2025 - 09 - 06 6:00 PM"),
                    VehicalType = "Sedan",

                    DailyRate = 40.00,
                    LateFees = 20.00,
                    TotalDue = 140.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true

                },
                new TransactionModel
                {
                    CustomerID = 103,
                    TransactionDate = DateTime.Parse("2025 - 09 - 05"),

                    CarID = 503,
                    CarPlateNumber = "7890EF",
                    DurationOfRental = 1,
                    PickupLocation = "City Center",
                    PickUpTime = DateTime.Parse("2025 - 09 - 05 9:00 AM"),
                    DropoffLocation = "City Center",
                    ReturnTime = DateTime.Parse("2025 - 09 - 05 9:00 PM"),
                    VehicalType = "Convertible",

                    DailyRate = 150.00,
                    LateFees = 0.00,
                    TotalDue = 150.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true

                },
                new TransactionModel
                {
                    CustomerID = 104,
                    TransactionDate = DateTime.Parse("2025 - 09 - 07"),

                    CarID = 504,
                    CarPlateNumber = "8901GH",
                    DurationOfRental = 7,
                    PickupLocation = "Downtown",
                    PickUpTime = DateTime.Parse("2025 - 09 - 07 11:00 AM"),
                    DropoffLocation = "Beach Resort",
                    ReturnTime = DateTime.Parse("2025 - 09 - 14 2:00 PM"),
                    VehicalType = "Minivan",

                    DailyRate = 70.00,
                    LateFees = 30.00,
                    TotalDue = 510.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true,
                    ExtraNotes = "Car was damaged at the front before pickup"

                },
                new TransactionModel
                {
                    CustomerID = 105,
                    TransactionDate = DateTime.Parse("2025 - 09 - 10"),

                    CarID = 505,
                    CarPlateNumber = "9012IJ",
                    DurationOfRental = 10,
                    PickupLocation = "Airport",
                    PickUpTime = DateTime.Parse("2025 - 09 - 10 7:00 AM"),
                    DropoffLocation = "City Center",
                    ReturnTime = DateTime.Parse("2025 - 09 - 20 9:00 AM"),
                    VehicalType = "Pickup Truck",

                    DailyRate = 100.00,
                    LateFees = 0.00,
                    TotalDue = 1000.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true

                },
                new TransactionModel
                {
                    CustomerID = 106,
                    TransactionDate = DateTime.Parse("2025 - 09 - 12"),

                    CarID = 506,
                    CarPlateNumber = "2345KL",
                    DurationOfRental = 4,
                    PickupLocation = "Beach Resort",
                    PickUpTime = DateTime.Parse("2025 - 09 - 12 10:00 AM"),
                    DropoffLocation = "Downtown",
                    ReturnTime = DateTime.Parse("2025 - 09 - 16 8:00 AM"),
                    VehicalType = "Coupe",

                    DailyRate = 80.00,
                    LateFees = 0.00,
                    TotalDue = 320.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true

                },
                new TransactionModel
                {
                    CustomerID = 107,
                    TransactionDate = DateTime.Parse("2025 - 09 - 15"),

                    CarID = 507,
                    CarPlateNumber = "3456MN",
                    DurationOfRental = 2,
                    PickupLocation = "City Center",
                    PickUpTime = DateTime.Parse("2025 - 09 - 15 2:00 PM"),
                    DropoffLocation = "Airport",
                    ReturnTime = DateTime.Parse("2025 - 09 - 17 6:00 PM"),
                    VehicalType = "Luxury Sedan",

                    DailyRate = 200.00,
                    LateFees = 50.00,
                    TotalDue = 450.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true

                },
                new TransactionModel
                {
                    CustomerID = 108,
                    TransactionDate = DateTime.Parse("2025 - 09 - 18"),

                    CarID = 508,
                    CarPlateNumber = "4567OP",
                    DurationOfRental = 6,
                    PickupLocation = "Airport",
                    PickUpTime = DateTime.Parse("2025 - 09 - 18 3:00 PM"),
                    DropoffLocation = "Downtown",
                    ReturnTime = DateTime.Parse("2025 - 09 - 24 9:00 AM"),
                    VehicalType = "Hatchback",

                    DailyRate = 45.00,
                    LateFees = 0.00,
                    TotalDue = 270.00,
                    TransactionStatus = "Completed",

                    ConfirmationStatus = true
                }
            );
            context.SaveChanges();
        }
    }
}
