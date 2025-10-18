using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class UserEvent
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}
