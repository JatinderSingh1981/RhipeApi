using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RhipeApi.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RhipeApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/{controller}")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductBL _productBL;

        public ProductController(IProductBL productBL, ILogger<ProductController> logger)
        {
            _productBL = productBL;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productBL.GetProducts().ConfigureAwait(false);
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
