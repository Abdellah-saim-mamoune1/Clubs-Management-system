using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Dtos
{
    public class ClubRequestCreationDto
    {
        
        public int ClubTypeId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

    }
}
