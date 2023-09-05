using System.Text.RegularExpressions;
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
    if (user.Password != user.RepeatPassword) return false;
    if (user.Password.Length < config.PasswordMinChars) return false;
    if (user.Password.Length > config.PasswordMaxChars) return false;
    if (Regex.IsMatch(user.Username, config.UsernameRegex) == false) return false;
    return true;
  }
}