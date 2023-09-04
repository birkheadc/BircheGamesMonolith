using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserService
{
  public Task<ActionResult<UserResponseDTO>> CreateNewUser(UserCreateRequestDTO newUser);
}