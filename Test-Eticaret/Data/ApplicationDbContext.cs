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



        //public DbSet<TopLikedMovies_View> TopLikedMovies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Admin_Role> Admin_Roles { get; set; }
        //public DbSet<MoviesByCategory_View> MoviesByCategory_Views { get; set; }
        //public DbSet<PopularMoviesImdb_View> PopularMoviesImdb_Views { get; set; }
        //public DbSet<RecentlyAddedMovies_View> RecentlyAddedMovies_Views { get; set; }
        //public DbSet<TopLikedMovies_View> TopLikedMovies_Views { get; set; }

    }
}
