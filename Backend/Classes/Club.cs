using System.ComponentModel.DataAnnotations.Schema;

namespace EventsManagement.Classes
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("ClubType")]
        public int TypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool OpenForRegistrations { get; set; }
        public ClubType? ClubType { get; set; }
        public RequestedClub? RequestedClub { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<ClubRequest> Requests { get; set; } = new List<ClubRequest>();
        public ICollection<UserClub> UserClubs { get; set; } = new List<UserClub>();
        public ICollection<ClubJoiningRequest>? ClubJoiningRequests { get; set; } = new List<ClubJoiningRequest>();

    }
}
