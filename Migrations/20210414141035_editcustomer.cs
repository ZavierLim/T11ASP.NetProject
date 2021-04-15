using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class editcustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImgUrl",
                value: "https://bit.ly/3rX5aOq");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImgUrl",
                value: "https://bit.ly/39R0N14");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImgUrl",
                value: "https://bit.ly/3t02j8u");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImgUrl",
                value: "https://bit.ly/3mqIk0l ");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImgUrl",
                value: "https://bit.ly/3fQtf6Q");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ImgUrl",
                value: "https://bit.ly/2Q3XnAW ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/a/af/Adobe_Photoshop_CC_icon.svg/1200px-Adobe_Photoshop_CC_icon.svg.png");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Adobe_Illustrator_CC_icon.svg/1280px-Adobe_Illustrator_CC_icon.svg.png");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Adobe_XD_CC_icon.svg/1280px-Adobe_XD_CC_icon.svg.png");

            migrationBuilder.UpdateData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ImgUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png");
        }
    }
}
