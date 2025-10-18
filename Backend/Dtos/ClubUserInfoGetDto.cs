namespace EventsManagement.Dtos
{
    public class ClubUserInfoGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string YearOfDegree { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public DateOnly JoinedAt { get; set; }
    }
}
