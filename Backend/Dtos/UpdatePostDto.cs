namespace EventsManagement.Dtos
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        public bool IsPrivate { get; set; }
        public int MaxRegistrationCount { get; set; }
    }
}
