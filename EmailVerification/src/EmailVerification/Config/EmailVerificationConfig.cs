namespace EmailVerification.Config;

public class EmailVerificationConfig
{
  public string EmailVerificationSecretKey { get; set; } = "";
  public int LifespanHours { get; set; } = 1;
  public string Issuer { get; set; } = "";
  public string Audience { get; set; } = "";
  public string VerificationEmailTemplatePath { get; set; } = "";
  public string SenderAddress { get; set; } = "";
}