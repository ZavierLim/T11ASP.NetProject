using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductList ProductList { get; set; }
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        //ProductListProductId = table.Column<int>(type: "int", nullable: true),
        //ordersOrderId = table.Column<int>(type: "int", nullable: true)
    }
}
