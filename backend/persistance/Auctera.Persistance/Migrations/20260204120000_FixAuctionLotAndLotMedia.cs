using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auctera.Persistance.Migrations
{
    /// <summary>
    /// Represents the fix auction lot and lot media class.
    /// </summary>
    public partial class FixAuctionLotAndLotMedia : Migration
    {
        /// <summary>
        /// Performs the up operation.
        /// </summary>
        /// <param name="migrationBuilder">Migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lots_auctions_AuctionId",
                table: "lots");

            migrationBuilder.DropIndex(
                name: "IX_lots_AuctionId",
                table: "lots");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "lots");

            migrationBuilder.CreateTable(
                name: "lot_media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LotId = table.Column<Guid>(type: "uuid", nullable: false),
                    media_key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    media_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lot_media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lot_media_lots_LotId",
                        column: x => x.LotId,
                        principalTable: "lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auctions_LotId",
                table: "auctions",
                column: "LotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lot_media_LotId_media_key_media_type",
                table: "lot_media",
                columns: new[] { "LotId", "media_key", "media_type" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_auctions_lots_LotId",
                table: "auctions",
                column: "LotId",
                principalTable: "lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <summary>
        /// Performs the down operation.
        /// </summary>
        /// <param name="migrationBuilder">Migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auctions_lots_LotId",
                table: "auctions");

            migrationBuilder.DropTable(
                name: "lot_media");

            migrationBuilder.DropIndex(
                name: "IX_auctions_LotId",
                table: "auctions");

            migrationBuilder.AddColumn<Guid>(
                name: "AuctionId",
                table: "lots",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_lots_AuctionId",
                table: "lots",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_lots_auctions_AuctionId",
                table: "lots",
                column: "AuctionId",
                principalTable: "auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
