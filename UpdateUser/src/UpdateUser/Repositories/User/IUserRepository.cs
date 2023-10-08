using Domain.Models;

namespace UpdateUser.Repositories;

public interface IUserRepository
{
  public Task<UserEntity?> GetUser(string id);
}