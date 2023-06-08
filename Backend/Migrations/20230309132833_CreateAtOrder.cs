using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
      /// <inheritdoc />
      public partial class CreateAtOrder : Migration
      {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropForeignKey(
                      name: "FK_Order_Address_AdressId",
                      table: "Order");

                  migrationBuilder.RenameColumn(
                      name: "AdressId",
                      table: "Order",
                      newName: "AddressId");

                  migrationBuilder.RenameIndex(
                      name: "IX_Order_AdressId",
                      table: "Order",
                      newName: "IX_Order_AddressId");

                  migrationBuilder.AddColumn<DateTime>(
                      name: "CreateAt",
                      table: "Order",
                      type: "timestamp with time zone",
                      nullable: false,
                      defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                      name: "FK_Order_Address_AddressId",
                      table: "Order");

                  migrationBuilder.DropColumn(
                      name: "CreateAt",
                      table: "Order");

                  migrationBuilder.RenameColumn(
                      name: "AddressId",
                      table: "Order",
                      newName: "AdressId");

                  migrationBuilder.RenameIndex(
                      name: "IX_Order_AddressId",
                      table: "Order",
                      newName: "IX_Order_AdressId");

                  migrationBuilder.AddForeignKey(
                      name: "FK_Order_Address_AdressId",
                      table: "Order",
                      column: "AdressId",
                      principalTable: "Address",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            }
      }
}
