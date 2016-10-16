// 32 - .NET Frameworks Overview\Reading Web Pages
// copyright 2000 Eric Gunnerson
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class QuoteFetch
{
    public QuoteFetch(string symbol)
    {
        this.symbol = symbol;
    }
    
    public string Last
    {
        get
        {
            string url = "http://moneycentral.msn.com/scripts/webquote.dll?ipage=qd&Symbol=";
            url += symbol;
            
            ExtractQuote(ReadUrl(url));
            return(last);
        }
    }
    string ReadUrl(string url)
    {
        Uri uri = new Uri(url);
        
        //Create the request object
        
        WebRequest req = WebRequest.Create(uri);
        WebResponse resp = req.GetResponse();
        Stream stream = resp.GetResponseStream();
        StreamReader sr = new StreamReader(stream);
        
        string s = sr.ReadToEnd();
        
        return(s);
        
    }
    void ExtractQuote(string s)
    {
        // Line like: "Last</TD><TD ALIGN=RIGHT NOWRAP><B>&nbsp;78 3/16"
        
        Regex lastmatch = new Regex(@"Last\D+(?<last>.+)<\/B>");
        last = lastmatch.Match(s).Groups[1].ToString();
    }
    string    symbol;
    string    last;
}

class Test
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        Console.WriteLine("Quote <symbol>");
        else
        {
            // GlobalProxySelection.Select = new DefaultControlObject("proxy", 80);
            QuoteFetch q = new QuoteFetch(args[0]);
            Console.WriteLine("{0} = {1}", args[0], q.Last);
        }
    }
}