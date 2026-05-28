-- =============================================================
-- Finance Tracker - Database Schema
-- Advanced Programming Semester Project | SP26
-- =============================================================
--
-- HOW TO RUN THIS:
-- 1. Open SQL Server Object Explorer in Visual Studio
-- 2. Right-click on (localdb) or your SQL Server -> New Query
-- 3. Paste this entire file and press the green play button
-- 4. Refresh SQL Server Object Explorer to see FinanceTrackerDB
-- =============================================================

-- Create the database (skip if it already exists)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FinanceTrackerDB')
BEGIN
    CREATE DATABASE FinanceTrackerDB;
END
GO

USE FinanceTrackerDB;
GO

-- -------------------------------------------------------------
-- Drop existing tables (safe to re-run this script)
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.Transactions', 'U') IS NOT NULL DROP TABLE dbo.Transactions;
IF OBJECT_ID('dbo.Categories',   'U') IS NOT NULL DROP TABLE dbo.Categories;
IF OBJECT_ID('dbo.Accounts',     'U') IS NOT NULL DROP TABLE dbo.Accounts;
GO

-- -------------------------------------------------------------
-- Accounts: Where money lives (Cash Wallet, Bank, Credit Card)
-- -------------------------------------------------------------
CREATE TABLE dbo.Accounts (
    Id              NVARCHAR(20)    NOT NULL PRIMARY KEY,
    Name            NVARCHAR(100)   NOT NULL,
    AccountType     NVARCHAR(20)    NOT NULL,  -- Cash / Bank / CreditCard
    OpeningBalance  DECIMAL(18, 2)  NOT NULL DEFAULT 0,
    Currency        NVARCHAR(10)    NOT NULL DEFAULT 'PKR',
    Status          NVARCHAR(20)    NOT NULL DEFAULT 'Active', -- Active / Inactive
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

-- -------------------------------------------------------------
-- Categories: Income/Expense buckets with optional monthly budget
-- -------------------------------------------------------------
CREATE TABLE dbo.Categories (
    Id              NVARCHAR(20)    NOT NULL PRIMARY KEY,
    Name            NVARCHAR(100)   NOT NULL,
    CategoryType    NVARCHAR(20)    NOT NULL,  -- Income / Expense
    MonthlyBudget   DECIMAL(18, 2)  NULL,      -- NULL = no budget set
    Status          NVARCHAR(20)    NOT NULL DEFAULT 'Active'
);
GO

-- -------------------------------------------------------------
-- Transactions: Every income/expense/transfer event
-- -------------------------------------------------------------
CREATE TABLE dbo.Transactions (
    Id                   NVARCHAR(20)    NOT NULL PRIMARY KEY,
    AccountId            NVARCHAR(20)    NOT NULL,   -- no FK constraint per spec
    CategoryId           NVARCHAR(20)    NOT NULL,   -- no FK constraint per spec
    Amount               DECIMAL(18, 2)  NOT NULL,
    TransactionDate      DATE            NOT NULL,
    Description          NVARCHAR(500)   NULL,
    IsRecurring          BIT             NOT NULL DEFAULT 0,
    RecurringFrequency   NVARCHAR(20)    NULL,        -- Daily / Weekly / Monthly / Yearly
    Status               NVARCHAR(20)    NOT NULL DEFAULT 'Cleared' -- Cleared / Pending
);
GO

-- -------------------------------------------------------------
-- Seed data: gives you something to look at on first run
-- -------------------------------------------------------------
INSERT INTO dbo.Accounts (Id, Name, AccountType, OpeningBalance, Currency, Status) VALUES
    ('A-CASH01', 'Cash Wallet',     'Cash',       50000.00,  'PKR', 'Active'),
    ('A-BANK01', 'HBL Current',     'Bank',       250000.00, 'PKR', 'Active'),
    ('A-CARD01', 'Business Card',   'CreditCard', 0.00,      'PKR', 'Active');

INSERT INTO dbo.Categories (Id, Name, CategoryType, MonthlyBudget, Status) VALUES
    ('C-SAL01', 'Client Payments',   'Income',  NULL,      'Active'),
    ('C-RNT01', 'Office Rent',       'Expense', 40000.00,  'Active'),
    ('C-UTL01', 'Utilities',         'Expense', 15000.00,  'Active'),
    ('C-SAL02', 'Staff Salaries',    'Expense', 80000.00,  'Active'),
    ('C-SUB01', 'Subscriptions',     'Expense', 10000.00,  'Active'),
    ('C-MSC01', 'Miscellaneous',     'Expense', 5000.00,   'Active');

INSERT INTO dbo.Transactions (Id, AccountId, CategoryId, Amount, TransactionDate, Description, IsRecurring, RecurringFrequency, Status) VALUES
    ('T-000001', 'A-BANK01', 'C-SAL01', 120000.00, '2026-05-02', 'Client invoice #1042',     0, NULL,      'Cleared'),
    ('T-000002', 'A-BANK01', 'C-RNT01', 40000.00,  '2026-05-03', 'Office rent for May',      1, 'Monthly', 'Cleared'),
    ('T-000003', 'A-CASH01', 'C-UTL01', 8500.00,   '2026-05-05', 'Electricity bill',         1, 'Monthly', 'Cleared'),
    ('T-000004', 'A-BANK01', 'C-SUB01', 2500.00,   '2026-05-10', 'Cloud hosting',            1, 'Monthly', 'Cleared'),
    ('T-000005', 'A-CASH01', 'C-MSC01', 1200.00,   '2026-05-15', 'Office supplies',          0, NULL,      'Cleared');
GO

PRINT 'FinanceTrackerDB created successfully with seed data.';
GO
