using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Models;
using Marketplace.UserAPI.Models; // Para acessar User
using CategoryService.Models; // Para acessar Category
using System.Collections.Generic;
using System.Linq;
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
        private readonly MarketplaceContextUser _usercontext;

        public ProductsController(MarketplaceContextProduct context, MarketplaceContextUser usercontext)
        {
            _context = context;
            _usercontext = usercontext;
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

            // Valida se o usuário existe (supondo que você tem acesso ao microserviço de usuários via HTTP)
            var user = await _usercontext.Users.FindAsync(product.UserID);
            if (user == null)
            {
                return BadRequest("Usuário inválido.");
            }

            // Adiciona o produto ao banco de dados
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
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

        // Método para verificar se o UserID existe no banco de dados de usuários (simulação)
        private async Task<bool> UserExists(int userId)
        {
            // Simulando a verificação no banco de dados de usuários (você precisa fazer essa consulta em um banco de dados separado, usando uma API ou algo similar)
            // Aqui está um exemplo de como a validação pode ser feita se você estiver acessando um banco de dados de usuários remoto.

            // Este exemplo assume que você tem uma conexão separada com o banco de dados de usuários
            // Neste caso, estamos simulando como se você tivesse uma consulta que fosse até o banco de dados de usuários:
            var userExists = false;

            // Aqui seria o código real para buscar o usuário no banco de dados de usuários, usando API ou consulta direta

            // Exemplo fictício de consulta de usuários:
            // var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            // userExists = user != null;

            // Simulando o retorno (faça a validação com a real implementação)
            return userExists;
        }
    }
}
