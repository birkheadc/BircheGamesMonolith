namespace UserRegistration.Config;

public class UserValidatorConfig
{
  public string DisplayNameRegex { get; init; } = "";
  public string TagRegex { get; init; } = "";
  public int PasswordMinChars { get; init; } = 0;
  public int PasswordMaxChars { get; init; } = 0;
}