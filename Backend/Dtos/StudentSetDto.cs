namespace EventsManagement.Dtos
{
    public class StudentSetDto
    {
        public string uuid { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string YearOfDegree { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
