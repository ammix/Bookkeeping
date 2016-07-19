
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
	Amount INT NOT NULL,
	SnapshotDate DATETIME NOT NULL,
	Note NVARCHAR(255)
)

CREATE VIEW MainView
AS SELECT
trs.TransactionDate AS Date,
cps.Name AS Counterparty,
ars.Label AS Article,
trs.Amount,
ils.Price,
CONCAT(trs.Note, ils.Note) AS Note,
acs.Name AS Acount,
sns.Amount AS Balance,
acs.Currency

FROM Transactions trs 
	LEFT OUTER JOIN InvoiceLines ils ON trs.Id = ils.TransactionId
	LEFT OUTER JOIN Articles ars ON ars.Id = ils.ArticleId
	INNER JOIN CounterParties cps ON trs.CounterpartyId = cps.Id
	INNER JOIN Accounts acs ON trs.AccountId = acs.Id
	INNER JOIN Snapshots sns ON sns.AccountId = trs.AccountId
WHERE DATEPART(day, sns.SnapshotDate)=1
