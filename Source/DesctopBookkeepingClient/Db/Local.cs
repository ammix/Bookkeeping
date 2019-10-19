using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class LocalDb
	{
		int? transactionId;
		int? lineId;
		DateTime date;
		string time;
		string counterparty;
		string article;
		string price;
		string note;
		string comment;
		decimal amount;
		string account;
		string balance;
		string currency;

		readonly static string connectionString = ConfigurationManager.ConnectionStrings["BookkeepingDb"].ConnectionString;

		public static string[] GetArticles()
		{
			var lables = new List<string>();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT [Label] FROM [Articles]";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						lables.Add((string)dr["Label"]);
					}
				}
			}
			return lables.ToArray();
		}

		public static string[] GetCounterparties()
		{
			var counterparties = new List<string>();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT [Name] FROM [Counterparties]";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						counterparties.Add((string)dr["Name"]);
					}
				}
			}
			return counterparties.ToArray();
		}

		public static string[] GetAccounts()
		{
			var accounts = new List<string>();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT [Name] FROM [Accounts]";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						accounts.Add((string)dr["Name"]);
					}
				}
			}
			return accounts.ToArray();
		}

		public static int InsertTransaction(TransactionModel transaction)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $@"
INSERT INTO [Transactions] (UserId, AccountId, CounterpartyId, Amount, IsTrsBalance, Balance, [Date], [Time], Note, Invoice, [Order])
	SELECT 1,
	(SELECT [Id] FROM [Accounts] WHERE [Name] = N'{transaction.Account}'), 
	(SELECT [Id] FROM [Counterparties] WHERE [Name] = N'{transaction.Counterparty}'), 
	{transaction.Amount},
	'FALSE',
	{transaction.Amount} + (SELECT TOP 1 [Balance] FROM [Transactions] ORDER BY [Date] DESC, [Order] DESC),
	@Date,
	NULL,
	N'{transaction.Comment}',
	NULL,
	(SELECT COALESCE(MAX([Order]), 0)+1 FROM [Transactions] WHERE Date = @Date)";

				var param = new SqlParameter
				{
					ParameterName = "@Date",
					Value = transaction.Date,
					SqlDbType = SqlDbType.Date
				};

				var command = new SqlCommand(cmdText, connection);
				command.Parameters.Add(param);
				command.ExecuteNonQuery();

				var lastIdSelect = "SELECT TOP(1) Id FROM [Transactions] ORDER BY [Id] DESC";
				var getLastIdCommand = new SqlCommand(lastIdSelect, connection);
				using (var dr = getLastIdCommand.ExecuteReader())
				{
					dr.Read();
					return (int)dr["Id"];
				}
			}
		}

		public static void UpdateTransaction(TransactionModel transaction)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"UPDATE t SET " +
					$"AccountId = a.Id, " +
					$"CounterpartyId = c.Id, " +
					$"Note = N'{transaction.Comment}', " +
					$"Amount = {transaction.Amount} " +
					$"FROM [Transactions] t " +
					$"INNER JOIN [Accounts] a ON a.Name = N'{transaction.Account}' " +
					$"LEFT OUTER JOIN [Counterparties] c ON c.Name = N'{transaction.Counterparty}' " +
					$"WHERE t.Id = {transaction.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void InsertInvoiceLine(InvoiceLineModel invoiceLine)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"INSERT INTO [InvoiceLines] " +
					$"(UserId, TransactionId, ArticleId, Price, Note) " +
					$"SELECT 1, " + // Values(...)
					$"{invoiceLine.ParentTransactionId}, " +
					$"(SELECT [Id] FROM [Articles] WHERE [Label] = N'{invoiceLine.Article}'), " +
					$"{invoiceLine.Price}, " +
					$"N'{invoiceLine.Note}'";

				var command = new SqlCommand(cmdText, connection);

				command.ExecuteNonQuery();
			}
		}

		public static void UpdateInvoiceLine(InvoiceLineModel invoiceLine)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"UPDATE line SET " +
					$"ArticleId = a.Id, " + //TODO: TransactionId not can be changed ??
					$"Price = {invoiceLine.Price}, " +
					$"Note = N'{invoiceLine.Note}' " +
					$"FROM [InvoiceLines] line " +
					$"INNER JOIN [Articles] a ON a.Label = N'{invoiceLine.Article}' " +
					$"WHERE line.Id = {invoiceLine.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void RemoveTransaction(TransactionModel transaction)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $"DELETE FROM [Transactions] WHERE Id = {transaction.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void RemoveInvoiceLine(InvoiceLineModel invoiceLine)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $"DELETE FROM [InvoiceLines] WHERE Id = {invoiceLine.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void MoveTransaction(TransactionModel transaction1, TransactionModel transaction2)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				// use DB TRANSACTION
				var cmdText = $"DECLARE @order1 INT = (SELECT [Order] FROM [Transactions] WHERE Id = {transaction1.Id}) " +
							  $"DECLARE @order2 INT = (SELECT [Order] FROM [Transactions] WHERE Id = {transaction2.Id}) " +

				              "UPDATE [Transactions] " +
				              "SET [Order] = @order2 " +
				              $"WHERE Id = {transaction1.Id} " +

				              "UPDATE [Transactions] " +
				              "SET [Order] = @order1 " +
				              $"WHERE Id = {transaction2.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void MoveTransactionIntoDate(TransactionModel transaction, FinDayModel finDay, string flag)
		{
			throw new NotImplementedException();
		}

		// Months have to be in order (without gaps)
		public List<ITreeListViewModel> GetTransactions(/*int month*/)
		{
			var finDays = new List<ITreeListViewModel>();
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var command = new SqlCommand(mainViewSql, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						transactionId = (int?) dr["Id"];
						lineId = ConvertFromDbVal<int?> (dr["LineId"]);
						var dateTime = (DateTime)dr["Date"];
						//date = dateTime.ToString("d MMMM yyyy (dddd)", culture);
						date = dateTime.Date;
						time = dateTime.ToString(culture.DateTimeFormat.ShortTimePattern, culture);
						counterparty = dr["Counterparty"].ToString();
						article = GetValue(dr, "Article");
						price = GetValue(dr, "Price");
						note = GetValue(dr, "Note");
						comment = GetValue(dr, "Comment");
						amount = (decimal)dr["Amount"]; //$"{dr["Amount"]:N}";
						account = dr["Account"].ToString();
						balance = $"{dr["Balance"]:N}";
						//balance = snapshotReader.GetInitialAccountValue(account, dateTime);
						//balance = Balance.GetValue(account, transactionId).ToString("N");
						currency = GetValue(dr, "Currency");

						//if (finDays.Exists(trs => trs.Column1 == date))
						if (finDays.Exists(trs => trs.Date.Date == date.Date))
						{
							var finDay = finDays.Find(trs => trs.Date.Date == date.Date);
							//if (finDay.Children.Exists(x => x.Column1 == counterparty && decimal.Parse(x.Column2) == amount))
							if (finDay.Children.Exists(x => x.Id == transactionId))
							{
								if (article != null)
								{
									//var invoiceLine = finDay.Children.Find(x => x.Column1 == counterparty);
									var invoiceLine = finDay.Children.Find(x => x.Id == transactionId);
									invoiceLine.AddChild(CreateInvoiceLineView());
								}
							}
							else
							{
								finDay.AddChild(CreateFinTransactionView());
							}
						}
						else
						{
							finDays.Add(CreateFinDayView()); //CreateNewFinDay
						}
					}
				}
			}

			finDays.Reverse();
			foreach (var day in finDays)
				day.Children.Reverse();

			return finDays;
		}

		public void RebuildBalance()
		{
			var cmdText = @"
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
			INSERT INTO @Balances(AccountId, Balance)
			SELECT[Id], 0 AS Balance FROM[Bookkeeping].[dbo].[Accounts]

			DECLARE row CURSOR FOR
				SELECT[Id], [AccountId], [Amount], [IsTrsBalance], [Balance] AS RealBalance
					FROM[Bookkeeping].[dbo].[Transactions]
						ORDER BY [Date], [Order]

			UPDATE[Bookkeeping].[dbo].[Transactions]
				SET[Balance] = 0
					WHERE[IsTrsBalance] = 0

			OPEN row
				FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance
				WHILE @@FETCH_STATUS = 0
				BEGIN
					SET @Balance = (SELECT Balance FROM @Balances WHERE AccountId = @AccountId)
					SET @Balance = @Balance + @Amount

					IF(@IsTrsBalance = 1 AND @Balance != @RealBalance)
					BREAK

					UPDATE @Balances SET Balance = @Balance
						WHERE AccountId = @AccountId

					IF(@IsTrsBalance = 0)
						UPDATE[Bookkeeping].[dbo].[Transactions] SET[Balance] = @Balance
							WHERE Id = @Id

					FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance
				END
			CLOSE row
			DEALLOCATE row";

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}

		}

		FinDayModel CreateFinDayView()
		{
			return new FinDayModel
			(
				date: date,
				transactions: new List<ITreeListViewModel> { CreateFinTransactionView() }
			);
		}

		ITreeListViewModel CreateFinTransactionView()
		{
			var transactionModel = new TransactionModel
			(
				id: transactionId,
				counterparty: counterparty,
				amount: amount, //.ToString("N"), //TODO
				comment: comment,
				account: account,
				//balance: balance.ContainsKey(account)? balance[account].ToString("N"): "",
				balance: balance,
				time: time
			);
			transactionModel.AddChildren((article != null) ? new List<ITreeListViewModel> { CreateInvoiceLineView() } : null);
			return transactionModel;
		}

		ITreeListViewModel CreateInvoiceLineView()
		{
			return new InvoiceLineModel
			(

				article: article,
				price: decimal.Parse(price), //TODO
				note: note,
				id: lineId
			);
		}

		static string GetValue(IDataReader dr, string fieldName)
		{
			var field = dr[fieldName];
			if (field.GetType().ToString() == "System.String")
				return (field is DBNull) ? null : field.ToString();
			else
				return (field is DBNull) ? null : ((decimal)field).ToString("N");
		}

		static T ConvertFromDbVal<T>(object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return default(T); // returns the default value for the type
			}
			else
			{
				return (T)obj;
			}
		}

		const string mainViewSql = @"
			SELECT
			t.Id,
			i.Id AS LineId,
			t.Date AS Date,
			c.Name AS Counterparty,
			ar.Label AS Article,
			i.Price,
			i.Note AS Note,
			t.Note AS Comment,
			t.Amount,
			ac.Name AS Account,
			t.Balance,
			ac.Currency

			FROM Transactions t
				LEFT OUTER JOIN InvoiceLines i ON i.TransactionId = t.Id
				LEFT OUTER JOIN Articles ar ON ar.Id = i.ArticleId
				LEFT OUTER JOIN CounterParties c ON c.Id = t.CounterpartyId
				INNER JOIN Accounts ac ON ac.Id = t.AccountId

			ORDER BY [Date], [Order]
			";

		const string getSnapshotSql = @"
			SELECT
			a.[Name] AS [Account],
			s.[Amount]

			FROM [Snapshots] s
				INNER JOIN [Accounts] a ON s.AccountId = a.Id
				WHERE DATEPART(day, s.[SnapshotDate]) = 1
			";
	}
}

