
DELETE FROM Snapshoots
DELETE FROM Transaction
DELETE FROM Accounts
DELETE FROM Counterparties
DELETE FROM Users
GO

INSERT INTO [Bookkeeping].[dbo].[Users] (Id, Name, Pwd)
VALUES (1, 'Maxim', NULL)

INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (1, 1, '������', '���', NULL)
INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (2, 1, '������', '$', NULL)
INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (3, 1, '��������', '���', NULL)
INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (4, 1, '2600 �������', '���', NULL)
INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (5, 1, '������', '���', NULL)
INSERT INTO [Bookkeeping].[dbo].[Accounts] (Id, UserId, Name, Currency, Note)
VALUES (6, 1, '������', '���', NULL)

INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (1, 1, 1, 'C�����', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (2, 1, 1, '������', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (3, 1, 1, '�������', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (4, 1, 1, '˳��-���', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (5, 1, 1, '�����', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (6, 1, 0, '�������', NULL)
INSERT INTO [Bookkeeping].[dbo].[Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (7, 1, 0, '�������', NULL)

INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (1, 1, 1, 1, -100, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (2, 1, 1, 2, -52.3, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (3, 1, 3, 3, -2500, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (4, 1, 2, 6, 100, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (5, 1, 4, 7, 50000, '2.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (6, 1, 5, 4, -7500, '2.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (7, 1, 6, 5, -65.43, '2.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (8, 1, 1, 2, 152.55, '2.06.2016', NULL)

INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (1, 1, 1, 3668, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (2, 1, 2, 0, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (3, 1, 3, 4650, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (4, 1, 4, 25000, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (5, 1, 5, 22069, '1.06.2016', NULL)
INSERT INTO [Bookkeeping].[dbo].[Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (6, 1, 6, 5943, '1.06.2016', NULL)











