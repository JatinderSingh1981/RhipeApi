using Microsoft.Extensions.Options;
using System;

namespace RhipeApi.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public int? MaximumQuantity { get; set; }

    }
}
