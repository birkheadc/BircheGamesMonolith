namespace AuthenticationTests.Tests.Services;

using System.Threading.Tasks;
using Authentication.Models;
using Authentication.Services;
using AuthenticationTests.Mocks.Repositories;
using AuthenticationTests.Mocks.Services;
using Xunit;

public class SecurityTokenServiceTests
{
  [Fact]
  public async Task AuthenticateUser_Returns401_WhenUserNotFound()
  {
    SecurityTokenService securityTokenService = new
    (
      new UserRepositoryMock_NoUser(),
      new SecurityTokenGeneratorMock(),
      new PasswordValidatorMock_BadPassword()
    );

    Credentials credentials = new()
    {
      EmailAddress = "example@test.com",
      Password = "12345678"
    };

    SecurityTokenWrapper? token = await securityTokenService.AuthenticateUser(credentials);
    
    Assert.Null(token);
  }
  
  [Fact]
  public async Task AuthenticateUser_Returns401_WhenPasswordWrong()
  {
    SecurityTokenService securityTokenService = new
    (
      new UserRepositoryMock_ReturnsUser(),
      new SecurityTokenGeneratorMock(),
      new PasswordValidatorMock_BadPassword()
    );

    Credentials credentials = new()
    {
      EmailAddress = "example@test.com",
      Password = "12345678"
    };

    SecurityTokenWrapper? token = await securityTokenService.AuthenticateUser(credentials);
    
    Assert.Null(token);
  }

  [Fact]
  public async Task AuthenticateUser_ReturnsOkWithSecurityToken_WhenCredentialsAreValid()
  {
    SecurityTokenService securityTokenService = new
    (
      new UserRepositoryMock_ReturnsUser(),
      new SecurityTokenGeneratorMock(),
      new PasswordValidatorMock_GoodPassword()
    );

    Credentials credentials = new()
    {
      EmailAddress = "example@test.com",
      Password = "12345678"
    };

    SecurityTokenWrapper? token = await securityTokenService.AuthenticateUser(credentials);
    
    Assert.NotNull(token);
  }
}