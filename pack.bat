@echo off
chcp 65001 >nul
title Pack %PACKAGE_NAME%

:: ============================================================
::  GLOBAL CONFIGURATION - EDIT ONLY THESE VARIABLES
:: ============================================================
set PACKAGE_NAME=BoxaraXLibrary.GenenicLib.LTS
set CSPROJ_FILE=%PACKAGE_NAME%.csproj
set NUGET_API_URL=https://api.nuget.org/v3/index.json
set NUGET_PACKAGE_URL=https://www.nuget.org/packages/%PACKAGE_NAME%
set OUTPUT_DIR=nupkgs
:: ============================================================

:: ===== STEP 1: BUILD NUGET FLATCONTAINER URL FROM PACKAGE NAME =====
set NUGET_FLATCONTAINER_URL=https://api.nuget.org/v3-flatcontainer/%PACKAGE_NAME:BoxaraXLibrary.GenenicLib.LTS=boxaraxlibrary.geneniclib.lts%/index.json

echo ============================================================
echo   Packing %PACKAGE_NAME%
echo ============================================================
echo.

:: ===== STEP 1: FETCH LATEST VERSION FROM NUGET.ORG =====
echo [STEP 1] Fetching latest version from NuGet.org...
for /f "delims=" %%i in ('powershell -Command "& { try { $url='%NUGET_FLATCONTAINER_URL%'; $json=(Invoke-WebRequest -Uri $url -UseBasicParsing -Headers @{'Accept'='application/json'} | ConvertFrom-Json); $versions=$json.versions; $latest=$versions[-1]; Write-Output $latest } catch { Write-Output 'ERROR' } }"') do set NUGET_LATEST=%%i

if "%NUGET_LATEST%"=="ERROR" (
    echo [WARN] Failed to fetch latest version from NuGet.org.
    echo [WARN] Make sure you are connected to the internet.
    echo [WARN] Skipping version check.
    set NUGET_LATEST=unknown
) else (
    echo [INFO] Latest version on NuGet.org: %NUGET_LATEST%
)
echo.

:: ===== STEP 1.5: READ TARGET FRAMEWORKS FROM CSPROJ =====
echo [STEP 1.5] Reading target frameworks from .csproj...
for /f "delims=" %%i in ('powershell -Command "& { $xml=[xml](Get-Content %CSPROJ_FILE%); $tfms=$xml.Project.PropertyGroup.TargetFrameworks; if ($tfms -eq $null) { $tfms=$xml.Project.PropertyGroup.TargetFramework }; Write-Output $tfms }"') do set TARGET_FRAMEWORKS=%%i

if "%TARGET_FRAMEWORKS%"=="" (
    echo [WARN] Cannot find TargetFrameworks in .csproj. Using fallback: net8.0
    set TARGET_FRAMEWORKS=net8.0
)
echo [INFO] Target frameworks: %TARGET_FRAMEWORKS%
echo.

:: ===== STEP 2: READ LOCAL VERSION FROM CSPROJ =====
echo [STEP 2] Reading local version from .csproj...
for /f "delims=" %%i in ('powershell -Command "& { $xml=[xml](Get-Content %CSPROJ_FILE%); $version=$xml.Project.PropertyGroup.Version; if ($version -eq $null) { $version='1.0.0' }; Write-Output $version }"') do set LOCAL_VERSION=%%i

if "%LOCAL_VERSION%"=="" (
    echo [WARN] Cannot find Version in .csproj. Using fallback 1.0.0
    set LOCAL_VERSION=1.0.0
)
echo [INFO] Local version: %LOCAL_VERSION%
echo.

