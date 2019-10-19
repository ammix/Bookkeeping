using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopBookkeepingClient.DomainModels
{
	public class BookkeepingTransaction
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public int Order { get; set; }
		public double Amount { get; set; }
		public double Balance { get; set; }
	}

	public interface ITransactionRepository
	{
		IEnumerable<BookkeepingTransaction> Get(DateTime date1, DateTime date2);
		BookkeepingTransaction Get(int transactionId);

		void Add(BookkeepingTransaction transaction);
		void Update(BookkeepingTransaction transaction);
		void Update(IEnumerable<BookkeepingTransaction> transaction);
	}

	public interface IAccountRepository
	{

	}

	public interface IConterpartyRepository
	{

	}

	public class BookkeepingTransactionRepository
	{ }

	public interface IBookkeepingTransactionService
	{
		IList<BookkeepingTransaction> GetTransactions();

		void AddNewTransaction(DateTime date, decimal amount, string account, string counterparty, string comment);
		void ModifyTransactionDetails(BookkeepingTransaction transaction);
		void RemoveTransaction(BookkeepingTransaction transaction);

		void MoveTransaction(int id, DateTime destinationDate, int destinationOrder);
		//void ModifyTransactionDateAndOrder(BookeepingTransaction transaction, DateTime destinationDate, int destinationOrder);
	}

	public class BookkeepingService : IBookkeepingTransactionService
	{
		private ITransactionRepository transactionRepository;
		private IAccountRepository accountRepository;
		private IConterpartyRepository counterpartyRepository;

		public BookkeepingService(ITransactionRepository repository)
		{
			transactionRepository = repository;
		}

		public void AddNewTransaction(DateTime date, decimal amount, string account, string counterparty, string comment)
		{
			//accountRepository.Get
			var transaction = new BookkeepingTransaction(/*...*/);

			transactionRepository.Add(transaction);
		}

		public void ModifyTransactionDetails(BookkeepingTransaction transaction)
		{
			throw new System.NotImplementedException();
		}

		public static IEnumerable<T> Combine<T>(params ICollection<T>[] toCombine)
		{
			return toCombine.SelectMany(x => x);
		}

		public void MoveTransaction(int id, DateTime destDate, int destOrder)
		{
			var srcTransaction = transactionRepository.Get(id);
			var date1 = destDate > srcTransaction.Date ? srcTransaction.Date : destDate;
			var date2 = destDate > srcTransaction.Date ? destDate : srcTransaction.Date;

			var trs = transactionRepository.Get(date1, date2).ToList();
			var balance = trs[0].Balance - trs[0].Amount;

			var movedTransaction = trs.First(x => x.Id == id);
			trs.Remove(movedTransaction);
			var shiftedTransaction = trs.FirstOrDefault(x => x.Date == destDate && x.Order == destOrder);

			var index = shiftedTransaction != null ? trs.IndexOf(shiftedTransaction) : trs.Count;
			if ((date1 == date2) && (destOrder > movedTransaction.Order))
				index++;

			//if (date1 == date2)
			//	trs.Remove(movingTransaction);
			movedTransaction.Date = destDate;
			trs.Insert(index, movedTransaction);
			//if (date1 != date2)
			//	trs.Remove(movingTransaction);

			var i = 1;
			var prevDate = trs[0].Date;
			foreach (var transaction in trs)
			{
				if (transaction.Date != prevDate) i = 1;
				prevDate = transaction.Date;

				transaction.Order = i++;
				balance += transaction.Amount;
				transaction.Balance = balance;
			}

			transactionRepository.Update(trs);
		}

		//Remove this after commit (save for history)
		//public void ModifyTransactionDateAndOrder(BookeepingTransaction sourceTransaction, DateTime destinationDate, int destinationOrder)
		//{
		//	//var sourceTransaction = transactionRepository.Get(transactionId);

		//	var date1 = DateTime.FromBinary(Math.Min(sourceTransaction.Date.Ticks, destinationDate.Ticks));
		//	var date2 = DateTime.FromBinary(Math.Max(sourceTransaction.Date.Ticks, destinationDate.Ticks));

		//	var trs = transactionRepository.Get(date1, date2);

		//	var trs1 = trs.Where(x => x.Date == date1).ToList();
		//	var trsm = trs.Where(x => x.Date != date1 && x.Date != date2).ToList();
		//	var trs2 = trs.Where(x => x.Date == date2).ToList();

		//	var trsSourceDate = destinationDate >= sourceTransaction.Date ? trs1 : trs2;
		//	var trsDestDate = destinationDate > sourceTransaction.Date ? trs2 : trs1;

		//	var sourceTrs = trsSourceDate.First(x => x.Id == sourceTransaction.Id);

		//	trsSourceDate.Remove(sourceTrs);
		//	trsDestDate.Insert(destinationOrder - 1, sourceTrs);

		//	var result = Combine(trs1, trsm, trs2);
		//	//Recalculate: balance, index

		//	transactionRepository.Update(result);
		//}

		public IList<BookkeepingTransaction> GetTransactions()
		{
			throw new System.NotImplementedException();
		}

		public void RemoveTransaction(BookkeepingTransaction transaction)
		{
			throw new System.NotImplementedException();
		}

		//void RebuildBalance()
	}
}
/*
			//var sourceIndex = trs.IndexOf(trs.First(x => x.Id == sourceTransaction.Id));
			//trs.RemoveAt(sourceIndex);

				//var trsInOneDate = trs.Where(x => x.Date == destinationDate).ToList();


 			--WHERE [Date] >= @StartDate AND [Date] <= @EndDate
			--WHERE ([Date] = @StartDate AND [Date] = @EndDate AND [Order] >= @StartOrder AND [Order] <= @EndOrder) OR
			--		([Date] > @StartDate AND [Date] < @EndDate)

--DECLARE @StartOrder INT = 1
--DECLARE @EndOrder INT = 2
--DECLARE @StartDate DATE = '2017-01-01'
--DECLARE @EndDate DATE = '2017-01-02'

--SELECT * FROM [Bookkeeping].[dbo].[Transactions]
--	WHERE [Date] = @StartDate
--	  ORDER BY [Order] DESC
----------------------------------------------

DECLARE @StartIndex INT = 1
DECLARE @EndIndex INT = 5

UPDATE[Bookkeeping].[dbo].[Transactions]
	SET[Balance] = 0
		WHERE[IsTrsBalance] = 0
UPDATE[Bookkeeping].[dbo].[Transactions]
	SET[Amount] = 0
		WHERE[IsTrsBalance] = 1

DECLARE @Id INT
DECLARE @AccountId INT
DECLARE @Amount MONEY
DECLARE @RealBalance MONEY
DECLARE @IsTrsBalance BIT
DECLARE @Order INT

DECLARE @i = 0
DECLARE @Balance MONEY = 0
DECLARE @Balances TABLE
(
	AccountId INT NOT NULL,
	Balance MONEY,
)
DECLARE row CURSOR FOR
	SELECT [Id], [AccountId], [Amount], [IsTrsBalance], [Balance] AS RealBalance, [Order]
		FROM[Bookkeeping].[dbo].[Transactions]
			WHERE [Order] >= @StartIndex AND [Order] <= @EndIndex
				ORDER BY [Order]

OPEN row
	FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance, @Order
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Balance = (SELECT Balance FROM @Balances WHERE AccountId = @AccountId)
		IF @Balance IS NULL
			BEGIN
				INSERT INTO @Balances (AccountId, Balance)
					VALUES(@AccountId, @RealBalance)
				CONTINUE
			END

		SET @i = @i + 1
		SET @Balance = @Balance + @Amount

		-- Error processing
		IF(@IsTrsBalance = 1 AND @Balance <> @RealBalance)
		BREAK

		UPDATE @Balances SET Balance = @Balance
			WHERE AccountId = @AccountId

		UPDATE [Bookkeeping].[dbo].[Transactions] SET [Order] = @i
			WHERE Id = @Id
		IF(@IsTrsBalance = 0)
			UPDATE [Bookkeeping].[dbo].[Transactions] SET [Balance] = @Balance
				WHERE Id = @Id

		FETCH NEXT FROM row INTO @Id, @AccountId, @Amount, @IsTrsBalance, @RealBalance, @Order
	END
CLOSE row
DEALLOCATE row
 */
