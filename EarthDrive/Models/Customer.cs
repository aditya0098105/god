using System.ComponentModel.DataAnnotations;

namespace EarthDrive.Models
{
    public class Customer
    {
        [Key]
        public int cId { get; set; } //primary key

        [Required(ErrorMessage = "Name field is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")] //simple validation for name
        [Display(Name = "Name")]
        public required string cName { get; set; } //required forces you to set property

        [Required(ErrorMessage = "Date of birth field is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime cDob { get; set; } //no ?, cannot be null


        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")] //phone number only accepts 10 digits
        [Display(Name = "Phone Number")]
        public string? cPhone { get; set; } //not required

        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")] //simple validation for email
        [Display(Name = "Email Address")]
        public required string cEmail { get; set; } //required
    }
}
