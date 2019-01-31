using System;
using System.Collections.Generic;

namespace HCrawler.TvOnlineModels
{
    public partial class TbChannel
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public string StreamUrl { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string ShortCode { get; set; }
        public string GetlinkUrl { get; set; }
        public int HasStaticStreamUrl { get; set; }
        public string LogoUrl { get; set; }
        public string ChannelGroup { get; set; }
    }
}
