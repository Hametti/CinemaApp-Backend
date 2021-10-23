using CinemaApp.Database.Entities.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Database
{
    public class CinemaAppDbContext : DbContext
    {
        public DbSet<DailyView> DailyViews { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ShowingHour> ShowingHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=CinemaAppData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyView>()
                .HasMany(s => s.MovieList)
                .WithMany(m => m.DailyViewList);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.ShowingHourList)
                .WithMany(h => h.MovieList);
        }
    }
}
