using Authentication.Models;
using Authentication.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Services;

public class SecurityTokenService : ISecurityTokenService
{
  private readonly IUserRepository userRepository;
  private readonly ISecurityTokenGenerator securityTokenGenerator;
  private readonly IPasswordValidator passwordValidator;

  public SecurityTokenService(IUserRepository userRepository, ISecurityTokenGenerator securityTokenGenerator, IPasswordValidator passwordValidator)
  {
    this.userRepository = userRepository;
    this.securityTokenGenerator = securityTokenGenerator;
    this.passwordValidator = passwordValidator;
  }

  public async Task<ActionResult<SecurityTokenWrapper>> AuthenticateUser(Credentials credentials)
  {
    UserEntity? user = null;
    try
    {
      user = await userRepository.GetUserByEmailAddress(credentials.EmailAddress);
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception when attempting to retrieve user from database.");
      Console.WriteLine(e);
      return new StatusCodeResult(500);
    }

    if (user == null || user.PasswordHash is null) return new StatusCodeResult(401);

    if (passwordValidator.Validate(credentials.Password, user.PasswordHash) == false) return new StatusCodeResult(401);
    
    try
    {
      SecurityTokenWrapper securityTokenWrapper = securityTokenGenerator.GenerateTokenForUser(user);
      return securityTokenWrapper;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception when attempting to create session token for user.");
      Console.WriteLine(e);
      return new StatusCodeResult(500);
    }
  }
}