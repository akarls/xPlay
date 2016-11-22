namespace xPlay
{
    public struct Constants
    {
        public struct ApiUri
        {
            // http://api.sr.se/api/v2/programs/index?channelid=163
            // Adressing different API methods
            public const string RightNow = "http://api.sr.se/api/v2/scheduledepisodes/rightnow?channelid=";
            public const string ChannelsUri = "http://api.sr.se/api/v2/channels/"; // Get all channels, paged
            public const string ProgramsUri = "http://api.sr.se/api/v2/programs/"; // http://api.sr.se/api/v2/programs/{id}
            public static string OneChannelUri(int id)
            {
                return Constants.ApiUri.ChannelsUri + id;
            }
        }

        public struct Channels
        {
            // This struct loads channels in to the list of available channels... 
            public const int P4_STHLM = 701;
            public const int P1 = 132;
            public const int P2 = 163;
            public const int P3 = 164;
        }
    }
   
}