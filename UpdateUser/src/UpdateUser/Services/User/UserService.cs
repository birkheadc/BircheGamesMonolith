using Domain.Models;
using UpdateUser.Models;
using UpdateUser.Repositories;

namespace UpdateUser.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserResponseDTO?> GetUser(string id)
  {
    UserEntity? entity = await _userRepository.GetUser(id);
    if (entity is null) return null;
    return UserConverter.ToResponseDTO(entity);
  }

  public async Task<Response> PatchUserDisplayNameAndTag(string userId, PatchDisplayNameAndTagRequest request)
  {
    ResponseBuilder responseBuilder = new();
    
    UserEntity? user = await _userRepository.GetUser(userId);
    if (user is null)
    {
      return responseBuilder
        .Fail()
        .WithGeneralError(404, "User with that ID does not exist.")
        .Build();
    }

    if (user.IsDisplayNameChosen == true)
    {
      return responseBuilder
        .Fail()
        .WithGeneralError(400, "User has already chosen their display name and tag.")
        .Build();
    }

    bool isAvailable = await _userRepository.IsDisplayNameAndTagAvailable(request);

    if (!isAvailable)
    {
      return responseBuilder
        .Fail()
        .WithGeneralError(409, "That display name and tag combination is already in use.")
        .Build();
    }

    user.DisplayName = request.DisplayName;
    user.Tag = request.Tag;
    user.IsDisplayNameChosen = true;

    await _userRepository.UpdateUser(user);

    return responseBuilder
      .Succeed()
      .Build();
  }
}