using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Http;

namespace BookkeepingServer.Controllers
{
	public class TransactionView
	{
		// Date, Counterparty, InvoiceLine
		public string ColumnWithHierarchy;
		public string Amount;
		public string Comment;
		public string Acount;
		public string Balance;
		public string Currency;
		public List<TransactionView> Children;
	}

	public class TransactionsController : ApiController
	{
		public IEnumerable<TransactionView> GetTransactions()
		{
			return GetTransactionsFromDb();
		}

		#region repository access
		static List<TransactionView> GetTransactionsFromDb()
		{
			var transactions = new List<TransactionView>();
			var connectionString = "workstation id=Bookkeeping.mssql.somee.com;packet size=4096;user id=ammix_SQLLogin_1;pwd=8h1c8vsmnk;data source=Bookkeeping.mssql.somee.com;persist security info=False;initial catalog=Bookkeeping";
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT * FROM MainView";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						var date = ((DateTime)dr["Date"]).ToString(culture.DateTimeFormat.ShortDatePattern, culture);
						var counterparty = dr["Counterparty"].ToString();
						var article = dr["Article"].ToString();
						var price = dr["Price"].ToString();
						var note = dr["Note"].ToString();
						var amount = dr["Amount"].ToString();
						var acount = dr["Acount"].ToString();
						var balance = dr["Balance"].ToString();
						var currency = dr["Currency"].ToString();

						if (transactions.Exists(x => x.ColumnWithHierarchy == date))
						{
							var transaction = transactions.Find(x => x.ColumnWithHierarchy == date);
							if (transaction.Children.Exists(x => x.ColumnWithHierarchy == counterparty))
							{
								var invoiceLine = transaction.Children.Find(x => x.ColumnWithHierarchy == counterparty);
								invoiceLine.Children.Add(GetItem(article, price, note));
							}
							else
							{
								transaction.Children.Add(GetItem(counterparty, article, price, note, amount, acount, balance, currency));
							}
						}
						else
						{
							transactions.Add(GetItem(date, counterparty, article, price, note, amount, acount, balance, currency));
						}
					}
				}
			}

			return transactions;
		}

		static TransactionView GetItem(string date, string counterparty, string article, string price, string note, string amount, string acount, string balance, string currency)
		{
			return new TransactionView
			{
				ColumnWithHierarchy = date,
				Children = new List<TransactionView> { GetItem(counterparty, article, price, note, amount, acount, balance, currency) }
			};
		}

		static TransactionView GetItem(string counterparty, string article, string price, string note, string amount, string acount, string balance, string currency)
		{
			return new TransactionView
			{
				ColumnWithHierarchy = counterparty,
				Amount = amount,
				Acount = acount,
				Balance = balance,
				Currency = currency,
				Children = new List<TransactionView> { GetItem(article, price, note) }
			};
		}

		static TransactionView GetItem(string article, string price, string note)
		{
			return new TransactionView
			{
				ColumnWithHierarchy = article,
				Amount = price,
				Comment = note
			};
		}
	#endregion
	}
}
