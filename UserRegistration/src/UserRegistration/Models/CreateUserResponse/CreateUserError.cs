namespace UserRegistration.Models;

public class CreateUserError
{
  public string Field { get; init; } = "";
  public int StatusCode { get; init; } = 0;
  public string? ErrorMessage { get; init; } = null;
}