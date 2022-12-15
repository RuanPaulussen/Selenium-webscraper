using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_webscraper
{
    internal class YTVideo
    {
            public string Link { get; set; }
            public string Title { get; set; }
            public string Channel { get; set; }
            public double Views { get; set; }
            public YTVideo(string link, string title, string channel, double views) 
        { 
            Link = link;
            Title = title;
            Channel = channel;
            Views = views;
        }
    }
}
