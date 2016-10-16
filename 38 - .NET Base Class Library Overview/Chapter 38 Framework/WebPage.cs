using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

static class PageFetcher
{
    public static string Fetch(string url)
    {
        WebRequest req = WebRequest.Create(new Uri(url));
        WebResponse resp = req.GetResponse();

        string contents = null;
        using (Stream stream = resp.GetResponseStream())
        using (StreamReader sr = new StreamReader(stream))
        {
            contents = sr.ReadToEnd();
        }

        return (contents);
    }
}
class WebPageTest
{
    public static void Mainly()
    {
        string page = PageFetcher.Fetch("http://www.microsoft.com");
        Console.WriteLine(page);
    }
}

