using System;
using RestSharp;
using System.IO;

namespace BookkeepingServerChecker
{
    static class Program
    {
        static void Main()
        {
            var client = new RestClient("http://money.somee.com/");
            var request = new RestRequest("api/transactions/6");

            IRestResponse response = client.Get(request);

            string path = @"D:\ASP.NET\Logs\" + DateTime.Now.ToString().Replace(':', '_') + ".log";
            File.WriteAllText(path, response.Content);
        }
    }
}
