namespace UpdateUser.Models;

public class PatchDisplayNameAndTagRequest
{
  public string DisplayName { get; init; } = "";
  public string Tag { get; init; } = "";
}