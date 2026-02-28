using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auctera.Persistance.Migrations
{
    public partial class AddOrdersAndLotAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "lots",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "lots",
                type: "text",
                nullable: false,
                defaultValue: "Other");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "lots",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "lots",
                type: "text",
                nullable: false,
                defaultValue: "Good");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "lots",
                type: "text",
                nullable: false,
                defaultValue: "Unisex");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "lots",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "OS");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    PaymentDeadlineUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StripeCheckoutSessionId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PaidAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_AuctionId",
                table: "orders",
                column: "AuctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_BuyerId",
                table: "orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_SellerId",
                table: "orders",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Status_PaymentDeadlineUtc",
                table: "orders",
                columns: new[] { "Status", "PaymentDeadlineUtc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropColumn(name: "Brand", table: "lots");
            migrationBuilder.DropColumn(name: "Category", table: "lots");
            migrationBuilder.DropColumn(name: "Color", table: "lots");
            migrationBuilder.DropColumn(name: "Condition", table: "lots");
            migrationBuilder.DropColumn(name: "Gender", table: "lots");
            migrationBuilder.DropColumn(name: "Size", table: "lots");
        }
    }
}
