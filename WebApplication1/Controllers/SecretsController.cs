using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;

namespace WebApplication1.Controllers;

[ApiController]
[Route("secrets")]
public class SecretsController : ControllerBase
{
    private readonly KeyVaultReader _keyVaultReader;

    public SecretsController(KeyVaultReader keyVaultReader)
    {
        _keyVaultReader = keyVaultReader;
    }

    [HttpGet("{secretName}")]
    public IActionResult GetSecret(string secretName)
    {
        var secret = _keyVaultReader.GetSecret(secretName);
        return Ok(secret);
    }
}