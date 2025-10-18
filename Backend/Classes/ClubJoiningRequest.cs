using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class ClubJoiningRequest
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int StudentId { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Motivation { get; set; } = string.Empty;
        public User? User { get; set; }
        public Club? Club { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
