using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace lw2_2
{
    class Program
    {
        static string CreateHtmlAndWriteLinks(string address, ref List<string> validLinks, ref List<string> invalidLinks, string host, StreamWriter validFile, StreamWriter invalidFile)
        {

            HtmlWeb client = new HtmlWeb();
            var baseUrl = new Uri(address);
            HtmlDocument doc = client.Load(address);
            FinaAndCreateLink(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "link", "href");
            FinaAndCreateLink(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "a", "href");
            FinaAndCreateLink(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "script", "src");
            FinaAndCreateLink(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "img", "src");
            return address;

        }

        static void FinaAndCreateLink(ref List<string> validLinks, ref List<string> invalidLinks, string host, StreamWriter validFile, StreamWriter invalidFile, Uri baseUrl, HtmlDocument doc, string teg, string attribute)
        {
            if (doc.DocumentNode.SelectNodes("//" + teg + "[@" + attribute + "]") != null)
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//" + teg + "[@" + attribute + "]"))
                {
                    string newValue = link.Attributes[attribute].Value;
                    var url = new Uri(baseUrl, newValue).AbsoluteUri;
                    if (url.Contains("#"))
                    {
                        url = url.Substring(0, url.IndexOf("#"));
                    }
                    Console.WriteLine(url);
                    if ((!validLinks.Contains(url)) && (!invalidLinks.Contains(url)) && (url.IndexOf(host) != -1) && (url.Length > 0) && (newValue != "#"))
                    {
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            //request.AllowAutoRedirect = false;
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            if ((response.StatusCode == HttpStatusCode.OK))
                            {
                                validLinks.Add(url);
                                validFile.WriteLine(url);
                                validFile.WriteLine((int)response.StatusCode);
                                CreateHtmlAndWriteLinks(url, ref validLinks, ref invalidLinks, host, validFile, invalidFile);
                            }
                        }
                        catch (WebException e)
                        {
                            HttpWebResponse response = (HttpWebResponse)e.Response;
                            invalidLinks.Add(url);
                            invalidFile.WriteLine(url);
                            if (response != null)
                            {
                                invalidFile.WriteLine((int)response.StatusCode);

                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            StreamWriter validFile = new StreamWriter("../../../Valid.txt");
            StreamWriter invalidFile = new StreamWriter("../../../Invalid.txt");
            string url = "https://elearning.volgatech.net/my";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine((int)response.StatusCode);
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                Console.WriteLine((int)response.StatusCode);
            }
            /* var host = new System.Uri(url).Host;
            List<string> validLinks = new List<string>();
            List<string> InvalidLinks = new List<string>();
            CreateHtmlAndWriteLinks(url, ref validLinks, ref InvalidLinks, host, validFile, invalidFile);
            validFile.WriteLine(validLinks.Count());
            invalidFile.WriteLine(InvalidLinks.Count());
            validFile.WriteLine(DateTime.Now.ToString());
            validFile.WriteLine(DateTime.Now.ToString());
            validFile.Close();
            invalidFile.Close();*/
        }
    }
}