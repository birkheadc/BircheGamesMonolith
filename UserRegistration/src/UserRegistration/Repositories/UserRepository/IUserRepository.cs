using Domain.Models;

namespace UserRegistration.Repositories;

public interface IUserRepository
{
  public Task<Response> CreateNewUser(UserEntity entity);
}