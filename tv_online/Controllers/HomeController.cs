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
using RestSharp;
using System.Net.Http;
using System.Threading.Tasks;

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

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRqst);
                    request.ContentType = "text/html; charset=UTF-8";
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var streamUrl = reader.ReadToEnd();
                        if (streamUrl.Contains("http") && ValidateM3U8(streamUrl))
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

                    //HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(new Uri(urlRqst));


                    //HttpWebResponse resp = (HttpWebResponse)rq.GetResponse();
                    //using (Stream s = resp.GetResponseStream())
                    //{
                    //    using (StreamReader reader = new StreamReader(s))
                    //    {
                    //        var streamUrl = reader.ReadLine();
                    //        if (streamUrl.Contains("http"))
                    //        {
                    //            model = new WatchTvModel()
                    //            {
                    //                TotalUrl = listUrls.Count,
                    //                Url = streamUrl,
                    //                Code = code
                    //            };
                    //            return View(model);
                    //        }
                    //        else
                    //        {
                    //            continue;
                    //        }

                    //    }
                    //}

                    //var task = GetDataAsync(urlRqst);
                    //task.Wait();
                    //var streamUrl = task.Result;
                    //if (streamUrl.Contains("http"))
                    //{
                    //    model = new WatchTvModel()
                    //    {
                    //        TotalUrl = listUrls.Count,
                    //        Url = streamUrl,
                    //        Code = code
                    //    };
                    //    return View(model);
                    //}
                    //else
                    //{
                    //    continue;
                    //}
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
            if (string.IsNullOrWhiteSpace(model.Url) && !string.IsNullOrWhiteSpace(channel.StreamUrl))
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
                //if (url.IndexOf("rtmp://") == 0) return true;
                //var request = (HttpWebRequest)WebRequest.Create(url);
                //request.Method = "HEAD";
                //var response = (HttpWebResponse)request.GetResponse();
                //var success = response.StatusCode == HttpStatusCode.OK && response.ContentLength > 0;

                //if (success) return true;
                //else return false;

                return true;
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
                return false;
            }
        }

        private async Task<string> GetDataAsync(string url)
        {
            //We will make a GET request to a really cool website...

            string baseUrl = url;
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            HttpClient client = new HttpClient();

                //Setting up the response...         
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    return data;
                }
            }
            return "nolink";
        }
    }
}
