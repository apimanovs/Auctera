using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auctera.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_Id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_lots_Id",
                table: "lots");

            migrationBuilder.DropIndex(
                name: "IX_bids_Id",
                table: "bids");

            migrationBuilder.DropIndex(
                name: "IX_auctions_Id",
                table: "auctions");

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_Token",
                table: "refresh_tokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId",
                table: "refresh_tokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Id",
                table: "orders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lots_Id",
                table: "lots",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bids_Id",
                table: "bids",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auctions_Id",
                table: "auctions",
                column: "Id",
                unique: true);
        }
    }
}
