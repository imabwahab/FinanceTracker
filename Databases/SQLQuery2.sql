-- For Accounts table
ALTER TABLE Accounts
ALTER COLUMN Id NVARCHAR(50) NOT NULL;

-- For Categories table
ALTER TABLE Categories
ALTER COLUMN Id NVARCHAR(50) NOT NULL;

-- For Transactions table
ALTER TABLE Transactions
ALTER COLUMN Id NVARCHAR(50) NOT NULL;