using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T11ASP.NetProject.Models;

//HG changes here
namespace T11ASP.NetProject.Util
{
    public class CartManager
    {
        static public List<CartDetails> JsonStringToList(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<CartDetails>>(jsonString);
        }
        static public string ListToJsonString(List<CartDetails> cartDetail)
        {
            return JsonConvert.SerializeObject(cartDetail);
        }

        static public Dictionary<ProductList, int> ListToDictionary(AppDbContext context, List<CartDetails> cartDetail)
        {
            Dictionary<ProductList, int> dict = new Dictionary<ProductList, int>();
            foreach (CartDetails cd in cartDetail)
            {
                ProductList prod = context.ProductList.FirstOrDefault(x => x.ProductId == cd.ProductId);
                dict.Add(prod, cd.Quantity);
            }
            return dict;
        }

        static public List<CartDetails> updateCart(string cartDetailString, ProductList prod, int qty)
        {
            bool existedItem = false;
            List<CartDetails> existingCartContent = new List<CartDetails>();

            if (cartDetailString != null)
            {
                existingCartContent = CartManager.JsonStringToList(cartDetailString);
                foreach (CartDetails cd in existingCartContent)
                {
                    if (cd.ProductId == prod.ProductId)
                    {
                        cd.Quantity = cd.Quantity + qty;
                        existedItem = true;
                        break;
                    }
                }
            }
            if (!existedItem)
            {
                existingCartContent.Add(new CartDetails() { ProductId = prod.ProductId, Quantity = qty });
            }

            return existingCartContent;
        }

        static public List<CartDetails> editCart(string cartDetailString, ProductList prod, int qty)
        {
            List<CartDetails> existingCartContent = new List<CartDetails>();
            existingCartContent = CartManager.JsonStringToList(cartDetailString);

            if (qty != 0)
            {
                foreach (CartDetails cd in existingCartContent)
                {
                    if (cd.ProductId == prod.ProductId)
                    {
                        cd.Quantity = qty;
                        break;
                    }
                }
            }
            else
            {
                CartDetails deletedItem = existingCartContent.FirstOrDefault(x => x.ProductId == prod.ProductId);
                existingCartContent.Remove(deletedItem);
            }

            return existingCartContent;
        }

        static public bool duplicateItem(string cartDetailString, ProductList prod)
        {
            bool existedItem = false;
            List<CartDetails> existingCartContent = new List<CartDetails>();

            if (cartDetailString != null)
            {
                existingCartContent = CartManager.JsonStringToList(cartDetailString);
                foreach (CartDetails cd in existingCartContent)
                {
                    if (cd.ProductId == prod.ProductId)
                    {
                        existedItem = true;
                        break;
                    }
                }
            }

            return existedItem;
        }

        static public void saveCart(AppDbContext context, List<CartDetails> cartList, string sessionId)
        {
            Cart newCart = new Cart()
            {
                CartId = Guid.NewGuid().ToString(),
                CustomerId = sessionId,
            };
            context.Cart.Add(newCart);
            context.SaveChanges();

            string newCartId = context.Cart.FirstOrDefault(x => x.CustomerId == sessionId).CartId;

            foreach (CartDetails cd in cartList)
            {
                CartDetails newCd = new CartDetails()
                {
                    CartId = newCartId,
                    ProductId = cd.ProductId,
                    Quantity = cd.Quantity
                };
                context.CartDetails.Add(newCd);
            }
            context.SaveChanges();
        }
    }
}
