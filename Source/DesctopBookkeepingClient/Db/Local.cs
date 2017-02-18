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
		string date;
		string time;
		string counterparty;
		string article;
		string price;
		string note;
		string comment;
		decimal amount;
		string account;
		Dictionary<string, decimal> balance = new Dictionary<string, decimal>();
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

		public static void InsertTransaction(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $@"
					INSERT INTO [Transactions] 
					(UserId, AccountId, CounterpartyId, Amount, TransactionDate, Invoice, Note) 
					SELECT 1, 
					(SELECT [Id] FROM [Accounts] WHERE [Name] = N'{viewModel.Account}'), 
					(SELECT [Id] FROM [Counterparties] WHERE [Name] = N'{viewModel.Counterparty}'), 
					{viewModel.Amount.Replace(',', '.')}, 
					CONVERT(DATETIME, '{viewModel.Date}', 104), 
					NULL, 
					N'{viewModel.Comment}'
				";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void InsertInvoiceLine(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"INSERT INTO [InvoiceLines] " +
					$"(UserId, TransactionId, ArticleId, Price, Note) " +
					$"SELECT 1, " + // Values(...)
					$"{viewModel._parent.Id}, " +
					$"(SELECT [Id] FROM [Articles] WHERE [Label] = N'{viewModel.Counterparty}'), " + //TODO: Counterparty ?
					$"{viewModel.Amount.Replace(',', '.')}, " +
					$"N'{viewModel.Comment}'";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void UpdateTransaction(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"UPDATE t SET " +
					$"AccountId = a.Id, " +
					$"CounterpartyId = c.Id, " +
					$"Note = N'{viewModel.Comment}', " +
					$"Amount = {viewModel.Amount.Replace(',', '.')} " +
					$"FROM [Transactions] t " +
					$"INNER JOIN [Accounts] a ON a.Name = N'{viewModel.Account}' " +
					$"LEFT OUTER JOIN [Counterparties] c ON c.Name = N'{viewModel.Counterparty}' " +
					$"WHERE t.Id = {viewModel.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void UpdateInvoiceLine(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText =
					$"UPDATE line SET " +
					$"ArticleId = a.Id, " + //TODO: TransactionId not can be changed ??
					$"Price = {viewModel.Amount.Replace(',', '.')}, " +
					$"Note = N'{viewModel.Comment}' " +
					$"FROM [InvoiceLines] line " +
					$"INNER JOIN [Articles] a ON a.Label = N'{viewModel.Counterparty}' " + // TODO: Counterpary not Article must be here??
					$"WHERE line.Id = {viewModel.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void RemoveTransaction(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $"DELETE FROM [Transactions] WHERE Id = {viewModel.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public static void RemoveInvoiceLine(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = $"DELETE FROM [InvoiceLines] WHERE Id = {viewModel.Id}";

				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		// Months have to be in order (without gaps)
		public List<TreeListViewModel> GetTransactions(/*int month*/)
		{
			var finDays = new List<TreeListViewModel>();
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Rule: snapshots must be on 00:00 of 1-st new month (and day if exist)
				//var getSnapshotsSql = new SqlCommand($"SELECT [Account], [Amount] FROM [SnapshotsView] WHERE DATEPART(month, [Date]) = {month}", connection);
				var getSnapshotsSql = new SqlCommand(getSnapshotSql, connection);
				using (var dr = getSnapshotsSql.ExecuteReader())
				{
					while (dr.Read())
						balance.Add((string)dr["Account"], (decimal)dr["Amount"]);
				}

				//var cmdText = $"SELECT * FROM [MainView] WHERE DATEPART(month, [DATE]) = {month} ORDER BY [Date] ASC";
				var command = new SqlCommand(mainViewSql, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						transactionId = (int?) dr["Id"];
						lineId = ConvertFromDbVal<int?> (dr["LineId"]);
						var dateTime = (DateTime)dr["Date"];
						date = dateTime.ToString(culture.DateTimeFormat.ShortDatePattern, culture);
						time = dateTime.ToString(culture.DateTimeFormat.ShortTimePattern, culture);
						counterparty = dr["Counterparty"].ToString();
						article = GetValue(dr, "Article");
						price = GetValue(dr, "Price");
						note = GetValue(dr, "Note");
						comment = GetValue(dr, "Comment");
						amount = (decimal)dr["Amount"]; //$"{dr["Amount"]:N}";
						account = dr["Account"].ToString();
						//balance = $"{dr["Balance"]:N}";
						//balance = snapshotReader.GetInitialAccountValue(account, dateTime);
						//balance = Balance.GetValue(account, transactionId).ToString("N");
						currency = GetValue(dr, "Currency");

						if (finDays.Exists(trs => trs.Tree == date))
						{
							var finDay = finDays.Find(trs => trs.Tree == date);
							//if (finDay.Nodes.Exists(x => x.Tree == counterparty && decimal.Parse(x.Amount) == amount))
							if (finDay.Nodes.Exists(x => x.Id == transactionId))
							{
								if (article != null)
								{
									//var invoiceLine = finDay.Nodes.Find(x => x.Tree == counterparty);
									var invoiceLine = finDay.Nodes.Find(x => x.Id == transactionId);
									invoiceLine.Add(CreateInvoiceLineView());
								}
							}
							else
							{
								finDay.Add(CreateFinTransactionView());
							}
						}
						else
						{
							finDays.Add(CreateFinDayView());
						}
					}
				}
			}

			finDays.Reverse();
			foreach (var day in finDays)
				day.Nodes.Reverse();

			return finDays;
		}

		TreeListViewModel CreateFinDayView()
		{
			return new TreeListViewModel
			(
				date: date,
				transactions: new List<TreeListViewModel> { CreateFinTransactionView() }
			);
		}

		TreeListViewModel CreateFinTransactionView()
		{
			if (balance.ContainsKey(account))
				balance[account] += amount;

			return new TreeListViewModel
			(
				id: transactionId,
				counterparty: counterparty,
				amount: amount.ToString("N"),
				comment: comment,
				account: account,
				balance: balance.ContainsKey(account)? balance[account].ToString("N"): "",
				time: time,
				articles: (article != null) ? new List<TreeListViewModel> { CreateInvoiceLineView() } : null
			);
		}

		TreeListViewModel CreateInvoiceLineView()
		{
			return new TreeListViewModel
			(

				article: article,
				price: price,
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
				LEFT OUTER JOIN CounterParties c ON c.Id = t.CounterpartyId
				INNER JOIN Accounts ac ON ac.Id = t.AccountId

			ORDER BY [Date] ASC
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
