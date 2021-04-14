using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class CartDetails
    {
        public string CartId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductList ProductList { get; set; }

        public int Quantity { get; set; }
    }
}