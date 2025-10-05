using DRIYA.Platform.Models;
using DRIYA.Platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.Extensions.Logging;

namespace DRIYA.Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "GlobalAdmin")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ApplicationController(IApplicationService applicationService, Microsoft.Extensions.Logging.ILogger<ApplicationController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            try
            {
                var applications = await _applicationService.GetAllApplicationsAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving applications");
                return StatusCode(500, new { message = "An error occurred while retrieving applications" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(Guid id)
        {
            try
            {
                var application = await _applicationService.GetApplicationByIdAsync(id);
                if (application == null)
                {
                    return NotFound(new { message = "Application not found" });
                }

                return Ok(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving application with ID: {ApplicationId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the application" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if app key is unique
                if (!await _applicationService.IsAppKeyUniqueAsync(request.AppKey))
                {
                    return BadRequest(new { message = "App key must be unique" });
                }

                // Check if subdomain is unique (if provided)
                if (!string.IsNullOrEmpty(request.Subdomain) && 
                    !await _applicationService.IsSubdomainUniqueAsync(request.Subdomain))
                {
                    return BadRequest(new { message = "Subdomain must be unique" });
                }

                var application = new Application
                {
                    Name = request.Name,
                    Description = request.Description,
                    AppKey = request.AppKey,
                    Domain = request.Domain,
                    Subdomain = request.Subdomain,
                    IsActive = request.IsActive,
                    Status = request.Status ?? "Active",
                    Icon = request.Icon,
                    PrimaryColor = request.PrimaryColor ?? "#3b82f6"
                };

                var createdApplication = await _applicationService.CreateApplicationAsync(application);
                return CreatedAtAction(nameof(GetApplication), new { id = createdApplication.Id }, createdApplication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating application: {ApplicationName}", request.Name);
                return StatusCode(500, new { message = "An error occurred while creating the application" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] UpdateApplicationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingApplication = await _applicationService.GetApplicationByIdAsync(id);
                if (existingApplication == null)
                {
                    return NotFound(new { message = "Application not found" });
                }

                // Check if app key is unique (excluding current application)
                if (existingApplication.AppKey != request.AppKey && 
                    !await _applicationService.IsAppKeyUniqueAsync(request.AppKey, id))
                {
                    return BadRequest(new { message = "App key must be unique" });
                }

                // Check if subdomain is unique (excluding current application)
                if (existingApplication.Subdomain != request.Subdomain && 
                    !string.IsNullOrEmpty(request.Subdomain) && 
                    !await _applicationService.IsSubdomainUniqueAsync(request.Subdomain, id))
                {
                    return BadRequest(new { message = "Subdomain must be unique" });
                }

                existingApplication.Name = request.Name;
                existingApplication.Description = request.Description;
                existingApplication.AppKey = request.AppKey;
                existingApplication.Domain = request.Domain;
                existingApplication.Subdomain = request.Subdomain;
                existingApplication.IsActive = request.IsActive;
                existingApplication.Status = request.Status ?? "Active";
                existingApplication.Icon = request.Icon;
                existingApplication.PrimaryColor = request.PrimaryColor ?? "#3b82f6";

                var updatedApplication = await _applicationService.UpdateApplicationAsync(existingApplication);
                return Ok(updatedApplication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application with ID: {ApplicationId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the application" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            try
            {
                var result = await _applicationService.DeleteApplicationAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Application not found or cannot be deleted" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting application with ID: {ApplicationId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the application" });
            }
        }

        [HttpGet("check-app-key/{appKey}")]
        public async Task<IActionResult> CheckAppKeyUnique(string appKey, [FromQuery] Guid? excludeId = null)
        {
            try
            {
                var isUnique = await _applicationService.IsAppKeyUniqueAsync(appKey, excludeId);
                return Ok(new { isUnique });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking app key uniqueness: {AppKey}", appKey);
                return StatusCode(500, new { message = "An error occurred while checking app key uniqueness" });
            }
        }

        [HttpGet("check-subdomain/{subdomain}")]
        public async Task<IActionResult> CheckSubdomainUnique(string subdomain, [FromQuery] Guid? excludeId = null)
        {
            try
            {
                var isUnique = await _applicationService.IsSubdomainUniqueAsync(subdomain, excludeId);
                return Ok(new { isUnique });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subdomain uniqueness: {Subdomain}", subdomain);
                return StatusCode(500, new { message = "An error occurred while checking subdomain uniqueness" });
            }
        }
    }

    public class CreateApplicationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string AppKey { get; set; } = string.Empty;
        public string? Domain { get; set; }
        public string? Subdomain { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Status { get; set; }
        public string? Icon { get; set; }
        public string? PrimaryColor { get; set; }
    }

    public class UpdateApplicationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string AppKey { get; set; } = string.Empty;
        public string? Domain { get; set; }
        public string? Subdomain { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Status { get; set; }
        public string? Icon { get; set; }
        public string? PrimaryColor { get; set; }
    }
}
