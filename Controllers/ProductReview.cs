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

        public IActionResult Index(int productId,string orderId)
        {
            var product = context.ProductList.Find(productId);
            var sessionname = HttpContext.Session.GetString("sessionId");
            ViewData["product"] = product;
            ViewData["orderid"] = orderId;
            ViewData["session"] = sessionname;


            //if product is already reviewed, the page displays message that product is reviewed.
            var isproductreviewed = context.ProductComment.FirstOrDefault(x=>x.ProductId==productId
                                    && x.OrderId==orderId && x.CustomerId==HttpContext.Session.GetString("sessionId"));
            if(isproductreviewed!=null)
            {
                ViewData["IsProductAlreadyReviewed"] = true;
            }
            else
            {
                ViewData["IsProductAlreadyReviewed"] = false;
            }


            return View();
        }

        //Add comments to DB
        [HttpPost]
        public IActionResult Index(int productId,string productRating,string comment,string orderId)
        {
            ProductComment customercomment = new ProductComment
            {
                ProductId = productId,
                CustomerId = HttpContext.Session.GetString("sessionId"),
                OrderId = orderId,
                Rating = Convert.ToInt32(productRating),
                Comment = comment,
                DateTimeofComment = DateTime.Now
            };
            context.ProductComment.Add(customercomment);
            context.SaveChanges();

            return RedirectToAction("index","orderHistory");
        }
    }
}
