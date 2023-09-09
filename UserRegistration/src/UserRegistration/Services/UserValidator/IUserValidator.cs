using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserValidator
{
  public List<CreateUserError> Validate(CreateUserRequestDTO user);
}