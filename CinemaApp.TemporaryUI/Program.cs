using CinemaApp.Database;
using CinemaApp.Database.Entities.Movie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApp.TemporaryUI
{
    class Program
    {
        private static CinemaAppDbContext _context = new CinemaAppDbContext();
        static void Main(string[] args)
        {
            DisplayMovies();
            //DisplayDailyViews();
            //AddDailyView();
            //UpdateRecord();

            Console.WriteLine("\nSucceed");
            Console.ReadKey();
        }

        private static void DeleteDailyViewById(int id)
        {
           
        }

        private static void AddDailyView()
        {
            
            DailyView objectToAdd = new DailyView
            {
                Date = "25-10",
                MovieList = new List<Movie>
                {
                    new Movie
                    {
                        Name= "No Time To Die",
                        ShowingHourList = new List<ShowingHour>{ new ShowingHour() { Hour = "19:15" } }
                    },
                    new Movie
                    {
                        Name= "Suicide Squad",
                        ShowingHourList = new List<ShowingHour>{ new ShowingHour() { Hour = "12:00" }, new ShowingHour() { Hour = "15:00" } }
                    }
                }
            };

            _context.DailyViews.Add(objectToAdd);
            _context.SaveChanges();
        }

        private static void DisplayDailyViews()
        {
            var dailyViews = _context.DailyViews.Include(v => v.MovieList).ThenInclude(s => s.ShowingHourList).ToList();

            foreach(DailyView dailyView in dailyViews)
            {
                Console.WriteLine($"Daily view id: {dailyView.Id}");
                Console.WriteLine($"Daily view date: {dailyView.Date}\n");

                Console.WriteLine("DailyView movies:");
                foreach(Movie movie in dailyView.MovieList)
                {
                    Console.WriteLine($"Movie Id: {movie.Id}");
                    Console.WriteLine($"Movie Id: {movie.Id}\n");

                    Console.WriteLine("Movie showing hours: ");
                    foreach(ShowingHour showingHour in movie.ShowingHourList)
                    {
                        Console.WriteLine($"{showingHour.Hour} ");
                    }
                }

                Console.WriteLine("-----------------------------------------");
                Console.WriteLine();
            }
            
        }

        private static void DisplayMovies()
        {
            var movies = _context.Movies.Include(m => m.DailyViewList).Include(m => m.ShowingHourList).ToList();

            Console.WriteLine("All movies:");
            
            foreach(Movie movie in movies)
            {
                Console.WriteLine($"Movie Id: {movie.Id}");
                Console.WriteLine($"Movie title: {movie.Name}\n");

                Console.WriteLine($"Movie DailyViewList: ");
                foreach(DailyView dailyView in movie.DailyViewList)
                    Console.WriteLine($"Movie DailyView: {dailyView.Id}");
                Console.WriteLine();

                Console.WriteLine("Movie ShowingHourList: ");
                foreach(ShowingHour showingHour in movie.ShowingHourList)
                {
                    Console.WriteLine($"ShowingHourId: {showingHour.Id}");
                    Console.WriteLine($"ShowingHour: {showingHour.Hour}");
                    Console.WriteLine();
                }
                Console.WriteLine("-----------------------------------------");
            }
        }

        static void UpdateRecord()
        {
            var movie = _context.Movies.Include(m => m.ShowingHourList).FirstOrDefault(m => m.Id == 2);
            var showingHourToAdd = _context.ShowingHours.FirstOrDefault(h => h.Id == 1);
            movie.ShowingHourList.Add(showingHourToAdd);
            _context.SaveChanges();
        }
    }
}
