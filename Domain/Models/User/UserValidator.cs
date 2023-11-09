using System.Net.Mail;
using System.Text.RegularExpressions;
using Domain.Config;

namespace Domain.Models;

public class UserValidator
{
  private readonly UserValidatorConfig _config;
  private List<ResponseError> _errors = new();

  public UserValidator()
  {
    _config = new()
    {
      DisplayNameMinChars = 1,
      DisplayNameMaxChars = 16,
      TagChars = 6,
      PasswordMinChars = 8,
      PasswordMaxChars = 32
    };
  }

  public UserValidator WithEmailAddress(string emailAddress)
  {
    if (emailAddress.Contains(' '))
    {
      _errors.Add(new(){ Field = "EmailAddress", StatusCode = 422, Message = "Email Address cannot contain spaces." });
    }
    try
    {
      MailAddress mailAddress = new(emailAddress);
    }
    catch
    {
      _errors.Add(new(){ Field = "EmailAddress", StatusCode = 422, Message = "Email Address format is invalid." });
    }
    return this;
  }
  
  public UserValidator WithDisplayName(string displayName)
  {
    if (displayName.Length < _config.DisplayNameMinChars)
    {
      _errors.Add(new ResponseError()
      {
        Field = "DisplayName",
        StatusCode = 422,
        Message = "Display name is too short."
      });
    }

    if (displayName.Length > _config.DisplayNameMaxChars)
    {
      _errors.Add(new ResponseError()
      {
        Field = "DisplayName",
        StatusCode = 422,
        Message = "Display name is too long."
      });
    }

    string pattern = "^(?!.*__)[a-zA-Z][a-zA-Z0-9_]*$";
    bool doesMatch = Regex.IsMatch(displayName, pattern);
    if (doesMatch == false)
    {
      _errors.Add(new ResponseError()
      {
        Field = "DisplayName",
        StatusCode = 422,
        Message = "Display name contains invalid characters. Must begin with a letter; and contain only letters, numbers, and underscores."
      });
    }

    return this;
  }

  public UserValidator WithTag(string tag)
  {  
    if (tag.Length != _config.TagChars)
    {
      _errors.Add(new ResponseError()
      {
        Field = "Tag",
        StatusCode = 422,
        Message = "Tag must be exactly 6 characters."
      });
    }

    string pattern = "^[a-zA-Z0-9]*$";
    bool doesMatch = Regex.IsMatch(tag, pattern);
    if (doesMatch == false)
    {
      _errors.Add(new ResponseError()
      {
        Field = "Tag",
        StatusCode = 422,
        Message = "Tag contains invalid characters. Must contain only letters and numbers."
      });
    }

    return this;
  }

  public UserValidator WithPassword(string password, string? repeatPassword = null)
  {
    if (repeatPassword is not null && password != repeatPassword) _errors.Add(new(){ Field = "Password", StatusCode = 422, Message = "Password and Repeat Password do not match."});
    if (password.Length < _config.PasswordMinChars) _errors.Add(new(){ Field = "Password", StatusCode = 422, Message = "Password is too short." });
    if (password.Length > _config.PasswordMaxChars) _errors.Add(new(){ Field = "Password", StatusCode = 422, Message = "Password is too long." });
    return this;
  }

  public Response Validate()
  {
    return new Response()
    {
      WasSuccess = _errors.Count == 0,
      Errors = _errors
    };
  }
}