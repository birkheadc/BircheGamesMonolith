using Microsoft.AspNetCore.Mvc;

namespace UserRegistration.Repositories;

public class UserRepository : IUserRepository
{
  public async Task<StatusCodeResult> CreateNewUser(UserEntity entity)
  {
    Console.WriteLine($"Add user {entity.Id} to database...");
    return new StatusCodeResult(201);
  }
}