using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using UserRegistration.Services;

namespace UserRegistration.Controllers;

[ApiController]
[Route("register")]
public class UserRegistrationController : ControllerBase
{
  private readonly IUserService userService;

  public UserRegistrationController(IUserService userService)
  {
    this.userService = userService;
  }

  [HttpPost]
  public async Task<IActionResult> PostNewUser([FromBody] CreateUserRequestDTO newUser)
  {
    try
    {
      CreateUserResponse response = await userService.CreateNewUser(newUser);
      if (response.WasSuccess)
      {
        return Ok();
      }
      return BadRequest(response);
    }
    catch
    {
      return BadRequest("Something terrible and unforeseen has happened");
    }
  }
}