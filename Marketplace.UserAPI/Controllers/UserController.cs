using Marketplace.Data;
using Marketplace.UserAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.UserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MarketplaceContextUser _context;

    public UserController(MarketplaceContextUser context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpPost]

    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        // Validar se os dados necessários estão presentes
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Role))
        {
            return BadRequest("Campos obrigatórios estão incompletos.");
        }
        
        // Criar objeto para realizar o hash
        var passwordHasher = new PasswordHasher<User>();
        
        // Gerar o hash da senha
        user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.UserId}, user);
    }

    [HttpPut]

    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Role = updatedUser.Role;
        
        //Re-hash da senha

        if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
        {
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, updatedUser.PasswordHash);
        }
        
        await _context.SaveChangesAsync();
        return NoContent();
        
    }

    [HttpDelete]

    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}