namespace EventsManagement.Dtos
{
    public class ServiceResponseDto<T>
    {
        public int Status { get; set; }
        public T? Data { get; set; }
    }

}
