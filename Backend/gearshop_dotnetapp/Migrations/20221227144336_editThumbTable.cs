using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshopdotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class editThumbTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaticPath",
                table: "Thumbnail",
                newName: "PublicId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Thumbnail",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Thumbnail");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Thumbnail",
                newName: "StaticPath");
        }
    }
}
