namespace EventsManagement.Dtos
{

    public class ClubJoiningRequestPaginatedGetDto
    {
        public List<ClubJoiningRequestGetDto> Applications { get; set; } = new();

        public int TotalCount { get; set; }
    }
        public class ClubJoiningRequestGetDto
    { 
        public int ApplicationId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }=string.Empty;
        public string StudentImageUrl { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentMotivation { get; set; }=string.Empty;
        public DateOnly Date { get; set; }
    }
}
