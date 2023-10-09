using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Services;

public interface IUserService
{
  public Task<Response> CreateNewUser(CreateUserRequestDTO newUser);
}