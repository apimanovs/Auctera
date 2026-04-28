using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auctera.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLocationAndLotYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "lots",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "users");

            migrationBuilder.DropColumn(
                name: "year",
                table: "lots");
        }
    }
}
