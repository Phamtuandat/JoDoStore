using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshop_dotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class isDefaultAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Address",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
