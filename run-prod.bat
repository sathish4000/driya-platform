@echo off
echo =======================================
echo Starting DRIYA Platform (Production Mode)
echo =======================================
echo.

echo Building frontend...
cd DRIYA.Platform\ClientApp
call npm run build
cd ..\..

if %ERRORLEVEL% neq 0 (
    echo Frontend build failed!
    pause
    exit /b 1
)

echo.
echo Frontend build complete!
echo.
echo Starting backend...
echo Application will be available at: https://localhost:7096
echo =======================================
echo.

dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj

