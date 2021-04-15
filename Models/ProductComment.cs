using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class ProductComment
    {
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
        public string OrderId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductList Product { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("OrderId")]
        public virtual Orders Order { get; set; }

        public double Rating { get; set; }
        public string Comment { get; set; }


    }
}
