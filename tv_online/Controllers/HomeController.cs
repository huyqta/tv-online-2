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

        public IActionResult Index()
        {
            //var model = context.TbChannel.FirstOrDefault(c => c.ShortCode == channelCode);
            return View(context.TbChannel.ToList());
        }

        
        public IActionResult Watch(string code, int? link = 1)
        {
            WatchTvModel model = new WatchTvModel();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(string.Format(captureUrl, code));
            var script = htmlDoc.DocumentNode.InnerText;
            Regex rx = new Regex(@"(?<=link=\[)(.*?)(?=\];)");
            MatchCollection matches = rx.Matches(script);
            // var listUrls = matches[0].Value.Split(",");
            var listUrls = matches[0].Value.Split(",").Where(l => l.Contains("http")).ToList();
            if (listUrls.Count() >= link && link != 0)
            {
                var urlRqst = listUrls[link.Value].Replace("'", "");
                //if (urlRqst.Contains("id.php"))
                //{
                //    urlRqst = urlRqst.Replace("id.php", "http://ap.tivi12h.net/next.php");
                //}
                //if (urlRqst.Contains("vc.php"))
                //{
                //    urlRqst = urlRqst.Replace("vc.php", "http://ap.tivi12h.net/next.php");
                //}
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRqst);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    model = new WatchTvModel()
                    {
                        TotalUrl = listUrls.Count,
                        Url = reader.ReadToEnd(),
                        Code = code
                    };
                    
                }
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
    }
}
