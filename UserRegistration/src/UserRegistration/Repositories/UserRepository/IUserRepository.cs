using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Repositories;

public interface IUserRepository
{
  public Task<StatusCodeResult> CreateNewUser(UserEntity entity);
}