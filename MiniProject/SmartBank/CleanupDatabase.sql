-- ============================================
-- SmartBank - Database Cleanup and Verification Script
-- Run this in SQL Server Management Studio (SSMS)
-- ============================================

PRINT '============================================'
PRINT 'SmartBank Database Cleanup Script'
PRINT '============================================'
PRINT ''

-- ============================================
-- STEP 1: Check if databases exist
-- ============================================
PRINT 'STEP 1: Checking if databases exist...'
PRINT ''

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SmartBankAccountDb')
    PRINT '✓ SmartBankAccountDb exists'
ELSE
    PRINT '✗ SmartBankAccountDb does NOT exist - You need to run migrations!'

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MySBTransactionDb')
    PRINT '✓ MySBTransactionDb exists'
ELSE
    PRINT '✗ MySBTransactionDb does NOT exist - You need to run migrations!'

PRINT ''
PRINT '--------------------------------------------'
PRINT ''

-- ============================================
-- STEP 2: Show current data (BEFORE cleanup)
-- ============================================
PRINT 'STEP 2: Showing current data BEFORE cleanup...'
PRINT ''

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SmartBankAccountDb')
BEGIN
    USE SmartBankAccountDb;
    
    DECLARE @AccountCount INT;
    SELECT @AccountCount = COUNT(*) FROM Accounts;
    
    PRINT 'Accounts in SmartBankAccountDb: ' + CAST(@AccountCount AS VARCHAR(10))
    
    IF @AccountCount > 0
    BEGIN
        SELECT 
            Id,
            UserId,
            AccountNumber,
            Balance,
            CreatedAt
        FROM Accounts
        ORDER BY CreatedAt DESC;
    END
END

PRINT ''

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MySBTransactionDb')
BEGIN
    USE MySBTransactionDb;
    
    DECLARE @TransactionCount INT;
    SELECT @TransactionCount = COUNT(*) FROM Transactions;
    
    PRINT 'Transactions in MySBTransactionDb: ' + CAST(@TransactionCount AS VARCHAR(10))
    
    IF @TransactionCount > 0
    BEGIN
        SELECT 
            Id,
            AccountId,
            Amount,
            Type,
            Date,
            AccountNumber
        FROM Transactions
        ORDER BY Date DESC;
    END
END

PRINT ''
PRINT '--------------------------------------------'
PRINT ''

-- ============================================
-- STEP 3: Delete old data (UNCOMMENT TO RUN)
-- ============================================
PRINT 'STEP 3: Deleting old data...'
PRINT ''

-- UNCOMMENT THE LINES BELOW TO DELETE OLD DATA:
/*
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MySBTransactionDb')
BEGIN
    USE MySBTransactionDb;
    DELETE FROM Transactions;
    PRINT '✓ Deleted all transactions from MySBTransactionDb'
END

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SmartBankAccountDb')
BEGIN
    USE SmartBankAccountDb;
    DELETE FROM Accounts;
    PRINT '✓ Deleted all accounts from SmartBankAccountDb'
END

PRINT ''
PRINT 'All old data has been deleted! You can now create new accounts.'
*/

PRINT 'ℹ Data deletion is COMMENTED OUT by default for safety'
PRINT 'To delete old data, edit this script and uncomment the DELETE statements'

PRINT ''
PRINT '--------------------------------------------'
PRINT ''

-- ============================================
-- STEP 4: Verify cleanup (AFTER deletion)
-- ============================================
PRINT 'STEP 4: Verifying current state...'
PRINT ''

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SmartBankAccountDb')
BEGIN
    USE SmartBankAccountDb;
    
    DECLARE @AccountCountAfter INT;
    SELECT @AccountCountAfter = COUNT(*) FROM Accounts;
    
    PRINT 'Accounts remaining: ' + CAST(@AccountCountAfter AS VARCHAR(10))
END

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MySBTransactionDb')
BEGIN
    USE MySBTransactionDb;
    
    DECLARE @TransactionCountAfter INT;
    SELECT @TransactionCountAfter = COUNT(*) FROM Transactions;
    
    PRINT 'Transactions remaining: ' + CAST(@TransactionCountAfter AS VARCHAR(10))
END

PRINT ''
PRINT '============================================'
PRINT 'Script completed!'
PRINT '============================================'
PRINT ''
PRINT 'Next Steps:'
PRINT '1. If databases do not exist, run: dotnet ef database update'
PRINT '2. If you want to delete old data, uncomment the DELETE section'
PRINT '3. Start your services and test account creation/deposit'
PRINT ''
