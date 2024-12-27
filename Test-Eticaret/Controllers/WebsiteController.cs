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
            var selected_movie = await _context.Movies.FirstOrDefaultAsync(m => m.movie_id == movieId);

            if (selected_movie == null)
            {

                return NotFound();
            }

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
        public async Task<IActionResult> categories()
        {
            // Veritabanından tüm filmleri alıyoruz
            var movies = await _context.Movies.ToListAsync();

            return View(movies); // Veriyi view'a gönderiyoruz
        }    // POST: Login

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

