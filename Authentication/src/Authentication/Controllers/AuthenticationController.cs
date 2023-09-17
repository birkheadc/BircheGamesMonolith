using System.Text;
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
  public async Task<ActionResult<SecurityTokenWrapper>> Authenticate()
  {
    Credentials? credentials = GetCredentialsFromHeaders(Request.Headers);
    if (credentials is null) return Forbid();

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

  private static Credentials? GetCredentialsFromHeaders(IHeaderDictionary headers)
  {
    try
    {
      string auth = headers.Authorization;
      string converted = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
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
}