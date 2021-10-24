﻿using CinemaApp.Database;
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
            //AddSampleMovies();
            //DisplayDailyViews();
            //AddDailyView();
            //UpdateRecord();
            //DeleteRecord();
            //DisplayMovies();

            Console.WriteLine("\nSucceed");
            Console.ReadKey();
        }

        private static void DeleteRecord()
        {
            var hourToDelete = _context.ShowingHours.FirstOrDefault(h => h.Hour == "19:15");

            _context.ShowingHours.Remove(hourToDelete);
            _context.SaveChanges();
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
                        Title= "No Time To Die",
                        ShowingHourList = new List<ShowingHour>{ new ShowingHour() { Hour = "19:15" } }
                    },
                    new Movie
                    {
                        Title= "Suicide Squad",
                        ShowingHourList = new List<ShowingHour>{ new ShowingHour() { Hour = "12:00" }, new ShowingHour() { Hour = "15:00" } }
                    }
                }
            };

            _context.DailyViews.Add(objectToAdd);
            _context.SaveChanges();
        }

        private static void AddSampleMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie
                    {
                        Title = "Inception",
                        PictureUrl = "inception.jpg",
                        ShortDescription = "Dominick \"Dom\" Cobb and Arthur are \"extractors\"; they perform corporate espionage using experimental military technology to infiltrate their targets' subconscious and extract information through a shared dream world.",
                        Director = "Christopher Nolan",
                        Cast = "Leonardo Di Caprio | Joseph Gordon-Levitt | Elliot Page | Tom Hardy | Ken Watanabe | Dileep Rao | Cillian Murphy | Tom Berenger | Marion Cotillard | Pete Postlethwaite | Michael Caine | Lukas Haas | Talulah Riley",
                        LongDescription = "Dominick \"Dom\" Cobb and Arthur are \"extractors\"; they perform corporate espionage using experimental military technology to infiltrate their targets' subconscious and extract information through a shared dream world. Their latest target, Saito, reveals he arranged their mission to test Cobb for a seemingly impossible job: implanting an idea in a person's subconscious, or \"inception\". Saito wants Cobb to convince Robert, the son of Saito's competitor Maurice Fischer, to dissolve his father's company. Saito promises to clear Cobb's criminal status, which prevents him from returning home to his children.",
                        ReleaseYear = "2010",
                        Language = "English",
                        Duration = "2h 28m",
                        Budget = "$160 million"
                    },
                new Movie
                {
                    Title = "No Time To Die",
                    PictureUrl = "no-time-to-die.jpg",
                    ShortDescription = "Bond has left active service and is enjoying a tranquil life in Jamaica. His peace is short-lived when his old friend Felix Leiter from the CIA turns up asking for help.",
                    Director = "Cary Joji Fukunaya",
                    Cast = "Rami Malek | Ben Whishaw | Ralph Fiennes | Lashana Lynch | Ana de Armas | Billy Magnussen | Daniel Craig | Jeffrey Wright | Lea Seydoux | Naomie Harris | Rory Kinnear | David Dencik | Dali Benssalah",
                    LongDescription = "In No Time To Die, Bond has left active service and is enjoying a tranquil life in Jamaica. His peace is short-lived when his old friend Felix Leiter from the CIA turns up asking for help. The mission to rescue a kidnapped scientist turns out to be far more treacherous than expected, leading Bond onto the trail of a mysterious villain armed with dangerous new technology.",
                    ReleaseYear = "2021",
                    Language = "2021",
                    Duration = "2h 43m",
                    Budget = "$250-301 million"
                },
                new Movie
                {
                    Title = "Green Mile",
                    PictureUrl = "green-mile.jpg",
                    ShortDescription = "At a Louisiana assisted-living home in 1999, elderly retiree Paul Edgecomb becomes emotional while viewing the film Top Hat. His companion Elaine becomes concerned, and Paul explains to her that the film reminded him of events that he witnessed in 1935 when he was an officer at Cold Mountain Penitentiary's death row, nicknamed \"The Green Mile\".",
                    Director = "Frank Darabont",
                    Cast = "Tom Hanks | David Morse | Bonnie Hunt | Michael Clarke Duncan | James Cromwell | Michael Jeter | Graham Greene | Doug Hutchison | Sam Rockwell | Barry Pepper | Jeffrey DeMunn | Patricia Clarkson | Harry Dean Stanton",
                    LongDescription = "In 1935, Paul supervises Corrections Officers Brutus Howell, Dean Stanton, Harry Terwilliger, and Percy Wetmore, reporting to chief warden Hal Moores. Paul is introduced to John Coffey, a physically imposing but mild-mannered black man, who has been sentenced to death after being convicted of raping and murdering two little white girls. He joins two other condemned convicts on the block: Eduard \"Del\" Delacroix and Arlen Bitterbuck, the latter of whom is the first to be executed. Percy, the nephew of the state governor's wife, demonstrates a sadistic streak but flaunts his family connections to avoid being held accountable; he is particularly abusive towards Del, breaking his fingers and killing his pet mouse Mr. Jingles.",
                    ReleaseYear = "1999",
                    Language = "English",
                    Duration = "3h 9m",
                    Budget = "$60 million"
                },
                new Movie
                {
                    Title = "Interstellar",
                    PictureUrl = "interstellar.jpg",
                    ShortDescription = "In 2067, crop blights and dust storms threaten humanity's survival. Cooper, a widowed engineer and former NASA pilot turned farmer, lives with his father-in-law, Donald, his 15-year-old son, Tom, and 10-year-old daughter, Murphy \"Murph\". After a dust storm, strange dust patterns inexplicably appear in Murphy's bedroom; she attributes the anomaly to a ghost. ",
                    Director = "Christopher Nolan",
                    Cast = "Matthew McConaughey | Anne Hathaway | Jessica Chastain | Bill Irwin | Ellen Burstyn | Michael Caine",
                    LongDescription = "In 2067, crop blights and dust storms threaten humanity's survival. Cooper, a widowed engineer and former NASA pilot turned farmer, lives with his father-in-law, Donald, his 15-year-old son, Tom, and 10-year-old daughter, Murphy \"Murph\". After a dust storm, strange dust patterns inexplicably appear in Murphy's bedroom; she attributes the anomaly to a ghost. Cooper eventually deduces the patterns were caused by gravity variations and they represent geographic coordinates in binary code. Cooper follows the coordinates to a secret NASA facility headed by Professor John Brand. 48 years earlier, unknown beings positioned a wormhole near Saturn, opening a path to a distant galaxy with 12 potentially habitable worlds located near a black hole named Gargantua. Twelve volunteers traveled through the wormhole to individually survey the planets and three — Dr. Mann, Laura Miller, and Wolf Edmunds — reported positive results. Based on their data, Professor Brand conceived two plans to ensure humanity's survival. Plan A involves developing a gravitational propulsion theory to propel settlements into space, while Plan B involves launching the Endurance spacecraft carrying 5,000 frozen human embryos to settle a habitable planet.",
                    ReleaseYear = "2014",
                    Language = "English",
                    Duration = "2h 49m",
                    Budget = "$165 million"
                },
                new Movie
                {
                    Title = "Suicide Squad",
                    PictureUrl = "suicide-squad.jpg",
                    ShortDescription = "In the aftermath of Superman's death, intelligence officer Amanda Waller convinces the US Government to greenlight Task Force X, a response team of criminals and supervillains. The team will be used to combat metahuman threats, under Waller's control via nanite bombs implanted in each criminal's neck, which can be remotely detonated. If successful, they will have their sentences shortened.",
                    Director = "David Ayer",
                    Cast = "Will Smith | Jared Leto | Margot Robbie | Joel Kinnaman | Viola Davis | Jai Courtney | Jay Hernandez | Adewale Akinnuoye-Agbaje | Ike Barinholtz | Scott Eastwood | Cara Delevingne",
                    LongDescription = "In the aftermath of Superman's death,[N 1] intelligence officer Amanda Waller convinces the US Government to greenlight Task Force X, a response team of criminals and supervillains. The team will be used to combat metahuman threats, under Waller's control via nanite bombs implanted in each criminal's neck, which can be remotely detonated. If successful, they will have their sentences shortened. Dr. June Moone, an American archaeologist, becomes possessed by demonic witch Enchantress. Waller can control the Enchantress by seizing her magical heart, which wounds her if it is struck. Waller's subordinate Colonel Rick Flag is in love with Moone, and is made a member of Task Force X. However, Enchantress betrays Waller, conquering Midway City, transforming humans into monsters, and summoning her brother Incubus to destroy mankind. Task Force X is formed to stop Enchantress, using six inmates from Belle Reve penitentiary. The roster consists of hitman Deadshot, who wants to reunite with his daughter Zoe.",
                    ReleaseYear = "2016",
                    Language = "English",
                    Duration = "2h 3m",
                    Budget = "$175 million"
                }
            };

            _context.AddRange(movies);
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
                Console.WriteLine($"Movie Title: {movie.Title}");
                Console.WriteLine($"Movie PictureUrl: {movie.PictureUrl}");
                Console.WriteLine($"Movie Director: {movie.Director}");
                Console.WriteLine($"Movie Cast: {movie.Cast}");
                Console.WriteLine($"Movie ShortDescription: {movie.ShortDescription}");
                Console.WriteLine($"Movie LongDescription: {movie.LongDescription}");
                Console.WriteLine($"Movie ReleaseYear: {movie.ReleaseYear}");
                Console.WriteLine($"Movie Language: {movie.Language}");
                Console.WriteLine($"Movie Duration: {movie.Duration}");
                Console.WriteLine($"Movie Budget: {movie.Budget}\n");

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
