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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction(Transaction model)
        {
            // Error checking
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.DateOfTransaction = DateTime.Now;

            Product product = await _context.Products.FindAsync(model.ProductSKU);

            // Null check

            // Verify in Stock
            if(!product.IsInStock)
            {
                return BadRequest("Product is out of stock.");
            }

            // Enough Product for the Transaction
            if(product.NumberInStock < model.ItemCount)
            {
                return BadRequest("Not enough in stock.");
            }

            // If transaction is made, deduct from stock
            product.NumberInStock -= model.ItemCount;

            _context.Transactions.Add(model);
            int example = await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        // Get by Id
        [HttpGet]
        [Route("api/Transaction/GetTransactionByCustomerId/{id}")]
        public async Task<IHttpActionResult> GetTransactionById(int customerId)
        {
            // CustomerId acutally exists
           

            List<Transaction> transactionsToReturn = new List<Transaction>();
            List<Transaction> transactionsInDatabase = await _context.Transactions.ToListAsync();

            /* foreach(var transaction in transactionsInDatabase)
             {
                 if(customerId == transaction.CustomerId)
                 {
                     transactionsToReturn.Add(transaction);
                 }
             }*/

            // Linq query                               // t is a variable representing the transaction variable        // a Linq query is not a collection and must be                                                                                                                 converted to a list
            transactionsToReturn = transactionsInDatabase.Where(t => t.CustomerId == customerId).ToList();

            return Ok(transactionsToReturn);
        }
        // Get by CustomerId



        // Update
        // If you update the number you need to adjust stock
        // Do you want to be able to change product associated with transaction


        // Delete


        // Re-add the product
        
    }
}
