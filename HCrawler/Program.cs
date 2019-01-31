using System;
using HtmlAgilityPack;
//using Movie.Services.Models;
using System.Globalization;
using HCrawler.CrawlSites;
//using HCrawler.CrawlSites;
//using HCrawler.SupportScripts;

namespace HCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TvCrawler tv = new TvCrawler();
                tv.SiteUrl = "http://www.xemtvhd.com/";
                tv.CrawlPage(0);
                
                //PhimMoi me = new PhimMoi();
                //for (int j = 1393; j < 2379; j++)
                //{
                //    //me.SiteUrl = "https://javfor.me";
                //    me.SiteUrl = "http://www.phimmoi.net";
                //    me.CrawlPage(j);
                //}
                //RandomViewAndLike rd = new RandomViewAndLike();
                //rd.GenerateViewAndLike(0, 0);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            
            // Actor crawler
            //var actor = new TbActor()
            //{
            //    ActorName = nodes[0].InnerText,
            //    ActorType = "JAV",
            //    Title = nodes[0].Attributes.Count == 2 ? nodes[0].Attributes[1].Value : string.Empty
            //};
            //foreach (var node in nodes)
            //{
            //    context.TbActor.Add(new TbActor()
            //    {
            //        ActorName = node.InnerText,
            //        ActorType = "JAV",
            //        Title = node.Attributes.Count == 2 ? node.Attributes[1].Value : string.Empty
            //    });
            //    Console.WriteLine(node.InnerText + "|" + nodes.IndexOf(node));
            //}

            //context.SaveChanges();
            //Console.ReadLine();
        }

        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}
