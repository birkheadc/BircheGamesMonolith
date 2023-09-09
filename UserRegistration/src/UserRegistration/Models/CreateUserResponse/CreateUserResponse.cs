using Domain.Models;

namespace UserRegistration.Models;

public class CreateUserResponse
{
  public bool WasSuccess { get; set; } = true;
  public UserDTO? CreatedUser { get; set; } = null;
  public List<CreateUserError> Errors { get; init; } = new();
}