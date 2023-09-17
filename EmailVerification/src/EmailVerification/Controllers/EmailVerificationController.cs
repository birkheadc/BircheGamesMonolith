using EmailVerification.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailVerification.Controllers;

[ApiController]
[Route("verify-email")]
public class EmailVerificationController : ControllerBase
{
  private readonly IEmailService emailService;

  public EmailVerificationController(IEmailService emailService)
  {
    this.emailService = emailService;
  }

  public async Task<IActionResult> GenerateEmail()
  {
    return Ok();
  }

  public async Task<IActionResult> Verify()
  {
    return Ok();
  }
}