namespace EventsManagement.Dtos
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string YearOfDegree { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<UserClubsDto> JoinedClubs { get; set; } = new();
    }

    public class UserClubsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
