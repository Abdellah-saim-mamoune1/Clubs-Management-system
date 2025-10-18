using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class UserClub
    {
        public int Id { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; } 
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateOnly JoinedAt { get; set; }
        public Club? Club { get; set; } 
        public User? User { get; set; }
    }
}
