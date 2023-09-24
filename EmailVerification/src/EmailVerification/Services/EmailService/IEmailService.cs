using EmailVerification.Models;

namespace EmailVerification.Services;

public interface IEmailService
{
  public Task ProcessGenerateRequest(GenerateVerificationEmailRequest request);
  public Task<bool> ProcessVerificationCode(string code);
  public Task<string> GetVerificationEmailTemplate();
}