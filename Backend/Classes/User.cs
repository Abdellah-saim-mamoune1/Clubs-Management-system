using System.ComponentModel.DataAnnotations;

namespace EventsManagement.Classes
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid Uuid { get; set; }= Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string YearOfDegree { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<UserEvent>? UserEvents { get; set; } = new List<UserEvent>();
        public ICollection<UserClub>? UserClubs { get; set; } = new List<UserClub>();
        public ICollection<ClubJoiningRequest>? ClubJoiningRequests { get; set; } = new List<ClubJoiningRequest>();
        public Token? Token { get; set; }
        public RequestedClub? RequestedClub { get; set; }

    }
}
