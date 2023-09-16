using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

[ApiController]
[Route("verify-email")]
public class EmailVerificationController : ControllerBase
{
  public async Task<IActionResult> GenerateEmail()
  {

  }

  public async Task<IActionResult> Verify()
  {

  }
}