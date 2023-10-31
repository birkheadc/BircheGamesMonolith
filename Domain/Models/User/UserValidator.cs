using System.Text.RegularExpressions;

namespace Domain.Models;

public class UserValidator
{
  List<ResponseError> _errors = new();
  public UserValidator WithDisplayName(string displayName)
  {
    ResponseBuilder responseBuilder = new();
    if (displayName.Length == 0)
    {
      _errors.Add(new ResponseError()
      {
        Field = "DisplayName",
        StatusCode = 422,
        Message = "Display name is too short."
      });
    }

    if (displayName.Length > 16)
    {
      _errors.Add(new ResponseError()
      {
        Field = "DisplayName",
        StatusCode = 422,
        Message = "Display name is too long."
      });
    }

    string pattern = "^[a-zA-Z][a-zA-Z0-9_]*$";
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
    ResponseBuilder responseBuilder = new();
    
    if (tag.Length != 6)
    {
      _errors.Add(new ResponseError()
      {
        Field = "Tag",
        StatusCode = 422,
        Message = "Tag must be exactly 6 characters."
      });
    }

    string pattern = "[a-zA-Z0-9]*$";
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

  public Response Validate()
  {
    return new Response()
    {
      WasSuccess = _errors.Count == 0,
      Errors = _errors
    };
  }
}