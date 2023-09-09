namespace UserRegistration.Models;

public class CreateUserRequestDTO
{
  public string EmailAddress { get; init; } = "";
  public string DisplayName { get; init; } = "";
  public string Tag { get; init; } = "";
  public string Password { get; init; } = "";
  public string RepeatPassword { get; init; } = "";
}