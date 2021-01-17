using AutoMapper;
using Microsoft.Extensions.Logging;
using RhipeApi.Models;
using RhipeApi.Service;
using RhipeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhipeApi.BL
{
    public class ProductBL : IProductBL
    {
        private readonly IProductService _productService;
        private readonly IProductVM _productVM;
        private readonly ILogger<ProductBL> _logger;
        private readonly IMapper _mapper;

        public ProductBL(IProductService productService, IProductVM productVM, IMapper mapper, ILogger<ProductBL> logger)
        {
            _productService = productService;
            _productVM = productVM;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductVM> GetProducts()
        {
            try
            {
                _productVM.IsSuccess = false;
                _productVM.Message = "Some error Occurred while fetching the data";
                var products = await _productService.GetProducts().ConfigureAwait(false);
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Product>>(products);
                    _productVM.Products = result;
                    _productVM.IsSuccess = true;
                    _productVM.Message = "Products Returned Successfully";
                    
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetProducts] -> {ErrorMessage}", ex.StackTrace);
                _productVM.Message = ex.Message;
            }
            _logger.LogDebug("[GetProducts] -> {Message}", _productVM.Message);
            return (ProductVM)_productVM;
        }
    }
       
}
