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

  public UserEntity ToEntity(CreateUserRequestDTO createRequestDTO)
  {
    return new UserEntity()
    {
      Id = Guid.NewGuid().ToString(),
      EmailAddress = createRequestDTO.EmailAddress,
      DisplayName = createRequestDTO.DisplayName,
      Tag = createRequestDTO.Tag,
      CreationDateTime = DateTime.Now.ToString(),
      PasswordHash = passwordHasher.HashPassword(createRequestDTO.Password),
      Role = UserRole.USER,
      IsEmailVerified = false
    };
  }

  public UserDTO ToResponse(UserEntity entity)
  {
    return new UserDTO()
    {
      Id = entity.Id,
      EmailAddress = entity.EmailAddress,
      DisplayName = entity.DisplayName,
      Tag = entity.Tag,
      Role = entity.Role,
      IsEmailVerified = entity.IsEmailVerified
    };
  }
}