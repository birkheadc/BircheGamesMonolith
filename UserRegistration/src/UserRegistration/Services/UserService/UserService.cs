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

  public async Task<Response> CreateNewUser(CreateUserRequestDTO newUser)
  {
    ResponseBuilder responseBuilder = new();

    Console.WriteLine("Attempting to create new user:");
    Console.WriteLine($"Email: {newUser.EmailAddress} | Password: {newUser.Password} | Repeat Password: {newUser.RepeatPassword}");
    List<ResponseError> errors = userValidator.Validate(newUser);
    if (errors.Count > 0)
    {
      return responseBuilder
        .Succeed()
        .WithErrors(errors)
        .Build();
    }
    UserEntity entity = userConverter.ToEntity(newUser);
    Response response = await userRepository.CreateNewUser(entity);
    return response;
  }
}