using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
      /// <inheritdoc />
      public partial class shippingRow : Migration
      {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.AddColumn<decimal>(
                      name: "ShippingCash",
                      table: "Order",
                      type: "numeric",
                      nullable: false,
                      defaultValue: 0m);

                  migrationBuilder.AddColumn<decimal>(
                      name: "SubTotal",
                      table: "Order",
                      type: "numeric",
                      nullable: false,
                      defaultValue: 0m);
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropColumn(
                      name: "ShippingCash",
                      table: "Order");

                  migrationBuilder.DropColumn(
                      name: "SubTotal",
                      table: "Order");
            }
      }
}
