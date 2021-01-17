using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RhipeApi.Infrastructure;
using RhipeApi.Service.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RhipeApi.Service
{
    public class ProductService : IProductService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly ILogger<ProductService> _logger;
        private readonly string _productUrl;

        public ProductService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<ProductService> logger)
        {
            _apiClient = httpClient;
            _settings = settings;
            _logger = logger;

            _productUrl = $"{_settings.Value.ProductUrl}";
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {

            var uri = API.ProductAPI.Get(_productUrl);
            _logger.LogDebug("[GetProducts] -> Calling {Uri} to get the list of products", uri);
            var response = await _apiClient.GetAsync(uri);
            _logger.LogDebug("[GetProducts] -> response code {StatusCode}", response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(responseString) ?
                null :
                JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(responseString);


        }
    }
}
