using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Migrations
{
      /// <inheritdoc />
      public partial class MutipleToSingleQuery : Migration
      {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_AddressBook_AddressBookId",
                      table: "Order");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Product_Category_CategoryId",
                      table: "Product");

                  migrationBuilder.DropTable(
                      name: "AddressBook");

                  migrationBuilder.DropTable(
                      name: "Photo");

                  migrationBuilder.DropTable(
                      name: "ProductTag");

                  migrationBuilder.DropTable(
                      name: "ImageCollections");

                  migrationBuilder.DropTable(
                      name: "Tag");

                  migrationBuilder.DropIndex(
                      name: "IX_Product_CategoryId",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "CategoryId",
                      table: "Product");

                  migrationBuilder.RenameColumn(
                      name: "AddressBookId",
                      table: "Order",
                      newName: "AddressId");

                  migrationBuilder.RenameIndex(
                      name: "IX_Order_AddressBookId",
                      table: "Order",
                      newName: "IX_Order_AddressId");

                  migrationBuilder.AddColumn<DateTime>(
                      name: "CreateAt",
                      table: "Product",
                      type: "timestamp with time zone",
                      nullable: false,
                      defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                  migrationBuilder.AddColumn<string[]>(
                      name: "ImagePaths",
                      table: "Product",
                      type: "text[]",
                      nullable: false,
                      defaultValue: new string[0]);

                  migrationBuilder.AddColumn<string>(
                      name: "Slug",
                      table: "Product",
                      type: "text",
                      nullable: false,
                      defaultValue: "");

                  migrationBuilder.AddColumn<string[]>(
                      name: "Tags",
                      table: "Product",
                      type: "text[]",
                      nullable: false,
                      defaultValue: new string[0]);

                  migrationBuilder.AddColumn<string>(
                      name: "Thumbnail",
                      table: "Product",
                      type: "text",
                      nullable: false,
                      defaultValue: "");

                  migrationBuilder.AddColumn<int>(
                      name: "ParentCategoryId",
                      table: "Category",
                      type: "integer",
                      nullable: true);

                  migrationBuilder.AddColumn<string>(
                      name: "Slug",
                      table: "Category",
                      type: "text",
                      nullable: false,
                      defaultValue: "");

                  migrationBuilder.CreateTable(
                      name: "Address",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            Street = table.Column<string>(type: "text", nullable: false),
                            City = table.Column<string>(type: "text", nullable: false),
                            State = table.Column<string>(type: "text", nullable: false),
                            ZipCode = table.Column<string>(type: "text", nullable: false),
                            UserId = table.Column<string>(type: "text", nullable: true)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Address", x => x.Id);
                            table.ForeignKey(
                          name: "FK_Address_Users_UserId",
                          column: x => x.UserId,
                          principalTable: "Users",
                          principalColumn: "Id");
                      });

                  migrationBuilder.CreateTable(
                      name: "ProductCategory",
                      columns: table => new
                      {
                            ProductId = table.Column<int>(type: "integer", nullable: false),
                            CategoryId = table.Column<int>(type: "integer", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_ProductCategory", x => new { x.CategoryId, x.ProductId });
                            table.ForeignKey(
                          name: "FK_ProductCategory_Category_CategoryId",
                          column: x => x.CategoryId,
                          principalTable: "Category",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                          name: "FK_ProductCategory_Product_ProductId",
                          column: x => x.ProductId,
                          principalTable: "Product",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateIndex(
                      name: "IX_Product_Slug",
                      table: "Product",
                      column: "Slug",
                      unique: true);

                  migrationBuilder.CreateIndex(
                      name: "IX_Category_ParentCategoryId",
                      table: "Category",
                      column: "ParentCategoryId");

                  migrationBuilder.CreateIndex(
                      name: "IX_Category_Slug",
                      table: "Category",
                      column: "Slug");

                  migrationBuilder.CreateIndex(
                      name: "IX_Address_UserId",
                      table: "Address",
                      column: "UserId");

                  migrationBuilder.CreateIndex(
                      name: "IX_ProductCategory_ProductId",
                      table: "ProductCategory",
                      column: "ProductId");

                  migrationBuilder.AddForeignKey(
                      name: "FK_Category_Category_ParentCategoryId",
                      table: "Category",
                      column: "ParentCategoryId",
                      principalTable: "Category",
                      principalColumn: "Id");

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_Address_AddressId",
                      table: "Order",
                      column: "AddressId",
                      principalTable: "Address",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropForeignKey(
                      name: "FK_Category_Category_ParentCategoryId",
                      table: "Category");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_Address_AddressId",
                      table: "Order");

                  migrationBuilder.DropTable(
                      name: "Address");

                  migrationBuilder.DropTable(
                      name: "ProductCategory");

                  migrationBuilder.DropIndex(
                      name: "IX_Product_Slug",
                      table: "Product");

                  migrationBuilder.DropIndex(
                      name: "IX_Category_ParentCategoryId",
                      table: "Category");

                  migrationBuilder.DropIndex(
                      name: "IX_Category_Slug",
                      table: "Category");

                  migrationBuilder.DropColumn(
                      name: "CreateAt",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "ImagePaths",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "Slug",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "Tags",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "Thumbnail",
                      table: "Product");

                  migrationBuilder.DropColumn(
                      name: "ParentCategoryId",
                      table: "Category");

                  migrationBuilder.DropColumn(
                      name: "Slug",
                      table: "Category");

                  migrationBuilder.RenameColumn(
                      name: "AddressId",
                      table: "Order",
                      newName: "AddressBookId");

                  migrationBuilder.RenameIndex(
                      name: "IX_Order_AddressId",
                      table: "Order",
                      newName: "IX_Order_AddressBookId");

                  migrationBuilder.AddColumn<int>(
                      name: "CategoryId",
                      table: "Product",
                      type: "integer",
                      nullable: false,
                      defaultValue: 0);

                  migrationBuilder.CreateTable(
                      name: "AddressBook",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            UserId = table.Column<string>(type: "text", nullable: false),
                            Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                            District = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                            IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                            Name = table.Column<string>(type: "text", nullable: false),
                            PhoneNumber = table.Column<string>(type: "text", nullable: false),
                            Province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                            Ward = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_AddressBook", x => x.Id);
                            table.ForeignKey(
                          name: "FK_AddressBook_Users_UserId",
                          column: x => x.UserId,
                          principalTable: "Users",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "ImageCollections",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            Name = table.Column<string>(type: "text", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_ImageCollections", x => x.Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "Tag",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            Description = table.Column<string>(type: "text", nullable: false),
                            Name = table.Column<string>(type: "text", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Tag", x => x.Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "Photo",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            ImageCollectionsId = table.Column<int>(type: "integer", nullable: true),
                            ProductId = table.Column<int>(type: "integer", nullable: true),
                            Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                            Description = table.Column<string>(type: "text", nullable: false),
                            ImageUrl = table.Column<string>(type: "text", nullable: false),
                            PublicId = table.Column<string>(type: "text", nullable: false),
                            Title = table.Column<string>(type: "text", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Photo", x => x.Id);
                            table.ForeignKey(
                          name: "FK_Photo_ImageCollections_ImageCollectionsId",
                          column: x => x.ImageCollectionsId,
                          principalTable: "ImageCollections",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.SetNull);
                            table.ForeignKey(
                          name: "FK_Photo_Product_ProductId",
                          column: x => x.ProductId,
                          principalTable: "Product",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "ProductTag",
                      columns: table => new
                      {
                            ProductsId = table.Column<int>(type: "integer", nullable: false),
                            TagsId = table.Column<int>(type: "integer", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_ProductTag", x => new { x.ProductsId, x.TagsId });
                            table.ForeignKey(
                          name: "FK_ProductTag_Product_ProductsId",
                          column: x => x.ProductsId,
                          principalTable: "Product",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                          name: "FK_ProductTag_Tag_TagsId",
                          column: x => x.TagsId,
                          principalTable: "Tag",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateIndex(
                      name: "IX_Product_CategoryId",
                      table: "Product",
                      column: "CategoryId");

                  migrationBuilder.CreateIndex(
                      name: "IX_AddressBook_UserId",
                      table: "AddressBook",
                      column: "UserId");

                  migrationBuilder.CreateIndex(
                      name: "IX_Photo_ImageCollectionsId",
                      table: "Photo",
                      column: "ImageCollectionsId");

                  migrationBuilder.CreateIndex(
                      name: "IX_Photo_ProductId",
                      table: "Photo",
                      column: "ProductId");

                  migrationBuilder.CreateIndex(
                      name: "IX_ProductTag_TagsId",
                      table: "ProductTag",
                      column: "TagsId");

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_AddressBook_AddressBookId",
                      table: "Order",
                      column: "AddressBookId",
                      principalTable: "AddressBook",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);

                  migrationBuilder.AddForeignKey(
                      name: "FK_Product_Category_CategoryId",
                      table: "Product",
                      column: "CategoryId",
                      principalTable: "Category",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
            }
      }
}
