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

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
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

                    // Şifreyi hash'lemek için PasswordHasher kullan
                    var passwordHasher = new PasswordHasher<User>();
                    user.user_password = passwordHasher.HashPassword(user, user.user_password);

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
            if (ModelState.IsValid)
            {
                // Veritabanından kullanıcıyı çek
                var user = await _context.Users.FirstOrDefaultAsync(u => u.user_email == usermodel.user_email && u.user_password == usermodel.user_password);

                // Eğer kullanıcı bulunamazsa
                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(usermodel);
                }

            }
            return View("index", "Website");

        }

    }
}

