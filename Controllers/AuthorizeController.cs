using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace LiteDBApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthorizeController : ControllerBase
	{
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			// Authenticate the user (you may use UserManager or your custom user store)
			var user = await userManager.FindByNameAsync(model.Username);
			if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
			{
				return Unauthorized();
			}

			// Create the JWT token
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Secret"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
		new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
		new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
	};

			var token = new JwtSecurityToken(
				issuer: Configuration["JwtSettings:Issuer"],
				audience: Configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(30), // Adjust the token expiration as needed
				signingCredentials: credentials
			);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token),
				expiration = token.ValidTo
			});
		}

	}
}
