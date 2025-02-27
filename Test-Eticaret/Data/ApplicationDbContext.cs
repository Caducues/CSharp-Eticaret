using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Test_Eticaret.Models;
using Test_Eticaret.Models.Views;

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
             Builder.Entity<Movie>()
                        .HasOne(o => o.Category) // Movie bir Category'e bağlı
                        .WithMany( c => c.Movies) // Category birçok Movie'ye sahip
                        .HasForeignKey(o => o.category_id) // Foreign Key belirleme
                        .OnDelete(DeleteBehavior.Cascade); // Cascade ayarı

            Builder.Entity<Category>()
                      .HasKey(p => p.category_id);

            Builder.Entity<Favorite>()
                      .HasKey(p => p.fav_id);

            Builder.Entity<Favorite>()
                        .HasOne(o => o.User) 
                        .WithMany(c => c.Favorites) 
                        .HasForeignKey(o => o.user_id) 
                        .OnDelete(DeleteBehavior.Cascade);
            Builder.Entity<Favorite>()
                        .HasOne(o => o.Movie)
                        .WithMany(c => c.Favorites)
                        .HasForeignKey(o => o.movie_id)
                        .OnDelete(DeleteBehavior.Cascade);
            Builder.Entity<Admin>()
                     .HasKey(p => p.admin_id);
            Builder.Entity<Admin_Role>()
                 .HasKey(p => p.role_id);
            Builder.Entity<Admin_Role>()
                        .HasOne(o => o.Admin)
                        .WithMany(c => c.Admin_Roles)
                        .HasForeignKey(o => o.admin_id)
                        .OnDelete(DeleteBehavior.Cascade);

            //viewlar

            Builder.Entity<MoviesByCategory_View>()
                        .HasNoKey().ToView("MoviesByCategory_View"); ; // View'lar için birincil anahtar yoktur.
                         base.OnModelCreating(Builder);

            Builder.Entity<PopularMoviesImdb_View>()
                        .HasNoKey().ToView("PopularMoviesImdb_View"); ; 
            base.OnModelCreating(Builder);

            Builder.Entity<RecentlyAddedMovies_View>()
                        .HasNoKey().ToView("RecentlyAddedMovies_View"); ;
            base.OnModelCreating(Builder);

            Builder.Entity<TopLikedMovies_View>()
                       .HasNoKey().ToView("TopLikedMovies_View"); ;
            base.OnModelCreating(Builder);

          

            


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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Admin_Role> Admin_Roles { get; set; }

        //fonsiyonlar
        public int CountMovies()
        {
            // SQL fonksiyonunu çağırıyoruz
            var totalMovies = this.Movies
                .FromSqlRaw("SELECT CountMovies()")
                .Select(x => x.movie_id) 
                .FirstOrDefault(); // Veritabanından gelen ilk sonucu alıyoruz

            return totalMovies == 0 ? 0 : totalMovies; // Null kontrolü
        }
        public string FormatMovieTime(float movieTime)
        {           
            var result = this.Set<Movie>()
                .FromSqlRaw("SELECT MovieTimeFormat({0})", movieTime)
                .AsEnumerable() // Veriyi belleğe alıyoruz
                .FirstOrDefault();  // İlk sonucu al

            return result == null ? string.Empty : result.movie_time.ToString();
        }

        //SP ler

        public void DeleteMovieById(int movieId)
        {
            // Stored Procedure'ü çağırıyoruz.
            this.Database.ExecuteSqlRaw("CALL DeleteMovieById({0})", movieId);
        }

        public void UpdateViewCount(int movieId)
        {
            this.Database.ExecuteSqlRaw("CALL UpdateViewCount({0})", movieId);
        }
    }
}
