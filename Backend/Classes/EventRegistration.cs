namespace EventsManagement.Classes
{
    public class EventRegistration
    {
        public int Id { get; set; }
        public int MaxRegistrationsCount { get; set; }
        public int CurrentRegistrationsCount { get; set; }
        public Event? Event { get; set; }
    }
}
