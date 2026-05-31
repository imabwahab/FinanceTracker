-- For Accounts table
ALTER TABLE dbo.Accounts
ALTER COLUMN Id NVARCHAR(50) NOT NULL;

-- For Categories table
ALTER TABLE dbo.Categories
ALTER COLUMN Id NVARCHAR(50) NOT NULL;

-- For Transactions table
ALTER TABLE dbo.Transactions
ALTER COLUMN Id NVARCHAR(50) NOT NULL;

ALTER TABLE dbo.Transactions
ALTER COLUMN AccountId NVARCHAR(50) NOT NULL;

ALTER TABLE dbo.Transactions
ALTER COLUMN CategoryId NVARCHAR(50) NOT NULL;