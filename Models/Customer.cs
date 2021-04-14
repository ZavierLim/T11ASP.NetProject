using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        
        public string SessionId { get; set; }
        public virtual List<Orders> Orders { get; set; } //reference navigation property

        public virtual List<Cart> Cart { get; set; }


    }
}
