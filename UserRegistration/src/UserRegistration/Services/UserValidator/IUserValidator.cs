using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserValidator
{
  public bool Validate(UserCreateRequestDTO user);
}