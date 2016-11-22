using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    class Program
    {
        static void Fetch(string[] args)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://api.sr.se/api/v2/channels/").Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    XDocument xdoc = new XDocument();
                    xdoc = XDocument.Parse(responseString);

                    string actNumber = xdoc.Root.Element("channels").FirstNode.ToString();

                    Console.WriteLine(actNumber);


                    Console.Read();
                }
            }
        }
    }
}
