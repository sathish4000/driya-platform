using Microsoft.EntityFrameworkCore;
using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using DRIYA.Platform.Services;
using DRIYA.Platform.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DRIYA.Platform;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build())
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

        try
        {
            Log.Information("Starting DRIYA Platform");
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://localhost:7001")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });

                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Configure database
            var databaseType = builder.Configuration["Database:Type"]; // "PostgreSQL" or "SQLServer"

            if (databaseType == "PostgreSQL")
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
            }
            else if (databaseType == "SQLServer")
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));
            }

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, Role>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configure Redis caching
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["Redis:ConnectionString"];
                options.InstanceName = "DRIYA_Platform";
            });

            // Add services to the container
            builder.Services.AddControllers();

            // Add API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "DRIYA Platform API", Version = "v1" });
                
                // Add JWT authentication to Swagger
                c.AddSecurityDefinition("Bearer", new()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!"))
                };
            });

            // Add authorization
            builder.Services.AddAuthorization(options =>
            {
                // Add policy for global admins
                options.AddPolicy("GlobalAdmin", policy =>
                    policy.RequireRole("GlobalAdmin"));

                // Add policy for tenant admins
                options.AddPolicy("TenantAdmin", policy =>
                    policy.RequireRole("TenantAdmin", "GlobalAdmin"));

                // Add policy for managers
                options.AddPolicy("Manager", policy =>
                    policy.RequireRole("Manager", "TenantAdmin", "GlobalAdmin"));
            });

            // Add HTTP context accessor for multi-tenancy
            builder.Services.AddHttpContextAccessor();

            // Add application services
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<IBillingService, BillingService>();
            builder.Services.AddScoped<IApiKeyService, ApiKeyService>();

            var app = builder.Build();

            // Do not run migrations automatically in the EF tool context
            if (!EF.IsDesignTime)
            {
                // Ensure database is created and migrated
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                    
                    // Seed initial data if needed
                    await SeedInitialDataAsync(context, scope.ServiceProvider);
                }
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DRIYA Platform API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add multi-tenant middleware
            app.UseMiddleware<TenantMiddleware>();

            app.MapControllers();

            // Configure SPA fallback routing
            app.MapFallbackToFile("index.html");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static async Task SeedInitialDataAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        // Create global admin user if it doesn't exist
        if (!await userManager.Users.AnyAsync(u => u.Email == "admin@driya.com"))
        {
            var globalAdmin = new ApplicationUser
            {
                UserName = "admin@driya.com",
                Email = "admin@driya.com",
                FirstName = "Global",
                LastName = "Administrator",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(globalAdmin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(globalAdmin, "GlobalAdmin");
                Log.Information("Global admin user created successfully");
            }
        }

        // Create demo tenant if it doesn't exist
        if (!await context.Tenants.AnyAsync(t => t.TenantId == "demo"))
        {
            var demoTenant = new Tenant
            {
                Name = "Demo Tenant",
                TenantId = "demo",
                Description = "Demo tenant for testing purposes",
                ContactEmail = "demo@example.com",
                IsActive = true,
                IsTrialActive = true,
                TrialEndDate = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow
            };

            context.Tenants.Add(demoTenant);
            await context.SaveChangesAsync();
            Log.Information("Demo tenant created successfully");
        }
    }
}
