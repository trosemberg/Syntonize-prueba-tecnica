using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using TechTest.DTO;
using TechTest.Services.Interface;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) 
        {
            _productsService = productsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productsService.GetAllAsync();
            if (products.Any())
                return Ok(products);

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutProductsAsync(int id, [FromBody] ProductsDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!product.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            if (id != product.Id)
            {
                return BadRequest("Body Id and Request Id are different.");
            }

            var check = await _productsService.UpdateAsync(product);

            if (check == null)
                return BadRequest("Failed to update");

            return Created();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostProducts(ProductsDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!product.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            await _productsService.InsertAsync(product);

            return Created();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var users = await _productsService.DeleteAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}