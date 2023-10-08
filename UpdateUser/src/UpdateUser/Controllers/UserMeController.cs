using System.Security.Claims;
using Amazon.DynamoDBv2.Model;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<UserResponseDTO>> GetUser()
  {
    string? id = GetCurrentUserId();
    if (id is null) return Unauthorized();
    UserResponseDTO? user = await userService.GetUser(id);
    if (user is null) return Unauthorized();
    return Ok(user);
  }

  [HttpPatch]
  [Authorize]
  public async Task<IActionResult> PatchDisplayNameAndTag([FromBody] PatchDisplayNameAndTagRequest request)
  {
    return Ok($"Change user to: {request.DisplayName}#{request.Tag}");
  }

  private string? GetCurrentUserId()
  {
    return HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
  }
}