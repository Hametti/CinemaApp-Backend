using CinemaApp.Data;
using CinemaApp.Data.Entities.Movie;
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
            //_context.Database.EnsureCreated();
            //DeleteDailyViewById(1);
            //AddDailyView();
            //GetDailyViews();
            //Console.ReadKey();

            //Correct linq query to get object by id
            //var viewToDisplay = _context.DailyViews.Where(d => d.DailyViewId == 2).Include(v => v.movieList).ThenInclude(s => s.ShowingHours).FirstOrDefault();
            //DisplayView(viewToDisplay);
            Console.ReadKey();
        }

        private static void DeleteDailyViewById(int id)
        {
            var viewsToDelete = _context.DailyViews.Include(v => v.movieList).ThenInclude(s => s.ShowingHours).ToList();
            var viewToDelete = new DailyView();
            foreach(DailyView item in viewsToDelete)
            {
                if(item.DailyViewId == 1)
                {
                    viewToDelete = item;
                    break;
                }
            }
            _context.Remove(viewToDelete);
            _context.SaveChanges();
        }

        private static void AddDailyView()
        {
            
            DailyView objectToAdd = new DailyView
            {
                Date = "25-10",
                movieList = new List<Movie>
                {
                    new Movie
                    {
                        Name= "No Time To Die",
                        ShowingHours = new List<ShowingHour>{ new ShowingHour() { Hour = "19:15" } }
                    },
                    new Movie
                    {
                        Name= "Suicide Squad",
                        ShowingHours = new List<ShowingHour>{ new ShowingHour() { Hour = "12:00" }, new ShowingHour() { Hour = "15:00" } }
                    }
                }
            };

            _context.DailyViews.Add(objectToAdd);
            _context.SaveChanges();
        }

        private static void GetDailyViews()
        {
            var dailyViews = _context.DailyViews.Include(v => v.movieList).ThenInclude(s => s.ShowingHours).ToList();
            Console.WriteLine("Number of dailyViews: " + dailyViews.Count);
            foreach (var dailyView in dailyViews)
            {
                Console.WriteLine("Daily view id: " + dailyView.DailyViewId);
                Console.WriteLine("Date:" + dailyView.Date);
                foreach (var movie in dailyView.movieList)
                {
                    Console.Write(movie.Name + " ");
                    foreach (var hour in movie.ShowingHours)
                    {
                        Console.Write(hour.Hour + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void DisplayView(DailyView viewToDisplay)
        {
            Console.WriteLine("Daily view id: " + viewToDisplay.DailyViewId);
            Console.WriteLine("Date:" + viewToDisplay.Date);
            foreach (var movie in viewToDisplay.movieList)
            {
                Console.Write(movie.Name + " ");
                foreach (var hour in movie.ShowingHours)
                {
                    Console.Write(hour.Hour + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
