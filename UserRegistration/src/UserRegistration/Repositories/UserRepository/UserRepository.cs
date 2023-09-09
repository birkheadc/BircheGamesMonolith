using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Domain.Models;
using UserRegistration.Models;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace UserRegistration.Repositories;

public class UserRepository : IUserRepository
{
  private IDynamoDBContext context;
  private AmazonDynamoDBClient client;

  public UserRepository(IAmazonDynamoDB amazonDynamoDB, IWebHostEnvironment webHostEnvironment)
  {
    DynamoDBContextConfig config = new()
    {
      TableNamePrefix = webHostEnvironment.EnvironmentName + "_"
    };
    context = new DynamoDBContext(amazonDynamoDB, config);
  }

  public async Task<CreateUserResponse> CreateNewUser(UserEntity entity)
  {
    bool isEmailAddressUnique = await IsEmailAddressUnique(entity.EmailAddress);
    CreateUserResponse response = new();
    
    if (await IsDisplayNameUnique(entity.DisplayName, entity.Tag) == false)
    {
      response.WasSuccess = false;
      response.Errors.Add(new() { Field = "DisplayName", StatusCode = 409, ErrorMessage = "Display Name is already in use." });
    }

    if (isEmailAddressUnique == false)
    {
      // As far as the rest of the pipeline is concerned, the Email Address was unique. We do not let the user know when they
      // attempt to use an already in-use Email Address, as this can allow attackers to build a list of used Email Addresses.
      // Instead, we just alert the user who is already registered  that someone is trying to use their Email Address.
      // If there are no other errors, the new user will think their form was submitted successfully, and be prompted to check their Email.

      // Todo: Logging
      // Todo: Alert the email service that a new user attempted to register with an existing email address.
      // Todo: Also, create the email service lol.
      return response;
    }
    if (response.WasSuccess == false)
    {
      return response;
    }

    try
    {
      await context.SaveAsync(entity);
      return response;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception when attempting to create new user.");
      Console.WriteLine(e);
      response.Errors.Add(new(){ Field = "", StatusCode = 0 });
      return response;
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

  private async Task<bool> IsDisplayNameUnique(string displayName, string tag)
  {
    try
    {
      DynamoDBOperationConfig config = new()
      {
        IndexName = "DisplayName-Tag-index",
      };

      List<UserEntity> users = await context.QueryAsync<UserEntity>(displayName, QueryOperator.Equal, new object[] { tag }, config).GetRemainingAsync();
      Console.WriteLine(users.Count == 0 ? $"Didn't find user with DisplayName {displayName} and Tag {tag}" : $"Found user with DisplayName {displayName} and Tag {tag}");

      return users.Count == 0;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Encountered exception when attempting to verify uniqueness of display name.");
      Console.WriteLine(e);
      return false;
    }
  }
}