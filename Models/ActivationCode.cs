using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class ActivationCode
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("OrderId,ProductId")]
        public virtual OrderDetails OrderDetails { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductList ProductList { get; set; }

        public string ActivationKey { get; set; }

    }
}
