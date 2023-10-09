using Domain.Models;
using UpdateUser.Models;

namespace UpdateUser.Repositories;

public interface IUserRepository
{
  public Task<UserEntity?> GetUser(string id);
  public Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag);
  public Task UpdateUser(UserEntity user); 
}