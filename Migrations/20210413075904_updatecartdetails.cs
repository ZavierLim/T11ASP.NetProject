using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class updatecartdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "ItemNumber",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "CartDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "CartDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ItemNumber",
                table: "CartDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "ItemNumber");
        }
    }
}
