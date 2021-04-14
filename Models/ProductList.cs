using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class ProductList
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public string ImgUrl { get; set; }
        public string ShortDescription { get; set; }
        public double Rating { get; set; }

        public int MaxStock { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
