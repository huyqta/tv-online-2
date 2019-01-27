using System;
using System.Collections.Generic;

namespace tv_online.Models
{
    public partial class TbChannel
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public string StreamUrl { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string ShortCode { get; set; }
    }
}
