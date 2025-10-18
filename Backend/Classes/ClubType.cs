namespace EventsManagement.Classes
{
    public class ClubType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public ICollection<Club> Clubs { get; set; } = new List<Club>();
    }
}
