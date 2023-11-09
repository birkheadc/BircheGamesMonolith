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

public class ResponseBuilder<T>
{
  private Response<T> _response = new();

  public Response<T> Build()
  {
    return _response;
  }

  public ResponseBuilder<T> Succeed()
  {
    _response.WasSuccess = true;
    return this;
  }

  public ResponseBuilder<T> Fail()
  {
    _response.WasSuccess = false;
    return this;
  }

  public ResponseBuilder<T> WithError(ResponseError error)
  {
    _response.Errors.Add(error);
    return this;
  }

  public ResponseBuilder<T> WithErrors(List<ResponseError> errors)
  {
    _response.Errors.AddRange(errors);
    return this;
  }

  public ResponseBuilder<T> WithGeneralError(int? statusCode = null, string? message = null)
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

  public ResponseBuilder<T> WithFieldError(string field, int? statusCode = null, string? message = null)
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

  public ResponseBuilder<T> WithValue(T value)
  {
    _response.Value = value;
    return this;
  }
}