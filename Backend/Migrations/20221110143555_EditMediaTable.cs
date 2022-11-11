using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class EditMediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_ProductId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Media");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelId",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProductModelId",
                table: "Media",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Products_ProductModelId",
                table: "Media",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Products_ProductModelId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_ProductModelId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Media");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProductId",
                table: "Media",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
