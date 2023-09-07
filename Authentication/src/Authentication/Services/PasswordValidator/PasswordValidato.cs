namespace Authentication.Services;

public class PasswordValidator : IPasswordValidator
{
  public bool Validate(string password, string hash)
  {
    return BCrypt.Net.BCrypt.Verify(password, hash);
  }
}