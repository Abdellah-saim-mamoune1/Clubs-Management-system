using EventsManagement.Classes;
using EventsManagement.Dtos;

namespace EventsManagement.db_samples
{
   static public class sample_data
    {


    static public List<StudentSetDto> students = new List<StudentSetDto>
     {
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Alice Johnson",
        Age = 20,
        Degree = "Computer Science",
        YearOfDegree = "2nd Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Michael Smith",
        Age = 22,
        Degree = "Mechanical Engineering",
        YearOfDegree = "3rd Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Sophia Williams",
        Age = 19,
        Degree = "Business Administration",
        YearOfDegree = "1st Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Daniel Brown",
        Age = 23,
        Degree = "Civil Engineering",
        YearOfDegree = "4th Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Emma Davis",
        Age = 21,
        Degree = "Psychology",
        YearOfDegree = "3rd Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "James Wilson",
        Age = 24,
        Degree = "Electrical Engineering",
        YearOfDegree = "4th Year",
        ImageUrl = ""
    },
    new StudentSetDto
    {
        uuid = Guid.NewGuid().ToString(),
        FullName = "Olivia Martinez",
        Age = 20,
        Degree = "Mathematics",
        YearOfDegree = "2nd Year",
        ImageUrl = ""
    },



};

        public static List<string> ClubsTypes = new List<string>
        {
           "Football",
           "Basketball",
           "Volleyball",
           "Tennis",
           "Swimming",
           "Chess",
           "Music",
           "Drama",
           "Photography",
           "Robotics",
           "Debate",
           "Art"
         };

        public static List<Club> clubs = new List<Club>
        { 
          new Club { Name = "Falcon FC", TypeId = 1, Description = "Professional football training club.", OpenForRegistrations = true },
          new Club { Name = "Hoops Academy", TypeId = 2,  Description = "Basketball skill development club.", OpenForRegistrations = true },
          new Club { Name = "Sky Volleyball", TypeId = 3, Description = "Competitive volleyball team.", OpenForRegistrations = false },
          new Club { Name = "Ace Tennis Club", TypeId = 4,  Description = "Tennis training and tournaments.", OpenForRegistrations = true },
          new Club { Name = "Blue Wave Swim", TypeId = 5, Description = "Swimming lessons and competitions.", OpenForRegistrations = true },
          new Club { Name = "Grandmaster Chess", TypeId = 6, Description = "Strategic chess learning club.", OpenForRegistrations = true },
          new Club { Name = "Harmony Music", TypeId = 7,  Description = "Music band and vocal training.", OpenForRegistrations = false },
          new Club { Name = "Stage Stars", TypeId = 8, Description = "Drama and acting club.", OpenForRegistrations = true },
          new Club { Name = "Lens Masters", TypeId = 9, Description = "Photography and editing workshops.", OpenForRegistrations = true },
          new Club { Name = "RoboTech", TypeId = 10,  Description = "Robotics design and competitions.", OpenForRegistrations = false },
          new Club { Name = "Elite Debaters", TypeId = 11,  Description = "Public speaking and debate training.", OpenForRegistrations = true },
          new Club { Name = "Creative Arts Hub", TypeId = 12,  Description = "Painting and creative design club.", OpenForRegistrations = true }
        };


        public static List<Employee> employees = new List<Employee>
        {
          new Employee
          {
         
           Name = "Admin User",
           Account = "admin",
           Password = "Admin123", 
        
          },
          new Employee
          {
        
           Name = "John Manager",
           Account = "john.manager",
           Password = "Manager123",
         
          },
          new Employee
          {
      
           Name = "Sara Supervisor",
           Account = "sara.sup",
           Password = "Supervisor123",
         
          },
          new Employee
          {
        
           Name = "David Staff",
           Account = "david.staff",
           Password = "Staff123!",
       
          }
        };




       public static List<Event> events = new List<Event>
       {
        new Event
        {

        Title = "Football Championship 2026",
        Description = "Annual inter-club football championship.",
        Views = 120,
        Date = new DateOnly(2026, 3, 15),
        From = "10:00 AM",
        To = "2:00 PM",
        Address = "City Stadium",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
        },
        new Event
        {
   
    
        Title = "Basketball Skills Camp",
        Description = "Intensive basketball training for beginners.",
        Views = 85,
        Date = new DateOnly(2026, 4, 10),
        From = "9:00 AM",
        To = "1:00 PM",
        Address = "Sports Hall A",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
        },
        new Event
        {
    
        Title = "Volleyball Friendly Match",
        Description = "Friendly match between local teams.",
        Views = 60,
        Date = new DateOnly(2026, 3, 20),
        From = "5:00 PM",
        To = "7:00 PM",
        Address = "Community Court",
        IsPrivate = true,
        CreatedAt = DateTime.UtcNow
        },
        new Event
       {
   
        Title = "Tennis Open Tournament",
        Description = "Open tournament for all skill levels.",
        Views = 150,
        Date = new DateOnly(2026, 5, 5),
        From = "8:00 AM",
        To = "6:00 PM",
        Address = "Tennis Arena",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
       },
        new Event
       {
    
        Title = "Swimming Competition",
        Description = "Regional swimming competition.",
        Views = 200,
        Date = new DateOnly(2026, 6, 1),
        From = "11:00 AM",
        To = "3:00 PM",
        Address = "Aquatic Center",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
       },
        new Event
       {
    
        Title = "Chess Strategy Workshop",
        Description = "Advanced chess strategies and tactics.",
        Views = 45,
        Date = new DateOnly(2026, 3, 28),
        From = "4:00 PM",
        To = "6:00 PM",
        Address = "Room 204",
        IsPrivate = true,
        CreatedAt = DateTime.UtcNow
       },
       new Event
       {
    
        Title = "Music Live Night",
        Description = "Live performance by club members.",
        Views = 300,
        Date = new DateOnly(2026, 4, 18),
        From = "7:00 PM",
        To = "10:00 PM",
        Address = "Main Auditorium",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
       },
       new Event
       {
   
        Title = "Drama Festival",
        Description = "Annual drama and theater festival.",
        Views = 175,
        Date = new DateOnly(2026, 5, 12),
        From = "6:00 PM",
        To = "9:00 PM",
        Address = "City Theater",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
        },
       new Event
       {

        Title = "Photography Exhibition",
        Description = "Showcase of student photography projects.",
        Views = 95,
        Date = new DateOnly(2026, 4, 25),
        From = "2:00 PM",
        To = "5:00 PM",
        Address = "Art Gallery Hall",
        IsPrivate = false,
        CreatedAt = DateTime.UtcNow
       },
       new Event
       {

        Title = "Robotics Competition",
        Description = "Robotics design and battle competition.",
        Views = 220,
        Date = new DateOnly(2026, 6, 20),
        From = "9:00 AM",
        To = "4:00 PM",
        Address = "Innovation Lab",
        IsPrivate = true,
        CreatedAt = DateTime.UtcNow
       }
};


    }
}
