using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;
using Test_Eticaret.Models;

namespace Test_Eticaret.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action method
        public async Task<IActionResult> Prod()
        {
            // Veritabanından ürünleri çekiyoruz
            var products = await _context.Products.ToListAsync();

            return View(products); // Veriyi view'a gönder
        }
    }
}
