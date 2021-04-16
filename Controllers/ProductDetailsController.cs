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


            var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
            var numberofitems = cartexists.Sum(x => x.Quantity);
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
