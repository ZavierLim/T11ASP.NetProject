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

            if(orderid!=null)
            {
                ViewData["orders"] = orderid;
            }
            return View();
        }
    }
}
