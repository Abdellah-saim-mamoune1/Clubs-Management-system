namespace EventsManagement.Dtos
{
    public class ClubsGetPaginatedDto
    {
       public int TotalCount { get; set; }
        public List<ClubInfoGetDto> Clubs { get; set; } = new();
    }


}
