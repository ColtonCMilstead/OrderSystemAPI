using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystemAPI.Models
{
    public class FullOrder
    {
        [Key]
        public Guid OrderID { get; set; }
        public OrderEntry orderEntry { get; set; }
        public List<Products> Products { get; set; }
        public double Total
        {
            get
            {   //Iterates through list of Order Product Prices & Quantities and calculates the Total 
                double TotalCost = 0;
                for (var i = 0; i < Products.Count; i++)
                {

                    TotalCost += (Products[i].ProductPrice * orderEntry.Items[i].Quantity);
                }

                return TotalCost;
            }
        }
    }

    public class Products
    {
        [Key]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
