using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_webscraper
{
    internal class Job
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Keywords { get; set; }
        public string Link { get; set; }
        public Job(string title, string company, string location, string keywords, string link)
        {
            Title = title;
            Company = company;
            Location = location;
            Keywords = keywords;
            Link = link;
        }
    }
}