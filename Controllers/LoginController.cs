using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;
using T11ASP.NetProject.Util;

namespace T11ASP.NetProject.Controllers
{
    public class LoginController : Controller
    {

        private readonly AppDbContext context;

        public LoginController(AppDbContext context)
        {
            this.context = context;
        }

        //Get request to Login Page
        public IActionResult Index()
        {
            string usernameInSession = HttpContext.Session.GetString("sessionId");
            if (usernameInSession!=null) 
            {
                return RedirectToAction("index","home");
            }
            ViewData["numberofproductsincart"] = HttpContext.Session.GetInt32("cartCount");

            return View();
        }
        
        //Authenticate User
        public IActionResult Authenticate(string username, string password)
        {
            var customer = context.Customer.Where(e => e.CustomerId == username && e.Password == password).FirstOrDefault();

            //if customer dont exist, error.
            if (customer==null)
            {
                ViewData["username"] = username;
                ViewData["errMsg"] = "no such user or incorrect password.";

                return View("index");
            }
            
            //if customer exist, set sessionID to customerId
            HttpContext.Session.SetString("sessionId", customer.CustomerId);
            ViewData["sessionId"] = customer.CustomerId;

            //this part will combine the existing cart in DB with the sessiondata cart
            string isthereanyitemasguest = HttpContext.Session.GetString("cartContent");
            if(isthereanyitemasguest!=null)
            {
                List<CartDetails> cd = CartManager.JsonStringToList(isthereanyitemasguest);
                foreach(var item in cd)
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
                        var CurrentCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId;
                        var itemInCart = context.CartDetails.FirstOrDefault(c => c.CartId == CurrentCartId && c.ProductId == item.ProductId);

                        //if product does not exists in cart
                        if (itemInCart == null)
                        {
                            var newproductincart = new CartDetails
                            {
                                CartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionname).CartId.ToString(),
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            };
                            context.CartDetails.Add(newproductincart);

                        }
                        //Product exists in cart, to update cartdetails quantity
                        else
                        {
                            itemInCart.Quantity += item.Quantity;
                            HttpContext.Session.SetInt32("cartCount", itemInCart.Quantity);
                        }

                        context.SaveChanges();
                    }

                }
            }
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("sessionId", customer.CustomerId);
 

            //go to cart after user logs in
            return RedirectToAction("index", "Cart");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username,string password,string address,string name)
        {
            var usernameexist = context.Customer.Find(username);
            if(usernameexist!=null)
            {
                ViewData["username"] = username;
                ViewData["errMsg"] = "You are already a registered user. please try another username";
                return View();

            }
            if (username==null|| password==null||usernameexist!=null)
            {
                ViewData["username"]=username;
                ViewData["errMsg"] = "failed to register for an account. please type username and password again.";
                return View();
            }
            var CreateCustomer = new Customer
            {
                CustomerId = username,
                Address = address,
                Name = name,
                Password = password
            };
            context.Customer.Add(CreateCustomer);
            context.SaveChanges();

            HttpContext.Session.SetString("sessionId", CreateCustomer.CustomerId);
            ViewData["userLoggedIn"] = CreateCustomer.CustomerId;


            return RedirectToAction("index", "home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "login");
        }
        
    }
}
