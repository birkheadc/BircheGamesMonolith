using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using UserRegistration.Config;
using UserRegistration.Repositories;
using UserRegistration.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
// Add services to the container.
services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
services.AddAWSService<IAmazonDynamoDB>();

UserValidatorConfig userValidatorConfig = new();
config.GetSection("UserValidatorConfig").Bind(userValidatorConfig);
services.AddSingleton(userValidatorConfig);

services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddSingleton<IUserConverter, UserConverter>();
services.AddSingleton<IUserValidator, UserValidator>();
services.AddSingleton<IUserRepository, UserRepository>();
services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment() == false)
{
  app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "You've reached the user registration api.");

app.Run();
