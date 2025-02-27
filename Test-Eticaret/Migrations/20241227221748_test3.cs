using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Eticaret.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin_Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    admin_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_Roles", x => x.role_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    admin_password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    admin_email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Admin_Rolerole_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.admin_id);
                    table.ForeignKey(
                        name: "FK_Admins_Admin_Roles_Admin_Rolerole_id",
                        column: x => x.Admin_Rolerole_id,
                        principalTable: "Admin_Roles",
                        principalColumn: "role_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Roles_admin_id",
                table: "Admin_Roles",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Admin_Rolerole_id",
                table: "Admins",
                column: "Admin_Rolerole_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Roles_Admins_admin_id",
                table: "Admin_Roles",
                column: "admin_id",
                principalTable: "Admins",
                principalColumn: "admin_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Roles_Admins_admin_id",
                table: "Admin_Roles");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Admin_Roles");
        }
    }
}
