using System.Collections.Generic;

namespace CinemaApp.Database.Entities.Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ReleaseYear { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public string Budget { get; set; }
    }
}
