# DRIYA Platform - Frontend (Vue.js)

This is the frontend application for the DRIYA Platform, built with Vue 3, TypeScript, Vite, and PrimeVue.

## Important Notes

### ⚠️ wwwroot Directory
**The `wwwroot` directory is NOT tracked in source control** because it contains build artifacts that are regenerated automatically.

- The frontend is built from `ClientApp/` into `../wwwroot/`
- Every build clears and recreates `wwwroot`
- Never manually edit files in `wwwroot` - they will be overwritten
- The `.gitignore` excludes `**/wwwroot/` from version control

## Development

### Frontend Only (with Hot Reload)
```bash
npm run dev
```
Runs Vite dev server on http://localhost:3000 with hot module replacement.

### Build for Production
```bash
npm run build
```
Builds optimized production assets into `../wwwroot/`.

### Type Checking
```bash
npm run type-check
```

## Project Structure

```
ClientApp/
├── src/
│   ├── assets/        # Global styles (main.css)
│   ├── components/    # Reusable Vue components
│   ├── stores/        # Pinia state stores
│   ├── views/         # Page components
│   ├── App.vue        # Root component
│   └── main.ts        # App entry point
├── public/            # Static assets (copied as-is)
├── package.json       # Dependencies and scripts
├── vite.config.ts     # Vite configuration
└── tsconfig.json      # TypeScript configuration
```

## Tech Stack

- **Vue 3** - Progressive JavaScript framework
- **TypeScript** - Type safety
- **Vite** - Fast build tool and dev server
- **Pinia** - State management
- **Vue Router** - Client-side routing
- **PrimeVue** - UI component library
- **Tailwind CSS v4** - Utility-first CSS
- **Axios** - HTTP client

## Build Process

1. **Development**: `npm run dev` starts Vite dev server with HMR
2. **Production**: 
   - `npm run build` compiles and optimizes the app
   - Output goes to `../wwwroot/`
   - Backend serves these static files in production

## Environment Variables

Create `.env.local` for local development:

```env
VITE_API_URL=https://localhost:7096
```

## Styling

- Global styles: `src/assets/main.css`
- Tailwind CSS v4 with custom theme
- PrimeVue components with custom styling
- All DataTable grids use centralized styling (no per-view overrides)

## Hot Reload in Development

When using `.\run.ps1` from the project root:
- Backend runs on https://localhost:7096 (with .NET hot reload)
- Frontend runs on http://localhost:3000 (with Vite HMR)
- Access the app at **http://localhost:3000**
- API calls are proxied to backend automatically

## Production Build

When using `.\run-prod.ps1`:
- Frontend is built and optimized
- Static files placed in `wwwroot`
- Backend serves the production build
- Access at **https://localhost:7096**
