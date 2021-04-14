using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class cartdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Cart_CartId_CustomerId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartId_CustomerId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CartDetails");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Cart_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Cart_CartId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "CartDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                columns: new[] { "CartId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId_CustomerId",
                table: "CartDetails",
                columns: new[] { "CartId", "CustomerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Cart_CartId_CustomerId",
                table: "CartDetails",
                columns: new[] { "CartId", "CustomerId" },
                principalTable: "Cart",
                principalColumns: new[] { "CartId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
