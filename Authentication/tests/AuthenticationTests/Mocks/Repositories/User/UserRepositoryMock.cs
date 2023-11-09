using System.Threading.Tasks;
using Authentication.Repositories;
using Domain.Models;

namespace AuthenticationTests.Mocks.Repositories;

public class UserRepositoryMock_NoUser : IUserRepository
{
  public Task<UserEntity?> GetUserByEmailAddress(string emailAddress)
  {
    return Task.FromResult<UserEntity?>(null);
  }
}

public class UserRepositoryMock_ReturnsUser : IUserRepository
{
  public Task<UserEntity?> GetUserByEmailAddress(string emailAddress)
  {
    return Task.FromResult<UserEntity?>
    (
      new UserEntity()
      {
        EmailAddress = emailAddress
      }
    );
  }
}