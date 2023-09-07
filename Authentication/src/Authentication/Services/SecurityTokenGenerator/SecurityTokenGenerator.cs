using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Authentication.Config;
using Authentication.Models;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services;

public class SecurityTokenGenerator : ISecurityTokenGenerator
{
  private readonly SecurityTokenConfig config;
  private readonly JwtSecurityTokenHandler handler;

  public SecurityTokenGenerator(SecurityTokenConfig config)
  {
    this.config = config;
    handler = new();
  }

  public SecurityTokenWrapper GenerateTokenForUser(UserEntity user)
  {
    SecurityToken token = handler.CreateToken(GetSecurityTokenDescriptor(user));
    return new SecurityTokenWrapper()
    {
      Token = handler.WriteToken(token)
    };
  }

  private SecurityTokenDescriptor GetSecurityTokenDescriptor(UserEntity user)
  {
    Dictionary<string, object> claims = new();
    claims.Add("user", user);
    return new SecurityTokenDescriptor()
    {
      Expires = DateTime.UtcNow.AddHours(config.LifespanHours),
      Issuer = config.Issuer,
      Audience = config.Audience,
      SigningCredentials = new(
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.SecretKey)),
        SecurityAlgorithms.HmacSha512Signature
      ),
      Claims = claims
    };
  }
}