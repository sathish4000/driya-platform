using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DRIYA.Platform.Services;

public class ApiKeyService : IApiKeyService
{
    private readonly ApplicationDbContext _context;

    public ApiKeyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiKey> CreateApiKeyAsync(string name, string description, Guid? tenantId = null, string? userId = null)
    {
        // Generate a secure API key
        var apiKey = GenerateSecureApiKey();
        var keyHash = HashApiKey(apiKey);

        var apiKeyEntity = new ApiKey
        {
            Name = name,
            Description = description,
            KeyHash = keyHash,
            TenantId = tenantId,
            UserId = userId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            LastUsageReset = DateTime.UtcNow
        };

        _context.ApiKeys.Add(apiKeyEntity);
        await _context.SaveChangesAsync();

        // Return the API key with the original key (this is the only time we'll have it)
        apiKeyEntity.KeyHash = apiKey; // Temporarily store the original key for return
        return apiKeyEntity;
    }

    public async Task<ApiKey?> GetApiKeyByHashAsync(string keyHash)
    {
        var hashedKey = HashApiKey(keyHash);
        return await _context.ApiKeys
            .Include(ak => ak.Tenant)
            .Include(ak => ak.User)
            .FirstOrDefaultAsync(ak => ak.KeyHash == hashedKey && ak.IsActive);
    }

    public async Task<IEnumerable<ApiKey>> GetTenantApiKeysAsync(Guid tenantId)
    {
        return await _context.ApiKeys
            .Where(ak => ak.TenantId == tenantId && ak.IsActive)
            .OrderByDescending(ak => ak.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ApiKey>> GetUserApiKeysAsync(string userId)
    {
        return await _context.ApiKeys
            .Where(ak => ak.UserId == userId && ak.IsActive)
            .OrderByDescending(ak => ak.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> RevokeApiKeyAsync(Guid id)
    {
        var apiKey = await _context.ApiKeys.FindAsync(id);
        if (apiKey == null) return false;

        apiKey.IsActive = false;
        apiKey.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateApiKeyAsync(string apiKey)
    {
        var hashedKey = HashApiKey(apiKey);
        var keyEntity = await _context.ApiKeys
            .FirstOrDefaultAsync(ak => ak.KeyHash == hashedKey && ak.IsActive);

        if (keyEntity == null) return false;

        // Check if key has expired
        if (keyEntity.ExpiresAt.HasValue && keyEntity.ExpiresAt.Value < DateTime.UtcNow)
            return false;

        // Update last used timestamp
        keyEntity.LastUsedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task LogApiUsageAsync(Guid apiKeyId, string endpoint, string method, int responseCode, int responseTimeMs)
    {
        var usage = new ApiKeyUsage
        {
            ApiKeyId = apiKeyId,
            Endpoint = endpoint,
            Method = method,
            ResponseCode = responseCode,
            ResponseTimeMs = responseTimeMs,
            Timestamp = DateTime.UtcNow
        };

        _context.ApiKeyUsage.Add(usage);

        // Update API key usage counters
        var apiKey = await _context.ApiKeys.FindAsync(apiKeyId);
        if (apiKey != null)
        {
            apiKey.TotalUsage++;
            apiKey.MonthlyUsage++;

            // Reset monthly usage if it's a new month
            if (apiKey.LastUsageReset?.Month != DateTime.UtcNow.Month)
            {
                apiKey.MonthlyUsage = 1;
                apiKey.LastUsageReset = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();
    }

    private string GenerateSecureApiKey()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var key = new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
        return $"driya_{key}";
    }

    private string HashApiKey(string apiKey)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
        return Convert.ToBase64String(hashBytes);
    }
}
