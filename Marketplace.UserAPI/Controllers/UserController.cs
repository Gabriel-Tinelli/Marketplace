using Marketplace.UserAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.UserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IEnumerable<User> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new User
            {
                Email = $"email{index}",
                Role = ("teste"),
                Username = $"username{index}",
                CreatedAt = DateTime.Now,
                PasswordHash = "password",
                UserId = index
            })
            .ToArray();
    }
}