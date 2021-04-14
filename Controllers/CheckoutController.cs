using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext context;
        public CheckoutController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var currentsession = HttpContext.Session.GetString("sessionId");
            if (currentsession != null)
            {
                var currentcustomer = context.Customer.FirstOrDefault(x => x.CustomerId == currentsession).CustomerId;
                ViewData["1"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomer).ToList();

            }
            return View();
        }
    }
}
