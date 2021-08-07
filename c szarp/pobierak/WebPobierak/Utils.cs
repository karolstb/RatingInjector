using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace WebPobierak
{
    public static class Utils
    {
        public static string GetOcena(string movieTitle)
        {
            try
            {
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
                        return "";// "";
                    using (var sr = new StreamReader(dataStream))
                    {
                        data = sr.ReadToEnd();
                    }
                }

                string val = GetOcenaPriv(data);

                //Console.Write(val);
                return val;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "(error)";
        }

        private static string GetOcenaPriv(string html, int order = 1)
        {
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
        }

    }
}
