using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using EmailVerification.Config;
using EmailVerification.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmailVerification.Services;

public class TokenValidator : ITokenValidator
{
  private readonly EmailVerificationConfig config;
  private readonly JwtSecurityTokenHandler handler;

  public TokenValidator(EmailVerificationConfig config)
  {
    this.config = config;
    this.handler = new();
  }

  public string? ValidateToken(string token)
  {
    try
    {
      handler.ValidateToken(token, GetTokenValidationParameters(), out SecurityToken validatedToken);
      // This is the single best line of code I have ever written and I am very proud of it.
      EmailVerificationSecurityTokenPayload? payload = JsonSerializer.Deserialize<EmailVerificationSecurityTokenPayload>(((JwtSecurityToken)validatedToken).Payload["payload"].ToString() ?? "");
      
      if (payload is null) return null;
      return payload.EmailAddress;
    }
    catch
    {
      return null;
    }
  }

  private TokenValidationParameters GetTokenValidationParameters()
  {
    return new TokenValidationParameters()
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.EmailVerificationSecretKey)),
      ValidateIssuer = false,
      ValidateAudience = false,
      ClockSkew = TimeSpan.Zero
    };
  }
}