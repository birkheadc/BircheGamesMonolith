using Authentication.Services;

namespace AuthenticationTests.Mocks.Services;

public class PasswordValidatorMock_BadPassword : IPasswordValidator
{
  public bool Validate(string password, string hash)
  {
    return false;
  }
}

public class PasswordValidatorMock_GoodPassword : IPasswordValidator
{
  public bool Validate(string password, string hash)
  {
    return true;
  }
}