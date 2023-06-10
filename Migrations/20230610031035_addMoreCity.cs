using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspCoreWebAPIDemos.Migrations
{
    /// <inheritdoc />
    public partial class addMoreCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Thai Lan capital where is a very attractive tourist place");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "China captial with many Chinese traditional food you can taste");

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 4, "A beautiful city of Japan located in the South East", "Okinawa" },
                    { 5, "Kingdom of fashion and France capital. You should definitely visit it at least once.", "Paris" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "This is Thai Lan capital");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "This is a very big city of China");
        }
    }
}
