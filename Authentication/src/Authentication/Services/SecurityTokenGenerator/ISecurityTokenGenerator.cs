using Authentication.Models;
using Domain.Models;

namespace Authentication.Services;

public interface ISecurityTokenGenerator
{
  public SecurityTokenWrapper GenerateTokenForUser(UserEntity user);
}