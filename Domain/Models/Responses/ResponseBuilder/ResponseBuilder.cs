namespace Domain.Models;

public class ResponseBuilder
{
  private Response _response = new();

  public Response Build()
  {
    return _response;
  }

  public ResponseBuilder Succeed()
  {
    _response.WasSuccess = true;
    return this;
  }

  public ResponseBuilder Fail()
  {
    _response.WasSuccess = false;
    return this;
  }

  public ResponseBuilder WithError(ResponseError error)
  {
    _response.Errors.Add(error);
    return this;
  }

  public ResponseBuilder WithErrors(List<ResponseError> errors)
  {
    _response.Errors.AddRange(errors);
    return this;
  }

  public ResponseBuilder WithGeneralError(int? statusCode = null, string? message = null)
  {
    ResponseError error = new()
    {
      Field = null,
      StatusCode = statusCode,
      Message = message
    };
    _response.Errors.Add(error);
    return this;
  }

  public ResponseBuilder WithFieldError(string field, int? statusCode = null, string? message = null)
  {
    ResponseError error = new()
    {
      Field = field,
      StatusCode = statusCode,
      Message = message
    };
    _response.Errors.Add(error);
    return this;
  }
}