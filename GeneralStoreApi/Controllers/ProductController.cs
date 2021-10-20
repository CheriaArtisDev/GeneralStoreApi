using GeneralStoreApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreApi.Controllers
{
    public class ProductController : ApiController
    {
        // Repo patter in API
        // Where we store the data
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //CRUD
        // PGPD

        [HttpPost]
        // Create - Post
        public async Task<IHttpActionResult> PostProduct(Product model)
        {
            if (model == null)
            {
                return BadRequest("Please enter information.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{model.Name} was added to the database.");
            }
            return InternalServerError();
        }

        // Read - Get
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        [Route("api/Product/GetProductBySKU/{sKU}")]
        public async Task<IHttpActionResult> GetProductBySKU(string sKU)
        {
            Product product = await _context.Products.FindAsync(sKU);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Update - Put
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProductBySKU([FromUri] string sKU, [FromBody]Product model)
        {
            if(model == null)
            {
                return BadRequest("Please enter the required information.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = await _context.Products.FindAsync(sKU);

            if(product == null)
            {
                return NotFound();
            }

            // do not update key values because it can mess up
            product.Name = model.Name;
            product.NumberInStock = model.NumberInStock;
            product.Price = model.Price;
            product.Description = model.Description;

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // Delete
        public async Task<IHttpActionResult> DeleteProductBySKU([FromUri]string sKU)
        {
            Product product = await _context.Products.FindAsync(sKU);

            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            
            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{product.Name} was deleted.");
            }
            return InternalServerError();
        }
    }
}
