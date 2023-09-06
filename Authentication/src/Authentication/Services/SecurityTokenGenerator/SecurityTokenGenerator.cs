using Authentication.Config;
using Authentication.Models;
using Domain.Models;

namespace Authentication.Services;

public class SecurityTokenGenerator : ISecurityTokenGenerator
{
  private readonly SecurityTokenConfig config;

  public SecurityTokenGenerator(SecurityTokenConfig config)
  {
    this.config = config;
  }

  public SecurityTokenWrapper GenerateTokenForUser(UserEntity user)
  {
    return new SecurityTokenWrapper()
    {
      Token = "token"
    };
  }
}