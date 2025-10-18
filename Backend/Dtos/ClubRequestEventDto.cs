namespace EventsManagement.Dtos
{
    public class ClubRequestEventDto
    {
        public int ClubId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
 
        public int MaxRegistrationsCount { get; set; }
    }
}
