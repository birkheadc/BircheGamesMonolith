using UserRegistration.Config;
using UserRegistration.Models;

namespace UserRegistration.Services;

public class UserValidator : IUserValidator
{
  private readonly UserValidatorConfig config;

  public UserValidator(UserValidatorConfig config)
  {
    this.config = config;
  }

  public bool Validate(UserCreateRequestDTO user)
  {
    // Todo
    return true;
  }
}