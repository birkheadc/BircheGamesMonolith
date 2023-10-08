using Domain.Models;
using UpdateUser.Models;

namespace UpdateUser.Services;

public interface IUserService
{
  public Task<UserResponseDTO?> GetUser(string id);
  public Task<bool> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request);
}