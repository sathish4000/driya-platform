# DRIYA Platform - Client Application

This is the Vue.js front-end application for the DRIYA Platform, integrated within the ASP.NET Core application.

## Overview

The ClientApp is a modern Vue 3 application built with:
- **Vue 3** with Composition API
- **TypeScript** for type safety
- **Vite** for fast development and building
- **Vue Router** for navigation
- **Pinia** for state management
- **Tailwind CSS** with Headless UI components
- **Axios** for HTTP requests

## Project Structure

```
ClientApp/
├── src/
│   ├── assets/          # Static assets (images, styles)
│   ├── components/      # Reusable Vue components
│   ├── stores/          # Pinia stores for state management
│   │   ├── auth.ts      # Authentication state
│   │   └── tenant.ts    # Tenant state
│   ├── views/           # Page components
│   │   ├── HomeView.vue
│   │   ├── LoginView.vue
│   │   ├── DashboardView.vue
│   │   ├── AdminDashboardView.vue
│   │   ├── TenantManagementView.vue
│   │   ├── UserManagementView.vue
│   │   ├── FeatureManagementView.vue
│   │   ├── BillingView.vue
│   │   ├── ApiKeyManagementView.vue
│   │   ├── SettingsView.vue
│   │   └── NotFoundView.vue
│   ├── App.vue          # Root component
│   └── main.ts          # Application entry point with router
├── index.html           # HTML template
├── package.json         # NPM dependencies
├── tsconfig.json        # TypeScript configuration
├── vite.config.ts       # Vite configuration
└── README.md            # This file
```

## Development

### Prerequisites
- Node.js (v18 or higher)
- npm (v9 or higher)

### Development Server

To run the development server independently:

```bash
cd ClientApp
npm install
npm run dev
```

The app will be available at `http://localhost:3000` with hot-reload enabled.

### Development with .NET

When running the .NET application in development mode, it will automatically proxy to the Vite dev server if it's running. The .NET app will serve the API at `https://localhost:7001` and the Vite dev server will proxy API calls.

### Building for Production

The ClientApp is automatically built when you build or publish the .NET application. The build output goes to `../wwwroot/`.

To manually build the client app:

```bash
cd ClientApp
npm run build
```

## Integration with ASP.NET Core

### Build Integration

The `.csproj` file includes the following integrations:

1. **Development**: Checks if `node_modules` exists; if not, runs `npm install` automatically
2. **Publish**: Runs `npm install` and `npm run build` before publishing
3. **Output**: Vite builds directly to `../wwwroot/` directory

### SPA Configuration

The ASP.NET Core application is configured to serve the Vue SPA:
- Static files from `wwwroot` are served
- API routes are handled by controllers under `/api`
- All other routes fall back to `index.html` for client-side routing

### API Proxy (Development)

During development, the Vite dev server proxies API requests:
- Requests to `/api/*` are forwarded to `https://localhost:7001`
- This avoids CORS issues during development

## Scripts

- `npm run dev` - Start development server with hot reload
- `npm run build` - Build for production
- `npm run preview` - Preview production build locally
- `npm run type-check` - Run TypeScript type checking

## Routing

The application uses Vue Router with the following routes:

| Path | Component | Auth Required | Admin Only |
|------|-----------|---------------|------------|
| `/` | HomeView | No | No |
| `/login` | LoginView | No | No |
| `/dashboard` | DashboardView | Yes | No |
| `/admin` | AdminDashboardView | Yes | Yes |
| `/tenants` | TenantManagementView | Yes | Yes |
| `/users` | UserManagementView | Yes | No |
| `/features` | FeatureManagementView | Yes | No |
| `/billing` | BillingView | Yes | No |
| `/api-keys` | ApiKeyManagementView | Yes | No |
| `/settings` | SettingsView | Yes | No |

## State Management

The application uses Pinia for state management with two main stores:

### Auth Store (`stores/auth.ts`)
- Manages user authentication state
- Handles login/logout
- Stores JWT tokens

### Tenant Store (`stores/tenant.ts`)
- Manages multi-tenant context
- Handles tenant selection
- Stores current tenant information

## API Communication

All API calls should go through the `/api` prefix. Example:

```typescript
import axios from 'axios'

// Get tenants
const response = await axios.get('/api/tenants')

// Create tenant
await axios.post('/api/tenants', tenantData)
```

## Deployment

When you publish the .NET application, the ClientApp is automatically built and included in the publish output. No separate deployment is needed.

```bash
dotnet publish -c Release
```

The published output will include the compiled Vue application in the `wwwroot` folder.

## Troubleshooting

### Build Errors

If you encounter build errors:
1. Delete `node_modules` and `package-lock.json`
2. Run `npm install` again
3. Clear the `wwwroot/assets` folder
4. Rebuild

### Development Server Issues

If the dev server doesn't start:
1. Check if port 3000 is already in use
2. Update the port in `vite.config.ts` if needed
3. Update the proxy URL in `DRIYA.Platform.csproj` SpaProxyServerUrl

### API Connection Issues

If API calls fail during development:
1. Ensure the .NET backend is running on `https://localhost:7001`
2. Check the proxy configuration in `vite.config.ts`
3. Verify CORS settings in `Program.cs`

## Contributing

When making changes to the ClientApp:
1. Run `npm run type-check` before committing
2. Test both dev and production builds
3. Ensure the .NET integration still works



