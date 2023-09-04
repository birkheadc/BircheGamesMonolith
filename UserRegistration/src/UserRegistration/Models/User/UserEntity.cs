public class UserEntity
{
  public Guid? Id { get; init; }
  public string? Username { get; init; }
  public string? PasswordHash { get; init; }
  public UserRole Role { get; init; }
  public bool IsEmailVerified { get; init; }
}