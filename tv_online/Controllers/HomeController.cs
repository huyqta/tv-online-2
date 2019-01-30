using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using tv_online.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace tv_online.Controllers
{
    public class HomeController : Controller
    {
        TvOnlineContext context = new TvOnlineContext();
        string captureUrl = "http://m.tivi12h.net/{0}.php";
        //string captureUrl = "http://m.xemtvhd.com/vtv6.php";

        public IActionResult Index()
        {
            //var model = context.TbChannel.FirstOrDefault(c => c.ShortCode == channelCode);
            return View(context.TbChannel.ToList());
        }

        
        public IActionResult Watch(string code, int? link = 1)
        {
            var channel = context.TbChannel.Where(c => c.ShortCode == code).FirstOrDefault();
            WatchTvModel model = new WatchTvModel();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(string.Format(channel.GetlinkUrl, code));
            var script = htmlDoc.DocumentNode.InnerText;
            Regex rx = new Regex(@"(?<=link=\[)(.*?)(?=\];)");
            MatchCollection matches = rx.Matches(script);
            // var listUrls = matches[0].Value.Split(",");
            if (matches.Count == 0)
            {
                return View();
            }
            var listUrls = matches[0].Value.Split(",").Where(l => l.Contains("http")).ToList();
            if (listUrls.Count == 0)
            {
                return View();
            }
            if (listUrls.Count() >= link && link != 0)
            {
                foreach (var url in listUrls)
                {
                    
                    var urlRqst = url.Replace("'", "");

                    var client = new WebClient();
                    var text = client.DownloadString(urlRqst);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRqst);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var streamUrl = reader.ReadToEnd();
                        if (streamUrl.Contains("http"))
                        {
                            model = new WatchTvModel()
                            {
                                TotalUrl = listUrls.Count,
                                Url = streamUrl,
                                Code = code
                            };
                            return View(model);
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                //var urlRqst = listUrls[link.Value].Replace("'", "");
                //if (urlRqst.Contains("id.php"))
                //{
                //    urlRqst = urlRqst.Replace("id.php", "http://ap.tivi12h.net/next.php");
                //}
                //if (urlRqst.Contains("vc.php"))
                //{
                //    urlRqst = urlRqst.Replace("vc.php", "http://ap.tivi12h.net/next.php");
                //}
                
            }
            //var textFromFile = (new WebClient()).DownloadString(model.Url);
            //var urls = textFromFile.Split("http");
            //model.Url = "http" + urls[urls.Count() - 1];
            //model.Url = model.Url.Replace("\n", "");
            //TbChannel channel = context.TbChannel.Where(c => c.ShortCode == code).FirstOrDefault();
            //channel.StreamUrl = model.Url;
            //context.Entry<TbChannel>(channel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //context.SaveChanges();
            //var model = context.TbChannel.FirstOrDefault(c => c.ShortCode == code);
            if (string.IsNullOrWhiteSpace(model.Url))
            {
                foreach (var url in channel.StreamUrl.Split("|"))
                {
                    if (ValidateM3U8(url))
                    {
                        model.Url = url;
                        return View(model);
                    }
                    else continue;
                    
                }
                model.Url = channel.StreamUrl;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool ValidateM3U8(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                var success = response.StatusCode == HttpStatusCode.OK && response.ContentLength > 0;

                if (success) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
