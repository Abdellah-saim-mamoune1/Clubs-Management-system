namespace EventsManagement.Dtos
{
    public class ClubsRequestsGetDto
    {
        public int StudentId { get; set; }
        public int ClubTypeId { get; set; }
        public int RequestId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
    }
}
