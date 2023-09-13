using System.Net.Mail;
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

  public List<CreateUserError> Validate(CreateUserRequestDTO user)
  {
    List<CreateUserError> errors = new();
    if (user.Password != user.RepeatPassword) errors.Add(new(){ Field = "Password", StatusCode = 422, ErrorMessage = "Password and Repeat Password do not match."});
    if (user.Password.Length < config.PasswordMinChars) errors.Add(new(){ Field = "Password", StatusCode = 422, ErrorMessage = "Password is too short." });
    if (user.Password.Length > config.PasswordMaxChars) errors.Add(new(){ Field = "Password", StatusCode = 422, ErrorMessage = "Password is too long." });
    if (IsEmailValid(user.EmailAddress) == false) errors.Add(new(){ Field = "EmailAddress", StatusCode = 422, ErrorMessage = "Email Address format is invalid." });
    return errors;
  }

  private static bool IsEmailValid(string email)
  {
    try
    {
      MailAddress mailAddress = new(email);
      return true;
    }
    catch
    {
      return false;
    }
  }
}