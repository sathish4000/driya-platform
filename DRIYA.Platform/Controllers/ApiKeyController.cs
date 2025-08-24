using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DRIYA.Platform.Models;
using DRIYA.Platform.Services;

namespace DRIYA.Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApiKey>>> GetApiKeys()
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        IEnumerable<ApiKey> apiKeys;
        if (tenantId.HasValue)
        {
            apiKeys = await _apiKeyService.GetTenantApiKeysAsync(tenantId.Value);
        }
        else if (!string.IsNullOrEmpty(userId))
        {
            apiKeys = await _apiKeyService.GetUserApiKeysAsync(userId);
        }
        else
        {
            return BadRequest("No tenant or user context found");
        }

        // Don't return the actual key hash for security
        var safeApiKeys = apiKeys.Select(ak => new
        {
            ak.Id,
            ak.Name,
            ak.Description,
            ak.IsActive,
            ak.CreatedAt,
            ak.LastUsedAt,
            ak.TotalUsage,
            ak.MonthlyUsage
        });

        return Ok(safeApiKeys);
    }

    [HttpPost]
    public async Task<ActionResult<ApiKey>> CreateApiKey([FromBody] CreateApiKeyRequest request)
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        var apiKey = await _apiKeyService.CreateApiKeyAsync(
            request.Name,
            request.Description,
            tenantId,
            userId);

        // Return the API key with the actual key (this is the only time we'll have it)
        return Ok(new
        {
            apiKey.Id,
            apiKey.Name,
            apiKey.Description,
            apiKey.IsActive,
            apiKey.CreatedAt,
            ApiKey = apiKey.KeyHash // This contains the actual key for this response only
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RevokeApiKey(Guid id)
    {
        var success = await _apiKeyService.RevokeApiKeyAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPost("validate")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> ValidateApiKey([FromBody] ValidateApiKeyRequest request)
    {
        var isValid = await _apiKeyService.ValidateApiKeyAsync(request.ApiKey);
        return Ok(new { IsValid = isValid });
    }

    [HttpGet("{id:guid}/usage")]
    public async Task<ActionResult<object>> GetApiKeyUsage(Guid id)
    {
        // This would require additional service method to get usage statistics
        // For now, return a placeholder
        return Ok(new
        {
            ApiKeyId = id,
            TotalUsage = 0,
            MonthlyUsage = 0,
            LastUsed = DateTime.UtcNow,
            UsageHistory = new object[0]
        });
    }
}

public class CreateApiKeyRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ValidateApiKeyRequest
{
    public string ApiKey { get; set; } = string.Empty;
}
