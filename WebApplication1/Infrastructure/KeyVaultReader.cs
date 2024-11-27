using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace WebApplication1.Infrastructure;

public class KeyVaultReader
{
    private readonly SecretClient _secretClient;

    public KeyVaultReader(IConfiguration configuration)
    {
        var keyVaultUri = new Uri(configuration["KeyVault:Uri"]);
        _secretClient = new SecretClient(vaultUri: keyVaultUri, credential: new DefaultAzureCredential());
    }

    public string GetSecret(string secretName)
    {
        try
        {
            KeyVaultSecret secret = _secretClient.GetSecret(secretName);
            return secret.Value;
        }
        catch (Exception ex)
        {
            // Log or handle exceptions appropriately
            throw new Exception($"Error retrieving secret {secretName}: {ex.Message}", ex);
        }
    }
}