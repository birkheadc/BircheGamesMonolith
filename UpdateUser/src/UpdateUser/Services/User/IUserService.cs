using UpdateUser.Models;

namespace UpdateUser.Services;

public interface IUserService
{
  public Task<bool> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request);
}