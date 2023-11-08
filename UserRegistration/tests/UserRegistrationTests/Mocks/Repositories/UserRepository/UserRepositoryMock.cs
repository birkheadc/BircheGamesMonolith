using System.Threading.Tasks;
using Domain.Models;
using UserRegistration.Repositories;

namespace UserRegistrationTests.Mocks.Repositories;

public class UserRepositoryMock : IUserRepository
{
  public Task<Response> CreateNewUser(UserEntity entity)
  {
    ResponseBuilder responseBuilder = new();

    return Task.FromResult(responseBuilder
      .Succeed()
      .Build());
  }
}