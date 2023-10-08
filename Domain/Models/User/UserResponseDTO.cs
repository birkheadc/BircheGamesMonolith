namespace Domain.Models;

public class UserResponseDTO
{
  public string Id { get; init; } = "";
  public string EmailAddress { get; init; } = "";
  public string DisplayName { get; init; } = "";
  public string Tag { get; init; } = "";
  public string CreationDateTime { get; init; } = "";
  public UserRole Role { get; init; }
  public bool IsEmailVerified { get; init; } = false;
  public bool IsDisplayNameChosen { get; init; } = false;
}