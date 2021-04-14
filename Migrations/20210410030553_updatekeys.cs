using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class updatekeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Orders_ordersOrderId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ordersOrderId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ordersOrderId",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ordersOrderId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ordersOrderId",
                table: "Customer",
                column: "ordersOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Orders_ordersOrderId",
                table: "Customer",
                column: "ordersOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
