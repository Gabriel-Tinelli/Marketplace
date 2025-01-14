using Marketplace.ProductsAPI.Clients;
using Marketplace.ProductsAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Models;



namespace Marketplace.ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MarketplaceContextProduct _context;
        private readonly UserClient _userClient;

        public ProductsController(MarketplaceContextProduct context, UserClient userClient)
        {
            _context = context;
            _userClient = userClient;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Produto não pode ser nulo.");
            }

            // Valida se a categoria existe
            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest("Categoria inválida.");
            }

            // Verifica se o user_id existe na API de Users
            bool userExists = await _userClient.UserExists(1);
            if (!userExists)
            {
                return BadRequest("O user_id fornecido não existe.");
            }

            // Se o user_id for válido, continue com a criação do produto
            _context.Products.Add(new Product(product));
            await _context.SaveChangesAsync();

            return Ok("Produto criado com sucesso.");
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            // Verificar se o UserId existe no banco de dados de usuários
            var userExists = await _userClient.UserExists(1);
            if (!userExists)
            {
                return BadRequest("Usuário inválido.");
            }

            // Verificar se a CategoryId existe no banco de dados de categorias
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == product.CategoryId);
            if (!categoryExists)
            {
                return BadRequest("Categoria inválida.");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        
    }
}
