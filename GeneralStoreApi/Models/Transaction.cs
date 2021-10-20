using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreApi.Models
{
    public class Transaction
    {
        // Unique Id
        [Key]
        public int Id { get; set; }

        // Connection to customer
        // Foreign Key
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Connection to product
        // Foreign Key
        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }
        public virtual Product Product { get; set; }

        // Item Count
        [Required]
        public int ItemCount { get; set; }

        // Date of Transaction
        public DateTime DateOfTransaction { get; set; }
    }
}