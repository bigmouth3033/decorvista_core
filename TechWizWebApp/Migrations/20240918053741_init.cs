﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWizWebApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Functionalities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functionalities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    functionality_id = table.Column<int>(type: "int", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Functionalities_functionality_id",
                        column: x => x.functionality_id,
                        principalTable: "Functionalities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gallery_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    room_type_id = table.Column<int>(type: "int", nullable: false),
                    imageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.id);
                    table.ForeignKey(
                        name: "FK_Galleries_RoomTypes_room_type_id",
                        column: x => x.room_type_id,
                        principalTable: "RoomTypes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "InteriorDesigners",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yearsofexperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    portfolio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daywork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteriorDesigners", x => x.id);
                    table.ForeignKey(
                        name: "FK_InteriorDesigners_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quanity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Carts_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "GalleryDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gallery_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_GalleryDetails_Galleries_gallery_id",
                        column: x => x.gallery_id,
                        principalTable: "Galleries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleryDetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Subcribes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gallery_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcribes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Subcribes_Galleries_gallery_id",
                        column: x => x.gallery_id,
                        principalTable: "Galleries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Subcribes_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interior_designer_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Blogs_InteriorDesigners_interior_designer_id",
                        column: x => x.interior_designer_id,
                        principalTable: "InteriorDesigners",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    designer_id = table.Column<int>(type: "int", nullable: false),
                    scheduled_datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Consultations_InteriorDesigners_designer_id",
                        column: x => x.designer_id,
                        principalTable: "InteriorDesigners",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Consultations_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    designer_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reviews_InteriorDesigners_designer_id",
                        column: x => x.designer_id,
                        principalTable: "InteriorDesigners",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reviews_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_interior_designer_id",
                table: "Blogs",
                column: "interior_designer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_product_id",
                table: "Carts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_user_id",
                table: "Carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_designer_id",
                table: "Consultations",
                column: "designer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_user_id",
                table: "Consultations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_room_type_id",
                table: "Galleries",
                column: "room_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryDetails_gallery_id",
                table: "GalleryDetails",
                column: "gallery_id");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryDetails_product_id",
                table: "GalleryDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_InteriorDesigners_user_id",
                table: "InteriorDesigners",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_functionality_id",
                table: "Products",
                column: "functionality_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_designer_id",
                table: "Reviews",
                column: "designer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_product_id",
                table: "Reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_user_id",
                table: "Reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subcribes_gallery_id",
                table: "Subcribes",
                column: "gallery_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subcribes_user_id",
                table: "Subcribes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_user_id",
                table: "UserDetails",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "GalleryDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Subcribes");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "InteriorDesigners");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Functionalities");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
