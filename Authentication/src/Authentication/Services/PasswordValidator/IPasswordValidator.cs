namespace Authentication.Services;

public interface IPasswordValidator
{
  public bool Validate(string password, string hash);
}