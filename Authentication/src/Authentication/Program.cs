using Amazon.DynamoDBv2;
using Authentication.Services;
using Authentication.Repositories;
using Authentication.Config;
using Domain.Config;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
services.AddAWSService<IAmazonDynamoDB>();

SecurityTokenConfig securityTokenConfig = new();
config.GetSection("SecurityTokenConfig").Bind(securityTokenConfig);
securityTokenConfig.SecretKey = AmazonSecretRetriever.GetAuthenticationSecret();
services.AddSingleton(securityTokenConfig);

// Todo: AWS secret handling for security token config

services.AddSingleton<IPasswordValidator, PasswordValidator>();
services.AddSingleton<ISecurityTokenGenerator, SecurityTokenGenerator>();
services.AddSingleton<ISecurityTokenService, SecurityTokenService>();
services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() == false)
{
  app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "You've reached the authentication service.");

app.Run();