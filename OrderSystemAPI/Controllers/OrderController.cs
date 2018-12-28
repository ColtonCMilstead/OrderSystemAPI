using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystemAPI.Data;
using OrderSystemAPI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace OrderSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OrderPolicy")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<FullOrder> GetFullOrders()
        {
            return _context.FullOrders
                .Include(x=>x.orderEntry)
                .Include(x=>x.orderEntry.Items)
                .Include(x=>x.Products);
        }

        // GET: api/Order/{OrderID}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFullOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ordersum = await _context.FullOrders
                .Include(i => i.orderEntry)
                .Include(i => i.orderEntry.Items)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(i => i.OrderID == id);

            if (ordersum == null)
            {
                return NotFound();
            }

            return Ok(ordersum);
        }

        // GET: api/Order/{CustomerID}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFullOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ordersum = await _context.FullOrders
                .Include(i => i.orderEntry)
                .Include(i => i.orderEntry.Items)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(i => i.orderEntry.CustomerID == id);

            if (ordersum == null)
            {
                return NotFound();
            }

            return Ok(ordersum);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderEntry([FromRoute] Guid id, [FromBody] FullOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostOrderEntry([FromBody] OrderEntry orderEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderEntries.Add(orderEntry);
            await _context.SaveChangesAsync();

            //Make the HTTP Client Call to Products API for product info, Add to Full Order DB
            //Get Full list of Products, URL: https://localhost:44350/api/products
            //Get Specific Product, URL: https://localhost:44350/api/products/id


            //List of Products to be added to FullOrder
            List<Products> products = new List<Products>();

          
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

                //Setting up the response... 

                //iterate through list of Product IDs, adding the products to Products List
                foreach (var id in orderEntry.Items)
                {
                    string baseUrl = $"https://localhost:44350/api/products/{id.ProductID}";
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    using (HttpContent content = res.Content)
                    {
                        Products data = await content.ReadAsAsync<Products>();
                        if (data != null)
                        {
                            products.Add(data);
                        }
                    }
                }

            //FullOrder object
            FullOrder fullOrder = new FullOrder
            {
                OrderID = Guid.NewGuid(),
                orderEntry = orderEntry,
                Products = products
            };

            //Add Fullorder object to FullOrder DB
            _context.FullOrders.Add(fullOrder);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetOrderEntry", new { id = orderEntry.CustomerID }, fullOrder);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ordersum = await _context.FullOrders.FindAsync(id);
            if (ordersum == null)
            {
                return NotFound();
            }

           
            _context.FullOrders.Remove(ordersum);
            await _context.SaveChangesAsync();

            return Ok(ordersum);
        }

        private bool OrderExists(Guid id)
        {
            return _context.FullOrders.Any(e => e.OrderID == id);
        }
    }
}