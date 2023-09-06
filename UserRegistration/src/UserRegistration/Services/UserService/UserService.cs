using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using UserRegistration.Repositories;

namespace UserRegistration.Services;

public class UserService : IUserService
{
  private readonly IUserConverter userConverter;
  private readonly IUserValidator userValidator;
  private readonly IUserRepository userRepository;

  public UserService(IUserConverter userConverter, IUserValidator userValidator, IUserRepository userRepository)
  {
    this.userConverter = userConverter;
    this.userValidator = userValidator;
    this.userRepository = userRepository;
  }

  public async Task<ActionResult<UserResponseDTO>> CreateNewUser(UserCreateRequestDTO newUser)
  {
    if (userValidator.Validate(newUser) == false)
    {
      return new StatusCodeResult(422);
    }
    UserEntity entity = userConverter.ToEntity(newUser);
    StatusCodeResult result = await userRepository.CreateNewUser(entity);
    return (result.StatusCode == 201) ? new ObjectResult(userConverter.ToResponse(entity)) : result;
  }
}