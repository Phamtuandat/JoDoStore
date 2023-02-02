﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshopdotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class OverideAdressColunm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AdressId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AdressId",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AdressId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AdressId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
