using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Eticaret.Migrations
{
    /// <inheritdoc />
    public partial class adddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "movie_time",
                table: "Movies",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "imdb",
                table: "Movies",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "add_date",
                table: "Movies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "add_date",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "movie_time",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "imdb",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
