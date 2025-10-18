namespace EventsManagement.Interfaces.Services
{
    public interface IProgressService
    {
        public Task<(Guid? uuid, string? token)> LoginStudentAsync(string id, string password);
        public Task Registre(Guid uuid, string token);
    }
}
