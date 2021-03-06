﻿using HtmlAgilityPack;
using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HCrawler.CrawlSites
{
    class JavforMe
    {
        webphimContext context = new webphimContext();

        public string SiteUrl { get; set; }

        public int CrawlPage(int page)
        {
            try
            {
                var url = string.Format("{0}/list/hot/{1}.html", SiteUrl, page);
                var web = new HtmlWeb();
                var htmlDoc = web.Load(url);
                int index = 1;
                var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='video-img']/a");
                foreach (var node in nodes)
                {
                    var movie = new TbMovie();
                    var movie_url = SiteUrl + node.Attributes[0].Value;
                    htmlDoc = web.Load(movie_url);
                    //movie.GoogleDrive = movie_url;
                    movie.MovieName = htmlDoc.DocumentNode.SelectNodes("//h5[@class='py-2']")[0].InnerHtml;
                    movie.PosterUrl = node.ChildNodes[1].Attributes[1].Value;
                    movie.ImageUrl = movie.PosterUrl;
                    var movie_infor = htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-description mt-3']/div");
                    for (int i = 0; i < 4; i++)
                    {
                        movie.ReleaseDate = DateTime.ParseExact(movie_infor[0].InnerText.Split(":")[1].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        movie.ActorTag = movie_infor[1].InnerText.Split(":")[1].Trim();
                        //movie.StudioTag = movie_infor[2].InnerText.Split(":")[1].Trim();
                        movie.MovieTag = movie_infor[3].InnerText.Split(":")[1].Trim();
                        movie.CategoryTag = "JAV,HD";
                        movie.Country = "Japan";
                        movie.ImdbId = movie.MovieName.Split(" ")[0] == "EN" ? movie.MovieName.Split(" ")[1] : movie.MovieName.Split(" ")[0];
                        movie.StudioTag = movie.ImdbId.Split("-")[0];
                        movie.MovieType = "PS";
                        movie.GoogleDrive = string.Empty;
                    }
                    if (context.TbMovie.Any(m => m.ImdbId.IndexOf(movie.ImdbId) > -1)) continue;
                    context.TbMovie.Add(movie);
                    Console.WriteLine(index + "-" + movie.ImdbId);
                    index++;
                }
                var result = context.SaveChanges();
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
