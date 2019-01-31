using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCrawler.CrawlSites
{
    public class TvCrawler
    {
        public string SiteUrl { get; set; }

        public int CrawlPage(int page)
        {
            try
            {
                //var url = string.Format("{0}/phim-le/page-{1}.html", SiteUrl, page);
                var web = new HtmlWeb();
                var htmlDoc = web.Load(SiteUrl);
                int index = 1;
                var nodes = htmlDoc.DocumentNode.SelectNodes("//ul/div/li[@class='channel']/a/@href");
                foreach (var node in nodes)
                {
                    var nodeDoc = web.Load(node.InnerText.Replace("./", "http://www.xemtvhd.com/"));
                    var urlCaptures = nodeDoc.DocumentNode.SelectNodes("//div[@class='container']/div/div/center/div/a[1]/@name");
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
