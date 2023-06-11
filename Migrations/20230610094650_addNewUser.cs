using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspCoreWebAPIDemos.Migrations
{
    /// <inheritdoc />
    public partial class addNewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CityEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityEntityId",
                        column: x => x.CityEntityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuestName = table.Column<string>(type: "TEXT", nullable: true),
                    Point = table.Column<int>(type: "INTEGER", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "This is Viet Nam capital", "Ha Noi" },
                    { 2, "Thai Lan capital where is a very attractive tourist place", "Bangkok" },
                    { 3, "China captial with many Chinese traditional food you can taste", "Beijing" },
                    { 4, "A beautiful city of Japan located in the South East", "Okinawa" },
                    { 5, "Kingdom of fashion and France capital. You should definitely visit it at least once.", "Paris" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "admin", "123456" },
                    { 2, "api_user", "api123456" }
                });

            migrationBuilder.InsertData(
                table: "Rate",
                columns: new[] { "Id", "CityId", "GuestName", "Point" },
                values: new object[,]
                {
                    { 1, 1, "Nguyen Son", 10 },
                    { 2, 1, "Thu Huong", 7 },
                    { 3, 1, "Sarah Chalez", 4 },
                    { 4, 2, "David Micheal", 8 },
                    { 5, 2, "Mariah Ozawa", 6 },
                    { 6, 3, "Okata Mutan", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_District_CityEntityId",
                table: "District",
                column: "CityEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_CityId",
                table: "Rate",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
