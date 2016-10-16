// 17 - Strings\Regular Expressions\More Complex Parsing
// copyright 2000 Eric Gunnerson
// file=logparse.cs
// compile with: csc logparse.cs
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

class Test
{
    public static void Main(string[] args)
    {
        if (args.Length  == 0) //we need a file to parse
        {
            Console.WriteLine("No log file specified.");
        }
        else 
        ParseLogFile(args[0]);
    }
    public static void ParseLogFile(string    filename)
    {
        if (!System.IO.File.Exists(filename))
        {
            Console.WriteLine ("The file specified does not exist.");
        }
        else 
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            StreamReader stream = new StreamReader(f);
            
            string line;
            line = stream.ReadLine();    // header line
            line = stream.ReadLine();    // version line
            line = stream.ReadLine();    // Date line
            
            Regex    regexDate= new Regex(@"\:\s(?<date>[^\s]+)\s");
            Match    match = regexDate.Match(line);
            string    date = "";
            if (match.Length != 0)
            date = match.Groups["date"].ToString();
            
            line = stream.ReadLine();    // Fields line
            
            Regex    regexLine = 
            new Regex(        // match digit or :
            @"(?<time>(\d|\:)+)\s" +
            // match digit or .
            @"(?<ip>(\d|\.)+)\s" +
            // match any non-white
            @"(?<method>\S+)\s" +
            // match any non-white
            @"(?<uri>\S+)\s" + 
            // match any non-white
            @"(?<status>\d+)");
            
            // read through the lines, add an 
            // IISLogRow for each line
            while ((line = stream.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                match = regexLine.Match(line);
                if (match.Length != 0)
                {
                    Console.WriteLine("date: {0} {1}", date, 
                    match.Groups["time"]);
                    Console.WriteLine("IP Address: {0}", 
                    match.Groups["ip"]);
                    Console.WriteLine("Method: {0}", 
                    match.Groups["method"]);
                    Console.WriteLine("Status: {0}", 
                    match.Groups["status"]);
                    Console.WriteLine("URI: {0}\n", 
                    match.Groups["uri"]);
                }
            }
            f.Close();
        }
    }
}