using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColNameInBookingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripePaymentId",
                table: "Bookings",
                newName: "StripeSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripeSessionId",
                table: "Bookings",
                newName: "StripePaymentId");
        }
    }
}
