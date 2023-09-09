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

  public async Task<CreateUserResponse> CreateNewUser(CreateUserRequestDTO newUser)
  {
    List<CreateUserError> errors = userValidator.Validate(newUser);
    if (errors.Count > 0)
    {
      return new CreateUserResponse(){
        WasSuccess = false,
        Errors = errors
      };
    }
    UserEntity entity = userConverter.ToEntity(newUser);
    CreateUserResponse response = await userRepository.CreateNewUser(entity);
    if (response.WasSuccess)
    {
      response.CreatedUser = userConverter.ToResponse(entity);
    }
    return response;
  }
}