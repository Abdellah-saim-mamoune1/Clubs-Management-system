namespace EventsManagement.Dtos
{
    public class RefreshTokenRequestDto
    {

        public required string RefreshToken { get; set; }
        public required string Role { get; set; }


    }

}