# PowerShell script to run the DRIYA Platform application
# Usage: .\run.ps1

Write-Host "Starting DRIYA Platform..." -ForegroundColor Green
dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj
