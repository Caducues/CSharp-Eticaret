using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Eticaret.Models
{
    public class Favorite
    {
        [Key]
        public int fav_id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }
        public required User User { get; set; } // Navigation Property
        public required ICollection<User> Users { get; set; } // One-to-Many Relationship

        [ForeignKey("Movie")]
        public int movie_id { get; set; }
        public required Movie Movie { get; set; } // Navigation Property
        public required ICollection<Movie> Movies{ get; set; } // One-to-Many Relationship
    }
}
