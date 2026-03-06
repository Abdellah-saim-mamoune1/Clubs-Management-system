using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Dtos
{
    public class ClubRequestCreationDto
    {
        
        public int ClubTypeId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Image { get; set; } 

    }
}
