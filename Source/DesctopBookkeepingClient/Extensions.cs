using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	class Extensions
	{
		public static List<TransactionView> GetTransactionsFromRemoteDb()
		{
			var client = new RestClient("http://money.somee.com/");
			var request = new RestRequest("api/transactions");

			IRestResponse response = client.Get(request);
			var view = JsonConvert.DeserializeObject<List<FinPeriod>>(response.Content);

			var transactions = new List<TransactionView>();
			foreach (var finPeriod in view)
			{
				transactions.Add(new TransactionView { Hierarchy = finPeriod.Date, Children = ToView(finPeriod.FinTransactions) });
			}
			return transactions;
		}

		static List<TransactionView> ToView(List<FinTransaction> trs)
		{
			var transaction = new List<TransactionView>();
			foreach (var t in trs)
			{
				transaction.Add(new TransactionView { Hierarchy = t.Counterparty, Amount = t.Amount, Comment = t.Note, Acount = t.Account, Balance = t.Balance, Currency = t.Currency, Children = ToView(t.InvoiceLines) });
			}
			return transaction;
		}

		static List<TransactionView> ToView(List<InvoiceLine> ils)
		{
			if (ils == null) return null;
			var transaction = new List<TransactionView>();
			foreach (var i in ils)
			{
				transaction.Add(new TransactionView { Hierarchy = i.Article, Amount = i.Price, Comment = i.Note });
			}
			return transaction;
		}

		static string GetValue(IDataReader dr, string fieldName)
		{
			var field = dr[fieldName];
			return (field is DBNull) ? null : field.ToString();
		}

		public static List<TransactionView> GetTransactionsFromDb()
		{
			var transactions = new List<TransactionView>();
			//var connectionString = "workstation id=Bookkeeping.mssql.somee.com;packet size=4096;user id=ammix_SQLLogin_1;pwd=8h1c8vsmnk;data source=Bookkeeping.mssql.somee.com;persist security info=False;initial catalog=Bookkeeping";
			var connectionString = "data source=localhost;initial catalog=Bookkeeping;user=sa;password=1";
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
						var lineNote = GetValue(dr, "LineNote");
						var note = GetValue(dr, "Note");
						var amount = dr["Amount"].ToString();
						var acount = dr["Acount"].ToString();
						var balance = dr["Balance"].ToString();
						var currency = dr["Currency"].ToString();

						if (transactions.Exists(x => x.Hierarchy == date))
						{
							var transaction = transactions.Find(x => x.Hierarchy == date);
							if (transaction.Children.Exists(x => x.Hierarchy == counterparty && x.Amount == amount))
							{
								if (article != null)
								{
									var invoiceLine = transaction.Children.Find(x => x.Hierarchy == counterparty);
									invoiceLine.Children.Add(GetItem(article, price, lineNote));
								}
							}
							else
							{
								transaction.Children.Add(GetItem(counterparty, article, price, lineNote, note, amount, acount, balance, currency));
							}
						}
						else
						{
							transactions.Add(GetItem(date, counterparty, article, price, lineNote, note, amount, acount, balance, currency));
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
				Hierarchy = date,
				Children = new List<TransactionView> { GetItem(counterparty, article, price, lineNote, note, amount, acount, balance, currency) }
			};
		}

		static TransactionView GetItem(string counterparty, string article, string price, string lineNote, string note, string amount, string acount, string balance, string currency)
		{
			return new TransactionView
			{
				Hierarchy = counterparty,
				Amount = amount,
				Acount = acount,
				Balance = balance,
				Currency = currency,
				Comment = note,
				Children = (article != null) ? new List<TransactionView> { GetItem(article, price, lineNote) } : null
			};
		}

		static TransactionView GetItem(string article, string price, string lineNote)
		{
			return new TransactionView
			{
				Hierarchy = article,
				Amount = price,
				Comment = lineNote
			};
		}

		public static List<TransactionView> GetTransactions()
		{
			var trs = new List<TransactionView>
			{
				new TransactionView
				{
					Hierarchy = "1 липня 2016",
					Children = new List<TransactionView>
					{
						new TransactionView
						{
							Hierarchy = "Сільпо",
							Acount = "Готівка",
							Amount = "-100.00",
							Balance = "3568.95",
							Children = new List<TransactionView>
							{
								new TransactionView {Hierarchy = "• молоко", Amount = "      10.50"},
								new TransactionView {Hierarchy = "• хліб", Amount = "      15.20"},
								new TransactionView {Hierarchy = "• черешні", Amount = "      50.50", Comment = "60 грн/кг"}
							}
						},
						new TransactionView
						{
							Hierarchy = "Алейка",
							Acount = "Готівка",
							Amount = "-52.30",
							Balance = "3668.95",
							Children = new List<TransactionView>
							{
								new TransactionView {Hierarchy = "• помідори", Amount = "      30.49", Comment = "25 грн/кг"},
								new TransactionView {Hierarchy = "• яблука", Amount = "      25.25", Comment = "15 грн/кг"}
							}
						},
						new TransactionView
						{
							Hierarchy = "Обмін",
							Acount = "Гаманець UAH",
							Amount = "-2500.00",
							Comment = "25.00 UAH/$",
							Balance = "2150.00"
						},
						new TransactionView
						{
							Hierarchy = "Обмін",
							Acount = "Гаманець $",
							Amount = "100.00",
							Balance = "100.00",
							Currency = "$"
						}
					}
				},

				new TransactionView
				{
					Hierarchy = "2 липня 2016",
					Children = new List<TransactionView>
					{
						new TransactionView
						{
							Hierarchy = "Twinfield",
							Acount = "2600 Агріколь",
							Amount = "50000.00",
							Balance = "75 000.00",
						},
						new TransactionView
						{
							Hierarchy = "Ліко-Світ",
							Acount = "Приват",
							Amount = "-7500.00",
							Balance = "14 569.00",
							Comment = "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						},
						new TransactionView
						{
							Hierarchy = "Метро",
							Acount = "Картка",
							Amount = "-52.30",
							Balance = "5 890.85",
							Children = new List<TransactionView>
							{
								new TransactionView {Hierarchy = "• ікра", Amount = "      100.50"},
								new TransactionView {Hierarchy = "• торт", Amount = "      150.75"},
								new TransactionView {Hierarchy = "• серветки", Amount = "      30.00"}
							}
						},
						new TransactionView
						{
							Hierarchy = "Алейка",
							Acount = "Готівка",
							Amount = "-152.55",
							Balance = "3 700.00"
						}
					}
				}
			};

			return trs;
		}
	}
}
