using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("MovieGenre")]
    public class MovieGenre
    {
        // Database ->
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // .NET
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
