using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext context;

        public string CurrentCartId { get; set; }

        public CartController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int productId,int quantity)
        {
            ViewData["1"] = productId;
            ViewData["2"] = quantity;
            ViewData["3"] = context.ProductList.FirstOrDefault(c => c.ProductId == 1).Description.ToString();
            var currentsession = HttpContext.Session.GetString("sessionId");
            if(currentsession!=null)
            {
                var currentcustomer = context.Customer.FirstOrDefault(x => x.SessionId == currentsession).CustomerId;
                ViewData["4"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomer).ToList();

            }
            return View();
        }

        public IActionResult AddToCart(int productId,int quantity)
        {
            //retrieve the cartId from DB;
            string sessionIdInSession = HttpContext.Session.GetString("sessionId");
            var sessionindb = context.Customer.FirstOrDefault(x => x.SessionId == sessionIdInSession).SessionId.ToString();
            var CurrentCartExist = context.Cart.FirstOrDefault(x => x.CustomerId == sessionIdInSession);
            var customerName = context.Customer.FirstOrDefault(c => c.SessionId == sessionIdInSession).CustomerId.ToString();
            //cart does not exist

            if (CurrentCartExist==null &&sessionindb==null)
            {
                var CreateCart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    CustomerId = customerName
                };
                context.Cart.Add(CreateCart);
                context.SaveChanges();

            }
            //cart exist,to add items;
            CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId ==customerName).CartId;
            var itemInCart = context.CartDetails.FirstOrDefault(c =>c.CartId== CurrentCartId && c.ProductId==productId);
            if(itemInCart==null)
            {
                string nameofcustomer = context.Customer.FirstOrDefault(x => x.SessionId == sessionindb).CustomerId.ToString();
                var newproductincart = new CartDetails
                {
                    CartId = context.Cart.FirstOrDefault(x => x.CustomerId == nameofcustomer).CartId.ToString(),
                    ProductId = productId,
                    Quantity = quantity
                };
                context.CartDetails.Add(newproductincart);
                context.SaveChanges();
            }
            else
            {
                itemInCart.Quantity += quantity;
                context.SaveChanges();
            }
            return RedirectToAction("index","cart");
        }

        public void RemoveItemFromCart(int productId,int quantity)
        {
            Console.WriteLine("hi");
        }
    }
}
