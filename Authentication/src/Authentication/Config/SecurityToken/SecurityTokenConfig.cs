namespace Authentication.Config;

public class SecurityTokenConfig
{
  public string SecretKey { get; set; } = "";
  public int LifespanHours { get; set; } = 1;
  public string Issuer { get; set; } = "";
  public string Audience { get; set; } = "";
}