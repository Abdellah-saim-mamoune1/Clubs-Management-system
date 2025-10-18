namespace EventsManagement.Dtos
{
    public class TokenResponseDto
    {
        public required string Token { get; set; }
        public required Guid Uuid { get; set; }
    }
}
