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
            var sessionname = HttpContext.Session.GetString("sessionId");
            if(sessionname!=null)
            {
                var currentcustomer = context.Customer.FirstOrDefault(x => x.CustomerId == sessionname).CustomerId;
                ViewData["4"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomer).ToList();

            }
            return View();
        }

        public IActionResult AddToCart(int productId,int quantity)
        {
            //retrieve the cartId from DB;
            var sessionname = HttpContext.Session.GetString("sessionId");
            var CurrentCartExist = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);
            //cart does not exist, create new cart

            if (CurrentCartExist==null)
            {
                var CreateCart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    CustomerId = sessionname
                };
                context.Cart.Add(CreateCart);
                context.SaveChanges();

            }
            //To add items;
            CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId ==sessionname).CartId;
            var itemInCart = context.CartDetails.FirstOrDefault(c =>c.CartId== CurrentCartId && c.ProductId==productId);
            
            if(itemInCart==null)
            {
                var newproductincart = new CartDetails
                {
                    CartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId.ToString(),
                    ProductId = productId,
                    Quantity = quantity
                };
                context.CartDetails.Add(newproductincart);

            }
            else
            {
                itemInCart.Quantity += quantity;
            }
            context.ProductList.Find(productId).MaxStock -= quantity;
            context.SaveChanges();
            return RedirectToAction("index","cart");
        }

        public IActionResult RemoveItemFromCart(int productId,string cartId,int quantity)
        {
            var customercart = context.CartDetails.FirstOrDefault(x => x.CartId == cartId && x.ProductId == productId);
            if(customercart!=null)
            {
                if(quantity==1 && customercart.Quantity>1)
                {
                    customercart.Quantity -= 1;
                }
                else
                {
                    context.CartDetails.Remove(customercart);
                }
                context.ProductList.Find(productId).MaxStock += quantity;
                context.SaveChangesAsync();
            }

            return RedirectToAction("index", "cart");
        }
    }
}
    