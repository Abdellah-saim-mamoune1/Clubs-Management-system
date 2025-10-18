using EventsManagement.Dtos;

namespace EventsManagement.Interfaces.Repositories.Student
{
    public interface IStudentRepository
    {
        public  Task<StudentGetDto?> GetAsync(int Id);
        public  Task UpdateImageAsync(int Id, string ImageUrl);
    }
}
