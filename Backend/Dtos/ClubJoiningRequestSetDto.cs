namespace EventsManagement.Dtos
{
    public class ClubJoiningRequestSetDto
    {
        public int StudentId { get; set; }
        public int ClubId { get; set; }
        public string Email { get; set; }=string.Empty;
        public string Motivation { get; set; } = string.Empty;
    }
}
