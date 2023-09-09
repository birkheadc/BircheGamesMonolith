using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Repositories;

public interface IUserRepository
{
  public Task<CreateUserResponse> CreateNewUser(UserEntity entity);
}