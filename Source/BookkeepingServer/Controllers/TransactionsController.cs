using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Http;
using BookkeepingServer.Views;

namespace BookkeepingServer.Controllers
{
	public class TransactionsController : ApiController
	{
		// api/transactions/{month}
		public IEnumerable<FinPeriod> GetTransactions()
		{
			return GetTransactionsFromDb();
		}

		#region repository access
		static IEnumerable<FinPeriod> GetTransactionsFromDb()
		{
			var finPeriods = new List<FinPeriod>();

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
						var article = GetValue(dr, "Article");
						var price = GetValue(dr, "Price");
						var transacNote = GetValue(dr, "Note");
						var lineNote = GetValue(dr, "LineNote");
						var amount = dr["Amount"].ToString();
						var acount = dr["Acount"].ToString();
						var balance = dr["Balance"].ToString();
						var currency = dr["Currency"].ToString();
						var id = dr["Id"].ToString();

						if (finPeriods.Exists(x => x.Date == date))
						{
							var finPeriod = finPeriods.Find(x => x.Date == date);
							//if (finPeriod.FinTransactions.Exists(x => x.Counterparty == counterparty))
							if (finPeriod.FinTransactions.Exists(x => x.Id == id))
							{
								if (article != null)
								{
									//var finTransaction = finPeriod.FinTransactions.Find(x => x.Counterparty == counterparty);
									var finTransaction = finPeriod.FinTransactions.Find(x => x.Id == id);
									finTransaction.InvoiceLines.Add(CreateInvoiceLineView(article, price, lineNote));
								}
							}
							else
							{
								finPeriod.FinTransactions.Add(CreateFinTransactionView(counterparty, article, price, lineNote, transacNote, amount, acount, balance, currency));
							}
						}
						else
						{
							finPeriods.Add(CreateFinPeriodView(date, counterparty, article, price, lineNote, transacNote, amount, acount, balance, currency));
						}
					}
				}
			}

			return finPeriods;
		}

		static FinPeriod CreateFinPeriodView(string date, string counterparty, string article, string price, string lineNote, string note, string amount, string account, string balance, string currency)
		{
			return new FinPeriod
			{
				Date = date,
				FinTransactions = new List<FinTransaction> { CreateFinTransactionView(counterparty, article, price, lineNote, note, amount, account, balance, currency) }
			};
		}

		static FinTransaction CreateFinTransactionView(string counterparty, string article, string price, string lineNote, string note, string amount, string account, string balance, string currency)
		{
			return new FinTransaction
			{
				Counterparty = counterparty,
				Amount = amount,
				Account = account,
				Balance = balance,
				Currency = currency,
				Note = note,
				InvoiceLines = (article != null) ? new List<InvoiceLine> { CreateInvoiceLineView(article, price, lineNote) } : null
			};
		}

		static InvoiceLine CreateInvoiceLineView(string article, string price, string note)
		{
			return new InvoiceLine
			{
				Article = article,
				Price = price,
				Note = note
			};
		}

		static string GetValue(IDataRecord dr, string fieldName)
		{
			var field = dr[fieldName];
			return (field is DBNull) ? null : field.ToString();
		}
		#endregion
	}
}
