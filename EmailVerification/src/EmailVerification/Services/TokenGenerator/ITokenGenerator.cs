namespace EmailVerification.Services;

public interface ITokenGenerator
{
  public string GenerateTokenForEmailAddress(string emailAddress);
}