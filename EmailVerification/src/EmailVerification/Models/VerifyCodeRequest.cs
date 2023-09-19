namespace EmailVerification.Models;

public class VerifyCodeRequest
{
  public string VerificationCode { get; init; } = "";
}