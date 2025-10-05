# Stop DRIYA Platform Local Development
# This script stops the database containers

Write-Host "Stopping DRIYA Platform local development environment..." -ForegroundColor Red

# Stop database containers
Write-Host "Stopping database containers..." -ForegroundColor Yellow
docker-compose down

Write-Host "Local development environment stopped." -ForegroundColor Green
