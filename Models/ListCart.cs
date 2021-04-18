using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//HG Changes
namespace T11ASP.NetProject.Models
{
    public class ListCart
    {
        public string ProductId { get; set; }
        public int CartCount { get; set; }

        public int Quantity { get; set; }
    }
}
