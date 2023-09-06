using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Authentication.Models;
using Domain.Models;

namespace Authentication.Repositories;

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
  public async Task<UserEntity?> GetUserByCredentials(Credentials credentials)
  {
    DynamoDBOperationConfig config = new()
    {
      IndexName = "Username-index"
    };

    List<UserEntity> users = await context.QueryAsync<UserEntity>(credentials.Username, config).GetRemainingAsync();

    if (users.Count < 1) return null;
    if (users.Count > 1)
    {
      // Todo: Logging
      Console.WriteLine("Found more than 1 user with the same username!");
      return null;
    }

    // Todo: Check password you idiot

    return users[0];
  }
}