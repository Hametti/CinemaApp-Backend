using CinemaApp.Database.Entities.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Database
{
    public class CinemaAppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<ScreeningDay> ScreeningDays { get; set; }
        public DbSet<ScreeningHour> ScreeningHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=CinemaAppData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                
        }
    }
}
