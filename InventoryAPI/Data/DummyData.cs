using InventoryAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Data
{
    //A Dummy Data file for Inventory data 

    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ProductContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any products
                if (context.Products != null && context.Products.Any())
                    return;   // DB has already been seeded

                var products = GetProducts().ToArray();
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>() {
                new Product { ProductID = "8ed0e6f7", ProductName= "Dog Food", ProductPrice=12.99},
                 new Product { ProductID = "c0258525", ProductName= "Cat Food", ProductPrice=10.49},
                  new Product { ProductID = "0a207870", ProductName= "Dog Leash", ProductPrice= 5.99}
            };
            return products;
        }

      
    }
}
  
