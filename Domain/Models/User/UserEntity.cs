using Amazon.DynamoDBv2.DataModel;

namespace Domain.Models;

[DynamoDBTable("BircheGames_Users")]
public class UserEntity
{
  [DynamoDBHashKey("id")]
  public Guid? Id { get; init; }
  [DynamoDBProperty("username")]
  public string? Username { get; init; }
  [DynamoDBProperty("passwordHash")]
  public string? PasswordHash { get; init; }
  [DynamoDBProperty("role")]
  public UserRole Role { get; init; }
  [DynamoDBProperty("isEmailVerified")]
  public bool IsEmailVerified { get; init; }
}