using Microsoft.AspNetCore.Mvc;
using UpdateUser.Models;
using UpdateUser.Services;

namespace UpdateUser.Controllers;

[ApiController]
[Route("users/me")]
public class UserMeController : ControllerBase
{

  private readonly IUserService userService;

  public UserMeController(IUserService userService)
  {
    this.userService = userService;
  }

  [HttpPatch]
  public async Task<IActionResult> PatchDisplayNameAndTag([FromBody] PatchDisplayNameAndTagRequest request)
  {
    return Ok();
  }
}