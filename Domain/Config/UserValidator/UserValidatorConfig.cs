namespace Domain.Config;

public class UserValidatorConfig
{
  public int DisplayNameMinChars { get; init; } = 0;
  public int DisplayNameMaxChars { get; init; } = 0;
  public int TagChars { get; init; } = 0;
  public int PasswordMinChars { get; init; } = 0;
  public int PasswordMaxChars { get; init; } = 0;
}