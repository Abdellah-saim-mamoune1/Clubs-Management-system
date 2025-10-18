namespace EventsManagement.Classes
{
    public class ClubRequest
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool AcceptRegistration { get; set; }
        public int MaxRegistrationsCount { get; set; }
        public Club? Club { get; set; }
    }
}
