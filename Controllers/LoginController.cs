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
            var sessionindb = context.Customer.FirstOrDefault(x => x.SessionId == usernameInSession);
            if (sessionindb!=null)
            {
                return RedirectToAction("index","home");
            }
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
            //if customer exist, but no sessionId
            if (customer.SessionId == null)
            {
                customer.SessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("sessionId", customer.SessionId);
            }
            //customer exist, got sessionID
            HttpContext.Session.SetString("sessionId", customer.SessionId);
            ViewData["userLoggedIn"] = customer.CustomerId;
            return RedirectToAction("index", "home");
        }

        
    }
}
