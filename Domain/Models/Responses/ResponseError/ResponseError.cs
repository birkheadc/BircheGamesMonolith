namespace Domain.Models;

public class ResponseError
{
  public string? Field { get; init; }
  public int? StatusCode { get; init; } 
  public string? Message { get; init; }
}