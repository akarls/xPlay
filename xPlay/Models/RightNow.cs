using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;

namespace xPlay.Models
{
    public class RightNow
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentSubtitle { get; set; }
        public string CurrentDescription { get; set; }
        public string CurrentStart { get; set; }
        public string CurrentEnd { get; set; }
        public string CurrentProgramId { get; set; }
        public string CurrentProgramName { get; set; }
        public string CurrentSocialImage { get; set; }

        public RightNow(int id)
        {
            this.ChannelId = id;

            using (var client = new HttpClient())
            {

                var response = client.GetAsync(Constants.ApiUri.RightNow + ChannelId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    var xdoc = XDocument.Parse(responseString);

                    this.ChannelName = (string)xdoc.Root.Element("channel").Attribute("name");
                    this.CurrentTitle = (string)xdoc.XPathSelectElement("/sr/channel/currentscheduledepisode/title");
                }
            }
        }
    }
}