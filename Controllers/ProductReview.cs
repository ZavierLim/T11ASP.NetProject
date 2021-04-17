using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class ProductReview : Controller
    {
        private readonly AppDbContext context;

        public ProductReview(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int id,string orderId)
        {
            var product = context.ProductList.Find(id);
            ViewData["product"] = product;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int productId,int productRating,string comment,string orderId)
        {
            ProductComment customercomment = new ProductComment
            {
                ProductId = productId,
                CustomerId = HttpContext.Session.GetString("sessionId"),
                OrderId = orderId,
                Rating = productRating,
                Comment = comment
            };
            context.ProductComment.Add(customercomment);
            context.SaveChanges();

            return View();
        }
    }
}
