using RhipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhipeApi.ViewModels
{
    public interface IProductVM
    {
        IEnumerable<Product> Products { get; set; }
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
