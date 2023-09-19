namespace Domain.Models;

public class AmazonSecrets
{
  public string AuthenticationSecretKey { get; set; } = "";
  public string EmailVerificationSecretKey { get; set; } = "";
}