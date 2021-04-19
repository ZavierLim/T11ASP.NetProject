using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;
using T11ASP.NetProject.Util;

namespace T11ASP.NetProject.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly AppDbContext context;
        public ProductDetailsController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var sessionname = HttpContext.Session.GetString("sessionId");
            var product = context.ProductList.Find(id);
            
            //To display recommended products
            var allProducts = context.ProductList.OrderBy(emp => Guid.NewGuid()).Take(3).ToList();
            ViewData["products"] = allProducts;
            ViewData["product"] = product;
            ViewData["session"] = sessionname;

            // Calculate the average rating per product
            var prodComment = context.ProductComment.Where(x => x.ProductId == id).ToList();
            int numberofprods = prodComment.Count();
            var ratingSum = prodComment.Sum(x => x.Rating);
            if (numberofprods < 1)
            {
                ViewData["prodRating"] = 0.0;
                ViewData["numberofProds"] = 0;
            }
            else
            {
                ViewData["prodRating"] = ratingSum / numberofprods;
                ViewData["numberofProds"] = numberofprods;
            }

            //If logged in: Pass data to cartnumber in layout view
            if (sessionname != null)
            {
                var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
                var numberofitems = cartexists.Sum(x=>x.Quantity);
                if (numberofitems < 1)
                {
                    ViewData["numberofproductsincart"] = null;
                }
                else
                {
                    ViewData["numberofproductsincart"] = numberofitems;
                }
            }
            //If not logged in: use Sessiondata to pass cartnumber in layout view
            else
            {
                ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            }

            //To show product reviews page
            var AllProductReviews = context.ProductComment.Where(x => x.ProductId == id).ToList();
            ViewData["AllProductReviews"] = AllProductReviews;
            return View();
        }
    }
}
