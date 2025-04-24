using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOneRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Description", "ImageUrl", "Last_Update", "Name", "Occupancy", "Price" },
                values: new object[] { 1, "zzzzzzzzzzzzzzzzzzzzzzz", "https://placehold.co/600x401", new DateOnly(1, 1, 1), "Test", 27, 3980.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
