@echo off
REM ============================================
REM SmartBank - Database Migration Script
REM This script applies/updates databases
REM ============================================

echo.
echo ============================================
echo SmartBank Database Migration Tool
echo ============================================
echo.

REM Check if .NET is installed
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: .NET SDK is not installed or not in PATH
    echo Please install .NET 8 SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo [1/4] Checking .NET SDK...
dotnet --version
echo.

REM Navigate to AccountService
echo [2/4] Updating AccountService Database...
cd APIServices\SmartBank.AccountService
echo Current directory: %CD%

echo Applying migrations to SmartBankAccountDb...
dotnet ef database update
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to update AccountService database
    echo Make sure SQL Server is running and connection string is correct
    cd ..\..
    pause
    exit /b 1
)

echo ✓ AccountService database updated successfully!
echo.

REM Navigate back and go to TransactionService
cd ..\..
echo [3/4] Updating TransactionService Database...
cd SmartBank.TransactionService
echo Current directory: %CD%

echo Applying migrations to MySBTransactionDb...
dotnet ef database update
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to update TransactionService database
    echo Make sure SQL Server is running and connection string is correct
    cd ..
    pause
    exit /b 1
)

echo ✓ TransactionService database updated successfully!
echo.

REM Navigate back to solution root
cd ..

echo [4/4] Migration Complete!
echo.
echo ============================================
echo SUCCESS! Your databases are ready to use.
echo ============================================
echo.
echo Databases created:
echo   - SmartBankAccountDb
echo   - MySBTransactionDb
echo.
echo Next steps:
echo 1. Open SQL Server Management Studio
echo 2. Connect to .\sqlexpress
echo 3. Verify the databases exist
echo 4. Start your SmartBank application
echo.
pause
