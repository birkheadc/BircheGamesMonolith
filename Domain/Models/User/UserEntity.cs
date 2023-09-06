using Amazon.DynamoDBv2.DataModel;

namespace Domain.Models;

[DynamoDBTable("BircheGames_Users")]
public class UserEntity
{
  [DynamoDBHashKey("Id")]
  public string? Id { get; init; }
  [DynamoDBGlobalSecondaryIndexHashKey("Username")]
  public string? Username { get; init; }
  [DynamoDBProperty("PasswordHash")]
  public string? PasswordHash { get; init; }
  [DynamoDBProperty("Role")]
  public UserRole Role { get; init; }
  [DynamoDBProperty("IsEmailVerified")]
  public bool IsEmailVerified { get; init; }
}