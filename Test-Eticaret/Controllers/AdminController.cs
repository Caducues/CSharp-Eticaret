using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;

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

    }
}
