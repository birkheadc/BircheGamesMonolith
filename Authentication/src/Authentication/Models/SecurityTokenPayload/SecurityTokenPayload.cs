using Domain.Models;

namespace Authentication.Models;

public class SecurityTokenPayload
{
  public string Id { get; init; } = "";
  public string EmailAddress { get; init; } = "";
  public string DisplayName { get; init; } = "";
  public UserRole Role { get; init; }
  public bool IsEmailVerified { get; init; }
}