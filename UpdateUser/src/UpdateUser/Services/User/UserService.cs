using UpdateUser.Models;

namespace UpdateUser.Services;

public class UserService : IUserService
{
  public Task<bool> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request)
  {
    throw new NotImplementedException();
  }
}