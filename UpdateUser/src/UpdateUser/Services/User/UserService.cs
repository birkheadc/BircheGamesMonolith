using Domain.Models;
using UpdateUser.Models;
using UpdateUser.Repositories;

namespace UpdateUser.Services;

public class UserService : IUserService
{
  private readonly IUserRepository userRepository;

  public UserService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }

  public async Task<UserResponseDTO?> GetUser(string id)
  {
    UserEntity? entity = await userRepository.GetUser(id);
    if (entity is null) return null;
    return UserConverter.ToResponseDTO(entity);
  }

  public Task<bool> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request)
  {
    throw new NotImplementedException();
  }
}