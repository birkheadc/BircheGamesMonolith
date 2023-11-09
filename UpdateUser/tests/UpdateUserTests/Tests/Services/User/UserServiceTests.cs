namespace UpdateUserTests.Tests.Services;

using System.Threading.Tasks;
using Domain.Models;
using UpdateUser.Models;
using UpdateUser.Services;
using UpdateUserTests.Mocks.Repositories;
using Xunit;

public class UserServiceTests
{
  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenUserDoesNotExist()
  {
    UserService userService = new(new UserRepositoryMock_NoUsers());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "abcdefgh",
      Tag = "123456"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenDisplayName_Empty()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "",
      Tag = "123456"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenDisplayName_TooLong()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "abcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefghabcdefgh",
      Tag = "123456"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenDisplayName_ContainsInvalidCharacters()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());
    string[] displayNames = {
      "abcdefgh!",
      "abcdefgh?",
      "abcdefgh#",
      "abcdefgh$",
      "abcdefgh%",
      "abcdefgh/",
      "abcdefgh\\",
      "a\\bcdefgh!",
      "\\abcdefgh!",
      "a:bcdefgh!",
      "abc;defgh!",
      "abcd\"efgh!",
      "abcd'efgh!",
      "abcdeあgh!",
      "abc亜efgh!",
      "1abcdefgh",
      "abcd-efgh",
      "abcdefgh__"
    };

    foreach (string displayName in displayNames)
    {
      PatchDisplayNameAndTagRequest request = new()
      {
        DisplayName = displayName,
        Tag = "123456"
      };
      Response response = await userService.PatchUserDisplayNameAndTag("user", request);
      Assert.False(response.WasSuccess);
    }
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenTag_NotCorrectLength()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "abcdefgh",
      Tag = "12345"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenTag_ContainsInvalidCharacters()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());
    string[] tags = {
      "defgh!",
      "defgh?",
      "defgh#",
      "defgh$",
      "defgh%",
      "defgh/",
      "defgh\\",
      "a\\bjg!",
      "\\aefh!",
      "a:bch!",
      "abc;h!",
      "abcd\"!",
      "abcd'!",
      "agあgh!",
      "abc亜e!"
    };

    foreach (string tag in tags)
    {
      PatchDisplayNameAndTagRequest request = new()
      {
        DisplayName = "abcdefgh",
        Tag = tag
      };
      Response response = await userService.PatchUserDisplayNameAndTag("user", request);
      Assert.False(response.WasSuccess);
    }
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenDisplayNameAndTag_AlreadyChosen()
  {
    UserService userService = new(new UserRepositoryMock_ReturnsUser_WithDisplayNameAndTag_AlreadyChosen());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "abcdefgh",
      Tag = "123456"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Fails_WhenDisplayNameAndTag_NotUnique()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotUnique());
    PatchDisplayNameAndTagRequest request = new()
    {
      DisplayName = "abcdefgh",
      Tag = "123456"
    };
    Response response = await userService.PatchUserDisplayNameAndTag("user", request);
    Assert.False(response.WasSuccess);
  }

  [Fact]
  public async Task PatchUserDisplayNameAndTag_Succeeds_WhenValid_AndNotAlreadyChosen_AndUnique()
  {
    UserService userService = new(new UserRepositoryMock_WithDisplayNameAndTag_NotChosen_AndUnique());

    string[] displayNames = {
      "abcdefgh",
      "a1",
      "steak_sauce",
      "A",
      "a_1_steak_"
    };

    foreach (string displayName in displayNames)
    {
      PatchDisplayNameAndTagRequest request = new()
      {
        DisplayName = displayName,
        Tag = "123456"
      };
      Response response = await userService.PatchUserDisplayNameAndTag("user", request);
      Assert.True(response.WasSuccess);
    }
  }
}