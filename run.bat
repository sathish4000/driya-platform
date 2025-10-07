@echo off
echo =======================================
echo Starting DRIYA Platform with Hot Reload
echo =======================================
echo Backend: https://localhost:7096
echo Frontend: http://localhost:3000 (with hot reload)
echo =======================================
echo.
echo Press Ctrl+C to stop all services
echo.

REM Start backend in a new window
start "DRIYA Backend" cmd /k "dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj"

REM Wait a bit for backend to start
timeout /t 3 /nobreak > nul

REM Start frontend in a new window
start "DRIYA Frontend" cmd /k "cd DRIYA.Platform\ClientApp && npm run dev"

echo.
echo Services are starting in separate windows...
echo Close those windows or press Ctrl+C here to stop.
echo.
pause
