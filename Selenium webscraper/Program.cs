using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_webscraper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Xml.Linq;

double GetNumber(string numberstring)
{
    double number = double.Parse(numberstring.Where(x=> (Char.IsDigit(x)) | x == ',').ToArray());
    return number;
}
Console.WriteLine("Youtube search term:");
string videosearch = Console.ReadLine();
Console.WriteLine("Job search term:");
string jobsearch = Console.ReadLine();
Console.WriteLine("Used item search term:");
string itemsearch = Console.ReadLine();

IWebDriver driver = new ChromeDriver();

// YouTube
driver.Navigate().GoToUrl("https://www.youtube.com/");
Thread.Sleep(1000);
var acceptbuttonyt = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[2]/yt-button-shape/button"));
acceptbuttonyt.Click();
Thread.Sleep(2000);
var vidsearchbar = driver.FindElement(By.CssSelector("input#search"));
vidsearchbar.SendKeys(videosearch);
vidsearchbar.Submit();
var filters = driver.FindElement(By.XPath("//*[@id=\"container\"]/ytd-toggle-button-renderer/yt-button-shape/button"));
filters.Click();
var recent = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string"));
recent.Click();
Thread.Sleep(2000);
var links = driver.FindElements(By.XPath("(/html/body/ytd-app/div/ytd-page-manager/ytd-search/div/ytd-two-column-search-results-renderer/div/div/ytd-section-list-renderer/div/ytd-item-section-renderer/div/ytd-video-renderer/div/div/div/div/h3/a)[position()<6]"));
List<string> urllist = new();
foreach (var link in links)
{
    var url = link.GetAttribute("href");
    urllist.Add(url);
}
List<YTVideo> YTVideos = new();
foreach (var url in urllist)
{
    driver.Navigate().GoToUrl(url);
    Thread.Sleep(2000);
    var channel = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[2]/div[1]/ytd-video-owner-renderer/div[1]/ytd-channel-name/div/div/yt-formatted-string/a")).Text;
    var title = driver.FindElement(By.XPath("//*[@id=\"title\"]/h1/yt-formatted-string")).Text;
    var viewsbutton = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/div/yt-formatted-string/span[1]"));
    viewsbutton.Click();
    Thread.Sleep(500);
    var views = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/div/yt-formatted-string/span[1]")).Text;
    var viewsnmbr = GetNumber(views);
    YTVideos.Add(new YTVideo(url, title, channel, viewsnmbr));
}

//Ict jobs

driver.Navigate().GoToUrl("https://www.ictjob.be/nl/");
Thread.Sleep(1000);
var jobsearchbar = driver.FindElement(By.XPath("//*[@id=\"keywords-input\"]"));
jobsearchbar.SendKeys(jobsearch);
jobsearchbar.Submit();
Thread.Sleep(20000);

List<Job> jobs = new();
for (int i = 1; i < 7; i++)
{
    if (i != 4) {
        var title = driver.FindElement(By.XPath(("//*[@id=\"search-result-body\"]/div/ul/li[" + i.ToString() + "]/span[2]/a/h2"))).Text;
        var company = driver.FindElement(By.XPath(("//*[@id=\"search-result-body\"]/div/ul/li[" + i.ToString() + "]/span[2]/span[1]"))).Text;
        var location = driver.FindElement(By.XPath(("//*[@id=\"search-result-body\"]/div/ul/li[" + i.ToString() + "]/span[2]/span[2]/span[2]/span/span"))).Text;
        var keywords = driver.FindElement(By.XPath(("//*[@id=\"search-result-body\"]/div/ul/li[" + i.ToString() + "]/span[2]/span[3]"))).Text;
        var link = driver.FindElement(By.XPath(("//*[@id=\"search-result-body\"]/div/ul/li[" + i.ToString() + "]/span[2]/a"))).GetAttribute("href");
        jobs.Add(new Job(title, company, location, keywords,link));
    }
    
}


// 2dehands

driver.Navigate().GoToUrl("https://www.2dehands.be/");
Thread.Sleep(1000);
var acceptbutton2 = driver.FindElement(By.XPath("//*[@id=\"gdpr-consent-banner-accept-button\"]"));
acceptbutton2.Click();
Thread.Sleep(2000);
var itemsearchbar = driver.FindElement(By.XPath("//*[@id=\"header-root\"]/header/div[1]/div[3]/div/form/div[1]/div/div/input"));
itemsearchbar.SendKeys(itemsearch);
itemsearchbar.Submit();
Thread.Sleep(2000);
List<UsedItem> usedItems = new();
for (int i = 1; i < 6; i++)
{
    var title = ("\"" + driver.FindElement(By.XPath(("//*[@id=\"content\"]/div[3]/ul/li[" + i.ToString() + "]/a/div/div[1]/h3"))).Text + "\"");
    Console.WriteLine(title);
    var description = ("\"" + driver.FindElement(By.XPath(("//*[@id=\"content\"]/div[3]/ul/li[" + i.ToString() + "]/a/div/div[1]/p"))).Text + "\"");
    Console.WriteLine(description);
    var price = GetNumber(driver.FindElement(By.XPath(("//*[@id=\"content\"]/div[3]/ul/li[" + i.ToString() + "]/a/div/div[2]/span[1]"))).Text);
    Console.WriteLine(price);
    var location = driver.FindElement(By.XPath(("//*[@id=\"content\"]/div[3]/ul/li[" + i.ToString() + "]/div/span[2]"))).Text;
    Console.WriteLine(location);
    usedItems.Add(new UsedItem(title, description, price, location));
}
// write to files

using (var writer = new StreamWriter("C:\\Users\\RuanP\\Documents\\School\\Dev ops\\project\\youtubevideos.csv", false, Encoding.UTF8))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(YTVideos);
}
string ytjson = JsonSerializer.Serialize(YTVideos);
File.WriteAllText(@"C:\Users\RuanP\Documents\School\Dev ops\project\youtubevideos.json", ytjson, Encoding.UTF8);

using (var writer = new StreamWriter("C:\\Users\\RuanP\\Documents\\School\\Dev ops\\project\\ictjobs.csv", false, Encoding.UTF8))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(jobs);
}
string ictjson = JsonSerializer.Serialize(jobs);
File.WriteAllText(@"C:\Users\RuanP\Documents\School\Dev ops\project\ictjobs.json", ictjson, Encoding.UTF8);

using (var writer = new StreamWriter("C:\\Users\\RuanP\\Documents\\School\\Dev ops\\project\\useditems.csv", false, Encoding.UTF8))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(usedItems);
}
string usedjson = JsonSerializer.Serialize(usedItems);
File.WriteAllText(@"C:\Users\RuanP\Documents\School\Dev ops\project\useditems.json", usedjson, Encoding.UTF8);