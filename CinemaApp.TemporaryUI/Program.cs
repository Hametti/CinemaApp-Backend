using CinemaApp.Database;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CinemaApp.TemporaryUI
{
    class Program
    {
        private static CinemaAppDbContext _context = new CinemaAppDbContext();
        static void Main(string[] args)
        {
            //AddSampleMovies();
            //AddSampleScreeningDays();
            //AddSampleUser();
            //AddSampleReservationToUserWithEmail("test1", 1);
            //AddSampleReservationToScreningWithId(19);
            //AddRandomScreening()

            //DeleteRecord();
            //DeleteAllScreeningDays();
            //DeleteScreeningDayById(1);
            //DeleteScreeningById(1);
            //DeleteReservationById(1);
            //DeleteUserByEmail("test1");
            //DeleteMovieById(2);

            //UpdateRecord();
            //DeleteRecord();

            //AddSampleUser();
            //DisplayUserCreds();
            //GenerateSeats();

            //DisplayScreeningDays();
            //DisplayMovies();
            //DisplayScreenings();
            //DisplayScreeningById(1);
            //DisplayReservations();
            //DisplaySeatsOfScreeningWithId(1);
            //DisplaySeats();
            //DisplayUsers();

            var token = new JwtSecurityToken("asd");


            Console.WriteLine("\nSucceed");
            Console.ReadKey();
        }

        private static void DeleteScreeningDayById(int id)
        {
            var screenings = _context.Screenings.ToList();
            var seats = _context.Seats.ToList();
            var screeningDaysToDelete = _context.ScreeningDays
                                        .Include(s => s.Screenings)
                                        .Include(s => s.Screenings)
                                        .ThenInclude(s => s.Seats)
                                        .FirstOrDefault(s => s.Id == id);

            _context.Remove(screeningDaysToDelete);
            _context.SaveChanges();
        }
        private static void AddRandomScreening()
        {
            var screenings = _context.Screenings.ToList();
            var screeningDay = _context.ScreeningDays.Include(s => s.Screenings).FirstOrDefault(s => s.Date == "25-10");
            screeningDay.Screenings.Add(
                new Screening
                {
                    Movie = _context.Movies.ToList().OrderBy(o => Guid.NewGuid()).First(),
                    Hour = "15:00Dod1ane",
                    Seats = GenerateSeats(),
                });
            _context.SaveChanges();
        }

        private static void DeleteMovieById(int  id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            var screenings = _context.Screenings
                             .Include(s => s.Movie)
                             .Include(s => s.Seats)
                             .Include(s => s.Reservations)
                             .Where(s => s.Movie.Id == id)
                             .ToList();

            _context.RemoveRange(screenings);
            _context.Remove(movie);
            _context.SaveChanges();
        }

        private static void DeleteUserByEmail(string email)
        {
            var user = _context.Users.Include(u => u.Reservations).ThenInclude(r => r.ReservedSeats).FirstOrDefault(u => u.Email == email);
            var reservations = user.Reservations;
            var screenings = _context.Screenings.Include(s => s.Seats).ToList();
            if (reservations.Count > 0)
                foreach(Reservation reservation in reservations)
                {
                    var screening = screenings.FirstOrDefault(s => s.Id == reservation.ScreeningId);

                    foreach (Seat seat in reservation.ReservedSeats)
                        screening.Seats.FirstOrDefault(s => s.Id == seat.Id).IsOccupied = false;
                }

            var userCredToDelete = _context.UserCreds
                                   .Include(u => u.User)
                                   .ThenInclude(u => u.UniqueDiscount)
                                   .FirstOrDefault(u => u.Email == email);

            _context.UserCreds.Remove(userCredToDelete);
            _context.Remove(user);
            _context.SaveChanges();
        }

        private static void AddSampleReservationToUserWithEmail(string email, int id)
        {
            var user = _context.Users.Include(u => u.Reservations).FirstOrDefault(u => u.Email == email);
            var reservations = _context.Reservations.Include(r => r.ReservedSeats).ToList();

            var screeningId = id;
            var screening = _context.Screenings.Include(s => s.ScreeningDay)
                            .Include(s => s.Movie)
                            .Include(s => s.Seats)
                            .FirstOrDefault(s => s.Id == screeningId);

            var seat1 = screening.Seats.FirstOrDefault(s => s.Row == 1 && s.SeatNumber == 5);
            var seat2 = screening.Seats.FirstOrDefault(s => s.Row == 1 && s.SeatNumber == 6);
            var seat3 = screening.Seats.FirstOrDefault(s => s.Row == 2 && s.SeatNumber == 6);

            //fields required to build reservation
            var date = screening.ScreeningDay.Date;
            var reservationHour = screening.Hour;
            var movieTitle = screening.Movie.Title;
            var seats = new List<Seat> { seat1, seat2, seat3 };

            foreach (Seat seat in seats)
                seat.IsOccupied = true;

            var reservation = new Reservation
            {
                Date = date,
                ReservationHour = reservationHour,
                ScreeningId = screeningId,
                MovieTitle = movieTitle,
                ReservedSeats = seats
            };

            user.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        private static void DisplayUsers()
        {
            var users = _context.Users
                        .Include(u => u.UniqueDiscount)
                        .Include(u => u.Reservations)
                        .ThenInclude(r => r.ReservedSeats)
                        .ToList();

            foreach(User user in users)
            {
                Console.WriteLine($"User id: {user.Id}");
                Console.WriteLine($"User email: {user.Email}");
                Console.WriteLine($"User admin role: {user.IsAdmin}");
                Console.WriteLine($"User subscription: {user.Subscription}");
                Console.WriteLine($"User UniqueDiscount: {user.UniqueDiscount}");
                Console.WriteLine($"User sec question: {user.SecurityQuestion}");
                Console.WriteLine($"User sec question answer: {user.SecurityQuestionAnswer}");
                Console.WriteLine("Reservations: ");
                foreach(Reservation reservation in user.Reservations)
                {
                    Console.WriteLine($"Reservation id: {reservation.Id}");
                    Console.WriteLine($"Reservation date: {reservation.Date}");
                    Console.WriteLine($"Reservation hour: {reservation.ReservationHour}");
                    Console.WriteLine($"Reservation movie: {reservation.MovieTitle}");
                    Console.WriteLine($"Reservation seats:");
                    foreach(Seat seat in reservation.ReservedSeats)
                    {
                        Console.WriteLine($"Seat row: {seat.Row}");
                        Console.WriteLine($"Seat nuber: {seat.SeatNumber}");
                        Console.WriteLine($"Screening id: {seat.ScreeningId}");
                    }
                }
                Console.WriteLine("\n-----------------------\n");
            }
        }

        private static void AddSampleUser()
        {
            var userToAdd = new User
            {
                Email = "test1",
                Name = "Jan",
                UniqueDiscount = _context.Movies.ToList().OrderBy(o => Guid.NewGuid()).First(),
                UniqueDiscountValue = new Random().Next(10, 60),
                SecurityQuestion = "2+2?",
                SecurityQuestionAnswer = "4"
        };
            
            var userCredsToAdd = new UserCred 
            { 
                Email = "test1",
                Password = "password1",
                User = userToAdd 
            };

            _context.Users.Add(userToAdd);
            _context.UserCreds.Add(userCredsToAdd);
            _context.SaveChanges();
        }

        private static void DeleteReservationById(int id)
        {
            var reservation = _context.Reservations.Include(r => r.ReservedSeats).FirstOrDefault(r => r.Id == id);
            var seats = _context.Seats.ToList();
            
            foreach(Seat seat in reservation.ReservedSeats)
            {
                var seatToUpdate = _context.Seats.FirstOrDefault(s => s.Id == seat.Id);
                seatToUpdate.IsOccupied = false;
            }

            _context.Remove(reservation);
            _context.SaveChanges();
        }

        private static void DisplayReservations()
        {
            var reservations = _context.Reservations.Include(r => r.ReservedSeats).ToList();

            foreach(Reservation reservation in reservations)
            {
                Console.WriteLine($"Reservation Id: {reservation.Id}");
                Console.WriteLine($"Reservation date: {reservation.Date}");
                Console.WriteLine($"Reservation hour: {reservation.ReservationHour}");
                Console.WriteLine($"Reservation movie: {reservation.MovieTitle}");
                Console.WriteLine("Seats:");
                foreach (Seat seat in reservation.ReservedSeats)
                    Console.WriteLine($"Seat row: {seat.Row}, seat number: {seat.SeatNumber}");
                Console.WriteLine("\n");
            }
        }

        private static void DisplayScreeningById(int id)
        {
            var screening = _context.Screenings.Include(s => s.ScreeningDay).Include(s => s.Movie).FirstOrDefault(s => s.Id == id);
                Console.WriteLine($"Screening Id: {screening.Id}");
                Console.WriteLine($"Screening day date: {screening.ScreeningDay.Date}");
                Console.WriteLine($"Screening hour: {screening.Hour}\n");
                Console.WriteLine($"Screening movie title: {screening.Movie.Title}\n");
        }

        private static void DeleteScreeningById(int id)
        {
            var screening = _context.Screenings.FirstOrDefault(s => s.Id == id);
            var seats = _context.Seats.ToList();

            _context.Remove(screening);
            _context.SaveChanges();
        }

        private static void DisplaySeats()
        {
            var seats = _context.Seats.ToList().OrderBy(s => s.ScreeningId).ThenBy(s => s.Row).ThenBy(s => s.SeatNumber);
            foreach(Seat seat in seats)
                Console.WriteLine($"Seat Id: {seat.Id}, seat row: {seat.Row}, seat number: {seat.SeatNumber}, seat screening id: {seat.ScreeningId}");
        }

        private static void DisplaySeatsOfScreeningWithId(int id)
        {
            var seats = _context.Seats.Where(s => s.ScreeningId == id).ToList().OrderBy(s => s.Row).ThenBy(s => s.SeatNumber);
            foreach (Seat seat in seats)
                Console.WriteLine($"Id: {seat.Id}, Seat occupied: {seat.IsOccupied}, seat row: {seat.Row}, seat number: {seat.SeatNumber}, seat screening id: {seat.ScreeningId}");
        }

        private static void DisplayScreenings()
        {
            var screenings = _context.Screenings.Include(s => s.ScreeningDay).ToList();
            Console.WriteLine("Screenings: ");
            foreach (Screening screening in screenings)
            {
                Console.WriteLine($"Screening Id: {screening.Id}");
                Console.WriteLine($"Screening day date: {screening.ScreeningDay.Date}");
                Console.WriteLine($"Screening hour: {screening.Hour}\n");
            }
        }

        private static void DeleteAllScreeningDays()
        {
            var screenings = _context.Screenings.ToList();
            var seats = _context.Seats.ToList();
            var screeningDaysToDelete = _context.ScreeningDays
                                        .Include(s => s.Screenings)
                                        .Include(s => s.Screenings)
                                        .ThenInclude(s => s.Seats)
                                        .ToList();

            _context.RemoveRange(screeningDaysToDelete);
            _context.SaveChanges();
        }

        private static void AddSampleReservationToScreningWithId(int id)
        {
            var screeningId = id;
            var screening = _context.Screenings.Include(s => s.ScreeningDay)
                            .Include(s => s.Movie)
                            .Include(s => s.Seats)
                            .FirstOrDefault(s => s.Id == screeningId);

            var seat1 = screening.Seats.FirstOrDefault(s => s.Row == 1 && s.SeatNumber == 5);
            var seat2 = screening.Seats.FirstOrDefault(s => s.Row == 1 && s.SeatNumber == 6);
            var seat3 = screening.Seats.FirstOrDefault(s => s.Row == 2 && s.SeatNumber == 6);

            //fields required to build reservation
            var date = screening.ScreeningDay.Date;
            var reservationHour = screening.Hour;
            var movieTitle = screening.Movie.Title;
            var seats = new List<Seat> { seat1, seat2, seat3 };

            foreach (Seat seat in seats)
                seat.IsOccupied = true;

            var reservation = new Reservation
            {
                Date = date,
                ReservationHour = reservationHour,
                ScreeningId = screeningId,
                MovieTitle = movieTitle,
                ReservedSeats = seats
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        private static void DisplayUserCreds()
        {
            var users = _context.UserCreds.ToList();
            foreach(UserCred user in users)
            {
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Password: {user.Password}");
            }
        }

        private static void DeleteRecord()
        {
            var screeningDays = _context.ScreeningDays
                .Include(s => s.Screenings)
                .ThenInclude(s => s.Movie)
                .Include(s => s.Screenings);

            _context.ScreeningDays.RemoveRange(screeningDays);

            var movies = _context.Movies.ToList();
            _context.Movies.RemoveRange(movies);

            _context.SaveChanges();

            //working cascade movie deletion
            //var movie = _context.Movies.FirstOrDefault(m => m.Id == 3);
            //var screenings = _context.Screenings.Include(s => s.Movie).Include(s => s.ScreeningHours).ToList();
            //var screeningsToDelete = new List<Screening>();

            //foreach(Screening screening in screenings)
            //{
            //    if (screening.Movie.Id == movie.Id)
            //        screeningsToDelete.Add(screening);
            //}
            //_context.Screenings.RemoveRange(screeningsToDelete);

            //_context.Movies.Remove(movie);
            //_context.SaveChanges();
        }

        private static void DisplayScreeningDays()
        {
            var screeningDays = _context.ScreeningDays
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Movie)
                                .Include(s => s.Screenings)
                                .Include(s => s.Screenings)
                                .ThenInclude(s => s.Seats.OrderBy(s => s.Row).ThenBy(s => s.SeatNumber))
                                .ToList();

            foreach (ScreeningDay screeningDay in screeningDays)
            {
                Console.WriteLine($"Screening Day Id: {screeningDay.Id}");
                Console.WriteLine($"Screening Day Date: {screeningDay.Date}");
                Console.WriteLine("\nScreening Day Screenings:\n");
                foreach(Screening screening in screeningDay.Screenings)
                {
                    Console.WriteLine($"Screening Id: {screening.Id}");
                    Console.WriteLine($"Screening Movie Id: {screening.Movie.Id}");
                    Console.WriteLine($"Screening Movie Title: {screening.Movie.Title}");
                    Console.WriteLine("\nScreening seats:\n");
                    //foreach (Seat seat in screening.Seats)
                    //    Console.WriteLine($"Id: {seat.Id}, Row: {seat.Row}, Number: {seat.SeatNumber}");
                    Console.WriteLine($"\nScreening hour: {screening.Hour}");
                    Console.WriteLine("\n-----\n");
                }
                Console.WriteLine("\n-----------------------------------------\n");
            }
        }

        private static List<Seat> GenerateSeats()
        {
            var seats = new List<Seat>();
            
            for(int i=0; i<8; i++)
                for(int j=0; j<10; j++)
                {
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });
                }

            for(int i=8; i<10; i++)
                for(int j=0; j<14; j++)
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });

            return seats;
        }

        private static void AddSampleScreeningDays()
        {
            var movies = _context.Movies.ToList();

            List<ScreeningDay> screeningDays = new List<ScreeningDay>()
            {
                new ScreeningDay
                {
                    Date = "25-10",
                    Screenings = new List<Screening>
                    {
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "No Time To Die"),
                            Hour = "12:00",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "No Time To Die"),
                            Hour = "17:45",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "No Time To Die"),
                            Hour = "21:00",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Suicide Squad"),
                            Hour = "15:00",
                            Seats = GenerateSeats()
                        }
                    }
                },
                new ScreeningDay
                {
                    Date = "26-10",
                    Screenings = new List<Screening>
                    {
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Inception"),
                            Hour = "12:00",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Green Mile"),
                            Hour = "18:30",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Interstellar"),
                            Hour = "15:00",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Interstellar"),
                            Hour = "21:00",
                            Seats = GenerateSeats()
                        }
                    }
                },
                new ScreeningDay
                {
                    Date = "27-10",
                    Screenings = new List<Screening>
                    {
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "No Time To Die"),
                            Hour = "15:30",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "No Time To Die"),
                            Hour = "21:15",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Interstellar"),
                            Hour = "12:00",
                            Seats = GenerateSeats()
                        },
                        new Screening
                        {
                            Movie = movies.FirstOrDefault(m => m.Title == "Suicide Squad"),
                            Hour = "18:45",
                            Seats = GenerateSeats()
                        }
                    }
                }
            };

            _context.ScreeningDays.AddRange(screeningDays);
            _context.SaveChanges();
        }

        private static void AddSampleScreenings()
        {
            //var movies = _context.Movies.ToList();

            //List<Screening> screenings = new List<Screening>()
            //{
            //    new Screening
            //    {
            //        Movie = movies[0],
            //        ScreeningHours = new List<ScreeningHour>() { new ScreeningHour { Hour = "12:00" }, new ScreeningHour { Hour = "15:00" } }
            //    },
            //    new Screening
            //    {
            //        Movie = movies[2],
            //        ScreeningHours = new List<ScreeningHour>() { new ScreeningHour { Hour = "18:00" } }
            //    }
            //};
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

        private static void DisplayMovies()
        {
            var movies = _context.Movies.ToList();
            Console.WriteLine("Movies: ");
            foreach (Movie movie in movies)
            {
                Console.WriteLine($"Movie Id: {movie.Id}");
                Console.WriteLine($"Movie Title: {movie.Title}");
                Console.WriteLine("\n--------------------------------------------\n");
            }
        }
    }
}
