using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using auth.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace auth.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly string _key;
    private readonly IConfiguration _config;
    private readonly AuthContext _context;
    public AuthController(IConfiguration config, AuthContext context)
    {
      _config = config;
      _context = context;
      _key = config["key"];
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return Ok(user);
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
      if(!_context.Users.Any(u => u.Name == user.Name && u.Password == user.Password))
      {
        return Unauthorized();
      }
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenKey = Encoding.ASCII.GetBytes(_key);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Name)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(tokenKey),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return Ok(new {token=tokenHandler.WriteToken(token)});
    }
  }
}
