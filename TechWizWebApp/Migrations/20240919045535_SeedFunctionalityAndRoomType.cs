using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechWizWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedFunctionalityAndRoomType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Functionalities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Furniture" },
                    { 2, "Lighting" },
                    { 3, "Decor" },
                    { 4, "Rugs and Carpets" },
                    { 5, "Wall Art" },
                    { 6, " Curtains and Blinds" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Living Rooms" },
                    { 2, "Bedrooms" },
                    { 3, "Kitchens" },
                    { 4, "Bathrooms" },
                    { 5, "Offices" },
                    { 6, "Outdoor Spaces" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Functionalities",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 6);
        }
    }
}
