//using Movie.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HCrawler.SupportScripts
{
    public class RandomViewAndLike
    {
        //webphimContext context = new webphimContext("webphim");

        public void GenerateViewAndLike(int min, int max)
        {
            try
            {
                context.TbMovie.ToList();
                Random random = new Random();
                int i = 0;
                int total = context.TbMovie.Count();
                foreach (var movie in context.TbMovie)
                {
                    i++;
                    TimeSpan lifeTime = DateTime.Now - movie.ReleaseDate.Value;
                    int days = lifeTime.Days;
                    int randomNumberView = random.Next(100, 300);
                    int randomNumberLike = random.Next(50, 200);
                    movie.CountView = randomNumberView * days;
                    movie.CountLike = randomNumberLike * days;
                    if (movie.CountView < 0)
                    {
                        movie.CountView = movie.CountView * -1;
                        movie.CountLike = movie.CountLike * -1;
                    }
                    if (movie.CountLike > movie.CountView)
                    {
                        movie.CountLike = movie.CountLike / 2;
                        if (movie.CountLike > movie.CountView)
                        {
                            movie.CountLike = movie.CountLike / 2;
                        }
                    }
                    Console.WriteLine(string.Format("Mv {0} / {1}", i, total));
                    
                }
                context.SaveChanges();
                Console.WriteLine(string.Format("Mv {0} / {1} - DONE!!!", i, total));
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
