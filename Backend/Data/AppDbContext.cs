using EventsManagement.Classes;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Data
{
  
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

         
            public DbSet<User> Users { get; set; }
            public DbSet<Club> Clubs { get; set; }
            public DbSet<ClubType> ClubTypes { get; set; }
            public DbSet<Event> Events { get; set; }
            public DbSet<UserClub> UserClubs { get; set; }
            public DbSet<UserEvent> UserEvents { get; set; }
            public DbSet<ClubRequest> ClubsRequests { get; set; }
            public DbSet<EventRegistration> EventsRegistrations { get; set; }
            public DbSet<ClubJoiningRequest> ClubJoiningRequests { get; set; }
            public DbSet<Employee> Employees { get; set; }

        public DbSet<RequestedClub> RequestedClubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<Event>()
             .Property(l => l.CreatedAt)
             .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ClubJoiningRequest>()
              .Property(l => l.CreatedAt)
              .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<RequestedClub>()
              .Property(l => l.CreatedAt)
              .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);

            }
      
    }
}
