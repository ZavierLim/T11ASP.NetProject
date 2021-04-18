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

        //Route to cart page
        public IActionResult Index()
        {
            var sessionname = HttpContext.Session.GetString("sessionId");
            var numberofitems = 0;
            string cartContent = HttpContext.Session.GetString("cartContent");

            ViewData["session"] = sessionname;

            //if sessionId exists
            if (sessionname != null)
            {
                Cart theCard = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname);
                List<CartDetails> cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId")).ToList();
                string currentcustomerId = context.Customer.FirstOrDefault(x => x.CustomerId == sessionname).CustomerId;
                ViewData["currentShoppingCart"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomerId).ToList();

                //if there is not pending cart content
                if (cartContent == null)
                {
                    //pass the shopping cart stored in DB to view
                    
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


                //if there is an existing cart
                else
                {
                    // remove cart in db if it exist, even if the customer is logged in.
                    if (theCard != null) //if sessionId exist. means existing cart exist.
                    {
                        string existingCartId = theCard.CartId;
                        List<CartDetails> existingCartDetail = cartexists;
                        foreach (CartDetails c in existingCartDetail)
                        {
                            context.CartDetails.Remove(c);
                        }
                        context.Cart.Remove(theCard); //remove current cart
                        context.SaveChanges();
                    }
                    //generate new cart with details

                    List<CartDetails> cd = CartManager.JsonStringToList(cartContent);
                    CartManager.saveCart(context, cd, sessionname);

                    List<CartDetails> cd1 = context.CartDetails.Where(x => x.Cart.CustomerId == sessionname).ToList();
                    
                    
                    ViewData["currentShoppingCart"] = cd1;
                    ViewData["cartContent"] = CartManager.ListToDictionary(context, cd1);

                    ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
                }
            }

            //when sessionId is null(user not logged in)
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

        //This will add item to cart from HomePage/Product Details Page/In cart
        public IActionResult AddToCart(int productId,int quantity, string buynow)
        {
            //retrieve the cartId from DB;
            var sessionname = HttpContext.Session.GetString("sessionId");
            string cartContent = HttpContext.Session.GetString("cartContent");

            //if sessionId Exists
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

                //Add the product in the cart
                CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId;
                var itemInCart = context.CartDetails.FirstOrDefault(c => c.CartId == CurrentCartId && c.ProductId == productId);

                //if the product is a new product
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
                //else if the product already exist in the current cart
                else
                {
                    itemInCart.Quantity += quantity;
                    HttpContext.Session.SetInt32("cartCount", itemInCart.Quantity);
                }

                context.SaveChanges();
            } 
            ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");
            
            if(buynow=="yes")
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                //redirect to the current page of the product that the user is viewing
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

        }

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

        //if user is not logged in, and he update the quantity in the cart from gallery
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

        //if user is not logged in, and he update the quantity in the cart from cart
        public IActionResult UpdateCartFromCart([FromBody] ListCart listCart)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(listCart.ProductId));
            List<CartDetails> updatedCartContent = CartManager.editCart(cartContent, productAdded, listCart.Quantity);

            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            HttpContext.Session.SetInt32("cartCount", listCart.CartCount);

            //Debug.WriteLine(listCart.Quantity);
            return Json(new { isOkay = true });
        }


        // TODO: if user is not logged in, and he update the item from product details 
        //to refer to productdetailscontroller: CartFromDetail


        //HG changes here
        public IActionResult DeleteItem(string id, int qty)
        {
            string cartContent = HttpContext.Session.GetString("cartContent");
            int cartCount = HttpContext.Session.GetInt32("cartCount") ?? 0;
            ProductList productAdded = context.ProductList.FirstOrDefault(x => x.ProductId == int.Parse(id));
            List<CartDetails> updatedCartContent = CartManager.editCart(cartContent, productAdded, 0);
            cartCount = cartCount - qty;
            HttpContext.Session.SetInt32("cartCount", cartCount);
            HttpContext.Session.SetString("cartContent", CartManager.ListToJsonString(updatedCartContent));
            return RedirectToAction("Index");
        }
    }
}
    