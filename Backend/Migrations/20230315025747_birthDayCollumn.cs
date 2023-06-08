using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
      /// <inheritdoc />
      public partial class birthDayCollumn : Migration
      {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.RenameColumn(
                      name: "DateTime",
                      table: "Users",
                      newName: "CreateAt");

                  migrationBuilder.AddColumn<DateOnly>(
                      name: "Birthday",
                      table: "Users",
                      type: "date",
                      nullable: false,
                      defaultValue: new DateOnly(1, 1, 1));

                  migrationBuilder.AddColumn<int>(
                      name: "Gender",
                      table: "Users",
                      type: "integer",
                      nullable: false,
                      defaultValue: 0);
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropColumn(
                      name: "Birthday",
                      table: "Users");

                  migrationBuilder.DropColumn(
                      name: "Gender",
                      table: "Users");

                  migrationBuilder.RenameColumn(
                      name: "CreateAt",
                      table: "Users",
                      newName: "DateTime");
            }
      }
}
