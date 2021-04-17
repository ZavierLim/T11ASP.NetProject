using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;
using T11ASP.NetProject.Util;

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
        //HG Changes here
        public IActionResult Index()
        {
            var sessionname = HttpContext.Session.GetString("sessionId");
            var numberofitems = 0;
            string cartContent = HttpContext.Session.GetString("cartContent");

            ViewData["session"] = sessionname;
            if (sessionname != null)
            {
                var currentcustomer = context.Customer.FirstOrDefault(x => x.CustomerId == sessionname).CustomerId;
                ViewData["4"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomer).ToList();

                var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));

                foreach (CartDetails cd in cartexists)
                {
                    //numberofitems = cartexists.Count();
                    numberofitems = cd.Quantity + numberofitems;
                }
                if (numberofitems < 1)
                {
                    ViewData["numberofproductsincart"] = null;
                }
                else
                {
                    ViewData["numberofproductsincart"] = numberofitems;
                    HttpContext.Session.SetInt32("cartCount", numberofitems);
                }
            }
            else
            {
                List<CartDetails> cartList = CartManager.JsonStringToList(cartContent);
                ViewData["cartContent"] = CartManager.ListToDictionary(context, cartList);
                
                ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            }
            return View();
        }

        //HG Changes here
        public IActionResult AddToCart(int productId,int quantity, string returnUrl)
        {
            //retrieve the cartId from DB;
            var sessionname = HttpContext.Session.GetString("sessionId");
            string cartContent = HttpContext.Session.GetString("cartContent");

            if (sessionname != null)
            {
                var CurrentCartExist = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);
                //cart does not exist, create new cart

                if (CurrentCartExist == null)
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
                CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId;
                var itemInCart = context.CartDetails.FirstOrDefault(c => c.CartId == CurrentCartId && c.ProductId == productId);

                if (itemInCart == null)
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
                    HttpContext.Session.SetInt32("cartCount", itemInCart.Quantity);
                }

                context.SaveChanges();
            } 
            ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            //return Redirect(HttpContext.Request.Headers["Referer"]);
            return Json(new { isOkay = true });
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

                context.SaveChanges();
            }

            return RedirectToAction("index", "cart");
        }

        //HG changes here
        public IActionResult UpdateCartFromList ([FromBody] ListCart listCart)
        {
            string sessionname = HttpContext.Session.GetString("sessionId");
            string cartContent = HttpContext.Session.GetString("cartContent");
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(listCart.ProductId));
            List<CartDetails> updatedCartContent = CartManager.updateCart(cartContent, productAdded, 1);

            HttpContext.Session.SetInt32("cartCount", listCart.CartCount);
            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));

            if (sessionname != null)
            {
                var CurrentCartExist = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);

                //cart does not exist, create new cart

                if (CurrentCartExist == null)
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
                CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId;
                var itemInCart = context.CartDetails.FirstOrDefault(c => c.CartId == CurrentCartId && c.ProductId == int.Parse(listCart.ProductId));

                if (itemInCart == null)
                {
                    var newproductincart = new CartDetails
                    {
                        CartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId.ToString(),
                        ProductId = int.Parse(listCart.ProductId),
                        Quantity = 1
                    };
                    context.CartDetails.Add(newproductincart);
                }
                else
                {
                    itemInCart.Quantity += 1;
                }

                context.SaveChanges();
            }
            /*            if (!(CartManager.duplicateItem(cartContent, productAdded)))
                        {
                            HttpContext.Session.SetInt32("cartCount", listCart.CartCount + 1);
                            return Json(new { isOkay = true });
                        }*/

            return Json(new { isOkay = false });
        }
    }
}
    