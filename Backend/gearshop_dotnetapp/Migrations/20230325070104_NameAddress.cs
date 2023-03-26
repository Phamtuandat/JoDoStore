using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshop_dotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class NameAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Order",
                newName: "AddressBookId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                newName: "IX_Order_AddressBookId");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Address",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Address",
                newName: "District");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Address",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressBookId",
                table: "Order",
                column: "AddressBookId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressBookId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "AddressBookId",
                table: "Order",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressBookId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Address",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Address",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Address",
                newName: "StreetAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
