using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Domain.Config;

public static class AmazonSecretRetriever
{
  public static string GetAuthenticationSecret()
  {
    return GetSecret("ap-southeast-2", "BircheGames/Authentication/SecurityTokenConfig/SecretKey");
  }
  public static string GetSecret(string region, string secretName)
  {
    IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
    GetSecretValueRequest request = new()
    {
      SecretId = secretName,
      VersionStage = "AWSCURRENT"
    };

    string secretString;

    try
    {
      GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;
      if (response.SecretString is not null)
      {
        secretString = response.SecretString;
      }
      else
      {
        // var memoryStream = response.SecretBinary;
        // var reader = new StreamReader(memoryStream);
        // secretString = System.Text.UTF8Encoding.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        secretString = "bad_secret";
      }
      return secretString;
    }
    catch (Exception ex)
    {
      // Todo: Logging
      Console.WriteLine("Encountered exception while attempting to retrieve amazon secret.");
      Console.WriteLine(ex);
      return "bad_secret";
    }
  }
}