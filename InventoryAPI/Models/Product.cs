using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class Product
    {
        [Key]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
