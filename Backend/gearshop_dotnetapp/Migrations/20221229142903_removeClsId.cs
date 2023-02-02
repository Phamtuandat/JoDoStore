using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshopdotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class removeClsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnail_ImageCollections_CollectionsId",
                table: "Thumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnail_CollectionsId",
                table: "Thumbnail");

            migrationBuilder.DropColumn(
                name: "CollectionsId",
                table: "Thumbnail");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Product",
                newName: "CaegoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                newName: "IX_Product_CaegoryId");

            migrationBuilder.AddColumn<int>(
                name: "ImageCollectionsId",
                table: "Thumbnail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_ImageCollectionsId",
                table: "Thumbnail",
                column: "ImageCollectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CaegoryId",
                table: "Product",
                column: "CaegoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnail_ImageCollections_ImageCollectionsId",
                table: "Thumbnail",
                column: "ImageCollectionsId",
                principalTable: "ImageCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CaegoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnail_ImageCollections_ImageCollectionsId",
                table: "Thumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnail_ImageCollectionsId",
                table: "Thumbnail");

            migrationBuilder.DropColumn(
                name: "ImageCollectionsId",
                table: "Thumbnail");

            migrationBuilder.RenameColumn(
                name: "CaegoryId",
                table: "Product",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CaegoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CollectionsId",
                table: "Thumbnail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnail_CollectionsId",
                table: "Thumbnail",
                column: "CollectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnail_ImageCollections_CollectionsId",
                table: "Thumbnail",
                column: "CollectionsId",
                principalTable: "ImageCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
