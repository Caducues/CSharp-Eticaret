using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Identity.Client;
using Test_Eticaret.Data;
using Test_Eticaret.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Net.Sockets;


namespace Test_Eticaret.Controllers
{

    public class WebsiteController : Controller
    {
        private readonly string apiKey = "d55e9492";

        private readonly ApplicationDbContext _context;
        public WebsiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<string> GetMovieDetailsByTitleAsync(string movieTitle)
        {
            using (var client = new HttpClient())
            {
                // Film ismiyle arama yapıyoruz
                string response = await client.GetStringAsync($"http://www.omdbapi.com/?t={movieTitle}&apikey={apiKey}");
                int indeks = response.IndexOf("imdbRating");

                string rating = response.Substring(indeks+13, 3);

                  
				// JSON verisini deserialize ediyoruz
				//var movie = JsonConvert.DeserializeObject<Movie>(response);


                return rating;
            }
        }

        // Index action method
        public async Task<IActionResult> Index()
        {
            // Veritabanından tüm filmleri alıyoruz
            var movies = await _context.Movies.ToListAsync();
            

      

            return View(movies);  // Veriyi view'a gönderiyoruz
        }



       
        public IActionResult main()
        {
            return View();
        }

  

        [Route("website/anime_details/{movieId}")]
        public async Task<IActionResult> anime_details(int movieId)
        {

            var selected_movie = await _context.Movies
                                               .Include(m => m.Category)
                                               .FirstOrDefaultAsync(m => m.movie_id == movieId);
            
			// Eğer film bulunmazsa, 404 sayfası gösteriyoruz
			if (selected_movie == null)
            {
                return NotFound("Movie not found");
            }
            string rating = await GetMovieDetailsByTitleAsync(selected_movie.movie_name);

			selected_movie.imdb = float.Parse(rating);
			// Film verisi varsa, ilgili view'a gönderiyoruz
            
			return View(selected_movie);
        }

        [Route("website/anime_watching/{movieId}")]
        public async Task<IActionResult> anime_watching(int movieId)
        {
            var selected_movie = await _context.Movies.FirstOrDefaultAsync(m => m.movie_id == movieId);

            if (selected_movie == null)
            {

                return NotFound();
            }

            return View(selected_movie);
        }
        public IActionResult blog()
        {
            return View();
        }
        public IActionResult blog_details()
        {
            return View();
        }
        [Route("website/categories/{category_id}")]
        public async Task<IActionResult> Categories(int category_id)
        {
            // Kategori adı almak için kategori nesnesini buluyoruz
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.category_id == category_id);

            if (category == null)
            {
                return NotFound();  // Kategori bulunamazsa 404 döndür
            }

            // Seçilen kategoriye ait filmleri alıyoruz
            var selected_movies = await _context.Movies
                                                 .Where(m => m.category_id == category_id)
                                                 .ToListAsync();

            if (selected_movies == null || !selected_movies.Any())
            {
                return NotFound();  // Eğer kategoriye ait film yoksa 404 döndür
            }

            // Kategori adını ViewData'ya gönderiyoruz
            ViewData["CategoryName"] = category.category_name;

            // Filmleri view'a gönderiyoruz
            return View(selected_movies);
        }
        public IActionResult admin()
        {
            return View();
        }
    }
}

