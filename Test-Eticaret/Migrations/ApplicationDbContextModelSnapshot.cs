﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test_Eticaret.Data;

#nullable disable

namespace Test_Eticaret.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Test_Eticaret.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("category_id"));

                    b.Property<string>("category_name")
                        .HasColumnType("longtext");

                    b.HasKey("category_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Test_Eticaret.Models.Movie", b =>
                {
                    b.Property<int>("movie_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("movie_id"));

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<int>("imdb")
                        .HasColumnType("int");

                    b.Property<string>("movie_description")
                        .HasColumnType("longtext");

                    b.Property<string>("movie_name")
                        .HasColumnType("longtext");

                    b.HasKey("movie_id");

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

                    b.HasIndex("user_email")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
