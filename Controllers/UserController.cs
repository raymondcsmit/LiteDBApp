using LiteDBApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiteDBApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly UserService _userService;

	public UserController(UserService userService)
	{
		_userService = userService;
	}

	[HttpGet("{id}")]
	public IActionResult GetById(int id)
	{
		var user = _userService.GetUserById(id);
		if (user == null)
		{
			return NotFound();
		}
		return Ok(user);
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var users = _userService.GetAllUsers();
		return Ok(users);
	}

	[HttpPost]
	public IActionResult Insert([FromBody] User user)
	{
		if (user == null)
		{
			return BadRequest();
		}

		_userService.CreateUser(user);
		return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] User user)
	{
		if (user == null || id != user.Id)
		{
			return BadRequest();
		}

		var existingUser = _userService.GetUserById(id);
		if (existingUser == null)
		{
			return NotFound();
		}

		_userService.UpdateUser(user);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var existingUser = _userService.GetUserById(id);
		if (existingUser == null)
		{
			return NotFound();
		}

		_userService.DeleteUser(id);
		return NoContent();
	}

}
