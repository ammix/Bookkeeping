using System;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Security.Cryptography;
using RestSharp;
using System.Collections.Generic;
using System.Web;

namespace Privat24Module
{
	public static class Privat24
	{
		static string BytesToString(byte[] bytes)
		{
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				sBuilder.Append(bytes[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}

		static string GetSignature(string data, string password)
		{
			// PHP: $sign = sha1(md5($data.$password));

			using (var md5 = MD5.Create())
			using (var sha1 = SHA1.Create())
			{
				var md5Hash = BytesToString(md5.ComputeHash(Encoding.UTF8.GetBytes(data + password)));
				var sha1Hash = BytesToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(md5Hash)));

				return sha1Hash;
				// Return from using body? Is that Ok?
			}
		}

		static string GetRequestBodyForAccountStatements(int merchantId, string password, DateTime startDate, DateTime endDate, string cardNumber)
		{
			XDocument xml = XDocument.Parse(Resources.rest_fiz);
			var format = SaveOptions.DisableFormatting;

			xml.Descendants("prop").First(x => x.Attribute("name").Value == "sd").SetAttributeValue("value", startDate.ToShortDateString());
			xml.Descendants("prop").First(x => x.Attribute("name").Value == "ed").SetAttributeValue("value", endDate.ToShortDateString());
			xml.Descendants("prop").First(x => x.Attribute("name").Value == "card").SetAttributeValue("value", cardNumber);

			string data = xml.Descendants("data").Elements().Select(x => x.ToString(format)).Aggregate(string.Concat);
			string signature = GetSignature(data, password);

			xml.Descendants("merchant").First().SetElementValue("id", merchantId);
			xml.Descendants("merchant").First().SetElementValue("signature", signature);

			return xml.Declaration.ToString() + xml.ToString(format);
		}

		public static IEnumerable<FinTransaction> GetTransactions()
		{
			var body = Privat24.GetRequestBodyForAccountStatements(12345, "...", DateTime.Parse("1.10.2016"), DateTime.Parse("30.10.2016"), "...");

			var client = new RestClient("https://api.privatbank.ua/");
			var request = new RestRequest("p24api/rest_fiz", Method.POST);
			request.AddParameter("text/xml", body, ParameterType.RequestBody);

			IRestResponse response = client.Execute(request);
			//var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

			XDocument xml = XDocument.Parse(response.Content);
			foreach(XElement element in xml.Descendants("statements").Elements())
			{
				var date = element.Attribute("trandate").Value;
				var account = element.Attribute("card").Value;
				var amount = element.Attribute("amount").Value;
				var note = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(element.Attribute("terminal").Value));
				var balance = element.Attribute("rest").Value;
				var desc = element.Attribute("description").Value;

				Console.WriteLine($"{date}\t{amount}\t{balance}\t{note}\t{desc}");
			}

			return null;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Privat24.GetTransactions();

			//Console.WriteLine(HttpUtility.HtmlDecode("a&amp;b"));
			//Console.WriteLine(HttpUtility.HtmlDecode("a&#38;b"));
			//Console.WriteLine(HttpUtility.HtmlDecode("a&quot;b"));

			Console.ReadLine();
		}
	}
}