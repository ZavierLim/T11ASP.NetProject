using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly AppDbContext context;
        Random rnd = new Random();

        public ProductDetailsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            var product = context.ProductList.Find(id);
            var allProducts = context.ProductList.OrderBy(emp => Guid.NewGuid()).Take(3).ToList(); //Recommendations code
            ViewData["products"] = allProducts; //Recommendations code
            ViewData["product"] = product;
            ViewData["session"] = HttpContext.Session.GetString("sessionId");

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


            var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
            var numberofitems = cartexists.Count();
            if (numberofitems < 1)
            {
                ViewData["numberofproductsincart"] = null;
            }
            else
            {
                ViewData["numberofproductsincart"] = numberofitems;
            }
            return View(allProducts);
        }





    }
}
