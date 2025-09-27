using System.ComponentModel.DataAnnotations;

namespace EarthDrive.Models
{
    //Tranaction Detales Chris 22500937
    public class TransactionModel
    {
        [Key]
        public int TransactionID { get; set; }
        public int CustomerID { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        //Car Renteal Details
        public int CarID { get; set; }
        public string? CarPlateNumber { get; set; }
        public int DurationOfRental { get; set; }
        public string? PickupLocation { get; set; }
        [DataType(DataType.Date)]
        public DateTime PickUpTime { get; set; }
        public string? DropoffLocation { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnTime { get; set; }
        public string? VehicalType { get; set; }

        //Payment Details
        public double DailyRate { get; set; }
        public double LateFees { get; set; }
        public double TotalDue { get; set; }
        public string? TransactionStatus { get; set; }

        //Extras
        public bool ConfirmationStatus { get; set; }
        public string? ExtraNotes { get; set; }
    }
}
