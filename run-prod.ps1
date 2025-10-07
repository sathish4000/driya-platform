# PowerShell script to run DRIYA Platform in production mode
# Usage: .\run-prod.ps1
# This builds the frontend and runs the backend serving static files

Write-Host "Starting DRIYA Platform (Production Mode)..." -ForegroundColor Green
Write-Host "=======================================" -ForegroundColor Cyan

# Build frontend
Write-Host "Building frontend..." -ForegroundColor Yellow
Set-Location DRIYA.Platform/ClientApp
npm run build
Set-Location ../..

if ($LASTEXITCODE -ne 0) {
    Write-Host "Frontend build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "Frontend build complete!" -ForegroundColor Green
Write-Host ""
Write-Host "Starting backend..." -ForegroundColor Yellow
Write-Host "Application will be available at: https://localhost:7096" -ForegroundColor Cyan
Write-Host "=======================================" -ForegroundColor Cyan
Write-Host ""

# Run backend
dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj

