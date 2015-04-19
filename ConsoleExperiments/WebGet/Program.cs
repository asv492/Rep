using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
/*
 * Write a program which will:
1) accept an url and a destination path as command line arguments
2) download html of that url (not images)   [Level 0]
3) download html of all links in that url (not images) [Level 1]
4) find out what must be done to allow offline browsing between Level 0 and Level 1 
   (with internet OFF, and private window to prevent caching)
5) download Level N
 * 
 */
using System.Windows.Forms;

namespace WebGet
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TODO add N (= depth level) as parameter
            int depthLevel = 1;
            int currentDepthLevel = 0;
            // Test if input arguments were supplied: 
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: WebGet <Destination URL> <Local storage path>");
                System.Console.WriteLine("Example: WebGet www.bbc.com c:\\temp\\");

                Environment.Exit(0);
            }

            //TODO check if URL and path are valid
            string destinationUrl = args[0];
            string localStoragePath = args[1];

            //TODO don't download the same page twice

            System.Console.WriteLine("Downloading page " + destinationUrl + " to " + localStoragePath + "...");
            //get the date and time to use for naming a new folder
            string timeStamp = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();
            //replace ":" sign with "_"
            string folderName = timeStamp.Replace(":", "-");
            //create subfolder with timestamp as name
            string subFolderPath = localStoragePath + "\\" + folderName;
            System.IO.Directory.CreateDirectory(subFolderPath);
            string wikiFolder = subFolderPath + "\\" + "wiki";
            System.IO.Directory.CreateDirectory(wikiFolder);

            //different logic
            //get page content
            //for each link in page
            /*
             follow the link
             * but how?
             * create unique file name
             * save page at the link with the unique file name
             * replace the link in page content with the unique file name
             * 
             * limit number of requests per second
             * 
         
             */
            string pageContent = DownloadHtmlPage(destinationUrl);

            for (int i = 0; i < depthLevel; i++)
            {
                
            }

            var w = new WebClient();
            string str = w.DownloadString("http://www.play-hookey.com/");


            foreach (LinkItem i in LinkFinder.Find(str))
            {
                Console.WriteLine(i);
            }

            //var wb = new WebBrowser { Url = new Uri(destinationUrl) };
            //Thread.Sleep(3000);
            //Console.WriteLine(wb.DocumentText);

            //wb.DocumentCompleted += wb_DocumentCompleted;

            //string offlinePageContent = ""; //ChangeLinksForOfflineUse(pageContent);

            //end of different logic

            //string pageContent = DownloadWebPage(destinationUrl, subFolderPath);

            var urlList = GetUrlsInPage(pageContent);
            foreach (string s in urlList)
            {
                if (!s.EndsWith(".jpg") && s.StartsWith("/wiki"))
                {
                    string destinationChildUrl = "http://en.wikipedia.org" + s;
                    string subSubFolderPath = wikiFolder;
                    string subPageContent = DownloadWebPage(destinationChildUrl, subSubFolderPath);
                    string pageName = s.Substring(5);
                    File.WriteAllText(subSubFolderPath + "\\" + pageName, subPageContent);
                }
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }

        static void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            HtmlDocument source = ((WebBrowser)sender).Document;

            ExtractLink(source);

        }



        private static void ExtractLink(HtmlDocument source)
        {

            HtmlElementCollection anchorList = source.GetElementsByTagName("a");

            foreach (var item in anchorList)
            {

                Console.WriteLine(((HtmlElement)item).GetAttribute("href"));

            }

        }

        private static string ChangeLinksForOfflineUse(string pageContent)
        {
            throw new NotImplementedException();
        }

        private static string DownloadWebPage(string destinationUrl, string subFolderPath)
        {
            var client = new WebClient();
            string pageContent = "";
            Stream data = client.OpenRead(destinationUrl);
            if (data != null)
            {
                var reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                Console.WriteLine(s);

                SaveWebPage(s, subFolderPath, "index.htm");
                pageContent = s;
                data.Close();
                reader.Close();

            }
            return pageContent;
        }

        static void SaveWebPage(string pageContent, string path, string fileName)
        {
            string pathWithFileName = path + "\\" + fileName;
            File.WriteAllText(pathWithFileName, pageContent);

        }

        static List<string> GetUrlsInPage(string pageContent)
        {
            var urlList = new List<string>();
            if (pageContent != null)
            {
                Regex r = new Regex(@"(?<=href="")[^\""]*(?="")");
                // Match the regular expression pattern against a text string.
                Match m = r.Match(pageContent);
                while (m.Success)
                {
                    urlList.Add(m.ToString());
                    m = m.NextMatch();
                }


            }
            return urlList;
        }

        //different logic
        //get page content
        //for each link in page
        /*
         follow the link
         * but how?
         * create unique file name
         * save page at the link with the unique file name
         * replace the link in page content with the unique file name
         * 
         * limit number of requests per second
         * 
         
         */

        private static string DownloadHtmlPage(string url)
        {
            string pageContent = null;
            try
            {
                // Open a connection
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                // set timeout for 10 seconds (Optional)
                httpWebRequest.Timeout = 10000;

                // Request response:
                System.Net.WebResponse webResponse = httpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream webStream = webResponse.GetResponseStream();

                // Create reader object:
                if (webStream != null)
                {
                    var streamReader = new System.IO.StreamReader(webStream);

                    // Read the entire stream content:
                    pageContent = streamReader.ReadToEnd();

                    // Cleanup
                    streamReader.Close();
                }
                webStream.Close();
                webResponse.Close();
            }
            catch (Exception exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", exception.ToString());
                return null;
            }

            return pageContent;
        }

        public struct LinkItem
        {
            public string Href;
            public string Text;

            public override string ToString()
            {
                return Href + "\n\t" + Text;
            }
        }

        static class LinkFinder
        {
            public static List<LinkItem> Find(string file)
            {
                List<LinkItem> list = new List<LinkItem>();

                // 1.
                // Find all matches in file.
                MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                    RegexOptions.Singleline);

                // 2.
                // Loop over each match.
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    LinkItem i = new LinkItem();

                    // 3.
                    // Get href attribute.
                    Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        i.Href = m2.Groups[1].Value;
                    }

                    // 4.
                    // Remove inner tags from text.
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
                    RegexOptions.Singleline);
                    i.Text = t;

                    list.Add(i);
                }
                return list;
            }
        }




    }
}
