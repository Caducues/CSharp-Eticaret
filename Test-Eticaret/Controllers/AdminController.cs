using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;
using Test_Eticaret.Migrations;
using Test_Eticaret.Models;
using XAct.Users;

namespace Test_Eticaret.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
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
        public async Task<IActionResult> Category()
        {
            // Veritabanından tüm filmleri alıyoruz
            var categories = await _context.Categories.ToListAsync();

            return View(categories); // Veriyi view'a gönderiyoruz
        }
        public IActionResult Comment()
        {
      

            return View(); // Veriyi view'a gönderiyoruz
        }
        public async Task<IActionResult> User()
        {
            // Veritabanından tüm filmleri alıyoruz
            var users = await _context.Users.ToListAsync();

            return View(users); // Veriyi view'a gönderiyoruz
        }
        public async Task<IActionResult> Movie()
        {
            // Veritabanından tüm filmleri alıyoruz
            var movies = await _context.Movies.ToListAsync();

            return View(movies); // Veriyi view'a gönderiyoruz
        }


        [HttpGet]
        public IActionResult addmovie()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie, IFormFile picture_url, IFormFile movie_url)
        {
            if (ModelState.IsValid)
            {
                // Resmi kaydetme işlemi
                if (picture_url != null)
                {
                    var picturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(picture_url.FileName));
                    using (var stream = new FileStream(picturePath, FileMode.Create))
                    {
                        await picture_url.CopyToAsync(stream);
                    }
                    movie.picture_url = "/images/" + Path.GetFileName(picture_url.FileName); // Dosyanın URL'si
                }

                // Video kaydetme işlemi
                if (movie_url != null)
                {
                    var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", Path.GetFileName(movie_url.FileName));
                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await movie_url.CopyToAsync(stream);
                    }
                    movie.movie_url = "/videos/" + Path.GetFileName(movie_url.FileName); // Dosyanın URL'si
                }

                // Filmi veritabanına kaydetme
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Form geçerli değilse, hata mesajları ile birlikte formu tekrar göster
            return View(movie);
        }

    }
}
