using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Models;

namespace Test_Eticaret.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
