using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizWebApp.Migrations
{
    /// <inheritdoc />
    public partial class variant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    saleprice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Variants_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VariantAttributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    variantid = table.Column<int>(type: "int", nullable: false),
                    attributetype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority = table.Column<int>(type: "int", nullable: false),
                    attributevalue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantAttributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_VariantAttributes_Variants_variantid",
                        column: x => x.variantid,
                        principalTable: "Variants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttributes_variantid",
                table: "VariantAttributes",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_productid",
                table: "Variants",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantAttributes");

            migrationBuilder.DropTable(
                name: "Variants");
        }
    }
}
