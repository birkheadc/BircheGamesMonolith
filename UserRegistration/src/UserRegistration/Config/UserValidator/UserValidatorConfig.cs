namespace UserRegistration.Config;

public class UserValidatorConfig
{
  public int PasswordMinChars { get; init; } = 0;
  public int PasswordMaxChars { get; init; } = 0;
}