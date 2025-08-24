using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DRIYA.Platform.Models;
using DRIYA.Platform.Services;

namespace DRIYA.Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FeatureController : ControllerBase
{
    private readonly IFeatureService _featureService;

    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feature>>> GetAllFeatures()
    {
        var features = await _featureService.GetAllFeaturesAsync();
        return Ok(features);
    }

    [HttpGet("check/{featureKey}")]
    public async Task<ActionResult<bool>> IsFeatureEnabled(string featureKey)
    {
        // Get tenant ID from context (set by middleware)
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        var isEnabled = await _featureService.IsFeatureEnabledAsync(featureKey, tenantId, userId);
        return Ok(new { FeatureKey = featureKey, IsEnabled = isEnabled });
    }

    [HttpGet("flags")]
    [Authorize(Policy = "TenantAdmin")]
    public async Task<ActionResult<IEnumerable<FeatureFlag>>> GetFeatureFlags()
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        if (!tenantId.HasValue)
            return BadRequest("Tenant context not found");

        var flags = await _featureService.GetTenantFeatureFlagsAsync(tenantId.Value);
        return Ok(flags);
    }

    [HttpPost("flags/{featureKey}")]
    [Authorize(Policy = "TenantAdmin")]
    public async Task<ActionResult<FeatureFlag>> SetFeatureFlag(string featureKey, [FromBody] SetFeatureFlagRequest request)
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = request.UserId; // Optional user-specific override

        var flag = await _featureService.SetFeatureFlagAsync(featureKey, request.Value, tenantId, userId);
        return Ok(flag);
    }

    [HttpGet("flags/{featureKey}")]
    public async Task<ActionResult<FeatureFlag>> GetFeatureFlag(string featureKey)
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        var flag = await _featureService.GetFeatureFlagAsync(featureKey, tenantId, userId);
        if (flag == null)
            return NotFound();

        return Ok(flag);
    }

    [HttpDelete("flags/{featureKey}")]
    [Authorize(Policy = "TenantAdmin")]
    public async Task<ActionResult> RemoveFeatureFlag(string featureKey)
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        // Set the flag to its default value (effectively removing the override)
        var feature = (await _featureService.GetAllFeaturesAsync()).FirstOrDefault(f => f.FeatureKey == featureKey);
        if (feature == null)
            return NotFound();

        await _featureService.SetFeatureFlagAsync(featureKey, feature.DefaultValue ?? "false", tenantId, userId);
        return NoContent();
    }

    [HttpGet("bulk-check")]
    public async Task<ActionResult<object>> CheckMultipleFeatures([FromQuery] string[] featureKeys)
    {
        var tenantId = HttpContext.Items["TenantId"] as Guid?;
        var userId = User.Identity?.Name;

        var results = new Dictionary<string, bool>();
        foreach (var featureKey in featureKeys)
        {
            results[featureKey] = await _featureService.IsFeatureEnabledAsync(featureKey, tenantId, userId);
        }

        return Ok(results);
    }
}

public class SetFeatureFlagRequest
{
    public string Value { get; set; } = string.Empty;
    public string? UserId { get; set; } // Optional: for user-specific overrides
}
