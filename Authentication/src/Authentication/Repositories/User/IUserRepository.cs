namespace Authentication.Repositories;

using Authentication.Models;
using Domain.Models;

public interface IUserRepository
{
   public Task<UserEntity?> GetUserByUsername(string username);
}