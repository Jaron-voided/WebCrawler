using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Searcher
    {
        public static async Task<string[]> SearchPageForLinks(string filename)
        {
            Console.WriteLine("Starting to search...");
            List<string> links = new List<string>();

            try
            {
                // Pass the file path and file name to the StreamReader constructor
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    int pastHeader = 0;
                    // Continue to read each line until you reach end of file
                    while ((line = sr.ReadLine()) != null)
                    {

                        if(line.Contains("</header>"))
                        {
                            pastHeader++;

                        }
                        if(pastHeader < 2)
                        {
                            continue;
                        }
                        // Check if the line contains an <a> tag
                        if (line.Contains("href="))
                        {
                            // Regular expression to match <a href="...">
                            Regex regex = new Regex("href=\"(.*?)\"", RegexOptions.IgnoreCase);

                            // Match the line against the regex
                            Match match = regex.Match(line);

                            // If a match is found, extract the link
                            if (match.Success && line.Contains("/wiki/") && !line.Contains("cite") && !line.Contains("mw-data") && !line.Contains("https"))
                            {
                                string link = match.Groups[1].Value;
                                Console.WriteLine($"Found link: {link}");
                                links.Add(link);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.Message}");
            }

            return links.ToArray();
        }
    }
}
