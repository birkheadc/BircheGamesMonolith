namespace UserRegistration.Models;

public class UserCreateRequestDTO
{
  public string Username { get; init; } = "";
  public string Password { get; init; } = "";
  public string RepeatPassword { get; init; } = "";
}