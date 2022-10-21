using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddAdminTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adress_Users_UserId",
                table: "Adress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adress",
                table: "Adress");

            migrationBuilder.RenameTable(
                name: "Adress",
                newName: "Adresses");

            migrationBuilder.RenameIndex(
                name: "IX_Adress_UserId",
                table: "Adresses",
                newName: "IX_Adresses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admin-Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin-Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin-Table_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Users_UserId",
                table: "Adresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Users_UserId",
                table: "Adresses");

            migrationBuilder.DropTable(
                name: "Admin-Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Adress");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_UserId",
                table: "Adress",
                newName: "IX_Adress_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adress",
                table: "Adress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adress_Users_UserId",
                table: "Adress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
