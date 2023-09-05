using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using UserRegistration.Models;

namespace UserRegistration.Repositories;

public class UserRepository : IUserRepository
{
  private IDynamoDBContext context;

  public UserRepository(IDynamoDBContext context)
  {
    this.context = context;
  }

  public async Task<StatusCodeResult> CreateNewUser(UserEntity entity)
  {
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
}