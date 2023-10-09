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

  private readonly IUserService _userService;
  private readonly string _currentUserId;

  public UserMeController(IUserService userService)
  {
    _userService = userService;
    _currentUserId = GetCurrentUserId();
  }

  private string GetCurrentUserId()
  {
    string? id = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
    if (id is null)
    {
      throw new InternalServerErrorException("Error while attempting to get current user ID. NameIdentifier Claim was not found.");
    }
    return id;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<UserResponseDTO>> GetUser()
  {
    UserResponseDTO? user = await _userService.GetUser(_currentUserId);
    if (user is null) return NotFound();
    return Ok(user);
  }

  [HttpPatch]
  [Authorize]
  public async Task<IActionResult> PatchDisplayNameAndTag([FromBody] PatchDisplayNameAndTagRequest request)
  {
    bool wasSuccess = await _userService.PatchUserDisplayNameAndTag(_currentUserId, request);
    return wasSuccess ? Ok() : BadRequest();
  }
}