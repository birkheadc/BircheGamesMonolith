namespace EmailVerification.Repositories;

using Domain.Models;

public interface IUserRepository
{
   public Task<UserEntity?> GetUserByEmailAddress(string emailAddress);
   public Task<bool> UpdateUserEmailVerifiedToTrue(string id);
}