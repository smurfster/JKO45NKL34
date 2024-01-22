using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateProductRequestModel product)
        {
            var result = await productService.CreateProduct(product);
            return CreatedAtAction("/", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
