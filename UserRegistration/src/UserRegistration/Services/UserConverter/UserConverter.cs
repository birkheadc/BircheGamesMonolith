using Domain.Models;
using UserRegistration.Models;

namespace UserRegistration.Services;
public class UserConverter : IUserConverter
{
  private readonly IPasswordHasher passwordHasher;

  public UserConverter(IPasswordHasher passwordHasher)
  {
    this.passwordHasher = passwordHasher;
  }

  public UserEntity ToEntity(UserCreateRequestDTO createRequestDTO)
  {
    return new UserEntity()
    {
      Id = Guid.NewGuid().ToString(),
      Username = createRequestDTO.Username,
      PasswordHash = passwordHasher.HashPassword(createRequestDTO.Password),
      Role = UserRole.USER,
      IsEmailVerified = false
    };
  }

  public UserResponseDTO ToResponse(UserEntity entity)
  {
    return new UserResponseDTO()
    {
      Id = entity.Id,
      Username = entity.Username,
      Role = entity.Role,
      IsEmailVerified = entity.IsEmailVerified
    };
  }
}