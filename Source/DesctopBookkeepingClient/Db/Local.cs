using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class LocalDb
	{
		string date;
		string time;
		string counterparty;
		string article;
		string price;
		string note;
		string comment;
		decimal amount;
		string account;
		decimal balance = 0;
		string currency;

		//var connectionString = "workstation id=Bookkeeping.mssql.somee.com;packet size=4096;user id=ammix_SQLLogin_1;pwd=8h1c8vsmnk;data source=Bookkeeping.mssql.somee.com;persist security info=False;initial catalog=Bookkeeping";
		const string connectionString = "data source=localhost;initial catalog=Bookkeeping;user=sa;password=1";

		public static List<AccountModel> GetAccount()
		{
			var accounts = new List<AccountModel>();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT [Id], [Name] FROM [Accounts]";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while(dr.Read())
					{
						accounts.Add(new AccountModel { Id = (int)dr["Id"], Account = (string)dr["Name"] });
					}
				}
			}
			return accounts;
		}

		public static void PostTransaction(TreeListViewModel viewModel)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				//var cmdText = $"SELECT [Id] FROM [Accounts] WHERE [Name] = {viewModel.Account}";

				//INSERT INTO[Transactions] (Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)
				//VALUES(1, 1, 1, 1, -100, '2016-06-01', NULL)
				var cmdText = "INSERT INTO [Transactions](Id, UserId, AccountId, CounterpartyId, Amount, TransactionDate, Note)" +
					$"VALUES({viewModel.TransacId}, {viewModel.UserId}, {viewModel.AccountId}, {viewModel.CounterId}, {viewModel.Amount}, {viewModel.Tree}, {viewModel.Comment})";
				var command = new SqlCommand(cmdText, connection);
				command.ExecuteNonQuery();
			}
		}

		public List<TreeListViewModel> GetTransactions()
		{
			var finDays = new List<TreeListViewModel>();
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var command1 = new SqlCommand("SELECT [Amount] FROM [Snapshots]", connection);
				using (var dr = command1.ExecuteReader())
				{
					dr.Read();
					balance = (decimal)dr["Amount"];
				}

				var cmdText = "SELECT * FROM [MainView] ORDER BY [Id] ASC";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						//transactionId = (int)dr["Id"];
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
								finDay.Nodes.Add(CreateFinTransactionView());
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
			if (account == "Готівка")
				balance += amount;

			return new TreeListViewModel
			(
				counterparty: counterparty,
				amount: amount.ToString("N"),
				comment: comment,
				account: account,
				balance: account == "Готівка"? balance.ToString("N"): "",
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
