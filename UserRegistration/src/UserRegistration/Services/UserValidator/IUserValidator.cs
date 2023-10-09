using Domain.Models;
using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserValidator
{
  public List<ResponseError> Validate(CreateUserRequestDTO user);
}