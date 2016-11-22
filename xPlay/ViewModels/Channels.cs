using System.Collections.Generic;
using System.Reflection;
using xPlay.Models;

namespace xPlay.ViewModels
{
    public class ChannelsViewModel
    {
        public List<RadioChannel> RadioChannels { get; set; }

        public void LoadChannelProperties()
        {

            if (RadioChannels == null)
            {
                RadioChannels = new List<RadioChannel>();
            }

            foreach (FieldInfo field in typeof(Constants.Channels).GetFields())
            {
                RadioChannels.Add(new RadioChannel((int)field.GetRawConstantValue()));
            }
        }

        public ChannelsViewModel Load()
        {
            LoadChannelProperties();
            return this;
        }
    }
}