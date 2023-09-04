using Microsoft.AspNetCore.Mvc;

namespace UserRegistration.Repositories;

public interface IUserRepository
{
  public Task<StatusCodeResult> CreateNewUser(UserEntity entity);
}