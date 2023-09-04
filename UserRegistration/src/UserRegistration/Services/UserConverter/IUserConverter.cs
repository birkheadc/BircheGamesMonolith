using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserConverter
{
  public UserEntity ToEntity(UserCreateRequestDTO createRequestDTO);
  public UserResponseDTO ToResponse(UserEntity entity);
}