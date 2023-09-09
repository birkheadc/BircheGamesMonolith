using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserService
{
  public Task<CreateUserResponse> CreateNewUser(CreateUserRequestDTO newUser);
}