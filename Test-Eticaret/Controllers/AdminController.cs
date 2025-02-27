using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;
using Test_Eticaret.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Data;
namespace Test_Eticaret.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment; 
        }

        



        // Film ekleme sayfası
        public IActionResult addmovie()
        {
            // Kategorileri ViewData'ya ekliyoruz, bu kategoriler dropdown'da görünecek
            ViewData["Categories"] = new SelectList(_context.Categories, "category_id", "category_name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addmovie(Movie movie, IFormFile picture_url)
        {
            try
            {
                if (picture_url != null)
                {
                    var fileExtension = Path.GetExtension(picture_url.FileName).ToLower();           
                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                    {
                        var fileName = Guid.NewGuid().ToString() + fileExtension;
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        var filePath = Path.Combine(uploads, fileName);
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await picture_url.CopyToAsync(fileStream);
                        }
                        movie.picture_url = "/uploads/" + fileName;
                        


                    }
                }
                
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                ViewData["Categories"] = new SelectList(_context.Categories, "category_id", "category_name", movie.category_id);
                return View(movie);
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public async Task<IActionResult> Index(string name)
        {
            var movies = await _context.Movies.Include(m => m.Category).ToListAsync();
            return View(movies);  
        }
        public async Task<IActionResult> Movie()
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }
        public async Task<IActionResult> Category()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> User()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

     
    }

}
