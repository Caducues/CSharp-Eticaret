using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Test_Eticaret.Data;
using Test_Eticaret.Models;

namespace Test_Eticaret.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WebsiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action method
        public async Task<IActionResult> Index()
        {
            // Veritabanından tüm filmleri alıyoruz
            var movies = await _context.Movies.ToListAsync();

            return View(movies); // Veriyi view'a gönderiyoruz
        }
        public IActionResult main()
        {
            return View();
        }
        // GET: /Website/signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        // POST: /Kullanıcı Kayıt 
        [HttpPost]
        public async Task<IActionResult> Signup([Bind("user_email,first_name,last_name,tel_no,user_password")] User user)
        {
            if (!ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Website");
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
        public async Task<IActionResult> login(User usermodel)
        {
            if(ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.user_email == usermodel.user_email && u.user_password == usermodel.user_password);
                if (user == null || user.user_password != usermodel.user_password)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(usermodel);
                }
                else
                {
                    return RedirectToAction("Index", "Website");
                }
            }
          

            return View(usermodel);

        }
    }

}

