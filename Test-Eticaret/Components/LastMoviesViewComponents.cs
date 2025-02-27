using Microsoft.AspNetCore.Mvc;
using Test_Eticaret.Data;
using System.Linq;

namespace Test_Eticaret.Components
{
    public class RecentMoviesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RecentMoviesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Veritabanından son 5 filmi alıyoruz
            var recentMovies = _context.Movies
                                       .OrderByDescending(m => m.movie_id) // Yani son eklenen 5 film
                                       .Take(7)
                                       .ToList();

            // Filmleri view'a gönderiyoruz
            return View(recentMovies); // Bu, 'Views/Shared/Components/RecentMovies/Default.cshtml' dosyasına yönlendirecektir
        }
    }
}
