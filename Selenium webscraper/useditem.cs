using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_webscraper
{
    internal class UsedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public UsedItem(string title, string description, double price, string location)
        {
            Title = title;
            Description = description;
            Price = price;
            Location = location;
        }
    }
}