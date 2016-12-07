using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	class LocalDb
	{
		public static List<TransactionView> GetTransactions()
		{
			var transactions = new List<TransactionView>();
			var connectionString = "workstation id=Bookkeeping.mssql.somee.com;packet size=4096;user id=ammix_SQLLogin_1;pwd=8h1c8vsmnk;data source=Bookkeeping.mssql.somee.com;persist security info=False;initial catalog=Bookkeeping";
			//var connectionString = "data source=localhost;initial catalog=Bookkeeping;user=sa;password=1";
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var cmdText = "SELECT * FROM MainView ORDER BY [Id] DESC";
				var command = new SqlCommand(cmdText, connection);
				using (var dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						var date = ((DateTime)dr["Date"]).ToString(culture.DateTimeFormat.ShortDatePattern, culture);
						var counterparty = dr["Counterparty"].ToString();
						var article = GetValue(dr, "Article");
						var price = GetValue(dr, "Price");
						var lineNote = GetValue(dr, "Note");
						var note = GetValue(dr, "Comment");
                        var amount = $"{dr["Amount"]:N}";
						var account = dr["Account"].ToString();
						var balance = $"{dr["Balance"]:N}";
                        var currency = ""; // dr["Currency"].ToString();

						if (transactions.Exists(trs => trs.Counterparty == date))
						{
							var transaction = transactions.Find(trs => trs.Counterparty == date);
							if (transaction.Nodes.Exists(x => x.Counterparty == counterparty && x.Amount == amount))
							{
								if (article != null)
								{
									var invoiceLine = transaction.Nodes.Find(x => x.Counterparty == counterparty);
									invoiceLine.Nodes.Add(GetItem(article, price, lineNote));
								}
							}
							else
							{
								transaction.Nodes.Add(GetItem(counterparty, article, price, lineNote, note, amount, account, balance, currency));
							}
						}
						else
						{
							transactions.Add(GetItem(date, counterparty, article, price, lineNote, note, amount, account, balance, currency));
						}
					}
				}
			}

			return transactions;
		}

		static TransactionView GetItem(string date, string counterparty, string article, string price, string lineNote, string note, string amount, string acount, string balance, string currency)
		{
			return new TransactionView
			{
				Counterparty = date,
				Nodes = new List<TransactionView> { GetItem(counterparty, article, price, lineNote, note, amount, acount, balance, currency) }
			};
		}

		static TransactionView GetItem(string counterparty, string article, string price, string lineNote, string note, string amount, string acount, string balance, string currency)
		{
			return new TransactionView
			{
				Counterparty = counterparty,
                Time = "12:00",
				Amount = amount,
				Acount = acount,
				Balance = balance,
				Currency = currency,
				Comment = note,
				Nodes = (article != null) ? new List<TransactionView> { GetItem(article, price, lineNote) } : null
			};
		}

		static TransactionView GetItem(string article, string price, string lineNote)
		{
			return new TransactionView
			{
				Counterparty = article,
				Amount = price,
				Comment = lineNote
			};
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
