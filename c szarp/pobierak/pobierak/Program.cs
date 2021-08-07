using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace pobierak
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("Hello World!");

                //WebClient client = new WebClient();
                ////string downloadString = client.DownloadString(args[0]);
                //string downloadString = client.DownloadString("https://www.el12.pl");

                //Console.Write(downloadString);
                //Console.ReadLine();

                //string url = "https://www.google.com/search?q=" + "opeth" + "&tbm=isch";
                //string url = args[0];
                //string url = "https://www.google.pl/search?q=moneyball+imdb";
                string movieTitle = args[0];
                //movieTitle = "the+matrix";
                string url = $"https://www.google.pl/search?q=" + movieTitle + "+imdb";
                string data = "";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

                var response = (HttpWebResponse)request.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream == null)
                        return;// "";
                    using (var sr = new StreamReader(dataStream))
                    {
                        data = sr.ReadToEnd();
                    }
                }

                //string val = GetMatch(data);
                //string val = data;
                string val = GetOcena(data);

                Console.Write(val);
                //return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static string GetOcena(string html, int order = 1)
        {
            string url = "";
            int i = 1;
            int start = html.IndexOf("<span>Ocena:", StringComparison.OrdinalIgnoreCase);
            if (start < 0)
                return "(brak)";
            int end = html.IndexOf("</span>", start, StringComparison.OrdinalIgnoreCase);
            if (end < 0)
                return "(brak)";

            string ocena = html.Substring(start + 6, end - start - 6);

            //ocena = Encoding.ASCII.GetString(ocena);
            ocena = HttpUtility.HtmlDecode(ocena);

            return ocena;

            //while (ndx >= 0 && i <= order)
            //{
            //    ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
            //    ndx++;
            //    int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
            //    url = html.Substring(ndx, ndx2 - ndx);
            //    ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            //    i++;
            //}
            //return url;
        }

        private static string GetMatch(string text)
        {
            string pattern = @"<span>Ocena";
            Regex regex = new Regex(pattern);
            //1. if one occurence in a string:
            Match match = regex.Match(text);
            if (match.Success)
            {
                string myText = match.Groups[0].Value;

                return myText;

                string[] twoData = myText.Split(',');
                foreach (string data in twoData)
                {
                    myText = data;
                }
            }

            return "test";
        }
    }
}
