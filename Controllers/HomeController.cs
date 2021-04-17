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
        private readonly ILogger<HomeController> _logger;
        //private readonly IProductListRepository _ProductList;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allProducts = context.ProductList.ToList();
            ViewData["products"] = allProducts;
            ViewData["session"] = HttpContext.Session.GetString("sessionId");
            var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
            var numberofitems= cartexists.Count();
            if (numberofitems<1)
            {
                ViewData["numberofproductsincart"] = null;
            }
            else
            {
                ViewData["numberofproductsincart"] = numberofitems;
            }


            return View(allProducts);
        }

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
            
            
            //var searchedProducts = context.Search(searchterm);
            
            return View(await searchedProducts.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
