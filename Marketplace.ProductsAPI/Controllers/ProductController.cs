using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MarketplaceContextProduct _context;

        public ProductsController(MarketplaceContextProduct context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID}, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.ProductName = updatedProduct.ProductName;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;
            
            await _context.SaveChangesAsync();
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
            
        }
    }
}