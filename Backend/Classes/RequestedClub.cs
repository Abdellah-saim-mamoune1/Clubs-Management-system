using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class RequestedClub
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int StudentId { get; set; }

        [ForeignKey("ClubType")]
        public int ClubTypeId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageContentType { get; set; }
        public byte[]? ImageData { get; set; }
        public DateOnly CreatedAt { get; set; }
        public User? User { get; set; }
        public ClubType? Type { get; set; }

    }
}
