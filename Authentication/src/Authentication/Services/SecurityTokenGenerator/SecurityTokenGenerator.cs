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
    Dictionary<string, object> claims = new()
    {
      { "user", GeneratePayloadForUser(user) }
    };
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

  private SecurityTokenPayload GeneratePayloadForUser(UserEntity user)
  {
    return new SecurityTokenPayload()
    {
      Id = user.Id,
      EmailAddress = user.EmailAddress,
      DisplayName = user.DisplayName,
      Tag = user.Tag,
      CreationDateTime = user.CreationDateTime,
      Role = user.Role,
      IsEmailVerified = user.IsEmailVerified,
      IsDisplayNameChosen = user.IsDisplayNameChosen
    };
  }
}