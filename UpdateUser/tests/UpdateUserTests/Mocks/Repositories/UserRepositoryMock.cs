using System.Threading.Tasks;
using Domain.Models;
using UpdateUser.Models;
using UpdateUser.Repositories;

namespace UpdateUserTests.Mocks.Repositories;

public class UserRepositoryMock_NoUsers : IUserRepository
{
  public Task<UserEntity?> GetUser(string id)
  {
    return Task.FromResult<UserEntity?>(null);
  }

  public Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag)
  {
    return Task.FromResult(true);
  }

  public Task UpdateUser(UserEntity user)
  {
    return Task.Run(() => {});
  }
}

public class UserRepositoryMock_ReturnsUser_WithDisplayNameAndTag_AlreadyChosen : IUserRepository
{
  public Task<UserEntity?> GetUser(string id)
  {
    return Task.FromResult<UserEntity?>(new UserEntity()
    {
      Id = id,
      IsDisplayNameChosen = true
    });
  }

  public Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag)
  {
    return Task.FromResult(true);
  }

  public Task UpdateUser(UserEntity user)
  {
    return Task.Run(() => {});
  }
}

public class UserRepositoryMock_WithDisplayNameAndTag_NotUnique : IUserRepository
{
  public Task<UserEntity?> GetUser(string id)
  {
    return Task.FromResult<UserEntity?>(new UserEntity()
    {
      Id = id,
      IsDisplayNameChosen = false
    });
  }

  public Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag)
  {
    return Task.FromResult(false);
  }

  public Task UpdateUser(UserEntity user)
  {
    return Task.Run(() => {});
  }
}

public class UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique : IUserRepository
{
  public Task<UserEntity?> GetUser(string id)
  {
    return Task.FromResult<UserEntity?>(new UserEntity()
    {
      Id = id,
      IsDisplayNameChosen = false
    });
  }

  public Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag)
  {
    return Task.FromResult(true);
  }

  public Task UpdateUser(UserEntity user)
  {
    return Task.Run(() => {});
  }
}