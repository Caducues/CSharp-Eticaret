using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Eticaret.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "movie_id",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_movie_id",
                table: "Categories",
                column: "movie_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Movies_movie_id",
                table: "Categories",
                column: "movie_id",
                principalTable: "Movies",
                principalColumn: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Movies_movie_id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_movie_id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "movie_id",
                table: "Categories");
        }
    }
}
