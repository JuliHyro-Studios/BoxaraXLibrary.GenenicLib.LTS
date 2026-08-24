@echo off
chcp 65001 >nul
title Pack BoxaraXLibrary.GenenicLib.LTS

echo ============================================================
echo   Packing BoxaraXLibrary.GenenicLib.LTS
echo ============================================================
echo.

echo [1] Cleaning old nupkgs...
if exist .\nupkgs rmdir /s /q .\nupkgs
echo.

echo [2] Building Release...
dotnet build -c Release --no-restore
if %errorlevel% neq 0 (
    echo [X] Build failed.
    pause
    exit /b %errorlevel%
)
echo.

echo [3] Packing...
dotnet pack -c Release -o ./nupkgs
if %errorlevel% neq 0 (
    echo [X] Pack failed.
    pause
    exit /b %errorlevel%
)
echo.

echo ============================================================
echo   PACK SUCCESSFUL
echo ============================================================
echo.
echo Output: .\nupkgs\BoxaraXLibrary.GenenicLib.LTS.*.nupkg
echo.
pause