using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class datetimeofcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductComment",
                keyColumns: new[] { "CustomerId", "OrderId", "ProductId" },
                keyValues: new object[] { "zavierlim", "a1", 1 });

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
                keyValue: "a1");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: "a2");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ProductComment",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeofComment",
                table: "ProductComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeofComment",
                table: "ProductComment");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "ProductComment",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateofPurchase" },
                values: new object[] { "a1", "zavierlim", new DateTime(2021, 4, 16, 21, 20, 56, 231, DateTimeKind.Local).AddTicks(380) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateofPurchase" },
                values: new object[] { "a2", "zavierlim", new DateTime(2021, 4, 16, 21, 20, 56, 236, DateTimeKind.Local).AddTicks(1630) });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a1", 1, null, 1.0 });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a1", 2, null, 2.0 });

            migrationBuilder.InsertData(
                table: "ProductComment",
                columns: new[] { "CustomerId", "OrderId", "ProductId", "Comment", "Rating" },
                values: new object[] { "zavierlim", "a2", 1, null, 4.0 });
        }
    }
}
