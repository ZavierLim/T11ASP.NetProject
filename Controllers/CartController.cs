using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        //Get request to Cart Page
        public IActionResult Index()
        {
            var sessionname = HttpContext.Session.GetString("sessionId");
            var numberofitems = 0;
            string cartContent = HttpContext.Session.GetString("cartContent");
            ViewData["session"] = sessionname;

            //if sessionId exists--user is loggedin, retrieve current cart from DB. Sessiondata items are already combined during login.
            if (sessionname != null)
            {
                Cart theCard = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);
                List<CartDetails> cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId")).ToList();
                string currentcustomerId = context.Customer.FirstOrDefault(x => x.CustomerId == sessionname).CustomerId;
                ViewData["currentShoppingCart"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomerId).ToList();

                //This will update the number of items in the navigation bar
                foreach (CartDetails cd in cartexists)
                {
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
            //if sessionId does not exists--user is not loggedin, and guests has items in cart, cart shows item in sessiondata
            else
            {
                if (cartContent != null)
                {
                    List<CartDetails> cartList = CartManager.JsonStringToList(cartContent);
                    ViewData["cartContent"] = CartManager.ListToDictionary(context, cartList);
                }
                ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            }
            return View();
        }

        //To Add items to Cart for logged in users
        public IActionResult AddToCart(int productId,int quantity)
        {
            //retrieve the cartId from DB;
            var sessionname = HttpContext.Session.GetString("sessionId");
            //string cartContent = HttpContext.Session.GetString("cartContent");

            //if sessionId Exists
            if (sessionname != null)
            {
                var CurrentCartExist = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);               
            
                //cart does not exist, create new cart and update DB to cart entity
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

                //Add the product in cartdetails entity
                CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId;
                var itemInCart = context.CartDetails.FirstOrDefault(c => c.CartId == CurrentCartId && c.ProductId == productId);

                //if product does not exists in cart
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
                //Product exists in cart, to update cartdetails quantity
                else
                {
                    itemInCart.Quantity += quantity;
                    HttpContext.Session.SetInt32("cartCount", itemInCart.Quantity);
                }

                context.SaveChanges();
            } 
            ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            
           //redirect to the current page of the product that the user is viewing
           return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        //To remove items for logged in user
        public IActionResult RemoveItemFromCart(int productId,string cartId,int quantity)
        {

            var customercart = context.CartDetails.FirstOrDefault(x => x.CartId == cartId && x.ProductId == productId);
            
            //if customer cart exists, minus 1 quantity. if only 1 quantity, to remove the item from DB
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
                HttpContext.Session.SetInt32("cartCount",0);
            }

            return RedirectToAction("index", "cart");
        }

        //Action Methods for NON-REGISTERED USERS--Guests

        //Guest adds item to cart without logging in- to store data in sessionData
        public IActionResult UpdateCartFromList ([FromBody] ListCart listCart)
        {
            string sessionname = HttpContext.Session.GetString("sessionId");
            string cartContent = HttpContext.Session.GetString("cartContent");
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(listCart.ProductId));
            List<CartDetails> updatedCartContent = CartManager.updateCart(cartContent, productAdded, 1);

            HttpContext.Session.SetInt32("cartCount", listCart.CartCount);
            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            return Json(new { isOkay = false });
        }

        //Guest updates cart quantity in cart- to change quantity in SessionData
        public IActionResult UpdateCartFromCart([FromBody] ListCart listCart)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(listCart.ProductId));
            List<CartDetails> updatedCartContent = CartManager.editCart(cartContent, productAdded, listCart.Quantity);

            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            HttpContext.Session.SetInt32("cartCount", listCart.CartCount);

            return Json(new { isOkay = true });
        }

        //Guests remove items from cart
        [HttpPost]
        public IActionResult DeleteItem([FromBody] ListCart listCart)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");
            int cartCount = HttpContext.Session.GetInt32("cartCount") ?? 0;
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(listCart.ProductId));
            List<CartDetails> updatedCartContent = CartManager.editCart(cartContent, productAdded, 0);
            cartCount = cartCount - listCart.Quantity;
            Debug.WriteLine(listCart.Quantity);
            HttpContext.Session.SetInt32("cartCount", cartCount);
            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            return RedirectToAction("Index");
        }

        //Guest add items from CartDetails Page
        public IActionResult CartFromDetail(string prodId, int qty, string cmd)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");

            int cartCount = HttpContext.Session.GetInt32("cartCount") ?? 0;

            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(prodId));
            List<CartDetails> updatedCartContent = CartManager.updateCart(cartContent, productAdded, qty);

            cartCount = cartCount + qty;

            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            HttpContext.Session.SetInt32("cartCount", cartCount);

            //to check if it is from add-to-cart button or buy now button
            if (cmd != null)
            {
                string url = String.Format("/ProductDetails/Index?id={0}", prodId);
                return Redirect(url);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}
    