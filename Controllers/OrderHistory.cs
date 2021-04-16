using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class OrderHistory : Controller
    {
        private readonly AppDbContext context;

        public OrderHistory(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var currentsession = HttpContext.Session.GetString("sessionId");
            var orderid = context.Orders.Where(x => x.CustomerId == currentsession).ToList().OrderByDescending(x=>x.DateofPurchase);
            ViewData["session"] = currentsession;
            if(orderid!=null)
            {
                ViewData["orders"] = orderid;
            }


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

            return View();
        }
    }
}
