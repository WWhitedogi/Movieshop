using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
    // DataAnnotation
    // FluentAPI -> More Powerful
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } // Genre Name: Comedy

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
