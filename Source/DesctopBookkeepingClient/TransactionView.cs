using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	//public class ModelWithChildren
	//{
	//	public int ChildCount { get; set; }
	//	public List<ModelWithChildren> Children { get; set; }
	//	public string Label { get; set; }
	//	public ModelWithChildren Parent { get; set; }
	//	public string ParentLabel => Parent == null ? "none" : Parent.Label;

	//	public static List<ModelWithChildren> GetModel()
	//	{
	//		var childModel = new List<ModelWithChildren>();
	//		childModel.Add(new ModelWithChildren { ChildCount = 0, Label = "B", Parent = null });

	//		var model = new List<ModelWithChildren>();
	//		model.Add(new ModelWithChildren { ChildCount = 1, Label = "A", Parent = null, Children = childModel });
	//		return model;
	//	}
	//}


	class TransactionView
	{
		// Date, Counterparty, InvoiceLine
		public string ColumnWithHierarchy;
		public string Amount;
		public string Comment;
		public string Acount;
		public string Balance;
		public string Currency;

        public List<TransactionView> Children;
		public bool HasChildren => Children != null && Children.Count != 0;


		public static List<TransactionView> GetTransactionsFromDb()
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
				Children = new List<TransactionView>{GetItem(article, price, note)}
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

		public static List<TransactionView> GetTransactions()
		{
			var trs = new List<TransactionView>
			{
				new TransactionView
				{
					ColumnWithHierarchy = "1 липня 2016",
					Children = new List<TransactionView>
					{
						new TransactionView
						{
							ColumnWithHierarchy = "Сільпо",
							Acount = "Готівка",
							Amount = "-100.00",
							Balance = "3568.95",
							Children = new List<TransactionView>
							{
								new TransactionView {ColumnWithHierarchy = "• молоко", Amount = "      10.50"},
								new TransactionView {ColumnWithHierarchy = "• хліб", Amount = "      15.20"},
								new TransactionView {ColumnWithHierarchy = "• черешні", Amount = "      50.50", Comment = "60 грн/кг"}
							}
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Алейка",
							Acount = "Готівка",
							Amount = "-52.30",
							Balance = "3668.95",
							Children = new List<TransactionView>
							{
								new TransactionView {ColumnWithHierarchy = "• помідори", Amount = "      30.49", Comment = "25 грн/кг"},
								new TransactionView {ColumnWithHierarchy = "• яблука", Amount = "      25.25", Comment = "15 грн/кг"}
							}
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Обмін",
							Acount = "Гаманець UAH",
							Amount = "-2500.00",
							Comment = "25.00 UAH/$",
							Balance = "2150.00"
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Обмін",
							Acount = "Гаманець $",
							Amount = "100.00",
							Balance = "100.00",
							Currency = "$"
						}
					}
				},

				new TransactionView
				{
					ColumnWithHierarchy = "2 липня 2016",
					Children = new List<TransactionView>
					{
						new TransactionView
						{
							ColumnWithHierarchy = "Twinfield",
							Acount = "2600 Агріколь",
							Amount = "50000.00",
							Balance = "75 000.00",
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Ліко-Світ",
							Acount = "Приват",
							Amount = "-7500.00",
							Balance = "14 569.00",
							Comment = "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Метро",
							Acount = "Картка",
							Amount = "-52.30",
							Balance = "5 890.85",
							Children = new List<TransactionView>
							{
								new TransactionView {ColumnWithHierarchy = "• ікра", Amount = "      100.50"},
								new TransactionView {ColumnWithHierarchy = "• торт", Amount = "      150.75"},
								new TransactionView {ColumnWithHierarchy = "• серветки", Amount = "      30.00"}
							}
						},
						new TransactionView
						{
							ColumnWithHierarchy = "Алейка",
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
