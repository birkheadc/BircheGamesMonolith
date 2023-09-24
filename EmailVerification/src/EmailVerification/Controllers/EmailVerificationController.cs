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

  [HttpGet]
  [Route("template")]
  public async Task<ActionResult<string>> GetTemplate()
  {
    try
    {
      string template = await emailService.GetVerificationEmailTemplate();
      return Ok(template);
    }
    catch (Exception e)
    {
      Console.WriteLine("Exception encountered while attempting to get verification email template:");
      Console.WriteLine(e);
      return BadRequest();
    }
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