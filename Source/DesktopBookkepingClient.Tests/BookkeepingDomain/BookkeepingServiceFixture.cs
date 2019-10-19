using DesktopBookkeepingClient.DomainModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopBookkeepingClient.Tests.BookkeepingDomain
{
	public class BookkeepingTransaction2 : BookkeepingTransaction
	{
		public override bool Equals(object obj)
		{
			var other = (BookkeepingTransaction)obj;

			return Id.Equals(other.Id) &&
			       Date.Equals(other.Date) &&
			       Order.Equals(other.Order) &&
			       Amount.Equals(other.Amount) &&
			       Balance.Equals(other.Balance);
		}
	}

	[TestFixture]
	public class BookkeepingServiceFixture
	{
		[Test]
		public void Should_move_transaction_into_middle_of_next_day() //Should_move_transaction_up_into_new_date()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new []
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1},
				new BookkeepingTransaction2 {Id = 4, Date = date2, Amount = 800, Balance = 1501, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(1).Returns(new BookkeepingTransaction { Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(1, date2, 2);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 201, Order = 1},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 601, Order = 1},
				new BookkeepingTransaction2 {Id = 1, Date = date2, Amount = 100, Balance = 701, Order = 2},
				new BookkeepingTransaction2 {Id = 4, Date = date2, Amount = 800, Balance = 1501, Order = 3}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_transaction_into_beginning_of_next_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date2, Amount = 200, Balance = 301, Order = 1},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(1).Returns(new BookkeepingTransaction { Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(1, date2, 1);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date2, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date2, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 3}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_transaction_into_ending_of_next_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date2, Amount = 200, Balance = 301, Order = 1},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(1).Returns(new BookkeepingTransaction { Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(1, date2, 3);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 2, Date = date2, Amount = 200, Balance = 201, Order = 1},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 601, Order = 2},
				new BookkeepingTransaction2 {Id = 1, Date = date2, Amount = 100, Balance = 701, Order = 3}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_alone_transaction_into_other_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(1).Returns(new BookkeepingTransaction { Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(1, date2, 1);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date2, Amount = 100, Balance = 101, Order = 1}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_transaction_into_middle_of_previous_day() //Should_move_transaction_down_into_new_date()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1},
				new BookkeepingTransaction2 {Id = 4, Date = date2, Amount = 800, Balance = 1501, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(4).Returns(new BookkeepingTransaction { Id = 4, Date = date2, Amount = 800, Balance = 1501, Order = 2 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(4, date1, 2);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 4, Date = date1, Amount = 800, Balance = 901, Order = 2},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 1101, Order = 3},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 1501, Order = 1}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_transaction_into_beginning_of_previous_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(3).Returns(new BookkeepingTransaction { Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(3, date1, 1);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 3, Date = date1, Amount = 400, Balance = 401, Order = 1},
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 501, Order = 2},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 701, Order = 3},
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_move_transaction_into_ending_of_previous_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var date2 = DateTime.Parse("2.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(3).Returns(new BookkeepingTransaction { Id = 3, Date = date2, Amount = 400, Balance = 701, Order = 1 });
			repository.Get(date1, date2).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(3, date1, 3);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2},
				new BookkeepingTransaction2 {Id = 3, Date = date1, Amount = 400, Balance = 701, Order = 3}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_change_transaction_order_up_in_one_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(1).Returns(new BookkeepingTransaction { Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1 });
			repository.Get(date1, date1).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(1, date1, 2);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 201, Order = 1},
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 301, Order = 2}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}

		[Test]
		public void Should_change_transaction_order_down_in_one_day()
		{
			var date1 = DateTime.Parse("1.10.2019");
			var testTransactions = new[]
			{
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 101, Order = 1},
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2}
			};

			var repository = Substitute.For<ITransactionRepository>();
			repository.Get(2).Returns(new BookkeepingTransaction { Id = 2, Date = date1, Amount = 200, Balance = 301, Order = 2 });
			repository.Get(date1, date1).Returns(new List<BookkeepingTransaction>(testTransactions));

			var service = new BookkeepingService(repository);

			service.MoveTransaction(2, date1, 1);

			var expected = new[]
			{
				new BookkeepingTransaction2 {Id = 2, Date = date1, Amount = 200, Balance = 201, Order = 1},
				new BookkeepingTransaction2 {Id = 1, Date = date1, Amount = 100, Balance = 301, Order = 2}
			};
			repository.Received().Update(Arg.Is<List<BookkeepingTransaction>>(x => expected.SequenceEqual(x)));
		}
	}
}
