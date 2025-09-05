@echo off
echo Building MyDashboard.WPF...
dotnet build
if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo Build successful! Starting application...
dotnet run
if %ERRORLEVEL% NEQ 0 (
    echo Application failed to start!
    pause
    exit /b 1
) 