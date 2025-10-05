# Quick Start Guide

## 🚀 Running the Application

### Development Mode (Hot Reload)

**Terminal 1 - Backend API:**
```powershell
cd DRIYA.Platform
dotnet run
```

**Terminal 2 - Frontend Dev Server:**
```powershell
cd DRIYA.Platform/ClientApp
npm run dev
```

Then open: `http://localhost:3000`

### Production Mode (Single Process)

```powershell
cd DRIYA.Platform
dotnet run
```

Then open: `https://localhost:7001`

## 🔨 Building

### Build Frontend Only
```powershell
cd DRIYA.Platform/ClientApp
npm run build
```
Output: `DRIYA.Platform/wwwroot/`

### Build Everything
```powershell
cd DRIYA.Platform
dotnet build
```

### Publish for Deployment
```powershell
cd DRIYA.Platform
dotnet publish -c Release -o ./publish
```

## 📂 Important Files

| File | Purpose |
|------|---------|
| `ClientApp/src/main.ts` | App entry point, router config |
| `ClientApp/src/App.vue` | Root component, navigation |
| `ClientApp/vite.config.ts` | Build configuration |
| `DRIYA.Platform.csproj` | .NET project with SPA integration |
| `Program.cs` | Backend startup, SPA fallback routing |

## 🌐 URLs

| Environment | URL | Purpose |
|-------------|-----|---------|
| Dev Frontend | http://localhost:3000 | Vite dev server (hot reload) |
| Dev Backend | https://localhost:7001 | .NET API |
| Production | https://localhost:7001 | Unified app |
| Swagger | https://localhost:7001/swagger | API documentation |

## 🔑 Default Login

- **Email**: admin@driya.com
- **Password**: Admin123!

## 📋 Common Tasks

### Install Dependencies
```powershell
cd DRIYA.Platform/ClientApp
npm install
```

### Clean Build
```powershell
# Clean frontend
cd DRIYA.Platform/ClientApp
Remove-Item -Recurse -Force node_modules
npm install

# Clean backend
cd ..
dotnet clean
dotnet build
```

### Update Database
```powershell
cd DRIYA.Platform
dotnet ef database update
```

## 🐛 Troubleshooting

**Frontend not updating?**
- Make sure `npm run dev` is running
- Hard refresh browser (Ctrl+Shift+R)

**API calls failing?**
- Check backend is running on port 7001
- Check Swagger: https://localhost:7001/swagger

**Build errors?**
- Delete `node_modules` and `npm install`
- Check Node.js version: `node --version` (need 18+)

## 📚 Documentation

- **Frontend Details**: [README.md](README.md)
- **Integration Guide**: [INTEGRATION.md](INTEGRATION.md)
- **Main Documentation**: [../../README.md](../../README.md)



