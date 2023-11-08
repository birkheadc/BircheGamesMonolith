namespace UserRegistrationTests.Tests.Services;

using System.Threading.Tasks;
using Domain.Models;
using UserRegistration.Models;
using UserRegistration.Services;
using UserRegistrationTests.Mocks.Repositories;
using Xunit;

public class UserServiceTests
{
  [Fact]
  public async Task ReturnsFailure_WhenEmailAddressInvalid()
  {
    string[] invalidAddresses = {
      "plainaddress", 
      "#@%^%#$@#$@#.com", 
      "@example.com", 
      "email.example.com", 
      "email@example@example.com", 
      ".email@example.com", 
      "email@example.com (Joe Smith)", 
    };
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    foreach (string emailAddress in invalidAddresses)
    {
      CreateUserRequestDTO newUser = new()
      {
        EmailAddress = emailAddress,
        Password = "12345678",
        RepeatPassword = "12345678"
      };
      Response response = await userService.CreateNewUser(newUser);
      Assert.False(response.WasSuccess);
    }
  }
  [Fact]
  public async Task ReturnsSuccess_WhenEmailAddressValid()
  {
    string[] validAddresses = {
      "email@example.com", 
      "firstname.lastname@example.com", 
      "email@subdomain.example.com", 
      "firstname+lastname@example.com", 
      "email@123.123.123.123", 
      "email@[123.123.123.123]", 
      "\"email\"@example.com", 
      "1234567890@example.com", 
      "email@example-one.com", 
      "_______@example.com", 
      "email@example.name", 
      "email@example.museum", 
      "email@example.co.jp", 
      "firstname-lastname@example.com",
    };
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    foreach (string emailAddress in validAddresses)
    {
      CreateUserRequestDTO newUser = new()
      {
        EmailAddress = emailAddress,
        Password = "12345678",
        RepeatPassword = "12345678"
      };
      Response response = await userService.CreateNewUser(newUser);
      Assert.True(response.WasSuccess);
    }
  }

  [Fact]
  public async Task ReturnsFailure_WhenPasswordTooShort()
  {
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    CreateUserRequestDTO newUser = new()
    {
      EmailAddress = "example@example.com",
      Password = "1234567",
      RepeatPassword = "1234567"
    };
    Response response = await userService.CreateNewUser(newUser);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task ReturnsFailure_WhenPasswordTooLong()
  {
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    CreateUserRequestDTO newUser = new()
    {
      EmailAddress = "example@example.com",
      Password = "123456789012345678901234567890123",
      RepeatPassword = "123456789012345678901234567890123"
    };
    Response response = await userService.CreateNewUser(newUser);
    Assert.False(response.WasSuccess);
  }

  public async Task ReturnsFailure_WhenPasswordDoesNotMatch()
  {
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    CreateUserRequestDTO newUser = new()
    {
      EmailAddress = "example@example.com",
      Password = "12345678",
      RepeatPassword = "abcdefgh"
    };
    Response response = await userService.CreateNewUser(newUser);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task ReturnsSuccess_WhenPasswordCorrectLengthAndMatches()
  {
    UserService userService = new(new UserRegistration.Services.UserConverter(new PasswordHasher()), new UserRepositoryMock());
    CreateUserRequestDTO newUser = new()
    {
      EmailAddress = "example@example.com",
      Password = "12345678",
      RepeatPassword = "12345678"
    };
    Response response = await userService.CreateNewUser(newUser);
    Assert.True(response.WasSuccess);

    newUser = new()
    {
      EmailAddress = "example@example.com",
      Password = "12345678901234567890123456789012",
      RepeatPassword = "12345678901234567890123456789012"
    };
    response = await userService.CreateNewUser(newUser);
    Assert.True(response.WasSuccess);
  }
}