using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class last_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SellOrder",
                table: "SellOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyOrder",
                table: "BuyOrder");

            migrationBuilder.RenameTable(
                name: "SellOrder",
                newName: "SellOrders");

            migrationBuilder.RenameTable(
                name: "BuyOrder",
                newName: "BuyOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellOrders",
                table: "SellOrders",
                column: "SellOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyOrders",
                table: "BuyOrders",
                column: "BuyOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SellOrders",
                table: "SellOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyOrders",
                table: "BuyOrders");

            migrationBuilder.RenameTable(
                name: "SellOrders",
                newName: "SellOrder");

            migrationBuilder.RenameTable(
                name: "BuyOrders",
                newName: "BuyOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellOrder",
                table: "SellOrder",
                column: "SellOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyOrder",
                table: "BuyOrder",
                column: "BuyOrderID");
        }
    }
}
