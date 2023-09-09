using Amazon.DynamoDBv2.DataModel;

namespace Domain.Models;

[DynamoDBTable("BircheGames_Users")]
public class UserEntity
{
  [DynamoDBHashKey]
  public string Id { get; init; } = "";

  [DynamoDBGlobalSecondaryIndexHashKey]
  public string EmailAddress { get; init; } = "";

  [DynamoDBGlobalSecondaryIndexHashKey]
  public string DisplayName { get; init; } = "";
  [DynamoDBGlobalSecondaryIndexRangeKey]
  public string Tag { get; init; } = "";
  [DynamoDBProperty]
  public string CreationDateTime { get; init; } = "";
  [DynamoDBProperty]
  public string PasswordHash { get; init; } = "";

  [DynamoDBProperty]
  public UserRole Role { get; init; }
  
  [DynamoDBProperty]
  public bool IsEmailVerified { get; init; }
}