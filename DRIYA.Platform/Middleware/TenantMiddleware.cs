using DRIYA.Platform.Services;
using Microsoft.AspNetCore.Http;

namespace DRIYA.Platform.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Extract tenant information from various sources
        var tenantId = ExtractTenantId(context);
        
        if (!string.IsNullOrEmpty(tenantId))
        {
            // Resolve the scoped service from the request scope
            var tenantService = context.RequestServices.GetRequiredService<ITenantService>();
            
            // Validate tenant and set context
            var tenant = await tenantService.GetTenantByTenantIdAsync(tenantId);
            if (tenant != null)
            {
                context.Items["CurrentTenant"] = tenant;
                context.Items["TenantId"] = tenant.Id;
            }
        }

        await _next(context);
    }

    private string? ExtractTenantId(HttpContext context)
    {
        // Priority order for tenant identification:
        // 1. Custom header
        // 2. Subdomain
        // 3. Query parameter
        // 4. Route parameter

        // 1. Check custom header
        var tenantId = context.Request.Headers["X-Tenant-ID"].FirstOrDefault();
        if (!string.IsNullOrEmpty(tenantId))
            return tenantId;

        // 2. Check subdomain
        var host = context.Request.Host.Host;
        if (host.Contains('.'))
        {
            var subdomain = host.Split('.')[0];
            if (subdomain != "www" && subdomain != "api" && subdomain != "admin")
                return subdomain;
        }

        // 3. Check query parameter
        tenantId = context.Request.Query["tenant"].FirstOrDefault();
        if (!string.IsNullOrEmpty(tenantId))
            return tenantId;

        // 4. Check route parameter (if using route-based tenancy)
        if (context.Request.RouteValues.TryGetValue("tenant", out var routeTenant))
            return routeTenant?.ToString();

        return null;
    }
}
