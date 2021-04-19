using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

namespace T11ASP.NetProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext context;
        public CheckoutController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var currentsession = HttpContext.Session.GetString("sessionId");
            ViewData["session"] = currentsession;
            if (currentsession != null)
            {
                var currentcustomer = context.Customer.FirstOrDefault(x => x.CustomerId == currentsession).CustomerId;
                ViewData["checkoutitems"] = context.CartDetails.Where(x => x.Cart.CustomerId == currentcustomer).ToList();

            }
            //when user is not logged in redirect to login page
            else
            {
                return RedirectToAction("Index","Login");
            }

            var cartexists = context.CartDetails.Where(x => x.Cart.CustomerId == HttpContext.Session.GetString("sessionId"));
            var numberofitems = cartexists.Count();
            if (numberofitems < 1)
            {
                ViewData["numberofproductsincart"] = null;
            }
            else
            {
                ViewData["numberofproductsincart"] = numberofitems;
            }

            return View();
        }

        //when user CLicks on "Make Payment" from the checkout page
        public IActionResult OrderConfirmed()
        {
            var currentsession = HttpContext.Session.GetString("sessionId");
            var currentcart = context.Cart.FirstOrDefault(x=>x.CustomerId==currentsession);

            //TODO: 1st Scenarioif customer not logged in -> add in logic to redirect to login. then save the sessiondata cart to DB with that customer ID


            //2nd scenario when the user is logged in: 
            if (currentsession!=null && currentcart!=null)
            {
                var currentcartId = currentcart.CartId;
                var allitemsincart = context.CartDetails.Where(x => x.CartId == currentcartId).ToList();
                Orders orderconfirmed = new Orders
                {
                    OrderId = Guid.NewGuid().ToString(),
                    DateofPurchase = DateTime.Now,
                    CustomerId = currentsession,
                };

                context.Orders.Add(orderconfirmed);
                context.SaveChanges();

                //for each item in the cart, add the item to orderdetails
                foreach (var cartitem in allitemsincart)
                {
                    OrderDetails orderitems = new OrderDetails
                    {
                        OrderId = orderconfirmed.OrderId,
                        ProductId = cartitem.ProductId,
                        Quantity = cartitem.Quantity,
                        UnitPrice = cartitem.ProductList.UnitPrice
                    };
                    context.OrderDetails.Add(orderitems);
                    context.SaveChanges();

                    //for each quantity in the item, generate the activation code
                    for (int i=0;i<cartitem.Quantity;i++)
                    {
                        ActivationCode ActivationCodePerItem = new ActivationCode
                        {
                            OrderId = orderconfirmed.OrderId,
                            ProductId = cartitem.ProductId,
                            ActivationKey = Guid.NewGuid().ToString()
                        };
                        context.ActivationCodes.Add(ActivationCodePerItem);
                        context.SaveChanges();
                    }
                }


                //2nd scenario: when user is logged in and clicks Make payment. The items in the cart will be deleted from DB (because already added in orders)
                var CartIdToDelete = context.Cart.Find(currentcartId);
                context.Cart.Remove(CartIdToDelete);
                context.SaveChanges();
                ViewData["orderId"] = orderconfirmed.OrderId;
            }

            //remove the sessiondata after the customer checks out;
            HttpContext.Session.Remove("cartContent");
            HttpContext.Session.SetInt32("cartCount", 0);
            return View();

        }
    }
}
