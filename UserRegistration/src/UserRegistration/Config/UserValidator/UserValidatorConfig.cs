namespace UserRegistration.Config;

public class UserValidatorConfig
{
  public string UsernameRegex { get; init; } = "";
  public int PasswordMinChars { get; init; } = 0;
  public int PasswordMaxChars { get; init; } = 0;

  public void Describe()
  {
    Console.WriteLine("User Validator Config:");
    Console.WriteLine($"Username must match the following regex: {UsernameRegex}");
    Console.WriteLine($"Password must be between {PasswordMinChars} and {PasswordMaxChars} characters.");
  }
}