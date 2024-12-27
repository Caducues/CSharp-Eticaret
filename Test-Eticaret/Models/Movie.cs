namespace Test_Eticaret.Models
{
    public class Movie
    {

        //Film mi Dizi mi ? 
        //Görüntülenme Sayısı 
        //Yorum Sayısı 
        //resim url'i


        //Studio
        //Yayınlanma tarihi
        //imdb 
        //Site PUanı 
        //Süresi
        //Kalitesi
        //İzleyen Kişi 

       

        // Anime Watching Kısmı

        // Video 



        [Key]
        public int movie_id { get; set; }
        public string? movie_name { get; set; }
        public int category_id { get; set; }
        public string? movie_description { get; set; }
        public int imdb {  get; set; }
      

    }
}
