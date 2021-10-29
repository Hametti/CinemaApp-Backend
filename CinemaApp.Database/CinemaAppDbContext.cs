using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Database
{
    public class CinemaAppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<ScreeningDay> ScreeningDays { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<UserCred> UserCreds { get; set; }
        public DbSet<WeeklyDiscountMovie> WeeklyDiscountMovie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=CinemaAppData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //.Entity<Blog>()
            //.HasOne(e => e.Owner)
            //.WithOne(e => e.OwnedBlog)
            //.OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ScreeningDay>()
                .HasMany(s => s.Screenings)
                .WithOne(s => s.ScreeningDay)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
