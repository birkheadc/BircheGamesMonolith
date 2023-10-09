namespace Domain.Models;

public class Response
{
  public bool WasSuccess { get; set; } = false;
  public List<ResponseError> Errors { get; set; } = new();
}