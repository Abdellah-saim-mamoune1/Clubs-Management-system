using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class EventImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }=string.Empty;

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public bool IsMainImage { get; set; }
        public Event? Event { get; set; }
    }
}
