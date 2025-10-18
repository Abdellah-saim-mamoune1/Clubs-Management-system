using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class Token
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
