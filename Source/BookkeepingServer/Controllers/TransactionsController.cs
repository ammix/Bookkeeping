using System.Collections.Generic;
using System.Web.Http;
using BookkeepingServer.Models;
using BookkeepingServer.Views;

namespace BookkeepingServer.Controllers
{
	public class TransactionsController : ApiController
	{
		readonly TransactionRepository repository = new TransactionRepository();

		// api/transactions/{month}
		public IEnumerable<FinDay> GetTransactions(int month)
		{
			return repository.GetTransactions(month);
		}
    }
}
