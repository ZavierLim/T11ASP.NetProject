using Microsoft.EntityFrameworkCore.Migrations;

namespace T11ASP.NetProject.Migrations
{
    public partial class seeddataandalotothers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imgUrl",
                table: "ProductList",
                newName: "ImgUrl");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.CartId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_Cart_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductList_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductList",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductComment",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComment", x => new { x.ProductId, x.CustomerId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ProductComment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductComment_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductComment_ProductList_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductList",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    ItemNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.ItemNumber);
                    table.ForeignKey(
                        name: "FK_CartDetails_Cart_CartId_CustomerId",
                        columns: x => new { x.CartId, x.CustomerId },
                        principalTable: "Cart",
                        principalColumns: new[] { "CartId", "CustomerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartDetails_ProductList_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductList",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "Name", "Password", "SessionId" },
                values: new object[] { "zavierlim", "NUS", "zavier", "123", null });

            migrationBuilder.InsertData(
                table: "ProductList",
                columns: new[] { "ProductId", "Description", "ImgUrl", "ProductName", "Rating", "ShortDescription", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Adobe Photoshop is a raster graphics and digital art editor developed  for Windows and macOS. The software has become the industry standard not only in raster graphics editing, but in digital art as a whole. Photoshop can edit and compose raster images in multiple layers and supports masks, alpha compositing and several color models including RGB, CMYK, CIELAB, spot color, and duotone. In addition to raster graphics, Photoshop with plug-ins supports editing or rendering text and vector graphics as well as 3D graphics and video. ", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/af/Adobe_Photoshop_CC_icon.svg/1200px-Adobe_Photoshop_CC_icon.svg.png", "Adobe Photoshop", 2.5, "Adobe Photoshop is a raster graphics and digital art editor developed for Windows and macOS", 150.0 },
                    { 2, "Adobe Illustrator is a vector graphics editor and design program. Adobe Illustrator is the industry standard design app that lets you capture your creative vision with shapes, color, effects, and typography. Work across desktop and mobile devices and quickly create beautiful designs that can go anywhere—print, web and apps, video and animations, and more.", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Adobe_Illustrator_CC_icon.svg/1280px-Adobe_Illustrator_CC_icon.svg.png", "Adobe Illustrator", 3.0, "Adobe Illustrator is a vector graphics editor and design program", 180.0 },
                    { 3, "Adobe Lightroom is a creative image organization and image manipulation software developed by Adobe Inc. as part of the Creative Cloud subscription family. Its primary uses include importing/saving, viewing, organizing, tagging, editing, and sharing large numbers of digital images. Lightroom's editing functions include white balance, tone, presence, tone curve, HSL, color grading, detail, lens corrections, and calibration manipulation, as well as transformation, spot removal, red eye correction, graduated filters, radial filters, and adjustment brushing.", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png", "Adobe Lightroom", 1.5, "Adobe Lightroom is a creative image organization and image manipulation software developed by Adobe Inc. as part of the Creative Cloud subscription family. ", 240.0 },
                    { 4, "Adobe InDesign is a desktop publishing and typesetting software application produced by Adobe Inc.. It can be used to create works such as posters, flyers, brochures, magazines, newspapers, presentations, books and e-books. InDesign can also publish content suitable for tablet devices in conjunction with Adobe Digital Publishing Suite. Graphic designers and production artists are the principal users, creating and laying out periodical publications, posters, and print media. It also supports export to EPUB and SWF formats to create e-books and digital publications, including digital magazines, and content suitable for consumption on tablet computers. In addition, InDesign supports XML, style sheets, and other coding markup, making it suitable for exporting tagged text content for use in other digital and online formats.", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png", "Adobe InDesign", 2.0, "Adobe InDesign is a desktop publishing and typesetting software application produced by Adobe Inc.", 88.0 },
                    { 5, "Adobe XD is a vector-based user experience design tool for web apps and mobile apps, developed and published by Adobe Inc. It is available for macOS and Windows, although there are versions for iOS and Android to help preview the result of work directly on mobile devices. Adobe XD supports website wireframing and creating click-through prototypes.", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Adobe_XD_CC_icon.svg/1280px-Adobe_XD_CC_icon.svg.png", "Adobe XD", 3.0, "Adobe XD is a vector-based user experience design tool for web apps and mobile apps, developed and published by Adobe Inc", 125.0 },
                    { 6, "Adobe Premiere Pro is a timeline-based video editing software application developed by Adobe Inc. and published as part of the Adobe Creative Cloud licensing program. First launched in 2003, Adobe Premiere Pro is a successor of Adobe Premiere (first launched in 1991). It is geared towards professional video editing, while its sibling, Adobe Premiere Elements, targets the consumer market.", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Adobe_InDesign_CC_icon.svg/1280px-Adobe_InDesign_CC_icon.svg.png", "Adobe Premier Pro", 1.5, "Adobe Premiere Pro is a timeline-based video editing software application developed by Adobe Inc. and published as part of the Adobe Creative Cloud licensing program. ", 240.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId_CustomerId",
                table: "CartDetails",
                columns: new[] { "CartId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ProductId",
                table: "CartDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_CustomerId",
                table: "ProductComment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_OrderId",
                table: "ProductComment",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductComment");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: "zavierlim");

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductList",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "ProductList",
                newName: "imgUrl");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customer",
                newName: "FirstName");
        }
    }
}
