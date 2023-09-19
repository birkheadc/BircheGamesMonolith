using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Models;
using EmailVerification.Config;
using EmailVerification.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmailVerification.Services;

public class TokenGenerator : ITokenGenerator
{
  private readonly EmailVerificationConfig config;
  private readonly JwtSecurityTokenHandler handler;

  public TokenGenerator(EmailVerificationConfig config)
  {
    this.config = config;
    this.handler = new();
  }

  public string GenerateTokenForEmailAddress(string emailAddress)
  {
    SecurityToken token = handler.CreateToken(GetSecurityTokenDescriptor(emailAddress));
    return handler.WriteToken(token);
  }

  private SecurityTokenDescriptor GetSecurityTokenDescriptor(string emailAddress)
  {
    EmailVerificationSecurityTokenPayload payload = GeneratePayload(emailAddress);
    Dictionary<string, object> claims = new()
    {
      { "payload", payload }
    };
    return new SecurityTokenDescriptor()
    {
      Expires = DateTime.UtcNow.AddHours(config.LifespanHours),
      Issuer = config.Issuer,
      Audience = config.Audience,
      SigningCredentials = new(
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.EmailVerificationSecretKey)),
        SecurityAlgorithms.HmacSha512Signature
      ),
      Claims = claims
    };
  }

  private EmailVerificationSecurityTokenPayload GeneratePayload(string emailAddress)
  {
    return new EmailVerificationSecurityTokenPayload()
    {
      EmailAddress = emailAddress
    };
  }
}
