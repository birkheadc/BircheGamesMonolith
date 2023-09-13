using Domain.Models;
using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserConverter
{
  public UserEntity ToEntity(CreateUserRequestDTO createRequestDTO);
}