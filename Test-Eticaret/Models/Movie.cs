namespace Test_Eticaret.Models
{
    public class Movie
    {
        [Key]
        public int movie_id { get; set; }
        public string? movie_name { get; set; }
        public int category_id { get; set; }
        public string? movie_description { get; set; }
        public int imdb {  get; set; }
    }
}
