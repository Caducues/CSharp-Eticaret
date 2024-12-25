using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> anime_details()
        {
            // Veritabanından tüm filmleri alıyoruz
            var movies = await _context.Movies.ToListAsync();

            return View(movies); // Veriyi view'a gönderiyoruz
        }
        public IActionResult anime_watching()
        {
            return View();
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
    } 
}

