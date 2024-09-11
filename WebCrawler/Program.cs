using System;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Start by downloading the initial page
            //await DownloadAndSearchRecursively("index.html", "Mueller_special_counsel_investigation");
            //await Downloader.DownloadWebPage();
            //await Searcher.SearchPageForLinks("index.html");
            string[] links = await Crawler.CrawlLinks();
            //List<string> newLinks = new List<string>();
            await Crawler.CrawlLinks(links);
        }
    }
}
