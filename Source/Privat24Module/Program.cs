using System;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Security.Cryptography;
using RestSharp;

namespace Privat24Module
{
	public static class Privat24
	{
        static string GetSignature(string data)
        {
            using (var md5 = MD5.Create())
            using (var sha1 = SHA1.Create())
            {
                // $sign = sha1(md5($data.$password));
                var md5Hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder sBuilder0 = new StringBuilder();
                for (int i = 0; i < md5Hash.Length; i++)
                {
                    sBuilder0.Append(md5Hash[i].ToString("x2"));
                }
                var q= sBuilder0.ToString();
                var w = Encoding.UTF8.GetBytes(q);

                var sha1Hash = sha1.ComputeHash(w);

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < sha1Hash.Length; i++)
                {
                    sBuilder.Append(sha1Hash[i].ToString("x2"));
                }
                return sBuilder.ToString(); // Return from using body? Is that Ok?
            }
        }

		public static string GetRequest(int merchantId, DateTime startDate, DateTime endDate, string cardNumber)
        {
            XDocument xml = XDocument.Parse(Resources.rest_fiz);

            var props = xml.Descendants("prop");
            props.First(x => x.Attribute("name").Value == "sd").SetAttributeValue("value", startDate.ToShortDateString());
            //props.First(x => x.Attribute("name").Value == "sd").Attribute("value").SetValue(startDate.ToShortDateString());
            props.First(x => x.Attribute("name").Value == "ed").SetAttributeValue("value", endDate.ToShortDateString());
            props.First(x => x.Attribute("name").Value == "card").SetAttributeValue("value", cardNumber);

            string data = xml.Descendants("data").Elements().Select(x => x.ToString(SaveOptions.DisableFormatting)).Aggregate(string.Concat);
            string password = "...";
            Console.WriteLine(data);

            string signature = GetSignature(data + password);

            xml.Descendants("merchant").First().SetElementValue("id", merchantId);
            //xml.Descendants("id").First().SetValue(merchantId);
            xml.Descendants("merchant").First().SetElementValue("signature", signature);

            //return xml.Declaration.ToString() +'\n'+ xml.ToString(SaveOptions.None);
            return xml.Declaration.ToString() + xml.ToString(SaveOptions.DisableFormatting);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var body = Privat24.GetRequest(12345, DateTime.Parse("1.10.2016"), DateTime.Parse("30.10.2016"), "...");
            Console.WriteLine(body);
            //Console.ReadLine();
            //return;

            var client = new RestClient("https://api.privatbank.ua/");
            var request = new RestRequest("p24api/rest_fiz", Method.POST);
            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

            Console.WriteLine(response.Content);
            Console.ReadLine();
        }
    }
}