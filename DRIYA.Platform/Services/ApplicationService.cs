using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Logging;

namespace DRIYA.Platform.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ApplicationService(ApplicationDbContext context, Microsoft.Extensions.Logging.ILogger<ApplicationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            try
            {
                return await _context.Applications
                    .Include(a => a.Tenants)
                    .Include(a => a.Features)
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all applications");
                throw;
            }
        }

        public async Task<Application?> GetApplicationByIdAsync(Guid id)
        {
            try
            {
                return await _context.Applications
                    .Include(a => a.Tenants)
                    .Include(a => a.Features)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving application by ID: {ApplicationId}", id);
                throw;
            }
        }

        public async Task<Application?> GetApplicationByAppKeyAsync(string appKey)
        {
            try
            {
                return await _context.Applications
                    .Include(a => a.Tenants)
                    .Include(a => a.Features)
                    .FirstOrDefaultAsync(a => a.AppKey == appKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving application by app key: {AppKey}", appKey);
                throw;
            }
        }

        public async Task<Application> CreateApplicationAsync(Application application)
        {
            try
            {
                application.CreatedAt = DateTime.UtcNow;
                application.UpdatedAt = DateTime.UtcNow;

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created new application: {ApplicationName} with key: {AppKey}", 
                    application.Name, application.AppKey);

                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating application: {ApplicationName}", application.Name);
                throw;
            }
        }

        public async Task<Application> UpdateApplicationAsync(Application application)
        {
            try
            {
                application.UpdatedAt = DateTime.UtcNow;

                _context.Applications.Update(application);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Updated application: {ApplicationName} with key: {AppKey}", 
                    application.Name, application.AppKey);

                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application: {ApplicationName}", application.Name);
                throw;
            }
        }

        public async Task<bool> DeleteApplicationAsync(Guid id)
        {
            try
            {
                var application = await _context.Applications.FindAsync(id);
                if (application == null)
                {
                    return false;
                }

                // Check if application has tenants
                var hasTenants = await _context.Tenants.AnyAsync(t => t.ApplicationId == id);
                if (hasTenants)
                {
                    _logger.LogWarning("Cannot delete application {ApplicationName} - it has associated tenants", application.Name);
                    return false;
                }

                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Deleted application: {ApplicationName} with key: {AppKey}", 
                    application.Name, application.AppKey);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting application with ID: {ApplicationId}", id);
                throw;
            }
        }

        public async Task<bool> IsAppKeyUniqueAsync(string appKey, Guid? excludeId = null)
        {
            try
            {
                var query = _context.Applications.Where(a => a.AppKey == appKey);
                
                if (excludeId.HasValue)
                {
                    query = query.Where(a => a.Id != excludeId.Value);
                }

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking app key uniqueness: {AppKey}", appKey);
                throw;
            }
        }

        public async Task<bool> IsSubdomainUniqueAsync(string subdomain, Guid? excludeId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(subdomain))
                {
                    return true; // Empty subdomain is allowed
                }

                var query = _context.Applications.Where(a => a.Subdomain == subdomain);
                
                if (excludeId.HasValue)
                {
                    query = query.Where(a => a.Id != excludeId.Value);
                }

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subdomain uniqueness: {Subdomain}", subdomain);
                throw;
            }
        }
    }
}
