using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DRIYA.Platform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentRoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HierarchyLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetRoles_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FeatureKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSystemFeature = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FeatureType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PlanCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    MaxStorageGB = table.Column<int>(type: "int", nullable: false),
                    MaxApiCalls = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemPlan = table.Column<bool>(type: "bit", nullable: false),
                    TrialDays = table.Column<int>(type: "int", nullable: false),
                    AllowTrial = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentGateways",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GatewayType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PublicKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WebhookSecret = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MerchantId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsTestMode = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SupportsCreditCards = table.Column<bool>(type: "bit", nullable: false),
                    SupportsBankTransfers = table.Column<bool>(type: "bit", nullable: false),
                    SupportsDigitalWallets = table.Column<bool>(type: "bit", nullable: false),
                    SupportsRecurringPayments = table.Column<bool>(type: "bit", nullable: false),
                    SupportedCurrencies = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TransactionFeePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionFeeFixed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentGateways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Resource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSystemPermission = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanFeatures_LicensePlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "LicensePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    SecondaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    CustomDomain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CurrentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTrialActive = table.Column<bool>(type: "bit", nullable: false),
                    TrialEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StripeCustomerId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_LicensePlans_CurrentPlanId",
                        column: x => x.CurrentPlanId,
                        principalTable: "LicensePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMfaEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MfaSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StripeInvoiceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BillingPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BillingPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_LicensePlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "LicensePlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantDatabases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatabaseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DatabaseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Port = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsShared = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaxConnections = table.Column<int>(type: "int", nullable: false),
                    ConnectionTimeout = table.Column<int>(type: "int", nullable: false),
                    UseSSL = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantDatabases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantDatabases_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KeyHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalUsage = table.Column<int>(type: "int", nullable: false),
                    MonthlyUsage = table.Column<int>(type: "int", nullable: false),
                    LastUsageReset = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApiKeys_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOverride = table.Column<bool>(type: "bit", nullable: false),
                    OverrideReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverrideBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureFlags_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeatureFlags_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureFlags_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FailureReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GatewayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GatewayTransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GatewayFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatformFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_PaymentGateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "PaymentGateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeyUsage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeyUsage_ApiKeys_ApiKeyId",
                        column: x => x.ApiKeyId,
                        principalTable: "ApiKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Description", "HierarchyLevel", "IsActive", "IsSystemRole", "Name", "NormalizedName", "ParentRoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { "1", null, new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1344), null, "Global system administrator with full access", 0, true, true, "GlobalAdmin", "GLOBALADMIN", null, null, null },
                    { "2", null, new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1533), null, "Tenant administrator with full tenant access", 1, true, true, "TenantAdmin", "TENANTADMIN", null, null, null },
                    { "3", null, new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1543), null, "Manager with limited administrative access", 2, true, true, "Manager", "MANAGER", null, null, null },
                    { "4", null, new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(1553), null, "Standard user with basic access", 3, true, true, "User", "USER", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultValue", "Description", "FeatureKey", "FeatureType", "IsActive", "IsSystemFeature", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1a6b74ca-26f2-4cb6-9754-2e15327e527f"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4340), null, "true", null, "api_access", "Boolean", true, true, "API Access", null, null },
                    { new Guid("5c4af8b1-64ca-405c-94bd-93fe437bd12b"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4355), null, "false", null, "advanced_security", "Boolean", true, true, "Advanced Security", null, null },
                    { new Guid("9a37e1b8-fdb7-435d-aecf-6115fdce8b14"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(3627), null, "true", null, "basic_analytics", "Boolean", true, true, "Basic Analytics", null, null },
                    { new Guid("a9f94f1e-ff56-48b5-92e6-fa6ef0a85398"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4352), null, "false", null, "sso_integration", "Boolean", true, true, "SSO Integration", null, null },
                    { new Guid("af0626f4-b2ab-4df5-afd4-8ad42e0ab077"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4342), null, "false", null, "white_label", "Boolean", true, true, "White Label", null, null },
                    { new Guid("b5a75ae9-5dd2-4a57-a9b8-c8689b61978c"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4338), null, "false", null, "custom_branding", "Boolean", true, true, "Custom Branding", null, null },
                    { new Guid("cbe71b91-78cd-437b-baf5-8f523427074d"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4351), null, "false", null, "priority_support", "Boolean", true, true, "Priority Support", null, null },
                    { new Guid("db861713-9542-485a-a2c5-360fe77b1fdf"), new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(4334), null, "false", null, "advanced_analytics", "Boolean", true, true, "Advanced Analytics", null, null }
                });

            migrationBuilder.InsertData(
                table: "LicensePlans",
                columns: new[] { "Id", "AllowTrial", "CreatedAt", "CreatedBy", "Currency", "Description", "IsActive", "IsSystemPlan", "MaxApiCalls", "MaxStorageGB", "MaxUsers", "MonthlyPrice", "Name", "PlanCode", "TrialDays", "UpdatedAt", "UpdatedBy", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("0697f369-4ac9-44c2-a762-9a711f5fc9f5"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2655), null, "USD", "Premium plan for enterprise customers", true, true, 100000, 1000, 100, 299.99m, "Premium", "premium", 14, null, null, 2999.99m },
                    { new Guid("d0f16e34-e034-490f-9aec-1600d5ef3d76"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2651), null, "USD", "Professional plan for growing businesses", true, true, 10000, 100, 25, 99.99m, "Professional", "pro", 14, null, null, 999.99m },
                    { new Guid("ea64641f-dc97-43a0-b28b-90fb5ac8367b"), true, new DateTime(2025, 10, 5, 0, 56, 5, 81, DateTimeKind.Utc).AddTicks(2542), null, "USD", "Basic plan for small teams", true, true, 1000, 10, 5, 29.99m, "Basic", "basic", 14, null, null, 299.99m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsSystemPermission", "Name", "Resource", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("01986367-0b38-438a-9f44-15eab7fdd50e"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8940), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("1c79f572-eefb-4492-89b5-d5971db791ac"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8948), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("1f82b577-a00a-42c0-af35-9a781e00e9fb"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8906), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("2c31d2ca-8211-4a42-a2d8-60f5b72a501b"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8912), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("2f7d79a8-90ec-4b08-a69b-2db15bea8ac8"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8946), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("3870f6ea-a502-4aa5-8d28-a405d136b797"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8017), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("47133edf-f7a0-4715-9150-098b5bf08a2d"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8950), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("5b739450-06d5-4fd9-bea5-30fbda2217b7"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8945), null, null, true, true, "API Key Management", "ApiKey", null, null },
                    { new Guid("77ba2224-e6a1-4d5b-88d9-23b551c351a5"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8903), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("7ec4d941-225b-4c6b-8e2e-9a7e64e47183"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8872), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("8d79dd95-228f-4fac-bb70-591e31351178"), "Delete", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8908), null, null, true, true, "Tenant Management", "Tenant", null, null },
                    { new Guid("8de5754e-9b63-4ddf-9de2-0a6c756f6934"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8870), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("9ac9bb7e-8e91-41d7-a994-2df6de4bcd22"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8942), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("ac6cd512-1db7-4129-930c-841046ee2d6d"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8909), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("acc83e75-9e4d-45ca-a5aa-8577a4f3007e"), "Update", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8915), null, null, true, true, "Billing Management", "Billing", null, null },
                    { new Guid("e7bb773f-1d47-46fb-9c72-fa64bb268c2a"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8938), null, null, true, true, "Feature Management", "Feature", null, null },
                    { new Guid("f7c160b9-2138-4f74-b6c3-50eca1423528"), "Read", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8858), null, null, true, true, "User Management", "User", null, null },
                    { new Guid("fb38fa8e-940f-4046-b748-82ff293f33e0"), "Create", new DateTime(2025, 10, 5, 0, 56, 5, 80, DateTimeKind.Utc).AddTicks(8874), null, null, true, true, "Tenant Management", "Tenant", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_KeyHash",
                table: "ApiKeys",
                column: "KeyHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_TenantId_UserId",
                table: "ApiKeys",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_UserId",
                table: "ApiKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeyUsage_ApiKeyId",
                table: "ApiKeyUsage",
                column: "ApiKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeyUsage_Timestamp",
                table: "ApiKeyUsage",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ParentRoleId",
                table: "AspNetRoles",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlags_FeatureId_TenantId_UserId",
                table: "FeatureFlags",
                columns: new[] { "FeatureId", "TenantId", "UserId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlags_TenantId",
                table: "FeatureFlags",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlags_UserId",
                table: "FeatureFlags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_FeatureKey",
                table: "Features",
                column: "FeatureKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PlanId",
                table: "Invoices",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TenantId",
                table: "Invoices",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistory_Timestamp",
                table: "LoginHistory",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistory_UserId",
                table: "LoginHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_GatewayId",
                table: "PaymentTransactions",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_InvoiceId",
                table: "PaymentTransactions",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TenantId",
                table: "PaymentTransactions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeatures_FeatureId",
                table: "PlanFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeatures_PlanId",
                table: "PlanFeatures",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantDatabases_TenantId",
                table: "TenantDatabases",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ContactEmail",
                table: "Tenants",
                column: "ContactEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_CurrentPlanId",
                table: "Tenants",
                column: "CurrentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantId",
                table: "Tenants",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_TenantId",
                table: "UserPermissions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_TenantId",
                table: "UserRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeyUsage");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FeatureFlags");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "LoginHistory");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PlanFeatures");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TenantDatabases");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentGateways");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "LicensePlans");
        }
    }
}
