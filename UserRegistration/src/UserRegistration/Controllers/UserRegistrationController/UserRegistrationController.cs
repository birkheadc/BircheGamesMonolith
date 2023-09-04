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
  public async Task<ActionResult<UserResponseDTO>> PostNewUser([FromBody] UserCreateRequestDTO newUser)
  {
    try
    {
      ActionResult<UserResponseDTO> result = await userService.CreateNewUser(newUser);
      return result;
    }
    catch
    {
      return StatusCode(9001);
    }
  }
}