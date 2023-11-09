namespace Domain.Models;

public class Response
{
  public bool WasSuccess { get; set; } = false;
  public List<ResponseError> Errors { get; set; } = new();
}

public class Response<T> : Response
{
  public T? Value { get; set; } = default;
}