using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystemAPI.Models
{
    public class OrderEntry
    {
        [Key]
        public string CustomerID { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [Key]
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
