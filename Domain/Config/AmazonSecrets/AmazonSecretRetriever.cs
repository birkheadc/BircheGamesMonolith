using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Domain.Models;

namespace Domain.Config;

public static class AmazonSecretRetriever
{
  public static string GetAuthenticationSecret()
  {
    return GetSecrets("ap-southeast-2", "BircheGames/Authentication/SecurityTokenConfig/SecretKey").AuthenticationSecretKey;
  }
  public static string GetEmailVerificationSecret()
  {
    return GetSecrets("ap-southeast-2", "BircheGames/Authentication/SecurityTokenConfig/SecretKey").EmailVerificationSecretKey;
  }
  public static AmazonSecrets GetSecrets(string region, string secretName)
  {
    IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
    GetSecretValueRequest request = new()
    {
      SecretId = secretName,
      VersionStage = "AWSCURRENT"
    };

    AmazonSecrets secrets = new();

    try
    {
      GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;
      if (response.SecretString is not null)
      {
        secrets = JsonSerializer.Deserialize<AmazonSecrets>(response.SecretString) ?? secrets;
      }
      else
      {
        // var memoryStream = response.SecretBinary;
        // var reader = new StreamReader(memoryStream);
        // secretString = System.Text.UTF8Encoding.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        // secretString = "bad_secret";
      }
      // Console.WriteLine($"Got secret string: {secretString}");
      return secrets;
    }
    catch (Exception ex)
    {
      // Todo: Logging
      Console.WriteLine("Encountered exception while attempting to retrieve amazon secret.");
      Console.WriteLine(ex);
      return secrets;
    }
  }
}