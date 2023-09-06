using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Domain.Models;

namespace UserRegistration.Repositories;

public class UserRepository : IUserRepository
{
  private IDynamoDBContext context;

  public UserRepository(IAmazonDynamoDB amazonDynamoDB, IWebHostEnvironment webHostEnvironment)
  {
    DynamoDBContextConfig config = new()
    {
      TableNamePrefix = webHostEnvironment.EnvironmentName + "_"
    };
    context = new DynamoDBContext(amazonDynamoDB, config);
  }

  public async Task<StatusCodeResult> CreateNewUser(UserEntity entity)
  {
    if (await IsUsernameUnique(entity.Username) == false)
    {
      return new StatusCodeResult(409);
    }
    try
    {
      await context.SaveAsync(entity);
      return new StatusCodeResult(201);
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Exception when attempting to create new user.");
      Console.WriteLine(e);
      // For now return 9002 so I know it came from here and not the framework.
      return new StatusCodeResult(9002);
    }
  }

  private async Task<bool> IsUsernameUnique(string? username)
  {
    if (username == null) return false;
    try
    {
      DynamoDBOperationConfig config = new()
      {
        IndexName = "Username-index"
      };

      List<UserEntity> users = await context.QueryAsync<UserEntity>(username, config).GetRemainingAsync();

      return users.Count == 0;
    }
    catch (Exception e)
    {
      // Todo: Logging
      Console.WriteLine("Encountered exception when attempting to verify uniqueness of new username.");
      Console.WriteLine(e);
      return false;
    }
  }
}