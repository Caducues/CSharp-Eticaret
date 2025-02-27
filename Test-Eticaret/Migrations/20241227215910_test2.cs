using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Eticaret.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Favoritefav_id",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Favoritefav_id",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "movie_date",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "movie_url",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "picture_url",
                table: "Movies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "view",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    fav_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    movie_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.fav_id);
                    table.ForeignKey(
                        name: "FK_Favorites_Movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "Movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Favoritefav_id",
                table: "Users",
                column: "Favoritefav_id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_category_id",
                table: "Movies",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Favoritefav_id",
                table: "Movies",
                column: "Favoritefav_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_movie_id",
                table: "Favorites",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_user_id",
                table: "Favorites",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Categories_category_id",
                table: "Movies",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Favorites_Favoritefav_id",
                table: "Movies",
                column: "Favoritefav_id",
                principalTable: "Favorites",
                principalColumn: "fav_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Favorites_Favoritefav_id",
                table: "Users",
                column: "Favoritefav_id",
                principalTable: "Favorites",
                principalColumn: "fav_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Categories_category_id",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Favorites_Favoritefav_id",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Favorites_Favoritefav_id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Users_Favoritefav_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Movies_category_id",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Favoritefav_id",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Favoritefav_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Favoritefav_id",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "movie_date",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "movie_url",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "picture_url",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "view",
                table: "Movies");
        }
    }
}
