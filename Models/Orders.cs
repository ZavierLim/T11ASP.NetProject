using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime DateofPurchase{get;set;}

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; } //reference navigation property

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
