CREATE TABLE Users
(
	Id INT NOT NULL  PRIMARY KEY,
	Name CHAR(16) NOT NULL,
	Pwd CHAR(16)
)

CREATE TABLE Accounts
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	Name CHAR(16) NOT NULL,
	Amount INT NOT NULL,
	Currency CHAR(3) NOT NULL,
	Note VARCHAR(255)
)

CREATE TABLE Categories
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	Label CHAR(16) NOT NULL,
	ParentCategory INT FOREIGN KEY REFERENCES Categories(Id)
)

CREATE TABLE Articles
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	Label CHAR(16) NOT NULL,
	CaterogyId INT FOREIGN KEY REFERENCES Categories(Id)
)

CREATE TABLE Counterparties
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	IsSupplier BIT NOT NULL,
	Name CHAR(16) NOT NULL,
	Note VARCHAR(255)
)

CREATE TABLE Transactions
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	CounterpartyId INT FOREIGN KEY REFERENCES Counterparties(Id),
	Amount INT NOT NULL,
	TransactionDate DATE NOT NULL,
	Note VARCHAR(255)
)

CREATE TABLE InvoiceLines
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	TransactionId INT NOT NULL FOREIGN KEY REFERENCES Transactions(Id),
	ArticleId INT NOT NULL FOREIGN KEY REFERENCES Articles(Id),
	Price INT NOT NULL,
	Note VARCHAR(255)
)

CREATE TABLE Snapshoots
(
	Id INT NOT NULL PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	Value INT NOT NULL,
	SnapshootDate DATE NOT NULL
)


