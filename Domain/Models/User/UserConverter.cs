namespace Domain.Models;

public static class UserConverter
{
  public static UserResponseDTO ToResponseDTO(UserEntity entity)
  {
    return new UserResponseDTO()
    {
      Id = entity.Id,
      EmailAddress = entity.EmailAddress,
      DisplayName = entity.DisplayName,
      Tag = entity.Tag,
      CreationDateTime = entity.CreationDateTime,
      Role = entity.Role,
      IsEmailVerified = entity.IsEmailVerified,
      IsDisplayNameChosen = entity.IsDisplayNameChosen
    };
  }
}