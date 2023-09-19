namespace EmailVerification.Models;

public class GenerateVerificationEmailRequest
{
  public string FrontendUrl { get; init; } = "";
  public string EmailAddress { get; init; } = "";
}