:: ===== STEP 3: COMPARE VERSIONS WITH Y/N OPTION =====
echo [STEP 3] Checking version conflict...
if "%NUGET_LATEST%"=="unknown" (
    echo [WARN] Skipping version comparison due to network error.
) else if "%LOCAL_VERSION%"=="%NUGET_LATEST%" (
    echo [WARN] Local version %LOCAL_VERSION% matches NuGet version %NUGET_LATEST%.
    echo [WARN] Publishing the same version will NOT work. NuGet rejects duplicate versions.
    echo [WARN] Only continue if you are RE-PUBLISHING the same version for fixing metadata.
    echo.
    
    :: ===== STEP 3.5: SHOW METADATA =====
    echo [INFO] Fetching package metadata from NuGet.org...
    echo [INFO] URL: %NUGET_PACKAGE_URL%/%LOCAL_VERSION%
    echo [INFO] Metadata:
    echo   - Package: %PACKAGE_NAME%
    echo   - Version: %LOCAL_VERSION%
    echo   - Status: Already listed on NuGet.org
    echo   - Note: This version is immutable. Re-publishing requires deleting the package first.
    echo.
    
    set /p CONFIRM="Do you want to continue anyway? (Y/N): "
    if /i not "%CONFIRM%"=="Y" (
        echo [INFO] Pack cancelled by user.
        echo [INFO] Please update the version in .csproj if you want to publish a new version.
        pause
        exit /b 0
    )
    echo [INFO] User confirmed to continue with version %LOCAL_VERSION%.
    echo [WARN] Note: NuGet will reject this push if the package already exists.
    echo [WARN] This only works if the package was deleted or not yet indexed.
) else (
    echo [INFO] Local version %LOCAL_VERSION% is different from NuGet latest %NUGET_LATEST%.
    echo [INFO] No version conflict. Ready to publish new version!
)
echo.

:: ===== STEP 4: CHECK LOCAL PACKAGE =====
echo [STEP 4] Checking local package...
set NUPKG_PATH=.\%OUTPUT_DIR%\%PACKAGE_NAME%.%LOCAL_VERSION%.nupkg
if exist "%NUPKG_PATH%" (
    echo [WARN] Package %LOCAL_VERSION% already exists in %OUTPUT_DIR% folder!
    echo [WARN] It will be overwritten during pack.
)
echo.

:: ===== STEP 5: CLEAN OLD OUTPUT FOLDER =====
echo [STEP 5] Cleaning old %OUTPUT_DIR% folder...
if exist .\%OUTPUT_DIR% (
    echo [INFO] Removing old %OUTPUT_DIR% folder...
    rmdir /s /q .\%OUTPUT_DIR%
) else (
    echo [INFO] No old %OUTPUT_DIR% folder found.
)
echo.

:: ===== STEP 6: BUILD RELEASE =====
echo [STEP 6] Building Release...
echo [INFO] Target frameworks: %TARGET_FRAMEWORKS%
echo [INFO] This may take a few seconds...
dotnet build -c Release --no-restore
if %errorlevel% neq 0 (
    echo [ERROR] Build failed with error code %errorlevel%.
    echo [ERROR] Please check the error messages above.
    pause
    exit /b %errorlevel%
)
echo [INFO] Build completed successfully.
echo.

:: ===== STEP 7: PACK =====
echo [STEP 7] Packing...
echo [INFO] Creating package: %PACKAGE_NAME%.%LOCAL_VERSION%.nupkg
dotnet pack -c Release -o ./%OUTPUT_DIR%
if %errorlevel% neq 0 (
    echo [ERROR] Pack failed with error code %errorlevel%.
    echo [ERROR] Please check the error messages above.
    pause
    exit /b %errorlevel%
)
echo [INFO] Pack completed successfully.
echo.

:: ===== STEP 8: DISPLAY RESULTS =====
echo ============================================================
echo   PACK SUCCESSFUL
echo ============================================================
echo.
echo [INFO] Package: %PACKAGE_NAME%.%LOCAL_VERSION%.nupkg
echo [INFO] Location: .\%OUTPUT_DIR%\
echo.
echo [INFO] Next steps:
echo   1. Test the package locally:
echo      dotnet add package %PACKAGE_NAME% --version %LOCAL_VERSION%
echo.
echo   2. Push to NuGet:
echo      dotnet nuget push .\%OUTPUT_DIR%\%PACKAGE_NAME%.%LOCAL_VERSION%.nupkg -k YOUR_API_KEY -s %NUGET_API_URL%
echo.
pause