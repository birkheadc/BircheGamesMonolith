using Domain.Models;

namespace UserRegistration.Models;

public class UserResponseDTO
{
  public string? Id { get; init; }
  public string? Username { get; init; }
  public UserRole Role { get; init; }
  public bool IsEmailVerified { get; init; }
}