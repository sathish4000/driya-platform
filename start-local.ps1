# Start DRIYA Platform Locally
# This script starts the database containers and runs the ASP.NET Core app locally

Write-Host "Starting DRIYA Platform in local development mode..." -ForegroundColor Green

# Start database containers
Write-Host "Starting database containers (SQL Server and Redis)..." -ForegroundColor Yellow
docker-compose up sqlserver redis -d

# Wait a moment for containers to start
Start-Sleep -Seconds 3

# Check if containers are running
Write-Host "Checking container status..." -ForegroundColor Yellow
docker ps --filter "name=driya_sqlserver" --filter "name=driya_redis" --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"

# Run the ASP.NET Core application
Write-Host "Starting ASP.NET Core application..." -ForegroundColor Yellow
Write-Host "Application will be available at: https://localhost:7096" -ForegroundColor Cyan
Write-Host "Swagger API will be available at: https://localhost:7096/swagger" -ForegroundColor Cyan
Write-Host ""
Write-Host "Demo Credentials:" -ForegroundColor Magenta
Write-Host "Email: admin@driya.com" -ForegroundColor White
Write-Host "Password: Admin123!" -ForegroundColor White
Write-Host ""
Write-Host "Press Ctrl+C to stop the application" -ForegroundColor Red

# Change to the project directory and run
Set-Location "DRIYA.Platform"
dotnet run
