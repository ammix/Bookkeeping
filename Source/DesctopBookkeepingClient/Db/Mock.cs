using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class MockDb
	{
		public static List<TreeListViewModel> GetTransactions()
		{
			var trs = new List<TreeListViewModel>
			{
				new TreeListViewModel
				(
					//id: 2,
					date: "2 липня 2016",
					transactions: new List<TreeListViewModel>
					{
						new TreeListViewModel
						(
							id: 1,
							counterparty: "Сільпо",
							amount: "- 100.00",
							account: "Готівка",
							balance: "3 568.95",
							time: "12:00",
							articles: new List<TreeListViewModel>
							{
								new TreeListViewModel (article: "молоко", price: "10.50"),
								new TreeListViewModel (article: "хліб", price: "15.20"),
								new TreeListViewModel (article: "черешні", price: "50.50", note: "60 грн/кг")
							}
						),
						new TreeListViewModel
						(
							id: 2,
							counterparty: "Алейка",
							amount: "- 52.30",
							account: "Готівка",
							balance: "3 668.95",
							time: "13:01",
							articles: new List<TreeListViewModel>
							{
								new TreeListViewModel (article: "помідори", price: "30.49", note: "25 грн/кг"),
								new TreeListViewModel (article: "яблука", price: "25.25", note: "15 грн/кг")
							}

						),
						new TreeListViewModel
						(
							id: 3,
							articles: null,
							counterparty: "Обмін",
							amount: "-2 500.00",
							account: "Гаманець UAH",
							comment: "25.00 UAH/$",
							balance: "2 150.00",
							time: "14:30"
						),
						new TreeListViewModel
						(
							id: 4,
							articles: null,
							counterparty: "Обмін",
							amount: "100.00",
							account: "Гаманець $",
							balance: "$ 100.00"
						)

					}
				),

				new TreeListViewModel
				(
					//id: 1,
					date: "1 липня 2016",
					transactions: new List<TreeListViewModel>
					{
						new TreeListViewModel
						(
							id: 6,
							articles: null,
							counterparty: "Twinfield",
							amount: "50 000.00",
							account: "2600 Агріколь",
							balance: "75 000.00"
						),
						new TreeListViewModel
						(
							id: 7,
							articles: null,
							counterparty: "Ліко-Світ",
							amount: "-7 500.00",
							account: "Приват",
							balance: "14 569.00",
							comment: "Аванс за послуги Інтернет, червень 2016, згідно договору №2525"
						),
						new TreeListViewModel
						(
							id: 8,
							counterparty: "Метро",
							amount: "-52.30",
							account: "Картка",
							balance: "5 890.85",
							articles: new List<TreeListViewModel>
							{
								new TreeListViewModel (article: "ікра", price: "100.50"),
								new TreeListViewModel (article: "торт", price: "150.75"),
								new TreeListViewModel (article: "серветки", price: "30.00")
							}
						),
						new TreeListViewModel
						(
							id: 9,
							articles: null,
							counterparty: "Алейка",
							amount: "-152.55",
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
