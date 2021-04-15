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
            var allProducts = context.ProductList.Take(3).ToList(); //Recommendations code
            ViewData["products"] = allProducts; //Recommendations code
            ViewData["product"] = product;
            return View(allProducts);
        }

        

            
        
    }
}
