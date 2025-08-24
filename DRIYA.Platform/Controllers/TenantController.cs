using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DRIYA.Platform.Models;
using DRIYA.Platform.Services;
using DRIYA.Platform.Data;
using Microsoft.EntityFrameworkCore;

namespace DRIYA.Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;
    private readonly ApplicationDbContext _context;

    public TenantController(ITenantService tenantService, ApplicationDbContext context)
    {
        _tenantService = tenantService;
        _context = context;
    }

    [HttpGet]
    [Authorize(Policy = "GlobalAdmin")]
    public async Task<ActionResult<IEnumerable<Tenant>>> GetAllTenants()
    {
        var tenants = await _tenantService.GetAllTenantsAsync();
        return Ok(tenants);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Tenant>> GetTenant(Guid id)
    {
        var tenant = await _tenantService.GetTenantByIdAsync(id);
        if (tenant == null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpGet("by-tenant-id/{tenantId}")]
    public async Task<ActionResult<Tenant>> GetTenantByTenantId(string tenantId)
    {
        var tenant = await _tenantService.GetTenantByTenantIdAsync(tenantId);
        if (tenant == null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpPost]
    [Authorize(Policy = "GlobalAdmin")]
    public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantRequest request)
    {
        var tenant = new Tenant
        {
            Name = request.Name,
            TenantId = request.TenantId,
            Description = request.Description,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            IsActive = true,
            IsTrialActive = request.AllowTrial,
            TrialEndDate = request.AllowTrial ? DateTime.UtcNow.AddDays(30) : null,
            CreatedBy = User.Identity?.Name
        };

        var createdTenant = await _tenantService.CreateTenantAsync(tenant);
        return CreatedAtAction(nameof(GetTenant), new { id = createdTenant.Id }, createdTenant);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "GlobalAdmin")]
    public async Task<ActionResult<Tenant>> UpdateTenant(Guid id, [FromBody] UpdateTenantRequest request)
    {
        var tenant = await _tenantService.GetTenantByIdAsync(id);
        if (tenant == null)
            return NotFound();

        tenant.Name = request.Name ?? tenant.Name;
        tenant.Description = request.Description ?? tenant.Description;
        tenant.ContactEmail = request.ContactEmail ?? tenant.ContactEmail;
        tenant.ContactPhone = request.ContactPhone ?? tenant.ContactPhone;
        tenant.IsActive = request.IsActive ?? tenant.IsActive;
        tenant.UpdatedBy = User.Identity?.Name;

        var updatedTenant = await _tenantService.UpdateTenantAsync(tenant);
        return Ok(updatedTenant);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "GlobalAdmin")]
    public async Task<ActionResult> DeleteTenant(Guid id)
    {
        var success = await _tenantService.DeleteTenantAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpGet("current")]
    public async Task<ActionResult<Tenant>> GetCurrentTenant()
    {
        var tenant = await _tenantService.GetCurrentTenantAsync();
        if (tenant == null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpGet("{id:guid}/users")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetTenantUsers(Guid id)
    {
        var users = await _context.Users
            .Where(u => u.TenantId == id && u.IsActive)
            .Select(u => new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.UserName,
                u.IsActive,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:guid}/statistics")]
    public async Task<ActionResult<object>> GetTenantStatistics(Guid id)
    {
        var userCount = await _context.Users.CountAsync(u => u.TenantId == id && u.IsActive);
        var invoiceCount = await _context.Invoices.CountAsync(i => i.TenantId == id);
        var activeApiKeys = await _context.ApiKeys.CountAsync(ak => ak.TenantId == id && ak.IsActive);

        return Ok(new
        {
            UserCount = userCount,
            InvoiceCount = invoiceCount,
            ActiveApiKeys = activeApiKeys,
            LastUpdated = DateTime.UtcNow
        });
    }
}

public class CreateTenantRequest
{
    public string Name { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ContactEmail { get; set; } = string.Empty;
    public string? ContactPhone { get; set; }
    public bool AllowTrial { get; set; } = true;
}

public class UpdateTenantRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool? IsActive { get; set; }
}
