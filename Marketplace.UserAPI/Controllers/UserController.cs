using Marketplace.Data;
using Marketplace.UserAPI.Models;
using Marketplace.UserAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Marketplace.UserAPI.Services;

namespace Marketplace.UserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MarketplaceContextUser _context;
    private readonly AuthenticationTokenService _tokenService;
    private readonly IConfiguration _configuration;

    public UserController(MarketplaceContextUser context, AuthenticationTokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ValidateUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
    
        if (user == null)
        {
            return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
        }

        return Ok(new { message = "Usuário encontrado.", userId = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        // Validar se os dados necessários estão presentes
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Role))
        {
            return BadRequest("Campos obrigatórios estão incompletos.");
        }

        // Gerar o hash da senha usando BCrypt, sem passar o salt
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, user);
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

        // Re-hash da senha (caso tenha sido alterada)
        if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        // Validar o usuário
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized("Credenciais inválidas");
        }

        // Gerar o token
        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { token = token });
    }
}