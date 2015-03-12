﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WebGet
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO add N (= depth level) as parameter
            int depthLevel = 0;
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



            System.Console.WriteLine("Downloading page " + destinationUrl + " to " + localStoragePath + "...");
            //get the date and time to use for naming a new folder
            string timeStamp = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();
            //replace : sign with _
            string folderName = timeStamp.Replace(":", "-");
            //create subfolder with timestamp as name
            string subFolderPath = localStoragePath + "\\" + folderName;
            System.IO.Directory.CreateDirectory(subFolderPath);
            string wikiFolder = subFolderPath + "\\" + "wiki";
            System.IO.Directory.CreateDirectory(wikiFolder);

            string pageContent = DownloadWebPage(destinationUrl, subFolderPath);
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

    }
}
