using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class updateProdcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateofPurchase" },
                values: new object[] { "a1", "zavierlim", new DateTime(2021, 4, 16, 21, 8, 2, 494, DateTimeKind.Local).AddTicks(3280) });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a1", 1, null, 1.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductComment",
                keyColumns: new[] { "CustomerId", "OrderId", "ProductId" },
                keyValues: new object[] { "zavierlim", "a1", 1 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "a1");
        }
    }
}
