using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {   
            var allProducts = context.ProductList.ToList();
            string username = HttpContext.Session.GetString("sessionId");
            var cartCount = HttpContext.Session.GetInt32("cartCount");

            //User logged in: To display total cart quantity based on DB in layout view
            if (username != null)
            {
                    var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
                    var numberofitems = cartexists.Sum(x => x.Quantity);
                    if (numberofitems < 1)
                    {
                        HttpContext.Session.SetInt32("cartCount", 0);
                        ViewData["numberofproductsincart"] = 0;
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("cartCount", numberofitems);
                        ViewData["numberofproductsincart"] = numberofitems;
                    }
                
            }
            //User not logged in: To display total cart quantity based on SessionData in layout view
            else
            {

                ViewData["numberofproductsincart"] = cartCount;
            }
            if(username==null)
            {
                ViewData["numberofproductsincart"] = cartCount;
            }

            ViewData["products"] = allProducts;
            ViewData["session"] = username;

            return View();
        }

        //Search Function
        [HttpPost]
        public async Task <IActionResult> Index(string searchterm)
        {
            var searchedProducts = context.ProductList.Where(e => e.ProductName.Contains(searchterm) || e.ShortDescription.Contains(searchterm));
            
            ViewData["products"] = searchedProducts;
            ViewData["searchedterm"] = searchterm;
            ViewData["session"] = HttpContext.Session.GetString("sessionId");

            if (string.IsNullOrEmpty(searchterm))
            {
                var allProducts = context.ProductList.ToList();
                ViewData["products"] = allProducts;
                return View(allProducts);
            }
                        
            return View(await searchedProducts.ToListAsync());
        }
    }
}
