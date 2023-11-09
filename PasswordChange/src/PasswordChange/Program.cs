using Amazon.DynamoDBv2;
using Amazon.SimpleEmail;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
services.AddAWSService<IAmazonDynamoDB>();
services.AddAWSService<IAmazonSimpleEmailService>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "You've reached the Password Change API.");

Console.WriteLine($"Password Change API launched in {app.Environment.EnvironmentName} mode.");

app.Run();
