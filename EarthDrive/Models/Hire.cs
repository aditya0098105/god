using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  
using System.Collections.Generic;

namespace EarthDrive.Models
{
    public class Hire : IValidatableObject   // 👈 Interface add
    {
        public int Id { get; set; }

        // --- Car selection ---
        [Required]
        public int CarId { get; set; }   // FK to Cars

        // --- Customer details ---
        [Required, StringLength(60)]
        public string CustomerName { get; set; }

        // --- Addresses ---
        [Required, StringLength(120)]
        public string PickupAddress { get; set; }

        [Required, StringLength(120)]
        public string DropoffAddress { get; set; }

        // --- Dates ---
        [Required, DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        // --- Money ---
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]   
        public decimal TotalAmount { get; set; }  

        // Navigation
        public Car Car { get; set; }

        // --- Extra validation for dates ---
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < HireDate)
            {
                yield return new ValidationResult(
                    "Return date cannot be earlier than hire date.",
                    new[] { nameof(ReturnDate) });
            }
        }
    }
}
