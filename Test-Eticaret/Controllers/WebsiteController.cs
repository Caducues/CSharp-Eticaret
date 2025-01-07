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

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid ==false )
            {
                try
                {
                    // Email'in benzersiz olduğunu kontrol et
                    var existingUser = await _context.Users
                                                     .FirstOrDefaultAsync(u => u.user_email == user.user_email);

                    if (existingUser != null)
                    {
                        ModelState.AddModelError("user_email", "Bu e-posta adresi zaten kullanılmakta.");
                        return View(user); // Hata varsa tekrar aynı sayfaya dön
                    }

					if (user.user_password == null || user.user_password == string.Empty)
					{
						ModelState.AddModelError("user_email", "Bu e-posta adresi zaten kullanılmakta.");
						return View(user); // Hata varsa tekrar aynı sayfaya dön
					}


					MD5 md5 = new MD5CryptoServiceProvider();

					//compute hash from the bytes of text  
					md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.user_password));

					//get hash result after compute it  
					byte[] result = md5.Hash;

					StringBuilder strBuilder = new StringBuilder();
					for (int i = 0; i < result.Length; i++)
					{
						//change it into 2 hexadecimal digits  
						//for each byte  
						strBuilder.Append(result[i].ToString("x2"));
					}

					user.user_password = strBuilder.ToString();

					// Yeni kullanıcıyı ekle
					_context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Website");
                }
                catch (Exception ex)
                {
                    // Hata durumunda log yazdırma
                    Console.WriteLine($"Veritabanı hatası: {ex.Message}");
                    ModelState.AddModelError("", "Kullanıcı kaydederken bir hata oluştu.");
                    return View(user); // Hata durumunda tekrar aynı sayfaya dön
                }
            }

            // Eğer model geçerli değilse, hata mesajlarını göster
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(user);
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

       

        public IActionResult login()
        {
            return View();
        }

        public IActionResult admin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(Models.User usermodel)
        {
            using (var client = new HttpClient())
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(usermodel.user_password));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder.Append(result[i].ToString("x2"));
                }

                var response =  await client.PostAsync("http://localhost:8000/login?email=" +  usermodel.user_email +"&password=" + strBuilder, null);
                var contents = await response.Content.ReadAsStringAsync();
                if (contents.Length < 3)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(usermodel);
                }
                else if(usermodel.user_email == "admin@admin.com" && usermodel.user_password == "123456")
                {
                    string[] parse = contents.Split('"');

                    return RedirectToAction("index", "Admin", new { name = parse[3] });
                }



                else
                {
                    string[] parse = contents.Split('"');

                    return RedirectToAction("index", "Website", new { name = parse[3] });
                }
                

            }
            return View();
        }
        /*
        [HttpPost]
        public async Task<IActionResult> login(User usermodel)
        {
            if (ModelState.IsValid == false)
			{

				MD5 md5 = new MD5CryptoServiceProvider();

				//compute hash from the bytes of text  
				md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(usermodel.user_password));

				//get hash result after compute it  
				byte[] result = md5.Hash;

				StringBuilder strBuilder = new StringBuilder();
				for (int i = 0; i < result.Length; i++)
				{
					//change it into 2 hexadecimal digits  
					//for each byte  
					strBuilder.Append(result[i].ToString("x2"));
				}

				usermodel.user_password = strBuilder.ToString();

				// Veritabanından kullanıcıyı çek
				var user = await _context.Users.FirstOrDefaultAsync(u => u.user_email == usermodel.user_email && u.user_password == usermodel.user_password);


                // Eğer kullanıcı bulunamazsa
                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(usermodel);
                }
				return RedirectToAction("Index", "Admin", new {name = user.first_name});
			}
            return View();

        }
        */


        
    }
}

