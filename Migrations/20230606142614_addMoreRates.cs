using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspCoreWebAPIDemos.Migrations
{
    /// <inheritdoc />
    public partial class addMoreRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CityId", "GuestName", "Point" },
                values: new object[] { 1, "Sarah Chalez", 4 });

            migrationBuilder.InsertData(
                table: "Rate",
                columns: new[] { "Id", "CityId", "GuestName", "Point" },
                values: new object[,]
                {
                    { 4, 2, "David Micheal", 8 },
                    { 5, 2, "Mariah Ozawa", 6 },
                    { 6, 3, "Okata Mutan", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CityId", "GuestName", "Point" },
                values: new object[] { 2, "David Micheal", 8 });
        }
    }
}
