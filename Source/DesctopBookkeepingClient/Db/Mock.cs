using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class MockDb
	{
		public static List<TransactionView> GetTransactions()
		{
			var trs = new List<TransactionView>
			{
				new TransactionView
				{
					Counterparty = "1 липня 2016",
					Nodes = new List<TransactionView>
					{
						new TransactionView
						{
							Counterparty = "Сільпо",
							Acount = "Готівка",
							Amount = "-100.00",
							Balance = "3568.95",
							Nodes = new List<TransactionView>
							{
								new TransactionView {Counterparty = "• молоко", Amount = "      10.50"},
								new TransactionView {Counterparty = "• хліб", Amount = "      15.20"},
								new TransactionView {Counterparty = "• черешні", Amount = "      50.50", Comment = "60 грн/кг"}
							}
						},
						new TransactionView
						{
							Counterparty = "Алейка",
							Acount = "Готівка",
							Amount = "-52.30",
							Balance = "3668.95",
							Nodes = new List<TransactionView>
							{
								new TransactionView {Counterparty = "• помідори", Amount = "      30.49", Comment = "25 грн/кг"},
								new TransactionView {Counterparty = "• яблука", Amount = "      25.25", Comment = "15 грн/кг"}
							}
						},
						new TransactionView
						{
							Counterparty = "Обмін",
							Acount = "Гаманець UAH",
							Amount = "-2500.00",
							Comment = "25.00 UAH/$",
							Balance = "2150.00"
						},
						new TransactionView
						{
							Counterparty = "Обмін",
							Acount = "Гаманець $",
							Amount = "100.00",
							Balance = "100.00",
							Currency = "$"
						}
					}
				},

				new TransactionView
				{
					Counterparty = "2 липня 2016",
					Nodes = new List<TransactionView>
					{
						new TransactionView
						{
							Counterparty = "Twinfield",
							Acount = "2600 Агріколь",
							Amount = "50000.00",
							Balance = "75 000.00",
						},
						new TransactionView
						{
							Counterparty = "Ліко-Світ",
							Acount = "Приват",
							Amount = "-7500.00",
							Balance = "14 569.00",
							Comment = "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						},
						new TransactionView
						{
							Counterparty = "Метро",
							Acount = "Картка",
							Amount = "-52.30",
							Balance = "5 890.85",
							Nodes = new List<TransactionView>
							{
								new TransactionView {Counterparty = "• ікра", Amount = "      100.50"},
								new TransactionView {Counterparty = "• торт", Amount = "      150.75"},
								new TransactionView {Counterparty = "• серветки", Amount = "      30.00"}
							}
						},
						new TransactionView
						{
							Counterparty = "Алейка",
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
