using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Models;
using Marketplace.UserAPI.Models; // Para acessar User
using CategoryService.Models; // Para acessar Category
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CategoryService.Data;
using Marketplace.Data;


namespace Marketplace.ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MarketplaceContextProduct _context;
        private readonly HttpClient _httpClient;

        public ProductsController(MarketplaceContextProduct context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
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
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Produto não pode ser nulo.");
            }

            // Valida se a categoria existe
            var category = await _context.Categories.FindAsync(product.CategoryID);
            if (category == null)
            {
                return BadRequest("Categoria inválida.");
            }

            // Verifica se o user_id existe na API de Users
            bool userExists = await UserExists(product.UserID);
            if (!userExists)
            {
                return BadRequest("O user_id fornecido não existe.");
            }

            // Se o user_id for válido, continue com a criação do produto
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Produto criado com sucesso.");
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            // Verificar se o UserID existe no banco de dados de usuários
            var userExists = await UserExists(product.UserID);
            if (!userExists)
            {
                return BadRequest("Usuário inválido.");
            }

            // Verificar se a CategoryID existe no banco de dados de categorias
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == product.CategoryID);
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
            return _context.Products.Any(e => e.ProductID == id);
        }

        public async Task<bool> UserExists(int userId)
        {
            // Verifica se o user_id existe na API de Users
            var response = await _httpClient.GetAsync($"http://localhost:5108/user/{userId}");
    
            if (response.IsSuccessStatusCode)
            {
                // Se a resposta for bem-sucedida (status 200 OK), o user_id existe
                return true;
            }

            // Se a resposta não for bem-sucedida, o user_id não existe
            return false;
        }
    }
}
