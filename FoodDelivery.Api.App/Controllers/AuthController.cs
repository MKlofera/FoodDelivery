using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Common.Models.Models.Auth;
using FoodDelivery.Common.Models.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Api.App.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly FoodDeliveryDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(FoodDeliveryDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("token-jwt")]
    public ActionResult GetToken([FromBody] UserCredentialsModel credentials)
    {
        var user = _context.Users.FirstOrDefault(opt => 
            opt.UserName == credentials.Login && opt.PasswordHash == credentials.Password);

        if (user == null)
            return BadRequest();
        var jwtSymmetricKey = _configuration["JWT_SymmetricKey"];
        var key = Encoding.ASCII.GetBytes(jwtSymmetricKey
                                          ?? throw new ArgumentException("Symmetric key for JWT is missing"));

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor();
        descriptor.Expires = DateTime.Now.AddHours(1);
        descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        descriptor.Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "user"),
        });

        var token = tokenHandler.CreateToken(descriptor);
        string accessToken = tokenHandler.WriteToken(token);

        return Ok(new AuthTokenModel()
        {
            AccessToken = accessToken,
            ExpiresIn = 3600,
            TokenType = "Bearer",
            Scope = "xyz"
        });
    }
}