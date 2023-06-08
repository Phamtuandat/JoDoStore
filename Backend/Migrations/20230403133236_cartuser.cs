using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Migrations
{
      /// <inheritdoc />
      public partial class cartuser : Migration
      {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropForeignKey(
                      name: "FK_Address_Users_UserId",
                      table: "Address");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_Address_AddressBookId",
                      table: "Order");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_Users_UserId",
                      table: "Order");

                  migrationBuilder.DropPrimaryKey(
                      name: "PK_Address",
                      table: "Address");

                  migrationBuilder.RenameTable(
                      name: "Address",
                      newName: "AddressBook");

                  migrationBuilder.RenameIndex(
                      name: "IX_Address_UserId",
                      table: "AddressBook",
                      newName: "IX_AddressBook_UserId");

                  migrationBuilder.AlterColumn<string>(
                      name: "UserId",
                      table: "Order",
                      type: "text",
                      nullable: true,
                      oldClrType: typeof(string),
                      oldType: "text");

                  migrationBuilder.AddPrimaryKey(
                      name: "PK_AddressBook",
                      table: "AddressBook",
                      column: "Id");

                  migrationBuilder.CreateTable(
                      name: "Cart",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            UserId = table.Column<string>(type: "text", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_Cart", x => x.Id);
                            table.ForeignKey(
                          name: "FK_Cart_Users_UserId",
                          column: x => x.UserId,
                          principalTable: "Users",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "CartItem",
                      columns: table => new
                      {
                            Id = table.Column<int>(type: "integer", nullable: false)
                              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                            ProductId = table.Column<int>(type: "integer", nullable: true),
                            Quantity = table.Column<int>(type: "integer", nullable: false),
                            UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                            CartId = table.Column<int>(type: "integer", nullable: true)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_CartItem", x => x.Id);
                            table.ForeignKey(
                          name: "FK_CartItem_Cart_CartId",
                          column: x => x.CartId,
                          principalTable: "Cart",
                          principalColumn: "Id");
                            table.ForeignKey(
                          name: "FK_CartItem_Product_ProductId",
                          column: x => x.ProductId,
                          principalTable: "Product",
                          principalColumn: "Id");
                      });

                  migrationBuilder.CreateIndex(
                      name: "IX_Cart_UserId",
                      table: "Cart",
                      column: "UserId",
                      unique: true);

                  migrationBuilder.CreateIndex(
                      name: "IX_CartItem_CartId",
                      table: "CartItem",
                      column: "CartId");

                  migrationBuilder.CreateIndex(
                      name: "IX_CartItem_ProductId",
                      table: "CartItem",
                      column: "ProductId");

                  migrationBuilder.AddForeignKey(
                      name: "FK_AddressBook_Users_UserId",
                      table: "AddressBook",
                      column: "UserId",
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_AddressBook_AddressBookId",
                      table: "Order",
                      column: "AddressBookId",
                      principalTable: "AddressBook",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_Users_UserId",
                      table: "Order",
                      column: "UserId",
                      principalTable: "Users",
                      principalColumn: "Id");
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropForeignKey(
                      name: "FK_AddressBook_Users_UserId",
                      table: "AddressBook");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_AddressBook_AddressBookId",
                      table: "Order");

                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_Users_UserId",
                      table: "Order");

                  migrationBuilder.DropTable(
                      name: "CartItem");

                  migrationBuilder.DropTable(
                      name: "Cart");

                  migrationBuilder.DropPrimaryKey(
                      name: "PK_AddressBook",
                      table: "AddressBook");

                  migrationBuilder.RenameTable(
                      name: "AddressBook",
                      newName: "Address");

                  migrationBuilder.RenameIndex(
                      name: "IX_AddressBook_UserId",
                      table: "Address",
                      newName: "IX_Address_UserId");

                  migrationBuilder.AlterColumn<string>(
                      name: "UserId",
                      table: "Order",
                      type: "text",
                      nullable: false,
                      defaultValue: "",
                      oldClrType: typeof(string),
                      oldType: "text",
                      oldNullable: true);

                  migrationBuilder.AddPrimaryKey(
                      name: "PK_Address",
                      table: "Address",
                      column: "Id");

                  migrationBuilder.AddForeignKey(
                      name: "FK_Address_Users_UserId",
                      table: "Address",
                      column: "UserId",
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_Address_AddressBookId",
                      table: "Order",
                      column: "AddressBookId",
                      principalTable: "Address",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_Users_UserId",
                      table: "Order",
                      column: "UserId",
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            }
      }
}
