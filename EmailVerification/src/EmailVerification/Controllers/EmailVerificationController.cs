using EmailVerification.Models;
using EmailVerification.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailVerification.Controllers;

[ApiController]
[Route("email-verification")]
public class EmailVerificationController : ControllerBase
{
  private readonly IEmailService emailService;

  public EmailVerificationController(IEmailService emailService)
  {
    this.emailService = emailService;
  }

  [HttpPost]
  [Route("generate")]
  public async Task<IActionResult> GenerateEmail([FromBody] GenerateVerificationEmailRequest request)
  {
    await emailService.ProcessGenerateRequest(request);
    return Ok();
  }

  [HttpPost]
  [Route("verify")]
  public async Task<IActionResult> Verify([FromBody] VerifyCodeRequest request)
  {
    bool wasSuccess = await emailService.ProcessVerificationCode(request.VerificationCode);
    return wasSuccess ? Ok() : Unauthorized();
  }
}