namespace EmailVerification.Services;

public interface ITokenValidator
{
  public string? ValidateToken(string token);
}