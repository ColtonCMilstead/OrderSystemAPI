using Microsoft.EntityFrameworkCore;
using OrderSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystemAPI.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<FullOrder> FullOrders { get; set; }
    }
}