/*
 
DECLARE @StartDate DATE = '2017-01-01'
DECLARE @EndDate DATE = '2017-01-02'

--SELECT * FROM [Bookkeeping].[dbo].[Transactions]
--	WHERE [Date] = @StartDate
--	  ORDER BY [Order] DESC
----------------------------------------------
UPDATE[Bookkeeping].[dbo].[Transactions]
	SET[Balance] = 0
		WHERE[IsTrsBalance] = 0
UPDATE[Bookkeeping].[dbo].[Transactions]
	SET[Amount] = 0
		WHERE[IsTrsBalance] = 1

DECLARE @Id INT
DECLARE @AccountId INT
DECLARE @Amount MONEY
DECLARE @RealBalance MONEY
DECLARE @IsTrsBalance BIT
DECLARE @Date DATE

DECLARE @Balance MONEY = 0
DECLARE @Balances TABLE
(
	AccountId INT NOT NULL,
	Balance MONEY
)
DECLARE row CURSOR FOR
	SELECT [Id], [AccountId], [Amount], [IsTrsBalance], [Balance] AS RealBalance, [Date]
		FROM[Bookkeeping].[dbo].[Transactions]
			WHERE [Date] >= @StartDate AND [Date] <= @EndDate
				ORDER BY [Date], [Order]

OPEN row
	FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance, @Date
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Balance = (SELECT Balance FROM @Balances WHERE AccountId = @AccountId)
		IF @Balance IS NULL
			BEGIN
				INSERT INTO @Balances (AccountId, Balance)
					VALUES(@AccountId, @RealBalance)
				CONTINUE
			END

		SET @Balance = @Balance + @Amount

		IF(@IsTrsBalance = 1 AND @Balance != @RealBalance)
		BREAK

		UPDATE @Balances SET Balance = @Balance
			WHERE AccountId = @AccountId

		IF(@IsTrsBalance = 0)
			UPDATE[Bookkeeping].[dbo].[Transactions] SET[Balance] = @Balance
				WHERE Id = @Id

		FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance, @Date
	END
CLOSE row
DEALLOCATE row
 */
