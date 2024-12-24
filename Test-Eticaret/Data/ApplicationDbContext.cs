using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Test_Eticaret.Models;

namespace Test_Eticaret.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
            Builder.Entity<User>()
                        .HasKey(p => p.user_id);
            Builder.Entity<User>()
           .HasIndex(u => u.user_email)   // Email alanını indexliyoruz
           .IsUnique();
            Builder.Entity<Movie>()
                       .HasKey(p => p.movie_id);
                      
            Builder.Entity<Category>()
                      .HasKey(p => p.category_id);

        }

        private bool HasOne(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        private void HasForeignKey(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
