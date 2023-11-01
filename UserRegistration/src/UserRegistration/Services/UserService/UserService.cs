using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using UserRegistration.Repositories;

namespace UserRegistration.Services;

public class UserService : IUserService
{
  private readonly IUserConverter userConverter;
  private readonly IUserRepository userRepository;

  public UserService(IUserConverter userConverter, IUserRepository userRepository)
  {
    this.userConverter = userConverter;
    this.userRepository = userRepository;
  }

  public async Task<Response> CreateNewUser(CreateUserRequestDTO newUser)
  {
    ResponseBuilder responseBuilder = new();

    Console.WriteLine("Attempting to create new user:");
    Console.WriteLine($"Email: {newUser.EmailAddress} | Password: {newUser.Password} | Repeat Password: {newUser.RepeatPassword}");

    List<ResponseError> errors = Validate(newUser);

    if (errors.Count > 0)
    {
      return responseBuilder
        .Fail()
        .WithErrors(errors)
        .Build();
    }
    UserEntity entity = userConverter.ToEntity(newUser);
    Response response = await userRepository.CreateNewUser(entity);
    return response;
  }

  private List<ResponseError> Validate(CreateUserRequestDTO request)
  {
    UserValidator validator = new();
    Response response = validator
      .WithPassword(request.Password, request.RepeatPassword)
      .WithEmailAddress(request.EmailAddress)
      .Validate();
    return response.Errors;
  }
}