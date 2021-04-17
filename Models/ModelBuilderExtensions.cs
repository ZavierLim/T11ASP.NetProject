using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductList>().HasData(
                new ProductList
                {
                    ProductId = 1,
                    ProductName = "Adobe Photoshop",
                    Description = "Adobe Photoshop is a raster graphics and digital art editor developed  " +
                    "for Windows and macOS. The software has become the industry standard not only in " +
                    "raster graphics editing, but in digital art as a whole. Photoshop can edit and compose " +
                    "raster images in multiple layers and supports masks, alpha compositing and several " +
                    "color models including RGB, CMYK, CIELAB, spot color, and duotone. In addition to " +
                    "raster graphics, Photoshop with plug-ins supports editing or rendering text and vector " +
                    "graphics as well as 3D graphics and video. ",
                    UnitPrice = 150,
                    ImgUrl = "https://bit.ly/3rX5aOq",
                    ShortDescription = "Adobe Photoshop is a raster graphics and digital art editor developed for Windows and macOS",
                    Rating = 2.5,

                },
                new ProductList
                {
                    ProductId = 2,
                    ProductName = "Adobe Illustrator",
                    Description = "Adobe Illustrator is a vector graphics editor and design program. Adobe Illustrator is the industry standard design app that lets you capture your creative vision with shapes, color, effects, and typography. Work across desktop and mobile devices and quickly create beautiful designs that can go anywhere—print, web and apps, video and animations, and more.",
                    UnitPrice = 180,
                    ImgUrl = "https://bit.ly/39R0N14",
                    ShortDescription = "Adobe Illustrator is a vector graphics editor and design program",
                    Rating = 3,
                },
                new ProductList
                {
                    ProductId = 3,
                    ProductName = "Adobe Lightroom",
                    Description = "Adobe Lightroom is a creative image organization and image manipulation software developed by Adobe Inc. as part of the Creative Cloud subscription family. Its primary uses include importing/saving, viewing, organizing, tagging, editing, and sharing large numbers of digital images. Lightroom's editing functions include white balance, tone, presence, tone curve, HSL, color grading, detail, lens corrections, and calibration manipulation, as well as transformation, spot removal, red eye correction, graduated filters, radial filters, and adjustment brushing.",
                    UnitPrice = 240,
                    ImgUrl = "https://bit.ly/3t02j8u",
                    ShortDescription = "Adobe Lightroom is a creative image organization and image manipulation software developed by Adobe Inc. as part of the Creative Cloud subscription family. ",
                    Rating = 1.5,
                },
                new ProductList
                {
                    ProductId = 4,
                    ProductName = "Adobe InDesign",
                    Description = "Adobe InDesign is a desktop publishing and typesetting software application produced by Adobe Inc.. It can be used to create works such as posters, flyers, brochures, magazines, newspapers, presentations, books and e-books. InDesign can also publish content suitable for tablet devices in conjunction with Adobe Digital Publishing Suite. Graphic designers and production artists are the principal users, creating and laying out periodical publications, posters, and print media. It also supports export to EPUB and SWF formats to create e-books and digital publications, including digital magazines, and content suitable for consumption on tablet computers. In addition, InDesign supports XML, style sheets, and other coding markup, making it suitable for exporting tagged text content for use in other digital and online formats.",
                    UnitPrice = 88,
                    ImgUrl = "https://bit.ly/3mqIk0l ",
                    ShortDescription = "Adobe InDesign is a desktop publishing and typesetting software application produced by Adobe Inc.",
                    Rating = 2,
                },
                new ProductList
                {
                    ProductId = 5,
                    ProductName = "Adobe XD",
                    Description = "Adobe XD is a vector-based user experience design tool for web apps and mobile apps, developed and published by Adobe Inc. It is available for macOS and Windows, although there are versions for iOS and Android to help preview the result of work directly on mobile devices. Adobe XD supports website wireframing and creating click-through prototypes.",
                    UnitPrice = 125,
                    ImgUrl = "https://bit.ly/3fQtf6Q",
                    ShortDescription = "Adobe XD is a vector-based user experience design tool for web apps and mobile apps, developed and published by Adobe Inc",
                    Rating = 3,
                },
                new ProductList
                {
                    ProductId = 6,
                    ProductName = "Adobe Premier Pro",
                    Description = "Adobe Premiere Pro is a timeline-based video editing software application developed by Adobe Inc. and published as part of the Adobe Creative Cloud licensing program. First launched in 2003, Adobe Premiere Pro is a successor of Adobe Premiere (first launched in 1991). It is geared towards professional video editing, while its sibling, Adobe Premiere Elements, targets the consumer market.",
                    UnitPrice = 240,
                    ImgUrl = "https://bit.ly/2Q3XnAW ",
                    ShortDescription = "Adobe Premiere Pro is a timeline-based video editing software application developed by Adobe Inc. and published as part of the Adobe Creative Cloud licensing program. ",
                    Rating = 1.5,
                }
                );

            modelBuilder.Entity<Customer>().HasData(
               new Customer
               {
                   CustomerId = "zavierlim",
                   Name = "zavier",
                   Address = "NUS",
                   Password = "123"
               });
        }
    }
}
