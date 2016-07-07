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
		public bool IsInvoiceLine {get{ return InvoiceLines != null && InvoiceLines.Count != 0;}}

		public static List<Transaction> GetTransactions()
		{
			var trs = new List<Transaction>
			{
				new Transaction
				{
					Date = "1 липня 2016", Account = "Готівка", CustomerOrSupplier = "Сільпо", Amount = "-100.00", Balance = "3568.95",
					InvoiceLines = new List<Transaction> {
						new Transaction {CustomerOrSupplier = "   • молоко", Amount = "      10.50"},
						new Transaction {CustomerOrSupplier = "   • хліб", Amount = "      15.20"},
						new Transaction {CustomerOrSupplier = "   • черешні", Amount = "      50.50"} }
				},
				new Transaction {Date = "1 липня 2016", Account = "Готівка", CustomerOrSupplier = "Алейка", Amount = "-52.30", Balance = "3668.95"},
				new Transaction {Date = "1 липня 2016", Account = "Гаманець UAH", CustomerOrSupplier = "Обмін", Amount = "-2500.00", Comment = "25.00 UAH/$", Balance = "2150.00"},
				new Transaction {Date = "1 липня 2016", Account = "Гаманець $", CustomerOrSupplier = "Обмін", Amount = "100.00", Balance = "100.00"},

				//new Transaction {Date = DateTime.Parse("15.06.2016"), Account = "Готівка", Increment = true, Supplier = "Ніколін", Amount = 150.0}
			};

			return trs;
		}
	}
}
