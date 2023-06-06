using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspCoreWebAPIDemos.Migrations
{
    /// <inheritdoc />
    public partial class addRateForCitySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_City_CityId",
                table: "District");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "District",
                newName: "CityEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_District_CityId",
                table: "District",
                newName: "IX_District_CityEntityId");

            migrationBuilder.InsertData(
                table: "Rate",
                columns: new[] { "Id", "CityId", "GuestName", "Point" },
                values: new object[,]
                {
                    { 1, 1, "Nguyen Son", 10 },
                    { 2, 1, "Thu Huong", 7 },
                    { 3, 2, "David Micheal", 8 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_District_City_CityEntityId",
                table: "District",
                column: "CityEntityId",
                principalTable: "City",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_City_CityEntityId",
                table: "District");

            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rate",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "CityEntityId",
                table: "District",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_District_CityEntityId",
                table: "District",
                newName: "IX_District_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_City_CityId",
                table: "District",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");
        }
    }
}
