# PowerShell script to run the DRIYA Platform application with hot reload
# Usage: .\run.ps1

Write-Host "Starting DRIYA Platform with Hot Reload..." -ForegroundColor Green
Write-Host "=======================================" -ForegroundColor Cyan
Write-Host "Backend: https://localhost:7096" -ForegroundColor Yellow
Write-Host "Frontend Dev Server: http://localhost:3000 (with hot reload)" -ForegroundColor Yellow
Write-Host "=======================================" -ForegroundColor Cyan
Write-Host ""

# Function to cleanup on exit
function Cleanup {
    Write-Host "`nShutting down..." -ForegroundColor Yellow
    Get-Job | Stop-Job
    Get-Job | Remove-Job
    # Kill any remaining processes
    Get-Process -Name "dotnet","node" -ErrorAction SilentlyContinue | Where-Object { $_.StartTime -gt (Get-Date).AddMinutes(-2) } | Stop-Process -Force -ErrorAction SilentlyContinue
    Write-Host "Cleanup complete." -ForegroundColor Green
}

# Register cleanup on exit
Register-EngineEvent -SourceIdentifier PowerShell.Exiting -Action { Cleanup } | Out-Null

# Trap Ctrl+C
$null = [Console]::TreatControlCAsInput = $false

try {
    # Start the backend with hot reload
    Write-Host "Starting Backend (.NET with Hot Reload)..." -ForegroundColor Green
    $backendJob = Start-Job -ScriptBlock {
        Set-Location $using:PWD
        dotnet run --project DRIYA.Platform/DRIYA.Platform.csproj
    }

    # Wait a bit for backend to start
    Start-Sleep -Seconds 3

    # Start the frontend dev server with hot reload
    Write-Host "Starting Frontend (Vite with Hot Reload)..." -ForegroundColor Green
    $frontendJob = Start-Job -ScriptBlock {
        Set-Location $using:PWD
        Set-Location DRIYA.Platform/ClientApp
        npm run dev
    }

    Write-Host "`nBoth services starting..." -ForegroundColor Cyan
    Write-Host "Press Ctrl+C to stop all services`n" -ForegroundColor Yellow

    # Monitor both jobs and show their output
    while ($true) {
        # Show backend output
        $backendOutput = Receive-Job -Job $backendJob
        if ($backendOutput) {
            $backendOutput | ForEach-Object { Write-Host "[Backend] $_" -ForegroundColor Blue }
        }

        # Show frontend output
        $frontendOutput = Receive-Job -Job $frontendJob
        if ($frontendOutput) {
            $frontendOutput | ForEach-Object { Write-Host "[Frontend] $_" -ForegroundColor Magenta }
        }

        # Check if jobs are still running
        if ($backendJob.State -eq "Failed" -or $frontendJob.State -eq "Failed") {
            Write-Host "One or more services failed. Exiting..." -ForegroundColor Red
            break
        }

        Start-Sleep -Milliseconds 500
    }
}
catch {
    Write-Host "Error: $_" -ForegroundColor Red
}
finally {
    Cleanup
}
