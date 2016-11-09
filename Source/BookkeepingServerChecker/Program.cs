using RestSharp;

namespace BookkeepingServerChecker
{
    static class Program
    {
        static void Main()
        {
            var client = new RestClient("http://money.somee.com");
            var request = new RestRequest("default.asp");

            IRestResponse response = client.Get(request);
            var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

            var transactions = new List<TransactionView>();
            foreach (var finDay in view)
            {
                transactions.Add(new TransactionView { Counterparty = finDay.Date, Nodes = ToView(finDay.FinTransactions) });
            }
            return transactions;

        }
    }
}
