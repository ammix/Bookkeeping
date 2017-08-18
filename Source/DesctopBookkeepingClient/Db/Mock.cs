using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class MockDb
	{
		public static List<ITreeListViewModel> GetTransactions()
		{
			var trs = new List<ITreeListViewModel>
			{
				new FinDayModel
				(
					//id: 2,
					date: DateTime.Parse("2 липня 2016"),
					transactions: new List<ITreeListViewModel>
					{
						new TransactionModel
						(
							id: 1,
							counterparty: "Сільпо",
							amount: -100.00m, // "- 100.00"
							account: "Готівка",
							balance: "3 568.95",
							time: "12:00",
							articles: new List<ITreeListViewModel>
							{
								new InvoiceLineModel (article: "молоко", price: 10.50m),
								new InvoiceLineModel (article: "хліб", price: 15.20m),
								new InvoiceLineModel (article: "черешні", price: 50.50m, note: "60 грн/кг")
							}
						),
						new TransactionModel
						(
							id: 2,
							counterparty: "Алейка",
							amount: -52.30m, // "- 52.30",
							account: "Готівка",
							balance: "3 668.95",
							time: "13:01",
							articles: new List<ITreeListViewModel>
							{
								new InvoiceLineModel (article: "помідори", price: 30.49m, note: "25 грн/кг"),
								new InvoiceLineModel (article: "яблука", price: 25.25m, note: "15 грн/кг")
							}

						),
						new TransactionModel
						(
							id: 3,
							articles: null,
							counterparty: "Обмін",
							amount: -2500.0m, // "- 2 500.00",
							account: "Гаманець UAH",
							comment: "25.00 UAH/$",
							balance: "2 150.00",
							time: "14:30"
						),
						new TransactionModel
						(
							id: 4,
							articles: null,
							counterparty: "Обмін",
							amount: 100.0m,
							account: "Гаманець $",
							balance: "$ 100.00"
						)

					}
				),

				new FinDayModel
				(
					//id: 1,
					date: DateTime.Parse("1 липня 2016"),
					transactions: new List<ITreeListViewModel>
					{
						new TransactionModel
						(
							id: 6,
							articles: null,
							counterparty: "Twinfield",
							amount: 50000.0m,
							account: "2600 Агріколь",
							balance: "75 000.00"
						),
						new TransactionModel
						(
							id: 7,
							articles: null,
							counterparty: "Ліко-Світ",
							amount: -7500.0m,
							account: "Приват",
							balance: "14 569.00",
							comment: "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						),
						new TransactionModel
						(
							id: 8,
							counterparty: "Метро",
							amount: -52.3m,
							account: "Картка",
							balance: "5 890.85",
							articles: new List<ITreeListViewModel>
							{
								new InvoiceLineModel (article: "ікра", price: 100.50m),
								new InvoiceLineModel (article: "торт", price: 150.75m),
								new InvoiceLineModel (article: "серветки", price: 30.00m)
							}
						),
						new TransactionModel
						(
							id: 9,
							articles: null,
							counterparty: "Алейка",
							amount: -152.55m,
							account: "Готівка",
							balance: "3 700.00"
						)
					}
				)
			};

			return trs;
		}
	}
}
