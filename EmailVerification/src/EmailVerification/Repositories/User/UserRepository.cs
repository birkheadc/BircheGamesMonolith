using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Domain.Models;

namespace EmailVerification.Repositories;

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
  public async Task<UserEntity?> GetUserByEmailAddress(string emailAddress)
  {
    DynamoDBOperationConfig config = new()
    {
      IndexName = "EmailAddress-index"
    };

    List<UserEntity> users = await context.QueryAsync<UserEntity>(emailAddress, config).GetRemainingAsync();

    if (users.Count < 1) return null;
    if (users.Count > 1)
    {
      // Todo: Logging
      Console.WriteLine("Found more than 1 user with the same email!");
      return null;
    }

    return users[0];
  }

  public Task<bool> UpdateUserEmailVerifiedToTrue(string id)
  {
    throw new NotImplementedException();
  }
}