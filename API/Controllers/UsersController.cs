using Microsoft.AspNetCore.Mvc;

using CryptoApp.Data;
using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
   
        _userService = userService;
    }

    [HttpGet(Name = "GetAllUsers")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        try {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public IActionResult GetUser(string userId)
    {
        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException)
        {
           return NotFound("No encontrado el usuario " + userId);
        }
    }

    [HttpPost]
    public IActionResult RegisterUser([FromBody] UserCreateDTO userCreateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var user = _userService.RegisterUser(userCreateDTO);
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(string userId, UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            _userService.UpdateUser(userId, userUpdateDTO);
            return NoContent();
        }     
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(string userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
