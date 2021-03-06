USE Bookkeeping
GO

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
DROP CONSTRAINT DF__Transacti__Trans__74444068

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
ALTER COLUMN TransactionDate DATE NOT NULL

EXEC sp_RENAME 'Transactions.TransactionDate', 'Date', 'COLUMN'

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
ADD Time TIME(0)

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
ADD Balance MONEY NOT NULL DEFAULT 0

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
ADD IsTrsBalance BIT NOT NULL DEFAULT 0

ALTER TABLE [Bookkeeping].[dbo].[Transactions]
ADD [Order] INT NOT NULL DEFAULT 0

GO

DECLARE @i INT = 0
DECLARE @id INT
DECLARE @date DATE
DECLARE @prev_date DATE
DECLARE row CURSOR FOR SELECT [Id], [Date] FROM [Bookkeeping].[dbo].[Transactions] ORDER BY [Date], [Id]

OPEN row
FETCH NEXT FROM row INTO @id, @date
SET @prev_date = @date
WHILE @@FETCH_STATUS = 0
BEGIN	
	IF @date <> @prev_date
		SET @i = 0
	SET @i = @i + 1
	UPDATE [Bookkeeping].[dbo].[Transactions] SET [Order] = @i
		WHERE Id = @Id
	SET @prev_date = @date
	FETCH NEXT FROM row INTO @Id, @Date
END
CLOSE row
DEALLOCATE row

GO

DECLARE @Id INT
DECLARE @AccountId INT
DECLARE @Amount MONEY
DECLARE @RealBalance MONEY
DECLARE @IsTrsBalance BIT

DECLARE @Balance MONEY = 0
DECLARE @Balances TABLE
(
	AccountId INT NOT NULL,
	Balance MONEY NOT NULL DEFAULT 0
)
INSERT INTO @Balances (AccountId, Balance)
SELECT [Id], 0 AS Balance FROM [Bookkeeping].[dbo].[Accounts]

DECLARE row CURSOR FOR SELECT [Id], [AccountId], [Amount], [IsTrsBalance], [Balance] AS RealBalance
	FROM [Bookkeeping].[dbo].[Transactions]
	ORDER BY [Date], [Order]

UPDATE [Bookkeeping].[dbo].[Transactions]
	SET [Balance] = 0
		WHERE [IsTrsBalance] = 0

OPEN row
	FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance
	WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @Balance = (SELECT Balance FROM @Balances WHERE AccountId = @AccountId)
			SET @Balance = @Balance + @Amount

			IF (@IsTrsBalance = 1 AND @Balance != @RealBalance)
				BREAK

			UPDATE @Balances SET Balance = @Balance 
				WHERE AccountId = @AccountId

			IF (@IsTrsBalance = 0)
				UPDATE [Bookkeeping].[dbo].[Transactions] SET [Balance] = @Balance
					WHERE Id = @Id

			FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance
		END
CLOSE row
DEALLOCATE row
                 