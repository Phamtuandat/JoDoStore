using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class RemovePrUId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Media",
                newName: "ProductModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_ProductId",
                table: "Media",
                newName: "IX_Media_ProductModelId");

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

            migrationBuilder.RenameColumn(
                name: "ProductModelId",
                table: "Media",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_ProductModelId",
                table: "Media",
                newName: "IX_Media_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
