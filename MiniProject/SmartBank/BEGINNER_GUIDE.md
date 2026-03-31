# 🏦 SmartBank - Beginner's Guide

## What Was Fixed 🔧

### Critical Bug #1: Wrong API Route ❌→✅
**Problem:** The TransactionController had `[Route("api/[controller]")]` which created the route `api/transactioncontroller` (with "controller" suffix), but the AccountService was calling `api/transaction`.

**Fix:** Changed to `[Route("api/transaction")]` to match the expected route.

### Critical Bug #2: No Error Handling ❌→✅
**Problem:** If the transaction recording failed, the account balance would still be updated, causing data corruption.

**Fix:** Added try-catch blocks to rollback balance changes if transaction recording fails.

### Improvement: Beginner-Friendly Comments ✅
Added detailed comments throughout the code explaining what each part does.

---

## How the System Works 🔄

### Architecture Overview

```
┌─────────────────┐
│  SmartBank.Web  │  (Your browser - https://localhost:5001)
│   (MVC App)     │
└────────┬────────┘
         │ HTTP Calls
         ↓
┌──────────────────────┐
│ AccountService API   │  (https://localhost:7002)
│ Manages accounts &   │
│ balances             │
└────────┬─────────────┘
         │ HTTP Calls
         ↓
┌──────────────────────┐
│ TransactionService   │  (https://localhost:7185)
│ Records transaction  │
│ history              │
└──────────────────────┘
         │
         ↓
┌──────────────────────┐
│   SQL Server DBs     │
│ • SmartBankAccountDb │
│ • MySBTransactionDb  │
└──────────────────────┘
```

### When You Create an Account 📝

1. **SmartBank.Web** sends a POST request to AccountService
2. **AccountService** creates a new account with:
   - Unique account number (10 digits)
   - Balance = 0
   - Links it to your user ID
3. **AccountService** saves it to SQL Server database `SmartBankAccountDb`
4. You see the new account on your dashboard

### When You Deposit Money 💰

1. **SmartBank.Web** sends the deposit amount to AccountService
2. **AccountService** does these steps:
   - Finds your account in the database
   - Adds the deposit amount to your balance
   - **Saves the new balance** to `SmartBankAccountDb`
   - Calls **TransactionService** to record the transaction
3. **TransactionService** saves the transaction details to `MySBTransactionDb`
4. Your dashboard reloads and shows the new balance

### When You Withdraw Money 💸

1. **SmartBank.Web** sends the withdrawal amount to AccountService
2. **AccountService** does these steps:
   - Finds your account in the database
   - Checks if you have enough balance
   - Subtracts the withdrawal amount from your balance
   - **Saves the new balance** to `SmartBankAccountDb`
   - Calls **TransactionService** to record the transaction
3. **TransactionService** saves the transaction details to `MySBTransactionDb`
4. Your dashboard reloads and shows the new balance

---

## Database Setup 🗄️

Your system uses **TWO SQL Server databases**:

1. **SmartBankAccountDb** - Stores accounts and balances
2. **MySBTransactionDb** - Stores transaction history

### Check if Databases Exist

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to `.\sqlexpress` (or your SQL Server instance)
3. Look for these databases in the Object Explorer

### Create/Update Databases

If databases don't exist, you need to run migrations:

#### Option 1: Using Visual Studio Package Manager Console

1. Open **Tools → NuGet Package Manager → Package Manager Console**
2. For AccountService:
   ```powershell
   cd APIServices\SmartBank.AccountService
   Update-Database
   ```
3. For TransactionService:
   ```powershell
   cd ..\..\SmartBank.TransactionService
   Update-Database
   ```

#### Option 2: Using Command Line

1. Open **Command Prompt** in the solution folder
2. For AccountService:
   ```cmd
   cd APIServices\SmartBank.AccountService
   dotnet ef database update
   ```
3. For TransactionService:
   ```cmd
   cd ..\..\SmartBank.TransactionService
   dotnet ef database update
   ```

### Delete Old Data (Fresh Start)

If you want to clear all old accounts and start fresh:

1. Open **SQL Server Management Studio**
2. Run these commands:

```sql
-- Clear AccountService database
USE SmartBankAccountDb;
DELETE FROM Accounts;
GO

-- Clear TransactionService database
USE MySBTransactionDb;
DELETE FROM Transactions;
GO
```

---

## How to Test 🧪

### Step 1: Start All Services

You need to run **3 projects** at the same time:

1. Right-click on **Solution** in Visual Studio
2. Click **Properties**
3. Select **Multiple startup projects**
4. Set these to **Start**:
   - SmartBank.Web
   - SmartBank.AccountService
   - SmartBank.TransactionService
   - SmartBank.AuthService (if you have it)
