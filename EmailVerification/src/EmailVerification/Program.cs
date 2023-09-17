using Amazon.DynamoDBv2;
using EmailVerification.Repositories;
using EmailVerification.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
services.AddAWSService<IAmazonDynamoDB>();

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

app.Run();
