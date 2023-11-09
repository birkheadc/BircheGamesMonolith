using Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Services;

public interface ISecurityTokenService
{
  public Task<SecurityTokenWrapper?> AuthenticateUser(Credentials credentials);
}