using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizWebApp.Migrations
{
    /// <inheritdoc />
    public partial class product_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productid = table.Column<int>(type: "int", nullable: false),
                    imagename = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_productid",
                table: "ProductImage",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage");
        }
    }
}
