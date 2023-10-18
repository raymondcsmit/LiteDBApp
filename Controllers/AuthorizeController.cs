using LiteDBApp.IdentityStore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiteDBApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthorizeController : ControllerBase
	{
		private readonly UserManager<LiteDbUser> _userManager;
		private readonly SignInManager<LiteDbUser> _signInManager;
		private readonly RoleManager<LiteDbRole> _roleManager;
		private readonly IConfiguration _configuration;

		public AuthorizeController(
			UserManager<LiteDbUser> userManager,
			SignInManager<LiteDbUser> signInManager,
			RoleManager<LiteDbRole> roleManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(LiteDbUser model, string password)
		{
			var result = await _userManager.CreateAsync(model, password);
			if (result.Succeeded)
			{
				// Optionally, you could add roles here
				// await _userManager.AddToRoleAsync(model, "SomeRole");
				return Ok(new { message = "Registration successful" });
			}

			return BadRequest(result.Errors);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(string username, string password)
		{
			var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
			if (result.Succeeded)
			{
				var appUser = await _userManager.FindByNameAsync(username);
				return Ok(GenerateJwtToken(appUser));
			}

			return Unauthorized();
		}

		[HttpPost("AddRole")]
		public async Task<IActionResult> AddRole(string userId, string roleName)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				return NotFound("User not found");

			var result = await _userManager.AddToRoleAsync(user, roleName);
			if (result.Succeeded)
				return Ok(new { message = "Role added successfully" });

			return BadRequest(result.Errors);
		}

		private string GenerateJwtToken(LiteDbUser user)
		{
			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpireDays"]));

			var token = new JwtSecurityToken(
				_configuration["JwtSettings:Issuer"],
				_configuration["JwtSettings:Audience"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}

}
