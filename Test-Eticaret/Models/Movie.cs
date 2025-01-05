using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Eticaret.Models
{
    public class Movie
    {
        //Yorum Sayısı 
        //Studio
        //Yayınlanma tarihi
        //Süresi
        // Video

        //Kategori ismi



        // top views ve New Comment veritabanı açılacak


        [Key]
        public int movie_id { get; set; }
        public string? movie_name { get; set; }
        public string? movie_description { get; set; }
        public float imdb {  get; set; } //API kullanılacak
        public int view { get; set; }
        public string? picture_url { get; set; }
        public DateTime movie_date { get; set; }
        public DateTime add_date { get; set; }
        public string movie_url { get; set; }   
        public int like { get; set; }
        public float movie_time { get; set; }
         
 
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public required Category Category { get; set; } // Navigation Property
        public required ICollection<Category> Categories { get; set; } // One-to-Many Relationship
        public ICollection<Favorite> Favorites { get; set; }



    }
}
