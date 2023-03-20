using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gearshop_dotnetapp.Migrations
{
    /// <inheritdoc />
    public partial class renameSubtotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Order",
                newName: "SubtotalPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubtotalPrice",
                table: "Order",
                newName: "SubTotal");
        }
    }
}
