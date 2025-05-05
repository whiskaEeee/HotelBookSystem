using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last_Update",
                table: "Hotels");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Create_Date",
                table: "Hotels",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Update_Date",
                table: "Hotels",
                type: "date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create_Date", "Update_Date" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Create_Date", "Update_Date" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Create_Date", "Update_Date" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Create_Date", "Update_Date" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Create_Date", "Update_Date" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Create_Date",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Update_Date",
                table: "Hotels");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Last_Update",
                table: "Hotels",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Last_Update",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Last_Update",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4,
                column: "Last_Update",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5,
                column: "Last_Update",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6,
                column: "Last_Update",
                value: new DateOnly(1, 1, 1));
        }
    }
}
