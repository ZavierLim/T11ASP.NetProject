using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class LoginController : Controller
    {
        //private readonly SQLCustomerInfo customerinfo;
        private readonly AppDbContext context;

        public LoginController(AppDbContext context)
        {
            //this.customerinfo = customerinfo;
            this.context = context;
        }
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
            
            //if customer exist, set sessionID to his name
            HttpContext.Session.SetString("sessionId", customer.CustomerId);
            ViewData["userLoggedIn"] = customer.CustomerId;
            return RedirectToAction("index", "home");
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
            if(username==null|| password==null||usernameexist!=null)
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
