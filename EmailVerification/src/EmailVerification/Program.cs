using Amazon.DynamoDBv2;
using Amazon.SimpleEmail;
using Domain.Config;
using EmailVerification.Config;
using EmailVerification.Repositories;
using EmailVerification.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
// Add services to the container.

services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
services.AddAWSService<IAmazonDynamoDB>();
services.AddAWSService<IAmazonSimpleEmailService>();

EmailVerificationConfig emailVerificationConfig = new();
config.GetSection("EmailVerificationConfig").Bind(emailVerificationConfig);
emailVerificationConfig.EmailVerificationSecretKey = AmazonSecretRetriever.GetEmailVerificationSecret();
services.AddSingleton(emailVerificationConfig);

services.AddSingleton<IEmailSender, EmailSender>();
services.AddSingleton<ITokenValidator, TokenValidator>();
services.AddSingleton<ITokenGenerator, TokenGenerator>();
services.AddSingleton<IUserRepository, UserRepository>();
services.AddSingleton<IEmailService, EmailService>();

services.AddCors(o => 
{
  o.AddPolicy(name: "All", builder =>
  {
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
  });
});


var app = builder.Build();

app.UseCors("All");

if (app.Environment.IsDevelopment() == false)
{
  app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

Console.WriteLine($"Email Verification API launched in {app.Environment.EnvironmentName} mode.");

app.Run();