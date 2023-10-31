using System.Security.Claims;
using Amazon.DynamoDBv2.Model;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpdateUser.Models;
using UpdateUser.Services;
using Domain.Authorization;

namespace UpdateUser.Controllers;

[ApiController]
[Authorize]
[Route("users/me")]
public class UserMeController : ControllerBase
{

  private readonly IUserService _userService;

  public UserMeController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  [RequiresEmailVerified]
  public async Task<ActionResult<UserResponseDTO>> GetUser()
  {
    UserResponseDTO? user = await _userService.GetUser(GetCurrentUserId());
    if (user is null) return NotFound();
    return Ok(user);
  }

  [HttpPatch]
  [Route("display-name")]
  public async Task<ActionResult<Response>> PatchDisplayNameAndTag([FromBody] PatchDisplayNameAndTagRequest request)
  {
    Response response = await _userService.PatchUserDisplayNameAndTag(GetCurrentUserId(), request);
    if (response.WasSuccess) return Ok();
    return Problem(response.Errors[0].Message, statusCode: response.Errors[0].StatusCode);
  }

  private string GetCurrentUserId()
  {
    string? id = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
    if (id is null)
    {
      // Authorize attribute should guaruntee NameIdentifier is present.
      // If id is null here, something has broken very badly.
      throw new InternalServerErrorException("Unable to find user id in user claims.");
    }
    return id;
  }

}