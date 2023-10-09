using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Domain.Models;

namespace UserRegistration.Repositories;

public class UserRepository : IUserRepository
{
  private readonly IDynamoDBContext context;

  public UserRepository(IAmazonDynamoDB amazonDynamoDB, IWebHostEnvironment webHostEnvironment)
  {
    DynamoDBContextConfig config = new()
    {
      TableNamePrefix = webHostEnvironment.EnvironmentName + "_"
    };
    context = new DynamoDBContext(amazonDynamoDB, config);
  }

  public async Task<Response> CreateNewUser(UserEntity entity)
  {
    bool isEmailAddressUnique = await IsEmailAddressUnique(entity.EmailAddress);
    ResponseBuilder responseBuilder = new();

    if (isEmailAddressUnique == false)
    {
      // As far as the rest of the pipeline is concerned, the Email Address was unique. We do not let the user know when they
      // attempt to use an already in-use Email Address, as this can allow attackers to build a list of used Email Addresses.
      // Instead, we just alert the user who is already registered  that someone is trying to use their Email Address.
      // If there are no other errors, the new user will think their form was submitted successfully, and be prompted to check their Email.

      // Todo: Logging
      // Todo: Alert the email service that a new user attempted to register with an existing email address.
      return responseBuilder
        .Succeed()
        .Build();
    }

    try
    {
      await context.SaveAsync(entity);
      return responseBuilder
        .Succeed()
        .Build();
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception when attempting to create new user.");
      Console.WriteLine(e);
      return responseBuilder
        .Fail()
        .WithGeneralError(0, "Something unexpected happened.")
        .Build();
    }
  }

  private async Task<bool> IsEmailAddressUnique(string emailAddress)
  {
    try
    {
      DynamoDBOperationConfig config = new()
      {
        IndexName = "EmailAddress-index"
      };

      List<UserEntity> users = await context.QueryAsync<UserEntity>(emailAddress, config).GetRemainingAsync();

      return users.Count == 0;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Encountered exception when attempting to verify uniqueness of email address.");
      Console.WriteLine(e);
      return false;
    }
  }
}