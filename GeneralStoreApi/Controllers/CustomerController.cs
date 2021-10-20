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
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // CRUD
        // PGPD

        // Create - Post
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer model)
        {
            if (model == null)
            {
                return BadRequest("Please enter information.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{model.FirstName} {model.LastName} was added to the database.");
            }
            return InternalServerError();
        }

        // Read - Get
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        // Get by Id
        [HttpGet]
        [Route("api/Customer/GetCustomerById/{id}")]
        public async Task<IHttpActionResult> GetCustomerById(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // Update - Put
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerById([FromUri]int id, [FromBody]Customer model)
        {
            if(model == null)
            {
                return BadRequest("Please enter the required information.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = await _context.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{customer.FirstName} {customer.LastName} was deleted.");
            }
            return InternalServerError();
        }
    }
}
