using CinemaApp.DAL.Repositories.BaseRepository;
using CinemaApp.Database;
using CinemaApp.Database.Entities;
using CinemaApp.Database.Entities.MovieModels;
using CinemaApp.Database.Entities.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.DAL.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CinemaAppDbContext cinemaAppDbContext) : base(cinemaAppDbContext)
        {

        }

        public void AddUser(User user, UserCred userCred)
        {
            _cinemaAppDbContext.Users.Add(user);
            _cinemaAppDbContext.UserCreds.Add(userCred);
            _cinemaAppDbContext.SaveChanges();
        }
        public User GetUserByEmail(string email)
        {
            var user = _cinemaAppDbContext.Users.Include(u => u.UniqueDiscount).FirstOrDefault(u => u.Email == email);
            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            var users = _cinemaAppDbContext.Users
                .Include(u => u.UniqueDiscount)
                .Include(u => u.Reservations)
                .ThenInclude(r => r.ReservedSeats)
                .ToList();

            return users;
        }

        public void ChangePassword(string email, string currentPassword, string newPassword)
        {
            var user = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == currentPassword);

            user.Password = newPassword;
            _cinemaAppDbContext.SaveChanges();
        }

        public void DeleteAccount(string email, string password)
        {
            var userCredToDelete = _cinemaAppDbContext.UserCreds
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            var userToDelete = _cinemaAppDbContext.Users
                .Include(u => u.Reservations)
                .ThenInclude(u => u.ReservedSeats)
                .FirstOrDefault(u => u.Email == email);

            var reservations = userToDelete.Reservations.ToList();
            if (reservations.Count > 0)
                foreach (Reservation reservation in reservations)
                {
                    var screening = _cinemaAppDbContext.Screenings
                        .Include(s => s.Seats)
                        .FirstOrDefault(s => s.Id == reservation.ScreeningId);

                    foreach (Seat seat in reservation.ReservedSeats)
                        screening.Seats.FirstOrDefault(s => s.Id == seat.Id).IsOccupied = false;
                }

            _cinemaAppDbContext.UserCreds.Remove(userCredToDelete);
            _cinemaAppDbContext.Remove(userToDelete);
            _cinemaAppDbContext.SaveChanges();
        }



        public override User GetEntityById(int id)
        {
            var user = _cinemaAppDbContext.Users
                .Include(u => u.UniqueDiscount)
                .FirstOrDefault(u => u.Id == id);

            return user;
        }



        public bool IsPasswordCorrect(string email, string password)
        {
            var userCredFromDb = _cinemaAppDbContext.UserCreds.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (userCredFromDb == null)
                return false;
            else
                return true;
        }

        public void SubscribeNewsletter(User user)
        {
            var userToChange = _cinemaAppDbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            userToChange.Subscription = true;
            _cinemaAppDbContext.SaveChanges();
        }

        public void UnsubscribeNewsletter(User user)
        {
            var userToChange = _cinemaAppDbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            userToChange.Subscription = false;
            _cinemaAppDbContext.SaveChanges();
        }

        public void AddSampleData()
        {
            var movies = _cinemaAppDbContext.Movies.ToList().Count;
            var screenings = _cinemaAppDbContext.Screenings.ToList().Count;
            var screeningDays = _cinemaAppDbContext.ScreeningDays.ToList().Count;
            var seats = _cinemaAppDbContext.Seats.ToList().Count;
            var users = _cinemaAppDbContext.Users.ToList().Count;
            var userCreds = _cinemaAppDbContext.UserCreds.ToList().Count;
            var reservations = _cinemaAppDbContext.Reservations.ToList().Count;

            if (movies + screenings + screeningDays + seats + users + reservations + userCreds != 0)
                throw new Exception();

            AddSampleMovies();
            AddSampleUsers();
            AddSampleScreeningDays();
            AddSampleReservationToDefaultUser();

        }

        public void AddSampleMovies()
        {
            var currentMovies = _cinemaAppDbContext.Movies.ToList();
            if (currentMovies != null)
                if (currentMovies.Count != 0)
                    throw new Exception();

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

            _cinemaAppDbContext.AddRange(movies);
            _cinemaAppDbContext.SaveChanges();
        }

        public void AddSampleUsers()
        {
            var users = _cinemaAppDbContext.Users.ToList().Count;
            var userCreds = _cinemaAppDbContext.UserCreds.ToList().Count;
            var reservations = _cinemaAppDbContext.Reservations.ToList().Count;
            if (!(users + userCreds + reservations == 0))
                throw new Exception();

            var userToAdd = new User
            {
                Email = "user",
                Name = "Jan",
                UniqueDiscount = _cinemaAppDbContext.Movies.ToList().OrderBy(o => Guid.NewGuid()).First(),
                UniqueDiscountValue = new Random().Next(10, 60),
                SecurityQuestion = "2+2?",
                SecurityQuestionAnswer = "4"
            };

            var userCredsToAdd = new UserCred
            {
                Email = "user",
                Password = "user",
                User = userToAdd
            };

            var secondUserToAdd = new User
            {
                Email = "admin",
                Name = "Jan",
                UniqueDiscount = _cinemaAppDbContext.Movies.ToList().OrderBy(o => Guid.NewGuid()).First(),
                UniqueDiscountValue = new Random().Next(10, 60),
                SecurityQuestion = "2+2?",
                SecurityQuestionAnswer = "4",
                IsAdmin = true
            };

            var secondUserCredsToAdd = new UserCred
            {
                Email = "admin",
                Password = "admin",
                User = userToAdd
            };

            _cinemaAppDbContext.Users.Add(userToAdd);
            _cinemaAppDbContext.UserCreds.Add(userCredsToAdd);
            _cinemaAppDbContext.SaveChanges();

        }

        public void AddSampleScreeningDays()
        {
            var moviesCount = _cinemaAppDbContext.Movies.ToList().Count;
            var screeningsCount = _cinemaAppDbContext.Screenings.ToList().Count;
            var screeningDaysCount = _cinemaAppDbContext.ScreeningDays.ToList().Count;
            var seatsCount = _cinemaAppDbContext.Seats.ToList().Count;
            var reservationsCount = _cinemaAppDbContext.Reservations.ToList().Count;

            if (screeningDaysCount != 0 || moviesCount == 0)
                throw new Exception();

            var movies = _cinemaAppDbContext.Movies.ToList();

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

            _cinemaAppDbContext.ScreeningDays.AddRange(screeningDays);
            _cinemaAppDbContext.SaveChanges();
        }

        public void AddSampleReservationToDefaultUser()
        {
            var moviesCount = _cinemaAppDbContext.Movies.ToList().Count;
            var screeningDaysCount = _cinemaAppDbContext.ScreeningDays.ToList().Count;
            var usersCount = _cinemaAppDbContext.Users.ToList().Count;
            var reservationsCount = _cinemaAppDbContext.Reservations.ToList().Count;

            if (!(reservationsCount == 0 && usersCount != 0 && screeningDaysCount != 0 && moviesCount != 0))
                throw new Exception();

            var user = _cinemaAppDbContext.Users.Include(u => u.Reservations).FirstOrDefault(u => u.Email == "user");
            var reservations = _cinemaAppDbContext.Reservations.Include(r => r.ReservedSeats).ToList();

            var screeningId = 1;
            var screening = _cinemaAppDbContext.Screenings
                .Include(s => s.ScreeningDay)
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
            _cinemaAppDbContext.SaveChanges();
        }

        private List<Seat> GenerateSeats()
        {
            var seats = new List<Seat>();

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 10; j++)
                {
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });
                }

            for (int i = 8; i < 10; i++)
                for (int j = 0; j < 14; j++)
                    seats.Add(
                        new Seat
                        {
                            Row = i + 1,
                            SeatNumber = j + 1
                        });

            return seats;
        }
    }
}
