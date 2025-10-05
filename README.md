# DRIYA Platform - Multi-Tenant SaaS Platform

DRIYA is a comprehensive multi-tenant SaaS platform that provides reusable components and foundational services such as authentication, tenant/user management, licensing, and billing.

## 🚀 Quick Start

### Running the Application

You can run the application from the solution folder using any of these methods:

**Option 1: Using the PowerShell script (Recommended)**
```powershell
.\run.ps1
```

**Option 2: Using the batch file**
```cmd
run.bat
```

**Option 3: Using dotnet directly**
```bash
dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj
```

**Option 4: From the project folder**
```bash
cd DRIYA.Platform
dotnet run
```

### Default Login Credentials
- **Email**: `admin@driya.com`
- **Password**: `Admin123!`
- **Role**: Global Admin (access to all applications)

## 🚀 Features

### 1.1 Authentication and User Management
- **Multi-tenant User Support**: Users can belong to multiple tenants with different roles
- **Role-Based Access Control (RBAC)**: Hierarchical roles (GlobalAdmin, TenantAdmin, Manager, User)
- **Multi-Factor Authentication (MFA)**: Enhanced security with MFA support
- **API Key Management**: Secure API key generation, validation, and usage tracking
- **Login History**: Comprehensive audit trail of user logins

### 1.2 Tenant Management
- **Flexible Multi-tenancy**: Support for both shared and dedicated databases
- **Tenant Isolation**: Complete data isolation per tenant
- **White-Label Branding**: Custom themes, logos, and domain support
- **Database Configuration**: Per-tenant database connection settings

### 1.3 Licensing and Feature Management
- **Dynamic Feature Flags**: System, tenant, and user-level feature toggles
- **License Plans**: Configurable subscription plans with feature limits
- **Trial Management**: Built-in trial period support
- **Feature Overrides**: Admin-level feature control

### 1.4 Billing and Payment Processing
- **Multi-Gateway Support**: Stripe and PayPal integration
- **Invoice Generation**: Automated billing with detailed line items
- **Payment Tracking**: Complete payment transaction history
- **Tax Handling**: Support for tax calculations

### 1.5 Admin Dashboard
- **Global Admin Panel**: Platform-wide management
- **Tenant Admin Panel**: Tenant-specific configuration
- **Real-time Statistics**: Usage metrics and analytics

## 🏗️ Architecture

### Database Strategy
- **Database Agnostic**: Support for PostgreSQL and SQL Server
- **Flexible Multi-tenancy**: 
  - Shared database for smaller tenants
  - Dedicated databases for larger tenants
  - Per-tenant database configuration

### Technology Stack
- **Backend**: ASP.NET Core 9.0 with Entity Framework Core
- **Frontend**: Vue 3 with TypeScript, Vite, Vue Router, and Pinia
- **UI Framework**: Tailwind CSS with Headless UI components
- **Authentication**: ASP.NET Core Identity with custom multi-tenant support
- **Database**: PostgreSQL (primary) / SQL Server (secondary)
- **Caching**: Redis for performance optimization
- **API Documentation**: Swagger/OpenAPI
- **Logging**: Serilog with structured logging

## 📁 Project Structure

```
DRIYA.Platform/
├── ClientApp/             # Vue.js Frontend Application
│   ├── src/
│   │   ├── assets/        # Static assets
│   │   ├── components/    # Reusable Vue components
│   │   ├── stores/        # Pinia state management
│   │   ├── views/         # Page components
│   │   ├── App.vue        # Root component
│   │   └── main.ts        # App entry point
│   ├── index.html         # HTML template
│   ├── package.json       # NPM dependencies
│   ├── vite.config.ts     # Vite configuration
│   └── README.md          # Frontend documentation
├── Controllers/           # API Controllers
│   ├── TenantController.cs
│   ├── FeatureController.cs
│   ├── ApiKeyController.cs
│   └── HomeController.cs
├── Data/                  # Data Access Layer
│   └── DataAccess.cs      # Entity Framework Context
├── Models/                # Entity Models
│   ├── Tenant.cs          # Multi-tenant configuration
│   ├── ApplicationUser.cs # Extended user model
│   ├── Role.cs           # RBAC roles
│   ├── Permission.cs     # Granular permissions
│   ├── Feature.cs        # Feature definitions
│   ├── FeatureFlag.cs    # Dynamic feature toggles
│   ├── LicensePlan.cs    # Subscription plans
│   ├── Invoice.cs        # Billing invoices
│   ├── PaymentGateway.cs # Payment providers
│   ├── ApiKey.cs         # API key management
│   └── TenantDatabase.cs # Database configuration
├── Services/              # Business Logic Layer
│   ├── ITenantService.cs
│   ├── TenantService.cs
│   ├── IFeatureService.cs
│   ├── FeatureService.cs
│   ├── IBillingService.cs
│   ├── BillingService.cs
│   ├── IApiKeyService.cs
│   └── ApiKeyService.cs
├── Middleware/            # Custom Middleware
│   └── TenantMiddleware.cs
├── wwwroot/               # Static files (built frontend)
└── Program.cs            # Application startup
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js 18+ and npm 9+
- PostgreSQL or SQL Server
- Redis (optional, for caching)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd driya-platform
   ```

