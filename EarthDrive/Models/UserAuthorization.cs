using System.ComponentModel.DataAnnotations;

namespace EarthDrive.Models
{
    //User Authorization Class (Chris 22500937)
    public class UserAuthorization
    {
        [Key]
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
