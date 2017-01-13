
Use Bookkeeping

DROP TABLE InvoiceLines
DROP TABLE Articles
DROP TABLE Transactions
DROP TABLE Snapshots
DROP TABLE Accounts
DROP TABLE Counterparties
DROP TABLE Users
DROP VIEW MainView


CREATE TABLE Users
(
	Id INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(32) NOT NULL,
	Pwd NVARCHAR(16)
)

CREATE TABLE Accounts
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	IsActive BIT NOT NULL,
	Name NVARCHAR(32) NOT NULL,
	Currency NCHAR(3) NOT NULL,
	Note NVARCHAR(255)
)

CREATE TABLE Articles
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	CategoryId INT FOREIGN KEY REFERENCES Articles(Id),
	Label NVARCHAR(32) NOT NULL,
	IsArticle BIT NOT NULL,
	IsForPurchase BIT NOT NULL
)

CREATE TABLE Counterparties
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	IsSupplier BIT NOT NULL,
	Name NVARCHAR(32) NOT NULL,
	Note NVARCHAR(255)
)

CREATE TABLE Transactions
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	CounterpartyId INT FOREIGN KEY REFERENCES Counterparties(Id),
	Amount MONEY NOT NULL,
	TransactionDate DATETIME NOT NULL,
	Invoice VARBINARY(MAX),
	Note NVARCHAR(255)
)

CREATE TABLE InvoiceLines
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	TransactionId INT NOT NULL FOREIGN KEY REFERENCES Transactions(Id),
	ArticleId INT NOT NULL FOREIGN KEY REFERENCES Articles(Id),
	Price MONEY NOT NULL,
	Note NVARCHAR(255)
)

CREATE TABLE Snapshots
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	Amount MONEY NOT NULL,
	SnapshotDate DATETIME NOT NULL,
	Note NVARCHAR(255)
)

GO

CREATE VIEW MainView
AS SELECT
t.Id,
t.TransactionDate AS Date,
c.Name AS Counterparty,
ar.Label AS Article,
i.Price,
i.Note AS Note,
t.Note AS Comment,
t.Amount,
ac.Name AS Account,
ac.Currency

FROM Transactions t 
	LEFT OUTER JOIN InvoiceLines i ON i.TransactionId = t.Id
	LEFT OUTER JOIN Articles ar ON ar.Id = i.ArticleId
	INNER JOIN CounterParties c ON c.Id = t.CounterpartyId
	INNER JOIN Accounts ac ON ac.Id = t.AccountId

CREATE VIEW [SnapshotsView]
AS SELECT
a.[Name] AS [Account],
s.[Amount],
s.[SnapShotDate] AS [Date]

FROM [Snapshots] s
	INNER JOIN [Accounts] a ON s.AccountId = a.Id
	WHERE DATEPART(day, s.[SnapshotDate]) = 1
