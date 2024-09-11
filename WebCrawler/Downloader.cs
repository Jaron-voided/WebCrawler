using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Downloader
    {
        //public static string urlToDownload = "https://en.wikipedia.org/wiki/";

        public static async Task<string> DownloadWebPage(
            string filename = "index.html", 
            string urlEnding = "/wiki/Pokemon",
            string urlToDownload = "https://en.wikipedia.org")
        {
            Console.WriteLine("Starting download...");
            string urlToSearch = $"{urlToDownload}{urlEnding}";

            // Setup the HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                // Get the webpage asynchronously
                HttpResponseMessage resp = await httpClient.GetAsync(urlToSearch);

                // If we get a 200 response, then save it
                if (resp.IsSuccessStatusCode)
                {
                    Console.WriteLine("Got it...");

                    // Get the data
                    byte[] data = await resp.Content.ReadAsByteArrayAsync();

                    // Save it to a file
                    using (FileStream fStream = File.Create(filename))
                    {
                        await fStream.WriteAsync(data, 0, data.Length);
                    }

                    Console.WriteLine("Done!");
                }
                else
                {
                    Console.WriteLine($"Failed to download. Status code: {resp.StatusCode}");
                }
            }

            // Return the filename so it can be used later
            return filename;
        }
    }
}
