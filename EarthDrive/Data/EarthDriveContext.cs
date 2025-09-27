using Microsoft.EntityFrameworkCore;
using EarthDrive.Models;

namespace EarthDrive.Data
{
    public class EarthDriveContext : DbContext
    {
        public EarthDriveContext (DbContextOptions<EarthDriveContext> options)
            : base(options)
        {
        }

        public DbSet<EarthDrive.Models.TransactionModel> TransactionModel { get; set; } = default!;
        public DbSet<EarthDrive.Models.Customer> Customer { get; set; } = default!;
        public DbSet<EarthDrive.Models.UserAuthorization> AuthorizationTable { get; set; } = default!;
        public DbSet<EarthDrive.Models.Car> Cars { get; set; }

        public DbSet<EarthDrive.Models.Hire> Hires { get; set; }


    }
}
