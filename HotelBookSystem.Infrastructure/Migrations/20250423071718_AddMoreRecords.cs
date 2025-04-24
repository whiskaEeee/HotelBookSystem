using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBookSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Description", "ImageUrl", "Last_Update", "Name", "Occupancy", "Price" },
                values: new object[,]
                {
                    { 2, "A peaceful retreat by the ocean with stunning sunset views.", "https://placehold.co/600x402", new DateOnly(1, 1, 1), "Seaside Escape", 15, 5200.0 },
                    { 3, "Cozy lodge located in the heart of the mountains, perfect for ski lovers.", "https://placehold.co/600x403", new DateOnly(1, 1, 1), "Mountain Lodge", 10, 6100.0 },
                    { 4, "Modern hotel in the city center with quick access to all attractions.", "https://placehold.co/600x404", new DateOnly(1, 1, 1), "Urban Stay", 40, 4500.0 },
                    { 5, "Luxurious desert resort with pools, spa, and unforgettable night skies.", "https://placehold.co/600x405", new DateOnly(1, 1, 1), "Desert Oasis", 12, 7400.0 },
                    { 6, "Secluded cabins surrounded by ancient trees and wildlife.", "https://placehold.co/600x406", new DateOnly(1, 1, 1), "Forest Hideaway", 8, 3300.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Description", "ImageUrl", "Last_Update", "Name", "Occupancy", "Price" },
                values: new object[] { 1, "zzzzzzzzzzzzzzzzzzzzzzz", "https://placehold.co/600x401", new DateOnly(1, 1, 1), "Test", 27, 3980.0 });
        }
    }
}
