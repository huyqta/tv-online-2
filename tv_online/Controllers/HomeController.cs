using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tv_online.Models;

namespace tv_online.Controllers
{
    public class HomeController : Controller
    {
        TvOnlineContext context = new TvOnlineContext();

        public IActionResult Index()
        {
            //var model = context.TbChannel.FirstOrDefault(c => c.ShortCode == channelCode);
            return View(context.TbChannel.ToList());
        }

        [HttpGet]
        public IActionResult Watch(string code)
        {
            var model = context.TbChannel.FirstOrDefault(c => c.ShortCode == code);
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