5. Click **OK** and press **F5**

### Step 2: Register/Login

1. Open browser to https://localhost:5001
2. Register a new user or login
3. You'll be redirected to the dashboard

### Step 3: Create an Account

1. Click **"Create New Account"** button
2. Wait a moment - you should see success message
3. Your new account appears with:
   - Account number (10 digits)
   - Balance: ₹0.00

### Step 4: Test Deposit

1. Click **"Deposit"** button on your account
2. Enter an amount (e.g., 1000)
3. Click **"Confirm Deposit"**
4. Page reloads - balance should now show ₹1,000.00

### Step 5: Test Withdrawal

1. Click **"Withdraw"** button
2. Enter an amount less than your balance (e.g., 200)
3. Click **"Confirm Withdrawal"**
4. Page reloads - balance should now show ₹800.00

### Step 6: Verify Database

Open **SQL Server Management Studio** and check:

```sql
-- Check accounts
USE SmartBankAccountDb;
SELECT * FROM Accounts;

-- Check transactions
USE MySBTransactionDb;
SELECT * FROM Transactions;
```

You should see:
- ✅ Your account with updated balance
- ✅ Two transaction records (deposit and withdrawal)

---

## Common Issues & Solutions 🛠️

### Issue: "Account created but I can't see it"

**Cause:** Database might not be created or migrations not applied

**Solution:**
1. Check if `SmartBankAccountDb` exists in SQL Server
2. Run migrations: `dotnet ef database update` in AccountService folder

### Issue: "Deposit button does nothing"

**Cause:** TransactionService might not be running

**Solution:**
1. Make sure all 3 services are running (check console windows)
2. Check TransactionService is running on https://localhost:7185
3. Test the endpoint: https://localhost:7185/swagger

### Issue: "Balance updates but no transaction record"

**Cause:** This was the main bug - now fixed!

**Solution:**
- Make sure you're using the updated code with the fixed route
- Restart all services after the fix

### Issue: "SQL Server connection error"

**Cause:** Connection string might be wrong

**Solution:**
1. Check your SQL Server instance name
2. Update connection strings in:
   - `APIServices\SmartBank.AccountService\appsettings.json`
   - `SmartBank.TransactionService\appsettings.json`
3. Common SQL Server names:
   - `.\sqlexpress` (SQL Express)
   - `localhost` (Full SQL Server)
   - `(localdb)\MSSQLLocalDB` (LocalDB)

### Issue: "Cannot access the database"

**Cause:** SQL Server might not be running

**Solution:**
1. Open **Services** (Windows + R, type `services.msc`)
2. Find **SQL Server (SQLEXPRESS)**
3. Right-click → **Start**

---

## Understanding the Code 📚

### Key Files and What They Do

#### AccountService
- **AccountController.cs** - Receives HTTP requests for account operations
- **AccountService.cs** - Business logic (create account, deposit, withdraw)
- **AccountRepository.cs** - Database operations (SaveChangesAsync)
- **AccountDbContext.cs** - Database connection and table definitions

#### TransactionService
- **TransactionController.cs** - Receives HTTP requests to record transactions
- **TransactionService1.cs** - Business logic for transactions
- **TransactionRepository.cs** - Database operations (SaveChangesAsync)

#### Web (Frontend)
- **AccountController.cs** - Handles user requests from browser
- **Index.cshtml** - The dashboard UI you see in browser

### How Data is Saved to SQL Server

Every time you see `await _context.SaveChangesAsync()`, that's when data is written to SQL Server.

Example from AccountRepository.cs:
```csharp
public async Task AddAsync(Account account)
{
    _context.Accounts.Add(account);           // Prepare to save
    await _context.SaveChangesAsync();        // Actually save to SQL Server!
}
```

---

## Next Steps 🚀

1. ✅ Verify all services start without errors
2. ✅ Create a test account
3. ✅ Test deposit and withdrawal
4. ✅ Check SQL Server to see the saved data
5. ✅ Try creating multiple accounts
6. ✅ Test edge cases (withdraw more than balance)

---

## Need More Help? 💬

### Enable Detailed Logging

Add this to `appsettings.json` in each service:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

This will show SQL queries in the console!

### Check Console Output

When you run the services, watch the console windows for:
- ✅ "Now listening on: https://localhost:XXXX"
- ❌ Red error messages
- 💚 Green success messages

---

## Summary of Changes Made ✨

1. ✅ Fixed TransactionController route from `[controller]` to `"transaction"`
2. ✅ Added error handling with rollback in Deposit/Withdraw methods
3. ✅ Added comprehensive beginner-friendly comments throughout
4. ✅ Explained the data flow and architecture
5. ✅ Created this guide for testing

**Your data WILL NOW be saved to SQL Server correctly!** 🎉
