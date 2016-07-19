Use Bookkeeping

DELETE FROM [InvoiceLines]
DELETE FROM [Articles]
DELETE FROM [Transactions]
DELETE FROM [Snapshots]
DELETE FROM [Accounts]
DELETE FROM [Counterparties]
DELETE FROM [Users]

INSERT INTO [Users] (Id, Name, Pwd)
VALUES (1, N'���� ��''�', NULL)

INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (1, 1, N'������', N'���', NULL)
INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (2, 1, N'������', N'USD', NULL)
INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (3, 1, N'��������', N'���', NULL)
INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (4, 1, N'2600 �������', N'���', NULL)
INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (5, 1, N'������', N'���', NULL)
INSERT INTO [Accounts] (Id, UserId, Name, Currency, Note)
VALUES (6, 1, N'������', N'���', NULL)

INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (1, 1, 1, N'C�����', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (2, 1, 1, N'������', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (3, 1, 1, N'�������', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (4, 1, 1, N'˳��-���', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (5, 1, 1, N'�����', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (6, 1, 0, N'�������', NULL)
INSERT INTO [Counterparties] (Id, UserId, IsSupplier, Name, Note)
VALUES (7, 1, 0, N'�������', NULL)

INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (1, 1, 1, 1, -100, '2016-06-01', NULL)
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (2, 1, 1, 2, -52.3, '2016-06-01', NULL)
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (3, 1, 3, 3, -2500, '2016-06-01', N'25.00 ���/$')
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (4, 1, 2, 6, 100, '2016-06-01', NULL)
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (5, 1, 4, 7, 50000, '2016-06-02', NULL)
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (6, 1, 5, 4, -7500, '2016-06-02', N'����� �� ������� ��������, ������� 2016, ����� �������� �2525')
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (7, 1, 6, 5, -65.43, '2016-06-02', NULL)
INSERT INTO [Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
VALUES (8, 1, 1, 2, 152.55, '2016-06-02', NULL)

INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (1, 1, 1, 3668, '2016-06-01', NULL)
INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (2, 1, 2, 0, '2016-06-01', NULL)
INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (3, 1, 3, 4650, '2016-06-01', NULL)
INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (4, 1, 4, 25000, '2016-06-01', NULL)
INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (5, 1, 5, 22069, '2016-06-01', NULL)
INSERT INTO [Snapshots] (Id, UserId, AccountId, Amount, SnapshotDate, Note)
VALUES (6, 1, 6, 5943, '2016-06-01', NULL)

INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (1, 1, NULL, N'�������� ����������', 'false', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (2, 1, NULL, N'���������', 'false', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (3, 1, 1, N'������', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (4, 1, 1, N'���', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (5, 1, 1, N'�������', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (6, 1, 1, N'�������', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (7, 1, 1, N'������', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (8, 1, 1, N'����', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (9, 1, 1, N'����', 'true', 'true')
INSERT INTO [Articles] (Id, UserId, CategoryId, Label, IsArticle, IsForPurchase)
VALUES (10, 1, 1, N'��������', 'true', 'true')

INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (1, 1, 1, 3, 10.50, NULL)
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (2, 1, 1, 4, 15.20, NULL)
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (3, 1, 1, 5, 50.50, N'60 ���/��')
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (4, 1, 2, 6, 30.49, N'25 ���/��')
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (5, 1, 2, 7, 25.25, N'15 ���/��')
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (6, 1, 7, 8, 100.50, NULL)
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (7, 1, 7, 9, 150.75, NULL)
INSERT INTO [InvoiceLines] (Id, UserId, TransactionId, ArticleId, Price, Note)
VALUES (8, 1, 7, 10, 30.00, NULL)





