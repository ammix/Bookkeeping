using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using BookkeepingServer.Views;

namespace BookkeepingServer.Models
{
	public class TransactionRepository
	{
		int id;
		string date;
		string time;
		string counterparty;
		string article;
		string price;
		string note;
        string comment;
        string amount;
		string account;
		string balance;
		string currency;

		IDataReader dr;
		string connectionString = ConfigurationManager.ConnectionStrings["RemoteSqlServer"].ConnectionString;

        public IEnumerable<FinDay> GetTransactions(int month)
		{
			var finDays = new List<FinDay>();
			var culture = CultureInfo.GetCultureInfo("uk-UA");

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
                var cmdText = $"SELECT * FROM MainView WHERE DATEPART(month, [DATE]) = {month} ORDER BY [Id] DESC";
				var command = new SqlCommand(cmdText, connection);
				using (dr = command.ExecuteReader())
				{
					while (dr.Read())
					{
						id = (int) dr["Id"];
						var dateTime = (DateTime)dr["Date"];
						date = dateTime.ToString(culture.DateTimeFormat.ShortDatePattern, culture);
						time = dateTime.ToString(culture.DateTimeFormat.ShortTimePattern, culture);
						counterparty = GetValue("Counterparty");
						article = GetValue("Article");
						price = $"{GetValue("Price"):N}";
						note = GetValue("Note");
						comment = GetValue("Comment");
						amount = $"{GetValue("Amount"):N}";
						account = GetValue("Account");
						balance = $"{GetValue("Balance"):N}";
						currency = GetValue("Currency");

						if (finDays.Exists(x => x.Date == date))
						{
							var finPeriod = finDays.Find(x => x.Date == date);
							if (finPeriod.FinTransactions.Exists(x => x.Id == id))
							{
								var finTransaction = finPeriod.FinTransactions.Find(x => x.Id == id);
								if (article != null || price != null || note != null)
									finTransaction.InvoiceLines.Add(CreateInvoiceLineView());
							}
							else
							{
								finPeriod.FinTransactions.Add(CreateFinTransactionView());
							}
						}
						else
						{
							finDays.Add(CreateFinDayView());
						}
					}
				}
			}

			return finDays;
		}

		FinDay CreateFinDayView()
		{
			return new FinDay
			{
				Date = date,
				FinTransactions = new List<FinTransaction> { CreateFinTransactionView() }
			};
		}

		FinTransaction CreateFinTransactionView()
		{
			return new FinTransaction
			{
				Id = id,
				Time = time,
				Counterparty = counterparty,
				Amount = amount,
				Account = account,
				Balance = balance,
				Currency = currency,
				Note = comment,
				InvoiceLines = (article != null) ? new List<InvoiceLine> { CreateInvoiceLineView() } : null
			};
		}

		InvoiceLine CreateInvoiceLineView()
		{
            return new InvoiceLine
            {
                Article = "• " + article,
				Price = price,
				Note = note
			};
		}

		string GetValue(string fieldName)
		{
			var field = dr[fieldName];
			return (field is DBNull) ? null : field.ToString();
		}
	}
}