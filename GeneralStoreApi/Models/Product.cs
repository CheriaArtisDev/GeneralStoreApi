using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreApi.Models
{
    public class Product
    {
        // SKU
        [Key]
        public string SKU { get; set; }

        // Name
        [Required]
        public string Name { get; set; }

        // Price
        [Required]
        public decimal Price { get; set; }

        // Description
        public string Description { get; set; }

        // Number in Stock
        [Required]
        public int NumberInStock { get; set; }

        // In Stock
        public bool IsInStock 
        { 
            get 
            {
                return NumberInStock > 0;
            } 
        }
    }
}