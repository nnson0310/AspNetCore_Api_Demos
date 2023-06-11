using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspCoreWebAPIDemos.Migrations
{
    /// <inheritdoc />
    public partial class addCityToUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "City",
                value: "Ha Noi");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "City", "Name", "Password" },
                values: new object[] { "Bangkok", "api_consumer", "api123" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "City", "Name", "Password" },
                values: new object[,]
                {
                    { 3, "Beijing", "hello_api", "654321" },
                    { 4, "Okinawa", "apis", "Api123" },
                    { 5, "Paris", "api_demos", "666666" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "City",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Password" },
                values: new object[] { "api_user", "api123456" });
        }
    }
}
