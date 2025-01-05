﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test_Eticaret.Data;

#nullable disable

namespace Test_Eticaret.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250105044537_adddate")]
    partial class adddate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Test_Eticaret.Models.Admin", b =>
                {
                    b.Property<int>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("admin_id"));

                    b.Property<int?>("Admin_Rolerole_id")
                        .HasColumnType("int");

                    b.Property<string>("admin_email")
                        .HasColumnType("longtext");

                    b.Property<string>("admin_password")
                        .HasColumnType("longtext");

                    b.HasKey("admin_id");

                    b.HasIndex("Admin_Rolerole_id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Admin_Role", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("role_id"));

                    b.Property<int>("admin_id")
                        .HasColumnType("int");

                    b.Property<string>("role")
                        .HasColumnType("longtext");

                    b.HasKey("role_id");

                    b.HasIndex("admin_id");

                    b.ToTable("Admin_Roles");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("category_id"));

                    b.Property<string>("category_name")
                        .HasColumnType("longtext");

                    b.Property<int?>("movie_id")
                        .HasColumnType("int");

                    b.HasKey("category_id");

                    b.HasIndex("movie_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Favorite", b =>
                {
                    b.Property<int>("fav_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("fav_id"));

                    b.Property<int>("movie_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("fav_id");

                    b.HasIndex("movie_id");

                    b.HasIndex("user_id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Movie", b =>
                {
                    b.Property<int>("movie_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("movie_id"));

                    b.Property<int?>("Favoritefav_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("add_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<float>("imdb")
                        .HasColumnType("float");

                    b.Property<int>("like")
                        .HasColumnType("int");

                    b.Property<DateTime>("movie_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("movie_description")
                        .HasColumnType("longtext");

                    b.Property<string>("movie_name")
                        .HasColumnType("longtext");

                    b.Property<float>("movie_time")
                        .HasColumnType("float");

                    b.Property<string>("movie_url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("picture_url")
                        .HasColumnType("longtext");

                    b.Property<int>("view")
                        .HasColumnType("int");

                    b.HasKey("movie_id");

                    b.HasIndex("Favoritefav_id");

                    b.HasIndex("category_id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Test_Eticaret.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("user_id"));

                    b.Property<int?>("Favoritefav_id")
                        .HasColumnType("int");

                    b.Property<string>("first_name")
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .HasColumnType("longtext");

                    b.Property<int>("tel_no")
                        .HasColumnType("int");

                    b.Property<string>("user_email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("user_password")
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.HasIndex("Favoritefav_id");

                    b.HasIndex("user_email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Admin", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Admin_Role", null)
                        .WithMany("Admins")
                        .HasForeignKey("Admin_Rolerole_id");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Admin_Role", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Admin", "Admin")
                        .WithMany("Admin_Roles")
                        .HasForeignKey("admin_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Category", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Movie", null)
                        .WithMany("Categories")
                        .HasForeignKey("movie_id");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Favorite", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Movie", "Movie")
                        .WithMany("Favorites")
                        .HasForeignKey("movie_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test_Eticaret.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Movie", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Favorite", null)
                        .WithMany("Movies")
                        .HasForeignKey("Favoritefav_id");

                    b.HasOne("Test_Eticaret.Models.Category", "Category")
                        .WithMany("Movies")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Test_Eticaret.Models.User", b =>
                {
                    b.HasOne("Test_Eticaret.Models.Favorite", null)
                        .WithMany("Users")
                        .HasForeignKey("Favoritefav_id");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Admin", b =>
                {
                    b.Navigation("Admin_Roles");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Admin_Role", b =>
                {
                    b.Navigation("Admins");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Category", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Favorite", b =>
                {
                    b.Navigation("Movies");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Movie", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("Test_Eticaret.Models.User", b =>
                {
                    b.Navigation("Favorites");
                });
#pragma warning restore 612, 618
        }
    }
}
