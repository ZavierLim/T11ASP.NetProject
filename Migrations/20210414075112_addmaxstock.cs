using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class addmaxstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxStock",
                table: "ProductList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "MaxStock",
                value: 100);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "MaxStock",
                value: 100);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "MaxStock",
                value: 100);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "MaxStock",
                value: 100);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "MaxStock",
                value: 100);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "MaxStock",
                value: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxStock",
                table: "ProductList");
        }
    }
}
