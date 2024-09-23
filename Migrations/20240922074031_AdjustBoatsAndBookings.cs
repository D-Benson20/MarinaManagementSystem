using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarinaManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AdjustBoatsAndBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BoatId",
                table: "Bookings",
                column: "BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Boats_BoatId",
                table: "Bookings",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "BoatId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Boats_BoatId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BoatId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "Bookings");
        }
    }
}
