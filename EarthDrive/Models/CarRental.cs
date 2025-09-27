using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EarthDrive.Models
{
    public class CarRental : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [StringLength(60)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Phone]
        [StringLength(30)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(120)]
        public string PickupLocation { get; set; }

        [Required]
        [StringLength(120)]
        public string DropoffLocation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal EstimatedCost { get; set; }

        public Car Car { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < PickupDate)
            {
                yield return new ValidationResult(
                    "Return date cannot be earlier than pickup date.",
                    new[] { nameof(ReturnDate) });
            }
        }
    }
}
