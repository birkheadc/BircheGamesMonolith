using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Domain.Models;

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
}