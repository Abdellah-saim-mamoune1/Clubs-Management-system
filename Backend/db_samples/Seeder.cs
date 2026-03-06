using EventsManagement.Classes;
using EventsManagement.Data;
using EventsManagement.Dtos;

namespace EventsManagement.db_samples
{
    public class Seeder(AppDbContext _db)
    {
       
        public async Task Seed()
        {
            foreach (var e in sample_data.employees)
                await CreateEmployee(e);

            for (int i=0; i< sample_data.students.Count;i++)
            {
                int studentId = await CreateStudent(sample_data.students[i]);

                if (i >= sample_data.ClubsTypes.Count) break;

                int clubTypeId = await CreateClubType(sample_data.ClubsTypes[i]);
                sample_data.clubs[i].TypeId= clubTypeId;
                int clubId = await CreateClub(sample_data.clubs[i],studentId);

                if (i < sample_data.events.Count)
                {
                    sample_data.events[i].ClubId = clubId;
                    await CreateEvent(sample_data.events[i], studentId);

                }

            }
            
        }


        public async Task CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

        }
        public async Task<int> CreateStudent(StudentSetDto student)
        {
          
                Classes.User user = new Classes.User
                {
                    Uuid = Guid.Parse(student.uuid),
                    Age = student.Age,
                    Degree = student.Degree,
                    YearOfDegree = student.YearOfDegree,
                    FullName = student.FullName,
                  //  ImageUrl = student.ImageUrl,
                };
                _db.Add(user);
                await _db.SaveChangesAsync();
                return user.Id;
        }


         async Task<int> CreateClubType(string type)
        {
            ClubType clubType = new ClubType { Type = type };

            _db.ClubTypes.Add(clubType);

            await _db.SaveChangesAsync();

            return clubType.Id;
        }

         async Task<int> CreateClub(Club club,int adminId)
        {
            _db.Clubs.Add(club);
            await _db.SaveChangesAsync();

            _db.UserClubs.Add(new UserClub { ClubId = club.Id, UserId = adminId, Role = "Admin" });

            await _db.SaveChangesAsync();

            return club.Id;
        }

         async Task CreateEvent(Event Event,int participantId)
        {
            _db.Events.Add(Event);
            await _db.SaveChangesAsync();

            _db.UserEvents.Add(new UserEvent { UserId = participantId, EventId = Event.Id });
            await _db.SaveChangesAsync();
        }

    }
}
