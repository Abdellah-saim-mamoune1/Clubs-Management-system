namespace EventsManagement.Dtos
{
    public class ClubInfoGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool OpenForRegistrations { get; set; }
        public string JoiningStatus { get; set; } = string.Empty;
        public string StudentRole { get; set; } = string.Empty;
        public int EventsNumber { get; set; }
        public int MembersNumber { get; set; }
  
    }
}
