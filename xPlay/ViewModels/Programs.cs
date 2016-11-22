using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using xPlay.Models;

namespace xPlay.ViewModels
{
    public class ProgramsViewModel
    {
        public List<RadioProgram> RadioPrograms { get; set; }
        public string RightNow { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelMp3lUrl { get; set; }
        public string ChannelColor { get; set; }

        public ProgramsViewModel(int channelId)
        {
            this.ChannelId = channelId;

            if (RadioPrograms == null)
            {
                RadioPrograms = new List<RadioProgram>();
            }

            using (var client = new HttpClient())
            {

                var task1 = client.GetAsync(Constants.ApiUri.ProgramsUri + "index?channelid=" + channelId + "&size=0").Result;

                if (task1.IsSuccessStatusCode)
                {
                    var responseContent = task1.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    var xdoc = XDocument.Parse(responseString);
                    XElement totalhitsElement = xdoc.XPathSelectElement("/sr/pagination/totalhits");

                    var task2 = client.GetAsync(Constants.ApiUri.ProgramsUri + "index?channelid=" + channelId + "&size=" + totalhitsElement.Value).Result;
                    if (task2.IsSuccessStatusCode)
                    {
                        var responseContent2 = task2.Content;
                        var responseString2 = responseContent2.ReadAsStringAsync().Result;
                        var xdoc2 = XDocument.Parse(responseString2);
                        IEnumerable<XElement> programsElements2 = xdoc2.XPathSelectElements("./sr/programs/program");
                        foreach (var p in programsElements2)
                            RadioPrograms.Add(new RadioProgram() { Id = 111, Name = p.Attribute("name").Value });
                    }
                }

                var task3 = client.GetAsync("http://api.sr.se/api/v2/scheduledepisodes/rightnow?channelid=" + channelId).Result;
                if (task3.IsSuccessStatusCode)
                {
                    var responseContent = task3.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    var xdoc = XDocument.Parse(responseString);
                    XElement totalhitsElement = xdoc.XPathSelectElement("/sr/channel/currentscheduledepisode/title");

                    RightNow = (string) totalhitsElement.Value;
                }

                var task4 = client.GetAsync("http://api.sr.se/api/v2/channels/" + channelId).Result;
                if (task4.IsSuccessStatusCode)
                {
                    var responseContent = task4.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    var xdoc = XDocument.Parse(responseString);
                    XElement totalhitsElement = xdoc.XPathSelectElement("/sr/channel");
                    XElement srPlayElement = xdoc.XPathSelectElement("/sr/channel/liveaudio/url");
                    XElement srColorElement = xdoc.XPathSelectElement("/sr/channel/color");

                    ChannelName = (string)totalhitsElement.Attribute("name");
                    ChannelMp3lUrl = (string)srPlayElement.Value;
                    ChannelColor = (string)srColorElement.Value;


                }
            }

        }
    }
    

}