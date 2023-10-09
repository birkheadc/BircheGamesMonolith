using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Models;
using UpdateUser.Models;

namespace UpdateUser.Repositories;

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
  public async Task<UserEntity?> GetUser(string id)
  {
    return await context.LoadAsync<UserEntity>(id);
  }

  public async Task<bool> IsDisplayNameAndTagAvailable(PatchDisplayNameAndTagRequest displayNameAndTag)
  {
    DynamoDBOperationConfig config = new()
    {
      IndexName = "DisplayName-Tag-index",
      
    };
    try
    {
      List<UserEntity> users = await context.QueryAsync<UserEntity>(displayNameAndTag.DisplayName, QueryOperator.Equal, new List<object>(){displayNameAndTag.Tag}, config).GetRemainingAsync();
      if (users.Count == 0)
      {
        return true;
      }
      Console.WriteLine($"Found {users.Count} users with name {displayNameAndTag.DisplayName}#{displayNameAndTag.Tag}: {users[0].Id}");
      return false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Encountered exception while attempting to lookup user by displayname and tag: {ex.Message}");
      return false;
    }
  }

  public async Task UpdateUser(UserEntity user)
  {
    await context.SaveAsync(user);
  }
}