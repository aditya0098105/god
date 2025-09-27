using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EarthDrive.Models
{
    public class Car
    {
        public int Id { get; set; }  // Primary Key

        [Required, StringLength(12)]
        public string RegistrationNumber { get; set; }

        [Required, StringLength(50)]
        public string Model { get; set; }

        [Range(1, 9, ErrorMessage = "Capacity must be between 1 and 9.")]
        public int Capacity { get; set; }

        [Range(1, 100000, ErrorMessage = "RatePerDay must be positive.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal RatePerDay { get; set; }

        [Required, RegularExpression("Good|Fair|Poor", ErrorMessage = "Status must be Good, Fair, or Poor.")]
        public string Status { get; set; }  // Good, Fair, Poor

        public bool Available { get; set; } // Yes/No
    }
}
