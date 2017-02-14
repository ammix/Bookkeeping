using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class LocalDb
	{
		int transactionId;
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

		//var connectionString = "workstation id=Bookkeeping.mssql.somee.com;packet size=4096;user id=ammix_SQLLogin_1;pwd=8h1c8vsmnk;data source=Bookkeeping.mssql.somee.com;persist security info=False;initial catalog=Bookkeeping";
		const string connectionString = "data source=localhost;initial catalog=Bookkeeping;user=sa;password=1";

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
				var cmdText =
					$"INSERT INTO [Transactions] " +
					$"(UserId, AccountId, CounterpartyId, Amount, TransactionDate, Invoice, Note) " +
					$"SELECT 1, " +
					$"(SELECT [Id] FROM [Accounts] WHERE [Name] = N'{viewModel.Account}'), " +
					$"(SELECT [Id] FROM [Counterparties] WHERE [Name] = N'{viewModel.Counterparty}'), " +
					$"{viewModel.Amount.Replace(',', '.')}, " +
					$"CONVERT(DATETIME, '{viewModel.Date}', 104), " +
					$"NULL, " +
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
					$"INNER JOIN [Counterparties] c ON c.Name = N'{viewModel.Counterparty}' " +
					$"WHERE t.Id = {viewModel.Id}";

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
				var getSnapshotsSql = new SqlCommand("SELECT [Account], [Amount] FROM [SnapshotsView]", connection);
				using (var dr = getSnapshotsSql.ExecuteReader())
				{
					while (dr.Read())
						balance.Add((string)dr["Account"], (decimal)dr["Amount"]);
				}

				//var cmdText = $"SELECT * FROM [MainView] WHERE DATEPART(month, [DATE]) = {month} ORDER BY [Date] ASC";
				var cmdText = "SELECT * FROM [MainView] ORDER BY [Date] ASC";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						transactionId = (int)dr["Id"];
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
							if (finDay.Nodes.Exists(x => x.Tree == counterparty && decimal.Parse(x.Amount) == amount))
							{
								if (article != null)
								{
									var invoiceLine = finDay.Nodes.Find(x => x.Tree == counterparty);
									invoiceLine.Nodes.Add(CreateInvoiceLineView());
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
				note: note
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
	}
}
