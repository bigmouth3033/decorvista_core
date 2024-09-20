using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizWebApp.Migrations
{
    /// <inheritdoc />
    public partial class nhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "score",
                table: "Reviews",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "total",
                table: "Orders",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "quanity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "view_count",
                table: "Galleries",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "score",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "quanity",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "view_count",
                table: "Galleries");

            migrationBuilder.AlterColumn<int>(
                name: "total",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