2. **Configure the database**
   Update `appsettings.json` with your database connection:
   ```json
   {
     "Database": {
       "Type": "PostgreSQL"
     },
     "ConnectionStrings": {
       "PostgreSQLConnection": "Host=localhost;Database=driya_platform;Username=postgres;Password=your_password"
     }
   }
   ```

3. **Run migrations**
   ```bash
   cd DRIYA.Platform
   dotnet ef database update
   ```

4. **Install frontend dependencies** (optional, auto-installed on build)
   ```bash
   cd ClientApp
   npm install
   cd ..
   ```

5. **Start the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Application: https://localhost:7001/
   - API Documentation: https://localhost:7001/swagger
   - Default Admin: admin@driya.com / Admin123!

### Frontend Development

For frontend development with hot-reload:

1. **Terminal 1 - Start .NET Backend**:
   ```bash
   cd DRIYA.Platform
   dotnet run
   ```

2. **Terminal 2 - Start Vue Dev Server**:
   ```bash
   cd DRIYA.Platform/ClientApp
   npm run dev
   ```

The Vite dev server will run on `http://localhost:3000` with API proxy to the backend.

See [ClientApp/README.md](DRIYA.Platform/ClientApp/README.md) for detailed frontend documentation.

## 🔧 Configuration

### Multi-Tenant Database Configuration

The platform supports flexible database configurations per tenant:

```csharp
// Example tenant database configuration
var tenantDb = new TenantDatabase
{
    TenantId = tenantId,
    DatabaseType = "PostgreSQL", // or "SQLServer"
    ServerName = "localhost",
    DatabaseName = "tenant_specific_db",
    Username = "tenant_user",
    IsShared = false, // false for dedicated database
    IsActive = true
};
```

### Feature Flag Configuration

Feature flags can be set at multiple levels:

```csharp
// System-wide feature
await _featureService.SetFeatureFlagAsync("advanced_analytics", "true");

// Tenant-specific feature
await _featureService.SetFeatureFlagAsync("custom_branding", "true", tenantId);

// User-specific feature
await _featureService.SetFeatureFlagAsync("beta_features", "true", tenantId, userId);
```

### Payment Gateway Configuration

Support for multiple payment providers:

```csharp
// Stripe configuration
var stripeGateway = new PaymentGateway
{
    Name = "Stripe",
    GatewayType = "Stripe",
    PublicKey = "pk_test_...",
    SecretKey = "sk_test_...",
    IsActive = true,
    SupportsCreditCards = true,
    SupportsRecurringPayments = true
};

// PayPal configuration
var paypalGateway = new PaymentGateway
{
    Name = "PayPal",
    GatewayType = "PayPal",
    MerchantId = "your_merchant_id",
    IsActive = true,
    SupportsDigitalWallets = true
};
```

## 🔐 Security

### Authentication Flow
1. **Multi-tenant Context**: Tenant identification via header, subdomain, or route
2. **User Authentication**: ASP.NET Core Identity with custom multi-tenant support
3. **Role-based Authorization**: Hierarchical permissions system
4. **API Key Validation**: Secure API key management with usage tracking

### Data Isolation
- **Tenant Filtering**: Automatic data filtering based on tenant context
- **Database Separation**: Support for dedicated databases per tenant
- **Audit Trail**: Complete audit logging for all operations

## 📊 API Endpoints

### Tenant Management
- `GET /api/tenant` - Get all tenants (GlobalAdmin only)
- `POST /api/tenant` - Create new tenant
- `GET /api/tenant/{id}` - Get tenant details
- `PUT /api/tenant/{id}` - Update tenant
- `DELETE /api/tenant/{id}` - Delete tenant
- `GET /api/tenant/current` - Get current tenant context

### Feature Management
- `GET /api/feature` - Get all available features
- `GET /api/feature/check/{featureKey}` - Check if feature is enabled
- `GET /api/feature/flags` - Get tenant feature flags
- `POST /api/feature/flags/{featureKey}` - Set feature flag
- `GET /api/feature/bulk-check` - Check multiple features

### API Key Management
- `GET /api/apikey` - Get API keys
- `POST /api/apikey` - Create new API key
- `DELETE /api/apikey/{id}` - Revoke API key
- `POST /api/apikey/validate` - Validate API key

## 🧪 Testing

### Sample Data
The platform includes seeded data for testing:
- **Global Admin**: admin@driya.com / Admin123!
- **Demo Tenant**: "demo" tenant with trial period
- **Sample Features**: Basic analytics, advanced analytics, custom branding, etc.
- **License Plans**: Basic, Professional, Premium plans

### API Testing
Use the Swagger UI at `/swagger` to test all endpoints.

## 🔄 Deployment

### Docker Support
The platform includes Docker configuration for easy deployment:

```bash
# Build and run with Docker Compose
docker-compose up -d
```

### Environment Configuration
Configure different environments using:
- `appsettings.Development.json` - Development settings
- `appsettings.Production.json` - Production settings
- User Secrets - Sensitive configuration

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation

---

**DRIYA Platform** - Empowering SaaS ecosystems with flexible, scalable, and secure multi-tenant solutions.