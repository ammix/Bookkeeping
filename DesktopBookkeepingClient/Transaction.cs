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
		public string Account;
		public string CustomerOrSupplier;
		public string Amount;
		public string Comment;
		public string Balance;
		public List<Transaction> InvoiceLines;
		public bool HasInvoiceLine {get{ return InvoiceLines != null && InvoiceLines.Count != 0;}}

		public static List<Transaction> GetTransactions()
		{
			var trs = new List<Transaction>
			{
				new Transaction
				{
					Account = "1 липня 2016",
					InvoiceLines = new List<Transaction>
					{
						new Transaction
						{
							Account = "Готівка",
							CustomerOrSupplier = "Сільпо",
							Amount = "-100.00",
							Balance = "3568.95",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {CustomerOrSupplier = "   • молоко", Amount = "      10.50"},
								new Transaction {CustomerOrSupplier = "   • хліб", Amount = "      15.20"},
								new Transaction {CustomerOrSupplier = "   • черешні", Amount = "      50.50", Comment = "60 грн/кг"}
							}
						},
						new Transaction
						{
							Account = "Готівка",
							CustomerOrSupplier = "Алейка",
							Amount = "-52.30",
							Balance = "3668.95",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {CustomerOrSupplier = "   • помідори", Amount = "      30.49", Comment = "25 грн/кг"},
								new Transaction {CustomerOrSupplier = "   • яблука", Amount = "      25.25", Comment = "15 грн/кг"}
							}
						},
						new Transaction
						{
							Account = "Гаманець UAH",
							CustomerOrSupplier = "Обмін",
							Amount = "-2500.00",
							Comment = "25.00 UAH/$",
							Balance = "2150.00"
						},
						new Transaction
						{
							Account = "Гаманець $",
							CustomerOrSupplier = "Обмін",
							Amount = "100.00",
							Balance = "100.00"
						}
					}
				},

				new Transaction
				{
					Account = "2 липня 2016",
					InvoiceLines = new List<Transaction>
					{
						new Transaction
						{
							Account = "2600 Агріколь",
							CustomerOrSupplier = "Twinfield",
							Amount = "50 000.00 UAH",
							Balance = "75 000.00 UAH",
						},
						new Transaction
						{
							Account = "Приват",
							CustomerOrSupplier = "Ліко-Світ",
							Amount = "-7 500.00",
							Balance = "14 569.00 UAH",
							Comment = "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						},
						new Transaction
						{
							Account = "Картка",
							CustomerOrSupplier = "Метро",
							Amount = "-52.30",
							Balance = "5 890.85 UAH",
							InvoiceLines = new List<Transaction>
							{
								new Transaction {CustomerOrSupplier = "   • ікра", Amount = "      100.50"},
								new Transaction {CustomerOrSupplier = "   • торт", Amount = "      150.75"},
								new Transaction {CustomerOrSupplier = "   • серветки", Amount = "      30.00"}
							}
						},
						new Transaction
						{
							Account = "Готівка",
							CustomerOrSupplier = "Алейка",
							Amount = "-152.55",
							Balance = "3 700.00 UAH"
						}
					}
				}
			};

			return trs;
		}
	}
}
