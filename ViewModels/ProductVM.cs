using RhipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhipeApi.ViewModels
{
    public class ProductVM : IProductVM
    {
        public IEnumerable<Product> Products { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
