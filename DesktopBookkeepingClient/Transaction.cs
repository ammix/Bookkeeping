using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookkeepingClient
{
	class Transaction
	{
		public DateTime Date;
		public string Account;
		public bool Increment;
		public string Supplier;
		public double Amount;
		public string Comment;

		public static List<Transaction> GetTransactions()
		{
			var trs = new List<Transaction>
			{
				new Transaction {Date = DateTime.Parse("14.06.2016"), Account = "Готівка", Increment = false, Supplier = "Сільпо", Amount = 222.22},
				new Transaction {Date = DateTime.Parse("14.06.2016"), Account = "Готівка", Increment = false, Supplier = "Кишеня", Amount = 50.75},
				new Transaction {Date = DateTime.Parse("15.06.2016"), Account = "Готівка", Increment = true, Supplier = "Алейка", Amount = 123.56},
				new Transaction {Date = DateTime.Parse("15.06.2016"), Account = "Готівка", Increment = false, Supplier = "КиївПасТранс", Amount = 20.0},
				new Transaction {Date = DateTime.Parse("15.06.2016"), Account = "Готівка", Increment = true, Supplier = "Ніколін", Amount = 150.0}
			};

			return trs;
		}
	}
}
