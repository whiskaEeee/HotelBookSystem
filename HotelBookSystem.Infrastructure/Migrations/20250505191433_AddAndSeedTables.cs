using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBookSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAndSeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Update_Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelNumbers",
                columns: table => new
                {
                    Hotel_Number = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelNumbers", x => x.Hotel_Number);
                    table.ForeignKey(
                        name: "FK_HotelNumbers_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Create_Date", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Update_Date" },
                values: new object[,]
                {
                    { 1, null, "Современный отель Mirage предлагает стильный и комфортный отдых в самом сердце города. Интерьеры сочетают в себе элегантность и минимализм, создавая атмосферу покоя и уюта. Идеально подходит как для деловых поездок, так и для туристов.", "\\images\\HotelImages\\8ad226ff-832a-4db3-a6b4-b3cc4a6287a6.png", "Mirage", 30, 180.0, null },
                    { 2, null, "Hotel Veronica — это уютный 3-звездочный отель для тех, кто ищет комфортное и доступное жильё. Он расположен в спокойном районе, недалеко от общественного транспорта и популярных достопримечательностей.", "\\images\\HotelImages\\880d4c1c-bf94-497e-ab9d-c1258a386b01.jpg", "Veronica", 30, 90.0, null },
                    { 3, null, "Отель \"Marina\" — это идеальное место для отдыха у моря. С его террасы открывается захватывающий вид на бескрайний океан. Номера оформлены в современном морском стиле с использованием натуральных материалов и цветов, создающих атмосферу уюта и свежести.", "\\images\\HotelImages\\e19e7191-30d0-49f1-82a9-8442f105df3b.jpeg", "Marina", 30, 220.0, null },
                    { 4, null, "Отель Palace — это уникальное место для тех, кто ценит роскошь и комфорт. Номера, оформленные в классическом стиле, с панорамными окнами, предлагают потрясающий вид на город.", "\\images\\HotelImages\\66787e93-c4fe-4ab0-a741-8fb47dfa9440.png", "Palace", 30, 300.0, null }
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Description", "HotelId", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "Private Pool" },
                    { 2, null, 1, "Microwave" },
                    { 3, null, 1, "Private Balcony" },
                    { 4, null, 1, "1 king bed and 1 sofa bed" },
                    { 5, null, 2, "Private Plunge Pool" },
                    { 6, null, 2, "Microwave and Mini Refrigerator" },
                    { 7, null, 2, "Private Balcony" },
                    { 8, null, 2, "king bed or 2 double beds" },
                    { 9, null, 3, "Private Pool" },
                    { 10, null, 3, "Jacuzzi" },
                    { 11, null, 3, "Private Balcony" },
                    { 12, null, 4, "Infinity Pool" },
                    { 13, null, 4, "Spa and Wellness Center" },
                    { 14, null, 4, "Private Beach Access" }
                });

            migrationBuilder.InsertData(
                table: "HotelNumbers",
                columns: new[] { "Hotel_Number", "Details", "HotelId" },
                values: new object[,]
                {
                    { 101, null, 1 },
                    { 102, null, 1 },
                    { 103, null, 1 },
                    { 201, null, 2 },
                    { 202, null, 2 },
                    { 203, null, 2 },
                    { 301, null, 3 },
                    { 302, null, 3 },
                    { 303, null, 3 },
                    { 401, null, 4 },
                    { 402, null, 4 },
                    { 403, null, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_HotelId",
                table: "Amenities",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelNumbers_HotelId",
                table: "HotelNumbers",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "HotelNumbers");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
