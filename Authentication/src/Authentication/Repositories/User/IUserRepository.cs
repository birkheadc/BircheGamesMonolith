namespace Authentication.Repositories;

using Authentication.Models;
using Domain.Models;

public interface IUserRepository
{
   public Task<UserEntity?> GetUserByEmailAddress(string emailAddress);
}