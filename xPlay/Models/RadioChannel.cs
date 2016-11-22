using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;

namespace xPlay.Models
{
    public class RadioChannel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string SiteUrl { get; set; }
        public string ChannelType { get; set; }

        public RadioChannel(int id)
        {
            this.Id = id;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Constants.ApiUri.OneChannelUri(Id)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    var xdoc = XDocument.Parse(responseString);

                    this.Name = (string)xdoc.Root.Element("channel").Attribute("name");
                    this.Image = (string)xdoc.XPathSelectElement("/sr/channel/image");
                    this.Color = (string)xdoc.XPathSelectElement("/sr/channel/color");
                    this.SiteUrl = (string)xdoc.XPathSelectElement("/sr/channel/siteurl");
                    this.ChannelType = (string)xdoc.XPathSelectElement("/sr/channel/channeltype");
                }
                }
            }
        }
    }
