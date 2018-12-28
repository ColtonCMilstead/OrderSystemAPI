using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderSystemAPI.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderEntries",
                columns: table => new
                {
                    CustomerID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntries", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "FullOrders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(nullable: false),
                    orderEntryCustomerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullOrders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_FullOrders_OrderEntries_orderEntryCustomerID",
                        column: x => x.orderEntryCustomerID,
                        principalTable: "OrderEntries",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ProductID = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderEntryCustomerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Item_OrderEntries_OrderEntryCustomerID",
                        column: x => x.OrderEntryCustomerID,
                        principalTable: "OrderEntries",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<double>(nullable: false),
                    FullOrderOrderID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_FullOrders_FullOrderOrderID",
                        column: x => x.FullOrderOrderID,
                        principalTable: "FullOrders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullOrders_orderEntryCustomerID",
                table: "FullOrders",
                column: "orderEntryCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_OrderEntryCustomerID",
                table: "Item",
                column: "OrderEntryCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FullOrderOrderID",
                table: "Products",
                column: "FullOrderOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FullOrders");

            migrationBuilder.DropTable(
                name: "OrderEntries");
        }
    }
}
