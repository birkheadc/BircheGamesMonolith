using Domain.Models;
using UpdateUser.Models;

namespace UpdateUser.Services;

public interface IUserService
{
  public Task<UserResponseDTO?> GetUser(string id);
  public Task<Response> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request);
}