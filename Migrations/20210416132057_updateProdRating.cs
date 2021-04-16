using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class updateProdRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ProductList");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "a1",
                column: "DateofPurchase",
                value: new DateTime(2021, 4, 16, 21, 20, 56, 231, DateTimeKind.Local).AddTicks(380));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateofPurchase" },
                values: new object[] { "a2", "zavierlim", new DateTime(2021, 4, 16, 21, 20, 56, 236, DateTimeKind.Local).AddTicks(1630) });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a1", 2, null, 2.0 });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a2", 1, null, 4.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductComment",
                keyColumns: new[] { "CustomerId", "OrderId", "ProductId" },
                keyValues: new object[] { "zavierlim", "a2", 1 });

            migrationBuilder.DeleteData(
                table: "ProductComment",
                keyColumns: new[] { "CustomerId", "OrderId", "ProductId" },
                keyValues: new object[] { "zavierlim", "a1", 2 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "a2");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "ProductList",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "a1",
                column: "DateofPurchase",
                value: new DateTime(2021, 4, 16, 21, 8, 2, 494, DateTimeKind.Local).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "Rating",
                value: 2.5);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "Rating",
                value: 3.0);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "Rating",
                value: 1.5);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "Rating",
                value: 2.0);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "Rating",
                value: 3.0);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "Rating",
                value: 1.5);
        }
    }
}
