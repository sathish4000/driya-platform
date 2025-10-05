@echo off
echo Starting DRIYA Platform in local development mode...
echo.

REM Start database containers
echo Starting database containers (SQL Server and Redis)...
docker-compose up sqlserver redis -d

REM Wait a moment
timeout /t 3 /nobreak > nul

REM Check container status
echo Checking container status...
docker ps --filter "name=driya_sqlserver" --filter "name=driya_redis" --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"

echo.
echo Starting ASP.NET Core application...
echo Application will be available at: https://localhost:7096
echo Swagger API will be available at: https://localhost:7096/swagger
echo.
echo Demo Credentials:
echo Email: admin@driya.com
echo Password: Admin123!
echo.
echo Press Ctrl+C to stop the application
echo.

REM Change to project directory and run
cd DRIYA.Platform
dotnet run
