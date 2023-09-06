using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

[ApiController]
[Route("authenticate")]
public class AuthenticationController : ControllerBase
{
  private readonly ISecurityTokenService securityTokenService;

  public AuthenticationController(ISecurityTokenService securityTokenService)
  {
    this.securityTokenService = securityTokenService;
  }

  [HttpPost]
  public async Task<ActionResult<SecurityTokenWrapper>> Authenticate([FromBody] Credentials credentials)
  {
    try
    {
      return await securityTokenService.AuthenticateUser(credentials);
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Error when attempting to Authenticate user in controller.");
      Console.WriteLine(e);
      return StatusCode(500);
    }
  }
}