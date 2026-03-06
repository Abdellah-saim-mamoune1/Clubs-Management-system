namespace EventsManagement.Dtos
{
        public class EventsGetPaginatedDto
        {
        public List<EventsGetDto>? Events { get; set; }
        public int TotalPages { get; set; }

        }

        public class EventsGetDto
        {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public int Views { get; set; }
        public bool IsStudentJoined { get; set; }
        public bool IsStudentMember { get; set; }
        public bool IsStudentAdmin { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public RegistrationInfoDto? RegistrationInfo { get; set; }

        }

       public class RegistrationInfoDto
       {
        public int MaxRegistrationsCount { get; set; }
        public int CurrentRegistrationsCount { get; set; }
       }
    
}
