using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UpdateUser.Config;
using Domain.Config;
using System.Text;
using UpdateUser.Services;
using UpdateUser.Repositories;
using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
services.AddAWSService<IAmazonDynamoDB>();

SecurityTokenConfig securityTokenConfig = new();
config.GetSection("SecurityTokenConfig").Bind(securityTokenConfig);
securityTokenConfig.SecretKey = AmazonSecretRetriever.GetAuthenticationSecret();
services.AddSingleton(securityTokenConfig);

services.AddSingleton<IUserService, UserService>();
services.AddSingleton<IUserRepository, UserRepository>();

services.AddCors(o =>
{
  o.AddPolicy(name: "All", builder =>
  {
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
  });
});

services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidIssuer = securityTokenConfig.Issuer,
    ValidAudience = securityTokenConfig.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityTokenConfig.SecretKey)),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true
  };
});

services.AddAuthorization();

var app = builder.Build();

app.UseCors("All");

if (app.Environment.IsDevelopment() == false)
{
  app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"Update User API launched in {app.Environment.EnvironmentName} mode.");

app.Run();
