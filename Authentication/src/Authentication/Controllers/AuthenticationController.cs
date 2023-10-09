using System.Text;
using System.Text.Json;
using Authentication.Models;
using Authentication.Services;
using Domain.Authorization;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

[ApiController]
[Route("authentication")]
public class AuthenticationController : ControllerBase
{
  private readonly ISecurityTokenService securityTokenService;

  public AuthenticationController(ISecurityTokenService securityTokenService)
  {
    this.securityTokenService = securityTokenService;
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<ActionResult<SecurityTokenWrapper>> Authenticate()
  {
    Credentials? credentials = GetCredentialsFromHeaders(Request.Headers);
    if (credentials is null) return Unauthorized("Unable to retrieve credentials from Authorization header.");

    try
    {
      SecurityTokenWrapper? tokenWrapper = (await securityTokenService.AuthenticateUser(credentials)).Value;
      if (tokenWrapper is not null)
      {
        return Ok(tokenWrapper);
      }
      return Unauthorized();
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Error when attempting to Authenticate user in controller.");
      Console.WriteLine(e);
      return StatusCode(500);
    }
  }

  private static Credentials? GetCredentialsFromHeaders(IHeaderDictionary headers)
  {
    try
    {
      string auth = headers.Authorization;
      string token = auth.Substring(6);
      string converted = Encoding.UTF8.GetString(Convert.FromBase64String(token));
      string[] split = converted.Split(':');
      if (split.Length == 2)
      {
        return new Credentials()
        {
          EmailAddress = split[0],
          Password = split[1]
        };
      }
      return null;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception encountered while trying to retrieve credentials from header:");
      Console.WriteLine(e);
      return null;
    }
  }

  [HttpGet]
  [Route("token-verification")]
  [Authorize]
  public IActionResult VerifyTokenAny()
  {
    try
    {
      // SecurityTokenPayload? payload = JsonSerializer.Deserialize<SecurityTokenPayload>(HttpContext.User.Claims.Where(c => c.Type == "user").FirstOrDefault()?.Value ?? "");
      return Ok("This token is valid.");
    }
    catch
    {
      return Unauthorized("There was a problem verifying the token.");
    }
  }

  [HttpGet]
  [Route("token-verification/admin")]
  [Authorize]
  [RequiresRole(UserRole.ADMIN)]
  public IActionResult VerifyTokenAdmin()
  {
    try
    {
      // SecurityTokenPayload? payload = JsonSerializer.Deserialize<SecurityTokenPayload>(HttpContext.User.Claims.Where(c => c.Type == "user").FirstOrDefault()?.Value ?? "");
      return Ok("This token is valid and the user is an admin.");
    }
    catch
    {
      return Unauthorized("There was a problem verifying the token.");
    }
  }

  [HttpGet]
  [Route("token-verification/super")]
  [Authorize]
  [RequiresRole(UserRole.SUPER)]
  public IActionResult VerifyTokenSuper()
  {
    try
    {
      // SecurityTokenPayload? payload = JsonSerializer.Deserialize<SecurityTokenPayload>(HttpContext.User.Claims.Where(c => c.Type == "user").FirstOrDefault()?.Value ?? "");
      return Ok("This token is valid and the user is a super admin.");
    }
    catch
    {
      return Unauthorized("There was a problem verifying the token.");
    }
  }
}