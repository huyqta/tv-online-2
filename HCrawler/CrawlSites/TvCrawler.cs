using HCrawler.TvOnlineModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HCrawler.CrawlSites
{
    public class TvCrawler
    {
        TvOnlineContext context = new TvOnlineContext();

        public string SiteUrl { get; set; }

        public int CrawlPage(int page)
        {
            try
            {
                //var url = string.Format("{0}/phim-le/page-{1}.html", SiteUrl, page);
                var web = new HtmlWeb();
                var htmlDoc = web.Load("http://m.tivi12h.net/vtvcab7.php");
                int index = 1;
                var nodes = htmlDoc.DocumentNode.SelectNodes("//ul/div/li[@class='channel']/a/@href");
                foreach (var node in nodes)
                {
                    var nodeDoc = web.Load(node.Attributes["href"].Value.Replace("./", "http://www.xemtvhd.com/"));
                    var urlCaptures = nodeDoc.DocumentNode.SelectNodes("//div[@class='container']/div/div/center/div/a[1]/@name");
                    var code = urlCaptures[0].Attributes["name"].Value.Split("/")[3].Split(".")[0];
                    if (context.TbChannel.Count(c => c.ShortCode == code) > 0) continue;
                    else
                    {
                        context.TbChannel.Add(new TbChannel() {
                            Channel = node.ChildNodes["img"].Attributes["alt"].Value,
                            Country = "Vietnam",
                            Language = "Vietnam",
                            GetlinkUrl = "http://m.tivi12h.net/{0}.php",
                            HasStaticStreamUrl = 1,
                            ShortCode = code
                        });
                        context.SaveChanges();
                        Console.WriteLine("Added new channel: " + code);
                    }
                }
                var result = 1;
                if (result > -1)
                {
                    Console.WriteLine(string.Format("Saved {0} records in page {1} successfull!", result, page));
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
                return 0;
            }
        }
    }
}
