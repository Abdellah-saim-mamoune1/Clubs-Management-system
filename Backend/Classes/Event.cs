using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class Event
    {
        public int Id { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        [ForeignKey("EventRegistration")]

        public int? RegistrationId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? ImageContentType { get; set; }
        public byte[]? ImageData { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Views { get; set; }

        public DateOnly Date { get; set; }
        public string From { get; set; }=string.Empty;
        public string To { get; set; }= string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Club? Club { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
        public EventRegistration? EventRegistration { get; set; }


    }
}
