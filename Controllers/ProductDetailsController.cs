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
            var allProducts = context.ProductList.OrderBy(emp => Guid.NewGuid()).Take(3).ToList(); //Recommendations code
            ViewData["products"] = allProducts; //Recommendations code
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

            //To show number of items in cart on navigation bar
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
            else
            {
                ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            }

            //To show product reviews page
            var AllProductReviews = context.ProductComment.Where(x => x.ProductId == id).ToList();
            ViewData["AllProductReviews"] = AllProductReviews;
            return View();
        }

        //TODO: shift this to cart controller. 
        public IActionResult CartFromDetail(string prodId, int qty, string cmd)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");

            int cartCount = HttpContext.Session.GetInt32("cartCount") ?? 0;

            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(prodId));
            List<CartDetails> updatedCartContent = CartManager.updateCart(cartContent, productAdded, qty);

            cartCount = cartCount + qty;

            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            HttpContext.Session.SetInt32("cartCount", cartCount);

            //to check if it is from add-to-cart button or buy now button
            if (cmd != null)
            {
                string url = String.Format("/ProductDetails/Index?id={0}", prodId);
                return Redirect(url);
            }

            return RedirectToAction("Index", "Cart");
        }



    }
}
