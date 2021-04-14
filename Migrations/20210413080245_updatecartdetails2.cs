using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class updatecartdetails2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                columns: new[] { "CartId", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "CartId");
        }
    }
}
