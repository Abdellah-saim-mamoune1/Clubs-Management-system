namespace EventsManagement.Dtos
{
    public class ClubUpdateDto
    {
        public int ClubId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool OpenForRegistrations { get; set; }
        public IFormFile? image { get; set; } 
        public int TypeId { get; set; }
    }
}
