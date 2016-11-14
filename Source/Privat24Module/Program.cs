using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Privat24Module
{
    public class Property
    {
        [XmlAttribute("name")] public string Name;
        [XmlAttribute("value")] public string Value;
    }

    public class payment
    {
	    [XmlAttribute("id")] public string Id = "";
        [XmlElement("prop")] public List<Property> Properties;
    }

    public class data
    {
        public string oper;
        public string wait;
        public string test;
        public payment payment;
    }

    public class merchant
    {
        public string id;
        public string signature; // $sign=sha1 (md5($data.$password));
    }

    [XmlRoot("request")]
    public class Request
    {
        [XmlAttribute] public string version = "1.0";
        [XmlElement] public merchant merchant;
        [XmlElement] public data data;

	    public Request() { }

	    public Request(int merchantId, DateTime startDate, DateTime endDate, string cardNumber)
        {
			
        }
    }

	public class Privat24
	{
		
	}


    class Program
    {
        static void Main(string[] args)
        {
            var request = new Request
            {
                version = "1.0",
                merchant = new merchant
                {
                    id = "75482",
                    signature = "5abf5c7524bc2a835acb3a9e24ce10bc5ba82a99"
                },
                data = new data
                {
                    oper = "cmt",
                    wait = "0",
                    test = "0",
                    payment = new payment
                    {
                        Id = "",
                        Properties = new List<Property>()
                        {
                            new Property {Name = "sd", Value = "10.11.2016" },
                            new Property {Name = "ed", Value = "11.11.2016" },
                            new Property {Name = "card", Value = "5168742060221193" }
                        }
                    }
                }
            };



            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            //settings.Encoding = Encoding.UTF8;


            //DataContractSerializer serializer = new DataContractSerializer(typeof(payment));

            //using (var writer = XmlWriter.Create(Console.Out, settings))
            ////using (var writer = new MyXmlTextWriter(Console.Out))
            //{
            //    //XmlWriter writer = XmlWriter.Create(ms, settings);
            //    serializer.WriteObject(writer, payment);

            //    //writer.

            //    ////ms.Position = 0;
            //    //StreamReader sr = new StreamReader(ms);
            //    //string str = sr.ReadToEnd();
            //    //Console.WriteLine(str);
            //}

            //XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(someStream, Encoding.UTF8);



            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            XmlSerializer serializer = new XmlSerializer(typeof(Request)/*, new Type[] { typeof(payment) }*/);
            serializer.Serialize(Console.Out, request, ns);

            //using (var stream = File.Create("API.xml"))
            //{
            //    serializer.Serialize(stream, request, ns);
            //}

            var encoding = Encoding.UTF8;
            using (StreamWriter sw = new StreamWriter("API_UTF8.xml", false, encoding))
            {
                serializer.Serialize(sw, request, ns);
            }

            Console.ReadLine();

        }
    }
}

//https://api.privatbank.ua/p24api/rest_fiz

//<? xml version="1.0" encoding="UTF-8"?>
//     <request version = "1.0" >
//         < merchant >
//             < id > 75482 </ id >
//             < signature > 5abf5c7524bc2a835acb3a9e24ce10bc5ba82a99</signature>
//         </merchant>
//         <data>
//             <oper>cmt</oper>
//             <wait>0</wait>
//             <test>0</test>
//             <payment id = "" >
//                 <prop name="sd" value="11.08.2013" />
//                 <prop name = "ed" value="11.09.2013" />
//                 <prop name = "card" value="5168742060221193" />
//             </payment>
//         </data>
//     </request>


//<form method = "POST" action="https://api.privatbank.ua/p24api/ishop">
//<input type = "hidden" name="amt" value="0.00" />
//<input type = "hidden" name="ccy" value="UAH" />
//<input type = "hidden" name="merchant" value="123079" />
//<input type = "hidden" name="order" value="" />
//<input type = "hidden" name="details" value="Сторінка, що приймає клієнта після оплати" />
//<input type = "hidden" name="ext_details" value="Опис товару №..." />
//<input type = "hidden" name="pay_way" value="privat24" />
//<input type = "hidden" name="return_url" value="https://..." />
//<input type = "hidden" name="server_url" value="https://..." />
//<button type = "submit" >< img src="img/buttons/api_logo_1.jpg" border="0" /></button>
//</form>