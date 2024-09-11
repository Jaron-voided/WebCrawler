using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Crawler
    {
        public static async Task<string[]> CrawlLinks()
        {
            await Downloader.DownloadWebPage();
            string[] links = await Searcher.SearchPageForLinks("index.html");
            return links;

        }
        public static async Task<string[]> CrawlLinks(string[] links)
        {
            List<string> newLinks = new List<string>();
            for(var i = 0; i < 10; i++)
            {
                await Downloader.DownloadWebPage($"index{i}.html", links[i]);
                string[] newLinksArray = await Searcher.SearchPageForLinks($"index{i}.html");
                foreach(string newLink in newLinksArray)
                {
                    newLinks.Add(newLink);
                }
            }
            return newLinks.ToArray();
        }

    }
}