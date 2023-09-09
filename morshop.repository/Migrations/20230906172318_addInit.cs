using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace morshop.repository.Migrations
{
    public partial class addInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTypes = table.Column<int>(type: "int", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsHome = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfSales = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardItems_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Telefon", "telefon" },
                    { 2, "Tablet", "tablet" },
                    { 3, "Bilgisayar", "bilgisayar" },
                    { 4, "Televizyon", "televizyon" },
                    { 5, "Konsol", "konsol" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CurrentPrice", "Description", "ImageUrl", "IsApproved", "IsHome", "Name", "NumberOfSales", "PreviousPrice", "Url" },
                values: new object[,]
                {
                    { 1, 1, 68899m, "Iphone 14 Pro Max 1 tb Hafıza", "iphone-14-pro-max.jpeg", true, false, "Iphone 14 Pro Max", 1230, 70999m, "iphone-14-pro-max" },
                    { 2, 1, 59899m, "Iphone 14 1 tb Hafıza", "iphone-14.jpeg", true, false, "Iphone 14", 1453, 61999m, "iphone-14" },
                    { 3, 1, 45999m, "Iphone 13 Pro Max 512 gb Hafıza", "iphone-13-pro-max.jpeg", true, false, "Iphone 13 Pro Max", 1100, 48999m, "iphone-13-pro-max" },
                    { 4, 1, 35899m, "Iphone 12 512 gb Hafıza", "iphone-12.jpeg", true, false, "Iphone 12", 920, 37999m, "iphone-12" },
                    { 5, 1, 35999m, "Samsun S20 Ultra 1 tb Hafıza", "samsung-s23-ultra.jpeg", true, false, "Samsung S23 Ultra", 963, 37999m, "samsung-s23-ultra" },
                    { 6, 2, 30899m, "Ipad Air 5. Nesil 1 tb Hafıza", "ipad-air-5-nesil.jpeg", true, false, "Ipad Air 5. Nesil", 571, 32999m, "ipad-air-5-nesil" },
                    { 7, 2, 30999m, "Samsung Tab S8 Ultra 1 tb Hafıza", "samsung-tab-s8-ultra.jpeg", true, false, "Samsung Tab S8 Ultra", 236, 28999m, "samsung-tab-s8-ultra" },
                    { 8, 3, 70899m, "Macbook Pro 1 tb Hafıza", "macbook-pro.jpeg", true, false, "Macbook Pro", 1230, 65999m, "macbook-pro" },
                    { 9, 3, 29899m, "Monster Abra A5 1 tb Hafıza", "monster-abra-a5.jpeg", true, false, "Monster Abra A5", 851, 25999m, "monster-abra-a5" },
                    { 10, 3, 90899m, "Asus Rog 17.1 512 gb Hafıza Rtx4080 i7 13750h", "asus-rog-17.jpeg", true, false, "Asus Rog 17", 1230, 81999m, "asus-rog-17" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_CardId",
                table: "CardItems",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
