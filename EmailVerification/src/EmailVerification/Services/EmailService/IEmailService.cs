using EmailVerification.Models;

namespace EmailVerification.Services;

public interface IEmailService
{
  public void ProcessGenerateRequest(GenerateVerificationEmailRequest request);
  public Task<bool> ProcessVerificationCode(string code);
}