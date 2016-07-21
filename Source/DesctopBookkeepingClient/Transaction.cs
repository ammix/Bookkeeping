using System;
using System.Collections.Generic;

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


	class Transaction
	{
		public string Date;
		public string Acount;
		public string Amount;
		public string Comment;
		public string Balance;
		public string Currency;
        public List<Transaction> InvoiceLines;
		public bool HasInvoiceLine {get{ return InvoiceLines != null && InvoiceLines.Count != 0;}}

		public static List<Transaction> GetTransactions()
		{
			var trs = new List<Transaction>
			{
				new Transaction
				{
					Date = "1 липня 2016",
					InvoiceLines = new List<Transaction>
					{
						new Transaction
						{
							Date = "Сільпо",
							Acount = "Готівка",
							Amount = "-100.00",
							Balance = "3568.95",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {Date = "• молоко", Amount = "      10.50"},
								new Transaction {Date = "• хліб", Amount = "      15.20"},
								new Transaction {Date = "• черешні", Amount = "      50.50", Comment = "60 грн/кг"}
							}
						},
						new Transaction
						{
							Date = "Алейка",
							Acount = "Готівка",
							Amount = "-52.30",
							Balance = "3668.95",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {Date = "• помідори", Amount = "      30.49", Comment = "25 грн/кг"},
								new Transaction {Date = "• яблука", Amount = "      25.25", Comment = "15 грн/кг"}
							}
						},
						new Transaction
						{
							Date = "Обмін",
							Acount = "Гаманець UAH",
							Amount = "-2500.00",
							Comment = "25.00 UAH/$",
							Balance = "2150.00"
						},
						new Transaction
						{
							Date = "Обмін",
							Acount = "Гаманець $",
							Amount = "100.00",
							Balance = "100.00",
							Currency = "$"
						}
					}
				},

				new Transaction
				{
					Date = "2 липня 2016",
					InvoiceLines = new List<Transaction>
					{
						new Transaction
						{
							Date = "Twinfield",
							Acount = "2600 Агріколь",
							Amount = "50000.00",
							Balance = "75 000.00",
						},
						new Transaction
						{
							Date = "Ліко-Світ",
							Acount = "Приват",
							Amount = "-7500.00",
							Balance = "14 569.00",
							Comment = "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						},
						new Transaction
						{
							Date = "Метро",
							Acount = "Картка",
							Amount = "-52.30",
							Balance = "5 890.85",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {Date = "• ікра", Amount = "      100.50"},
								new Transaction {Date = "• торт", Amount = "      150.75"},
								new Transaction {Date = "• серветки", Amount = "      30.00"}
							}
						},
						new Transaction
						{
							Date = "Алейка",
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
