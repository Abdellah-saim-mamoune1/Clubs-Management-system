namespace EventsManagement.Classes
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Account { get; set; }=string.Empty;
        public string HashedPassword { get; set; }=string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
