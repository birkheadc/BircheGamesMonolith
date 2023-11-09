using Authentication.Models;
using Authentication.Services;
using Domain.Models;

namespace AuthenticationTests.Mocks.Services;

public class SecurityTokenGeneratorMock : ISecurityTokenGenerator
{
  public SecurityTokenWrapper GenerateTokenForUser(UserEntity user)
  {
    return new SecurityTokenWrapper()
    {
      Token = "this is a token"
    };
  }
}