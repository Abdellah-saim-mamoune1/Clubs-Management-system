namespace EventsManagement.Dtos
{
    public class StudentsGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Degree { get; set; } = string.Empty;
    }
}
