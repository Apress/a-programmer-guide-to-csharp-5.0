using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Chapter_35_Smugmug_Fetcher
{
    class SmugMugFeed
    {
        string m_name;

        public SmugMugFeed(string name)
        {
            m_name = name;
        }

        private List<string> ParseResponse(string responseText)
        {
            responseText = responseSample;

            XElement element = XElement.Parse(responseText);
            XNamespace media = "http://search.yahoo.com/mrss/";

            List<string> urls = new List<string>();

            foreach (XElement item in element.XPathSelectElements("channel/item"))
            {
                XElement largest = item.Descendants(media + "content").Skip(1).First();
                string url = largest.Attribute("url").Value;

                urls.Add(url);
            }

            return urls;
        }

        public List<string> Fetch()
        {
            return ParseResponse(null);

            string url = String.Format("http://api.smugmug.com/hack/feed.mg?Type={0}&Data=today&format=rss", m_name);

            WebRequest request = WebRequest.CreateHttp(url);

            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream);
                string content = reader.ReadToEnd();
            }
        }

        public byte[] LoadBitmapData(string url)
        {
            WebRequest request = WebRequest.CreateHttp(url);

            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                byte[] buffer;
                using (BinaryReader reader = new BinaryReader(responseStream))
                {
                    buffer = reader.ReadBytes((int) response.ContentLength);
                    return buffer;
                }
            }
        }

        public BitmapImage LoadBitmap(string url)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(url);
            bitmapImage.DecodePixelWidth = 400;
            bitmapImage.EndInit();

            return bitmapImage;
        }

#if fred
        public Image LoadBitmap(string url)
        {
            WebRequest request = WebRequest.CreateHttp(url);

            WebResponse response = request.GetResponse();

            Image image = null;
            using (Stream responseStream = response.GetResponseStream())
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(url);
                //bitmap.StreamSource = responseStream;
                bitmap.DecodePixelWidth = 400;
                bitmap.EndInit();
                image.Source = bitmap;
            }
            return image;
        }
#endif

        const string responseSample = @"<?xml version=""1.0"" encoding=""utf-8""?>
<rss version=""2.0"" xmlns:geo=""http://www.w3.org/2003/01/geo/wgs84_pos#"" xmlns:exif=""http://www.exif.org/specifications.html"" xmlns:media=""http://search.yahoo.com/mrss/"" xmlns:atom=""http://www.w3.org/2005/Atom"">
  <channel>
    <title>SmugMug's all-time most popular photos</title>
    <link>http://www.smugmug.com/popular/all/</link>
    <description>A feed of the all-time most popular photos on SmugMug, calculated using PhotoRank.</description>
    <atom:icon>http://www.smugmug.com/img/feed-icon.png</atom:icon>
    <pubDate>Sat, 21 Jul 2012 07:39:50 -0700</pubDate>
    <lastBuildDate>Sat, 21 Jul 2012 19:39:50 -0700</lastBuildDate>
    <generator>http://www.smugmug.com/</generator>
    <copyright>Copyright 2012, the copyright holder of each photograph.  Some portions copyright SmugMug.  All rights reserved.</copyright>
    <image>
      <url>http://www.smugmug.com/img/smuggy64.gif</url>
      <title>SmugMug's all-time most popular photos</title>
      <link>http://www.smugmug.com/popular/all/</link>
    </image>
    <atom:link rel=""self"" type=""application/rss+xml"" href=""http://api.smugmug.com/hack/feed.mg?Type=popular&amp;Data=all&amp;format=rss""/>
    <atom:link rel=""next"" type=""application/rss+xml"" href=""http://api.smugmug.com/hack/feed.mg?Type=popular&amp;Data=all&amp;format=rss&amp;start=100&amp;PageCount=100""/>
    <item>
      <title>Great Egret at dusk</title>
      <link>http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/2400674_93hKQw#!i=125787395&amp;k=hQSj9</link>
      <description>&lt;p&gt;&lt;a href=""http://jlvaillant.smugmug.com""&gt;Jean-Luc Vaillant&lt;/a&gt;&lt;br /&gt;Great Egret at dusk&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/2400674_93hKQw#!i=125787395&amp;k=hQSj9"" title=""Great Egret at dusk""&gt;&lt;img src=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Th-5.jpg"" width=""150"" height=""100"" alt=""Great Egret at dusk"" title=""Great Egret at dusk"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Animals</category>
      <pubDate>Sat, 27 Jan 2007 15:13:12 -0800</pubDate>
      <author>nobody@smugmug.com (Jean-Luc Vaillant)</author>
      <guid isPermaLink=""false"">http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Th-5.jpg</guid>
      <exif:DateTimeOriginal>2004-12-12 16:20:12</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Ti-5.jpg"" fileSize=""2474"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">a8e361a480d5a88a8aac171a4841cb79</media:hash>
        </media:content>
        <media:content url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Th-5.jpg"" fileSize=""3794"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">ced2fdfd485e092eb9a0d8bc0233ac2b</media:hash>
        </media:content>
        <media:content url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-S-5.jpg"" fileSize=""20438"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">7cc8e2d1af0c3d3dca885d7aad06d433</media:hash>
        </media:content>
        <media:content url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-M-5.jpg"" fileSize=""38057"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">564c9915cda5243b7c35b778b856a343</media:hash>
        </media:content>
        <media:content url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-L-5.jpg"" fileSize=""62021"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">74b8d2eaa33238efb9033ce2fef0788b</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Great Egret at dusk</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://jlvaillant.smugmug.com""&gt;Jean-Luc Vaillant&lt;/a&gt;&lt;br /&gt;Great Egret at dusk&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/2400674_93hKQw#!i=125787395&amp;k=hQSj9"" title=""Great Egret at dusk""&gt;&lt;img src=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Th-5.jpg"" width=""150"" height=""100"" alt=""Great Egret at dusk"" title=""Great Egret at dusk"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://jlvaillant.smugmug.com/Animals/Birds/Egrets-Herons/Bird-000132-Version-3/125787395_hQSj9-Th-5.jpg"" width=""150"" height=""100""/>
      <media:category>Animals</media:category>
      <media:keywords>bird, great egret, outdoor, bird 000132 version</media:keywords>
      <media:copyright url=""http://www.jlvaillant.com"">Jean-Luc Vaillant</media:copyright>
      <media:credit role=""photographer"">Jean-Luc Vaillant</media:credit>
    </item>
    <item>
      <title>winchester's photo</title>
      <link>http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=74475750&amp;k=mxdVR</link>
      <description>&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=74475750&amp;k=mxdVR"" title=""winchester's photo""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Th-3.jpg"" width=""150"" height=""100"" alt=""winchester's photo"" title=""winchester's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Fri, 09 Jun 2006 18:49:02 -0700</pubDate>
      <author>nobody@smugmug.com (winchester)</author>
      <guid isPermaLink=""false"">http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Th-3.jpg</guid>
      <media:group>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Ti-3.jpg"" fileSize=""4780"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">ddfdd8e66ca861cad79812ff40e4963d</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Th-3.jpg"" fileSize=""8799"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">6c165c43f7861c972066a3cf3c1084e1</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-S-3.jpg"" fileSize=""44532"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">bed8368949ee41baea8ef048be6b547b</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-M-3.jpg"" fileSize=""83530"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">4f548cb3db229e628e762ab3485365f2</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-L-3.jpg"" fileSize=""129823"" type=""image/jpeg"" medium=""image"" width=""800"" height=""535"">
          <media:hash algo=""md5"">0a1efc9ab6f956ff897f00b643265530</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-XL-3.jpg"" fileSize=""175373"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""676"">
          <media:hash algo=""md5"">6c043d836952e5cda70ca6b21d61c516</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-O-3.jpg"" fileSize=""175373"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""676"">
          <media:hash algo=""md5"">6c043d836952e5cda70ca6b21d61c516</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">winchester's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=74475750&amp;k=mxdVR"" title=""winchester's photo""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Th-3.jpg"" width=""150"" height=""100"" alt=""winchester's photo"" title=""winchester's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://winchester.smugmug.com/Landscapes/Favorites/final-gold/74475750_mxdVR-Th-3.jpg"" width=""150"" height=""100""/>
      <media:category>Landscapes</media:category>
      <media:keywords>connecticut, sunset, lake, water, rocks, reflection, blue, surreal, winchester lake</media:keywords>
      <media:copyright url=""http://winchester.smugmug.com"">winchester</media:copyright>
      <media:credit role=""photographer"">winchester</media:credit>
    </item>
    <item>
      <title>September 23, 2007&#13;
&#13;
Fog in the forest.</title>
      <link>http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/3015690_hkJHhb#!i=199474043&amp;k=aEViN</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;September 23, 2007&#13;
&#13;
Fog in the forest.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/3015690_hkJHhb#!i=199474043&amp;k=aEViN"" title=""September 23, 2007&#13;
&#13;
Fog in the forest.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Th-1.jpg"" width=""150"" height=""100"" alt=""September 23, 2007&#13;
&#13;
Fog in the forest."" title=""September 23, 2007&#13;
&#13;
Fog in the forest."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Sun, 23 Sep 2007 17:20:17 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2007-09-23 08:23:17</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Ti-1.jpg"" fileSize=""4281"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">75d36febf9b7d7611ee3d8526654f6dc</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Th-1.jpg"" fileSize=""7644"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">56c076f656d1c4ba09f5cdd8aac389b1</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-S-1.jpg"" fileSize=""38224"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">c0882f9db65a70ccc4f623e746ec7254</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-M-1.jpg"" fileSize=""78316"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">047244cfebae065a31437401909ac5b6</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-L-1.jpg"" fileSize=""137867"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">f5778ed518dd7963412b1904fa545158</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">September 23, 2007&#13;
&#13;
Fog in the forest.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;September 23, 2007&#13;
&#13;
Fog in the forest.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/3015690_hkJHhb#!i=199474043&amp;k=aEViN"" title=""September 23, 2007&#13;
&#13;
Fog in the forest.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Th-1.jpg"" width=""150"" height=""100"" alt=""September 23, 2007&#13;
&#13;
Fog in the forest."" title=""September 23, 2007&#13;
&#13;
Fog in the forest."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Daily-Album/Every-day-or-at-least-every/092348-copy-2/199474043_aEViN-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Daily Album</media:category>
      <media:keywords>0923</media:keywords>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ...</title>
      <link>http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=60948552&amp;k=nLkH5</link>
      <description>&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=60948552&amp;k=nLkH5"" title=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Th-3.jpg"" width=""150"" height=""100"" alt=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ..."" title=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Tue, 21 Mar 2006 13:35:24 -0800</pubDate>
      <author>nobody@smugmug.com (Ken Conger)</author>
      <guid isPermaLink=""false"">http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2006-03-08 20:22:26</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Ti-3.jpg"" fileSize=""4519"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">7b11933b7e2e36646dec8b171b4c3159</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Th-3.jpg"" fileSize=""8265"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">b800769b08ef7db43a1b9f90f27f5260</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-S-3.jpg"" fileSize=""44250"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">26136c3b418ae698e42bb00cd63656b9</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-M-3.jpg"" fileSize=""82689"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">66eb0f4f95469f63a58497e6911cee6a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=60948552&amp;k=nLkH5"" title=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Th-3.jpg"" width=""150"" height=""100"" alt=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ..."" title=""This Bald Eagle photograph was captured in Homer, Alaska (3/06). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Front-Fish/60948552_nLkH5-Th-3.jpg"" width=""150"" height=""100""/>
      <media:category>Nature</media:category>
      <media:keywords>eagle, eagles, alaska, homer, bald eagle</media:keywords>
      <media:copyright url=""http://www.kencongerphotography.com"">Ken Conger</media:copyright>
      <media:credit role=""photographer"">Ken Conger</media:credit>
    </item>
    <item>
      <title>Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.</title>
      <link>http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=90938221&amp;k=5prje</link>
      <description>&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=90938221&amp;k=5prje"" title=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Th-2.jpg"" width=""150"" height=""101"" alt=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening."" title=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Sun, 27 Aug 2006 00:12:39 -0700</pubDate>
      <author>nobody@smugmug.com (winchester)</author>
      <guid isPermaLink=""false"">http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Th-2.jpg</guid>
      <media:group>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Ti-2.jpg"" fileSize=""4673"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">3183163b770e899ce7dc225c08012274</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Th-2.jpg"" fileSize=""8390"" type=""image/jpeg"" medium=""image"" width=""150"" height=""101"" isDefault=""true"">
          <media:hash algo=""md5"">b525ec9c0a24577e57aa6fde37ac7aac</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-S-2.jpg"" fileSize=""44535"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">c573636a77bcc9b955013d861a963984</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-M-2.jpg"" fileSize=""86076"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">3073ed49257320df5326e7d7f92077de</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-L-2.jpg"" fileSize=""139355"" type=""image/jpeg"" medium=""image"" width=""800"" height=""536"">
          <media:hash algo=""md5"">93a0d883c0f89fef0d505db1738f4968</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-XL-2.jpg"" fileSize=""191228"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""677"">
          <media:hash algo=""md5"">55103b9132abfd3b54bf3b730d1641af</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-O-2.jpg"" fileSize=""191228"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""677"">
          <media:hash algo=""md5"">55103b9132abfd3b54bf3b730d1641af</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=90938221&amp;k=5prje"" title=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Th-2.jpg"" width=""150"" height=""101"" alt=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening."" title=""Sometimes we can go slightly beyond reality in a photograph. What this does reflect accurately is the mood that evening."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/beyond-reality/90938221_5prje-Th-2.jpg"" width=""150"" height=""101""/>
      <media:category>Landscapes</media:category>
      <media:keywords>alaska, boat, sunset, river, water</media:keywords>
      <media:copyright url=""http://winchester.smugmug.com"">winchester</media:copyright>
      <media:credit role=""photographer"">winchester</media:credit>
    </item>
    <item>
      <title>American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense</title>
      <link>http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/4514243_5TV6qq#!i=265764763&amp;k=5j3J4</link>
      <description>&lt;p&gt;&lt;a href=""http://hep.smugmug.com""&gt;H.E. Pherson Photography&lt;/a&gt;&lt;br /&gt;American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense&lt;/p&gt;&lt;p&gt;&lt;a href=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/4514243_5TV6qq#!i=265764763&amp;k=5j3J4"" title=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense""&gt;&lt;img src=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Th-14.jpg"" width=""150"" height=""113"" alt=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense"" title=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Wildlife</category>
      <pubDate>Fri, 14 Mar 2008 15:50:14 -0700</pubDate>
      <author>nobody@smugmug.com (H.E. Pherson Photography)</author>
      <guid isPermaLink=""false"">http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Th-14.jpg</guid>
      <exif:DateTimeOriginal>2008-03-05 08:50:16</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Ti-14.jpg"" fileSize=""6892"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">81e2e75dfa24b801de9db3c8936ae3d8</media:hash>
        </media:content>
        <media:content url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Th-14.jpg"" fileSize=""9873"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">9806ac02adf2bad5362a2fd9174117eb</media:hash>
        </media:content>
        <media:content url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-S-14.jpg"" fileSize=""37518"" type=""image/jpeg"" medium=""image"" width=""398"" height=""300"">
          <media:hash algo=""md5"">1c59b84358ebba48fad84aa9401b7b23</media:hash>
        </media:content>
        <media:content url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-M-14.jpg"" fileSize=""69076"" type=""image/jpeg"" medium=""image"" width=""597"" height=""450"">
          <media:hash algo=""md5"">15666a149516e983c0cf6f35f6def795</media:hash>
        </media:content>
        <media:content url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-L-14.jpg"" fileSize=""111673"" type=""image/jpeg"" medium=""image"" width=""796"" height=""600"">
          <media:hash algo=""md5"">91e522ef132019fa18319ae08e54cdc4</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://hep.smugmug.com""&gt;H.E. Pherson Photography&lt;/a&gt;&lt;br /&gt;American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense&lt;/p&gt;&lt;p&gt;&lt;a href=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/4514243_5TV6qq#!i=265764763&amp;k=5j3J4"" title=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense""&gt;&lt;img src=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Th-14.jpg"" width=""150"" height=""113"" alt=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense"" title=""American Bald Eagle.&#13;
&#13;
First Place finisher for March 2008 in the prestigious photography contest sponsored by BetterPhoto.com&#13;
Canon 5D with a 500mm IS Lense"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://hep.smugmug.com/Wildlife/American-Bald-Eagles-of-Homer/EAGLES-HOMER-391-copy/265764763_5j3J4-Th-14.jpg"" width=""150"" height=""113""/>
      <media:category>Wildlife</media:category>
      <media:copyright url=""http://www.hepherson.com"">H.E. Pherson Photography</media:copyright>
      <media:credit role=""photographer"">H.E. Pherson Photography</media:credit>
    </item>
    <item>
      <title>August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY</title>
      <link>http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/3245654_CMt3kV#!i=184727446&amp;k=JfNYM</link>
      <description>&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/3245654_CMt3kV#!i=184727446&amp;k=JfNYM"" title=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Th-13.jpg"" width=""150"" height=""107"" alt=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY"" title=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Thu, 16 Aug 2007 20:17:41 -0700</pubDate>
      <author>nobody@smugmug.com (Vandana)</author>
      <guid isPermaLink=""false"">http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Th-13.jpg</guid>
      <exif:DateTimeOriginal>2007-08-16 19:03:58</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Ti-13.jpg"" fileSize=""7675"" type=""image/jpeg"" medium=""image"" width=""100"" height=""71"">
          <media:hash algo=""md5"">7523034d4d494d612dbbd80539c8ddf3</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Th-13.jpg"" fileSize=""11185"" type=""image/jpeg"" medium=""image"" width=""150"" height=""107"" isDefault=""true"">
          <media:hash algo=""md5"">5a694c89887da241d9bb9c27ef8ac022</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-S-13.jpg"" fileSize=""36404"" type=""image/jpeg"" medium=""image"" width=""400"" height=""284"">
          <media:hash algo=""md5"">f9bb42fbe09ed660d2c71074bdaee11e</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-M-13.jpg"" fileSize=""59338"" type=""image/jpeg"" medium=""image"" width=""600"" height=""426"">
          <media:hash algo=""md5"">871b31018ed17a51c9a44a99032ca68a</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-L-13.jpg"" fileSize=""88549"" type=""image/jpeg"" medium=""image"" width=""800"" height=""569"">
          <media:hash algo=""md5"">fed50a88b056aa062819f21fb74d0d11</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-XL-13.jpg"" fileSize=""127980"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""728"">
          <media:hash algo=""md5"">791461cb9659af73abfd2445ea8e0acc</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-X2-13.jpg"" fileSize=""185742"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""910"">
          <media:hash algo=""md5"">de9c8ec961c8d81dcbf7dc14f4f11ce4</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-X3-13.jpg"" fileSize=""278964"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1137"">
          <media:hash algo=""md5"">51cc41a140767058f080f63366c95560</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/3245654_CMt3kV#!i=184727446&amp;k=JfNYM"" title=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Th-13.jpg"" width=""150"" height=""107"" alt=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY"" title=""August 16, 2007 - Self-portrait in the Clovis sunset. 

This picture was chosen as Kodak Picture of the day (KPOTD) for May 22, 2010 and featured at Times Square, NY"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://vandana.smugmug.com/Photography/Photo-a-day/August-2007/184727446-O/184727446_JfNYM-Th-13.jpg"" width=""150"" height=""107""/>
      <media:category>Photography</media:category>
      <media:keywords>sunset, silhouette, selfportrait, portrait, vandana, clovis, walmart, dandelion, sunflower, silhouettes, kpotd, new mexico, nm, daily, dailies, dandelion sunset, light</media:keywords>
      <media:copyright url=""http://www.vandanaphotography.com"">Vandana</media:copyright>
      <media:credit role=""photographer"">Vandana</media:credit>
    </item>
    <item>
      <title>This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ...</title>
      <link>http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/804818_BND7Jt#!i=83779304&amp;k=kx52v</link>
      <description>&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
&lt;FONT COLOR=""GREEN""&gt;&lt;h5&gt;&lt;strong&gt; Second place winner of the 2007 National Parks Photo Contest sponsored by Canon.&lt;/h5&gt;&lt;/strong&gt;&lt;/FONT COLOR=""GREEEN""&gt; &lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/804818_BND7Jt#!i=83779304&amp;k=kx52v"" title=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Th-3.jpg"" width=""150"" height=""120"" alt=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ..."" title=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Mon, 24 Jul 2006 18:06:29 -0700</pubDate>
      <author>nobody@smugmug.com (Ken Conger)</author>
      <guid isPermaLink=""false"">http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2006-07-21 19:45:56</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Ti-3.jpg"" fileSize=""4638"" type=""image/jpeg"" medium=""image"" width=""100"" height=""80"">
          <media:hash algo=""md5"">6b233263618f4a0ac820843b083dcca1</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Th-3.jpg"" fileSize=""9175"" type=""image/jpeg"" medium=""image"" width=""150"" height=""120"" isDefault=""true"">
          <media:hash algo=""md5"">0639130ac21c1362a3a6c518c6e0644f</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-S-3.jpg"" fileSize=""53889"" type=""image/jpeg"" medium=""image"" width=""375"" height=""300"">
          <media:hash algo=""md5"">ec6611f7d97c903ee799137467ba16a7</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-M-3.jpg"" fileSize=""113809"" type=""image/jpeg"" medium=""image"" width=""563"" height=""450"">
          <media:hash algo=""md5"">db9572d6f3ebc708e4f03378938f3f07</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
&lt;FONT COLOR=""GREEN""&gt;&lt;h5&gt;&lt;strong&gt; Second place winner of the 2007 National Parks Photo Contest sponsored by Canon.&lt;/h5&gt;&lt;/strong&gt;&lt;/FONT COLOR=""GREEEN""&gt; &lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/804818_BND7Jt#!i=83779304&amp;k=kx52v"" title=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Th-3.jpg"" width=""150"" height=""120"" alt=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ..."" title=""This photograph of a Brown Bear catching a salmon was captured at Katmai National Park (7/06).
 Second place winner of the 2007 National Parks Photo Contest sponsored by Canon. This photogra ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://kenconger.smugmug.com/Nature/Brown-Bear-Gallery/Catch-Great/83779304_kx52v-Th-3.jpg"" width=""150"" height=""120""/>
      <media:category>Nature</media:category>
      <media:keywords>grizzly, grizzly bear, katmai, brown bear</media:keywords>
      <media:copyright url=""http://www.kencongerphotography.com"">Ken Conger</media:copyright>
      <media:credit role=""photographer"">Ken Conger</media:credit>
    </item>
    <item>
      <title>Balluminaria, Balloon Glow</title>
      <link>http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=113056244&amp;k=L6TbS</link>
      <description>&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;Balluminaria, Balloon Glow&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=113056244&amp;k=L6TbS"" title=""Balluminaria, Balloon Glow""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Th-7.jpg"" width=""150"" height=""108"" alt=""Balluminaria, Balloon Glow"" title=""Balluminaria, Balloon Glow"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sun, 26 Nov 2006 17:16:05 -0800</pubDate>
      <author>nobody@smugmug.com (Eos-Muller)</author>
      <guid isPermaLink=""false"">http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Th-7.jpg</guid>
      <exif:DateTimeOriginal>2006-11-25 17:48:47</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Ti-7.jpg"" fileSize=""4903"" type=""image/jpeg"" medium=""image"" width=""100"" height=""72"">
          <media:hash algo=""md5"">4b00a4dec7d0d0ef33bded6aef5d73ea</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Th-7.jpg"" fileSize=""8550"" type=""image/jpeg"" medium=""image"" width=""150"" height=""108"" isDefault=""true"">
          <media:hash algo=""md5"">e90f5690d700372564c15100c0829055</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-S-7.jpg"" fileSize=""39699"" type=""image/jpeg"" medium=""image"" width=""400"" height=""289"">
          <media:hash algo=""md5"">972b6793b5639b3a2f559238f7bb0c3f</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-M-7.jpg"" fileSize=""71057"" type=""image/jpeg"" medium=""image"" width=""600"" height=""434"">
          <media:hash algo=""md5"">d4c0e7dfa9c9e7181c2be420bac705f8</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-L-7.jpg"" fileSize=""108814"" type=""image/jpeg"" medium=""image"" width=""800"" height=""578"">
          <media:hash algo=""md5"">b49e975ff4041aee0a49d90cb8d3e4fc</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-XL-7.jpg"" fileSize=""158894"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""740"">
          <media:hash algo=""md5"">a0442ac4247b7939d7a072b127387ffb</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Balluminaria, Balloon Glow</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;Balluminaria, Balloon Glow&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=113056244&amp;k=L6TbS"" title=""Balluminaria, Balloon Glow""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Th-7.jpg"" width=""150"" height=""108"" alt=""Balluminaria, Balloon Glow"" title=""Balluminaria, Balloon Glow"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C2517/113056244_L6TbS-Th-7.jpg"" width=""150"" height=""108""/>
      <media:category>Portfolio</media:category>
      <media:keywords>balloon glow, eden park, cincinnati, ohio, balluminaria, hotair</media:keywords>
      <media:copyright url=""http://eos-muller.smugmug.com"">Eos-Muller</media:copyright>
      <media:credit role=""photographer"">Eos-Muller</media:credit>
    </item>
    <item>
      <title>Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.</title>
      <link>http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/2283684_rQd7Kz#!i=119323964&amp;k=Vzmn6</link>
      <description>&lt;p&gt;&lt;a href=""http://alloutdoor.smugmug.com""&gt;Mike Reid&lt;/a&gt;&lt;br /&gt;Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/2283684_rQd7Kz#!i=119323964&amp;k=Vzmn6"" title=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.""&gt;&lt;img src=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Th.jpg"" width=""150"" height=""95"" alt=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise."" title=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Wed, 27 Dec 2006 18:57:15 -0800</pubDate>
      <author>nobody@smugmug.com (Mike Reid)</author>
      <guid isPermaLink=""false"">http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Th.jpg</guid>
      <exif:DateTimeOriginal>2006-11-24 19:29:39</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Ti.jpg"" fileSize=""3197"" type=""image/jpeg"" medium=""image"" width=""100"" height=""64"">
          <media:hash algo=""md5"">c72cc12e56bb789ad5843ef139e6618b</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Th.jpg"" fileSize=""5743"" type=""image/jpeg"" medium=""image"" width=""150"" height=""95"" isDefault=""true"">
          <media:hash algo=""md5"">36980b2e5ef87f8d3214fd849c81779f</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-S.jpg"" fileSize=""39138"" type=""image/jpeg"" medium=""image"" width=""400"" height=""254"">
          <media:hash algo=""md5"">6a1f5ab2801ec1d1d88cbcc7f7819397</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-M.jpg"" fileSize=""95984"" type=""image/jpeg"" medium=""image"" width=""600"" height=""381"">
          <media:hash algo=""md5"">a0338fd9a4ec93fe957ebdfa522ff477</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-L.jpg"" fileSize=""157692"" type=""image/jpeg"" medium=""image"" width=""800"" height=""509"">
          <media:hash algo=""md5"">c8dff2185fe790313addb6bc4e1bfe8f</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-XL.jpg"" fileSize=""240482"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""651"">
          <media:hash algo=""md5"">d97e844ae35bc18b8fb6c898f0162c1e</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-X2.jpg"" fileSize=""349223"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""814"">
          <media:hash algo=""md5"">f134a41d7e687fc9fbe255ff4f37bf33</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-X3.jpg"" fileSize=""491239"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1017"">
          <media:hash algo=""md5"">38f20b71fa5a7704c6ba0b9e5a12b0d6</media:hash>
        </media:content>
        <media:content url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-O.jpg"" fileSize=""1876886"" type=""image/jpeg"" medium=""image"" width=""3713"" height=""2360"">
          <media:hash algo=""md5"">5c0e1a3186cb1880bbd7a908c972a754</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://alloutdoor.smugmug.com""&gt;Mike Reid&lt;/a&gt;&lt;br /&gt;Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/2283684_rQd7Kz#!i=119323964&amp;k=Vzmn6"" title=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise.""&gt;&lt;img src=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Th.jpg"" width=""150"" height=""95"" alt=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise."" title=""Arch Light. Photo of Mesa Arch at Canyonlands National Park in Utah.&#13;
Photo by Mike Reid, All Outdoor Photography Boise."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://alloutdoor.smugmug.com/Landscapes/High-Resolution-Utah-Red-Rock/jtDSC0525/119323964_Vzmn6-Th.jpg"" width=""150"" height=""95""/>
      <media:category>Landscapes</media:category>
      <media:keywords>mesa, arch, canyonlands, national, park, utah, original, sunriseglow, sunburst, starburst, scenery, landscape</media:keywords>
      <media:copyright url=""http://alloutdoor.smugmug.com"">Mike Reid</media:copyright>
      <media:credit role=""photographer"">Mike Reid</media:credit>
    </item>
    <item>
      <title>Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..</title>
      <link>http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/2303040_cxx6tt#!i=126191736&amp;k=Krvvv</link>
      <description>&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/2303040_cxx6tt#!i=126191736&amp;k=Krvvv"" title=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Th-4.jpg"" width=""150"" height=""112"" alt=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought.."" title=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought.."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Mon, 29 Jan 2007 05:34:28 -0800</pubDate>
      <author>nobody@smugmug.com (Vandana)</author>
      <guid isPermaLink=""false"">http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2007-01-28 14:43:19</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Ti-4.jpg"" fileSize=""5477"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">552eda73d492996ef32c8cfe06da2722</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Th-4.jpg"" fileSize=""7294"" type=""image/jpeg"" medium=""image"" width=""150"" height=""112"" isDefault=""true"">
          <media:hash algo=""md5"">d2bb989710e9b2f014893c125360d5fb</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-S-4.jpg"" fileSize=""25158"" type=""image/jpeg"" medium=""image"" width=""400"" height=""299"">
          <media:hash algo=""md5"">d6a6673156eae1642cda13d125fba62e</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-M-4.jpg"" fileSize=""41727"" type=""image/jpeg"" medium=""image"" width=""600"" height=""449"">
          <media:hash algo=""md5"">578dbdaf11610b8a54f133c300053473</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-L-4.jpg"" fileSize=""61613"" type=""image/jpeg"" medium=""image"" width=""800"" height=""599"">
          <media:hash algo=""md5"">69d4b4a11878704a709a4fd7ecb06bcb</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-XL-4.jpg"" fileSize=""89000"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""766"">
          <media:hash algo=""md5"">20d8187615f6bb4e5a58fe1c309f7175</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-X2-4.jpg"" fileSize=""125675"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""958"">
          <media:hash algo=""md5"">968fbff2c37b98d8c4617b8e77712a56</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/2303040_cxx6tt#!i=126191736&amp;k=Krvvv"" title=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought..""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Th-4.jpg"" width=""150"" height=""112"" alt=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought.."" title=""Jan 28, 2007 - Mallard in flight&#13;
&#13;
the closest I have gotten, quite excited..I have a few more to process from yesterday but smug was down I thought.."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://vandana.smugmug.com/Photography/Photo-a-day/January-2007/DSC8253-copy/126191736_Krvvv-Th-4.jpg"" width=""150"" height=""112""/>
      <media:category>Photography</media:category>
      <media:keywords>mallard, mallard in flight, flight, daily photos, january 2007, bif, bird in flight, bird, birds, mallards, new mexico, flying</media:keywords>
      <media:copyright url=""http://www.vandanaphotography.com"">Vandana</media:copyright>
      <media:credit role=""photographer"">Vandana</media:credit>
    </item>
    <item>
      <title>lordv's photo</title>
      <link>http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120917883&amp;k=hsJdE</link>
      <description>&lt;p&gt;&lt;a href=""http://lordv.smugmug.com""&gt;lordv&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120917883&amp;k=hsJdE"" title=""lordv's photo""&gt;&lt;img src=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Th.jpg"" width=""150"" height=""104"" alt=""lordv's photo"" title=""lordv's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Macrophotography</category>
      <pubDate>Thu, 04 Jan 2007 00:55:14 -0800</pubDate>
      <author>nobody@smugmug.com (lordv)</author>
      <guid isPermaLink=""false"">http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Th.jpg</guid>
      <media:group>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Ti.jpg"" fileSize=""2536"" type=""image/jpeg"" medium=""image"" width=""100"" height=""70"">
          <media:hash algo=""md5"">228f921c76efdd1cacc8e06900dc4149</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Th.jpg"" fileSize=""3853"" type=""image/jpeg"" medium=""image"" width=""150"" height=""104"" isDefault=""true"">
          <media:hash algo=""md5"">077f60ceb03a294d8d80c5f2f3ce6ab1</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-S.jpg"" fileSize=""18512"" type=""image/jpeg"" medium=""image"" width=""400"" height=""278"">
          <media:hash algo=""md5"">90db6e8571f414efe0515c854a14dcbd</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-M.jpg"" fileSize=""43478"" type=""image/jpeg"" medium=""image"" width=""600"" height=""417"">
          <media:hash algo=""md5"">fffaf5d062d698883793586c318ff4cd</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">lordv's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://lordv.smugmug.com""&gt;lordv&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120917883&amp;k=hsJdE"" title=""lordv's photo""&gt;&lt;img src=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Th.jpg"" width=""150"" height=""104"" alt=""lordv's photo"" title=""lordv's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG3755sa/120917883_hsJdE-Th.jpg"" width=""150"" height=""104""/>
      <media:category>Macrophotography</media:category>
      <media:copyright url=""http://lordv.smugmug.com"">lordv</media:copyright>
      <media:credit role=""photographer"">lordv</media:credit>
    </item>
    <item>
      <title>Photo by Leping Zha. See photo in Leping's gallery</title>
      <link>http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/5363890_GxmcTJ#!i=329380088&amp;k=Xm3vx</link>
      <description>&lt;p&gt;&lt;a href=""http://cmac.smugmug.com""&gt;Chris MacAskill&lt;/a&gt;&lt;br /&gt;&lt;h2 class=""notopmargin""&gt;Photo by &lt;span class=""white""&gt;Leping Zha&lt;/span&gt;. See photo in Leping's &lt;a href=""http://lepingzha.smugmug.com/gallery/507003_WnwSh/1/20790845_b8UQB/Medium""&gt;gallery&lt;/a&gt;&lt;/h2&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/5363890_GxmcTJ#!i=329380088&amp;k=Xm3vx"" title=""Photo by Leping Zha. See photo in Leping's gallery""&gt;&lt;img src=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Th-2.jpg"" width=""150"" height=""115"" alt=""Photo by Leping Zha. See photo in Leping's gallery"" title=""Photo by Leping Zha. See photo in Leping's gallery"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>SmugMug</category>
      <pubDate>Thu, 10 Jul 2008 12:44:52 -0700</pubDate>
      <author>nobody@smugmug.com (Chris MacAskill)</author>
      <guid isPermaLink=""false"">http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Th-2.jpg</guid>
      <media:group>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Ti-2.jpg"" fileSize=""5999"" type=""image/jpeg"" medium=""image"" width=""100"" height=""76"">
          <media:hash algo=""md5"">fc8b630f40a72cb9a993367100ef579d</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Th-2.jpg"" fileSize=""12100"" type=""image/jpeg"" medium=""image"" width=""150"" height=""115"" isDefault=""true"">
          <media:hash algo=""md5"">2597b0b4465d6267b038708e8cec2c2c</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-S-2.jpg"" fileSize=""79011"" type=""image/jpeg"" medium=""image"" width=""393"" height=""300"">
          <media:hash algo=""md5"">a82342168b3ee892dd3731cb797d257d</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-M-2.jpg"" fileSize=""179693"" type=""image/jpeg"" medium=""image"" width=""590"" height=""450"">
          <media:hash algo=""md5"">fd2f8078a074751b3a768429d7bc8a91</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-L-2.jpg"" fileSize=""330400"" type=""image/jpeg"" medium=""image"" width=""786"" height=""600"">
          <media:hash algo=""md5"">1612b185271290681f0cd4b20f2ddc2c</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-XL-2.jpg"" fileSize=""557784"" type=""image/jpeg"" medium=""image"" width=""1006"" height=""768"">
          <media:hash algo=""md5"">e211f6f2e7d72d472611efab337c9bca</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-X2-2.jpg"" fileSize=""873927"" type=""image/jpeg"" medium=""image"" width=""1258"" height=""960"">
          <media:hash algo=""md5"">deee00ad5876948100d723f9cdb3ea66</media:hash>
        </media:content>
        <media:content url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-X3-2.jpg"" fileSize=""1338499"" type=""image/jpeg"" medium=""image"" width=""1572"" height=""1200"">
          <media:hash algo=""md5"">450f63b8d2b6ab9bbd4d4a0edbdc8a92</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Photo by Leping Zha. See photo in Leping's gallery</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://cmac.smugmug.com""&gt;Chris MacAskill&lt;/a&gt;&lt;br /&gt;&lt;h2 class=""notopmargin""&gt;Photo by &lt;span class=""white""&gt;Leping Zha&lt;/span&gt;. See photo in Leping's &lt;a href=""http://lepingzha.smugmug.com/gallery/507003_WnwSh/1/20790845_b8UQB/Medium""&gt;gallery&lt;/a&gt;&lt;/h2&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/5363890_GxmcTJ#!i=329380088&amp;k=Xm3vx"" title=""Photo by Leping Zha. See photo in Leping's gallery""&gt;&lt;img src=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Th-2.jpg"" width=""150"" height=""115"" alt=""Photo by Leping Zha. See photo in Leping's gallery"" title=""Photo by Leping Zha. See photo in Leping's gallery"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://cmac.smugmug.com/SmugMug/Marketing-shots/SmugMug-homepage-slide-show/Leping-Zha/329380088_Xm3vx-Th-2.jpg"" width=""150"" height=""115""/>
      <media:category>SmugMug</media:category>
      <media:keywords>leping, zha</media:keywords>
      <media:copyright url=""http://cmac.smugmug.com"">Chris MacAskill</media:copyright>
      <media:credit role=""photographer"">Chris MacAskill</media:credit>
    </item>
    <item>
      <title>Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ...</title>
      <link>http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=476011624&amp;k=efKkk</link>
      <description>&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Canyon is one I recently rediscovered in my archives from the early fall and reprocessed.  I used a number of different effects, including Topaz Adjust, Color EFEX Pro, and some regular Photoshop filter and blending effects.  If you view the large size, you will really see some detail!  I'm going to post this and go to bed.  I hope you all have a really nice day!  It is supposed to be quite warm here today.  It got up to 65 degrees in Colorado Springs today, not a good day to sleep away.  Oh. well.  Oh, if some of you should like to look at the other North Rim Grand Canyon shots, they are here:  http://fotoeffects.smugmug.com/gallery/6698528_KNq6d#476011624_efKkk.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=476011624&amp;k=efKkk"" title=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ...""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Th.jpg"" width=""150"" height=""102"" alt=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ..."" title=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily shots for the dailies forum on Dgrin</category>
      <pubDate>Tue, 17 Feb 2009 22:26:52 -0800</pubDate>
      <author>nobody@smugmug.com (fotoeffects)</author>
      <guid isPermaLink=""false"">http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-09-11 07:36:14</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Ti.jpg"" fileSize=""5140"" type=""image/jpeg"" medium=""image"" width=""100"" height=""68"">
          <media:hash algo=""md5"">881632e7781851b87b88d5e5439f0c36</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Th.jpg"" fileSize=""9527"" type=""image/jpeg"" medium=""image"" width=""150"" height=""102"" isDefault=""true"">
          <media:hash algo=""md5"">5ca17174721db8568eb2541bf8e830ee</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-S.jpg"" fileSize=""50548"" type=""image/jpeg"" medium=""image"" width=""400"" height=""273"">
          <media:hash algo=""md5"">5692c397c432aa516820c10f83ff1b9a</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-M.jpg"" fileSize=""96585"" type=""image/jpeg"" medium=""image"" width=""600"" height=""409"">
          <media:hash algo=""md5"">075048cbb68e1950d972d23e794f49a5</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-L.jpg"" fileSize=""157082"" type=""image/jpeg"" medium=""image"" width=""800"" height=""546"">
          <media:hash algo=""md5"">9b63f564872d046154672e49fae71bfb</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-XL.jpg"" fileSize=""238923"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""698"">
          <media:hash algo=""md5"">433127c1f89c097e3bcd74edd3c2c722</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-X2.jpg"" fileSize=""333998"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""873"">
          <media:hash algo=""md5"">b7a2e173eb2a609acec24fd82ee8377f</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-X3.jpg"" fileSize=""479629"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1091"">
          <media:hash algo=""md5"">6eaa1535dc266c8d65bd0bb461b75a4f</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Canyon is one I recently rediscovered in my archives from the early fall and reprocessed.  I used a number of different effects, including Topaz Adjust, Color EFEX Pro, and some regular Photoshop filter and blending effects.  If you view the large size, you will really see some detail!  I'm going to post this and go to bed.  I hope you all have a really nice day!  It is supposed to be quite warm here today.  It got up to 65 degrees in Colorado Springs today, not a good day to sleep away.  Oh. well.  Oh, if some of you should like to look at the other North Rim Grand Canyon shots, they are here:  http://fotoeffects.smugmug.com/gallery/6698528_KNq6d#476011624_efKkk.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=476011624&amp;k=efKkk"" title=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ...""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Th.jpg"" width=""150"" height=""102"" alt=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ..."" title=""Day 132 - The morning sun strikes rock formations in the Grand Canyon, as seen from the North Rim in Arizona; detail in this image is best viewed in a larger size.  This shot of the Grand Ca ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC7086b/476011624_efKkk-Th.jpg"" width=""150"" height=""102""/>
      <media:category>Daily shots for the dailies forum on Dgrin</media:category>
      <media:keywords>sun, morning, rock formation, sandstone, rocks, formations, nature, landscape, canyon, grand canyon, arizona, north rim, north, rim, clouds, sky, sunrise</media:keywords>
      <media:copyright url=""http://fotoeffects.smugmug.com"">fotoeffects</media:copyright>
      <media:credit role=""photographer"">fotoeffects</media:credit>
    </item>
    <item>
      <title>Another fabulous Sunset
Wailea, Maui</title>
      <link>http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/5932407_ktZMKq#!i=391615904&amp;k=r5Z3s</link>
      <description>&lt;p&gt;&lt;a href=""http://mikewilde.smugmug.com""&gt;mikewilde&lt;/a&gt;&lt;br /&gt;Another fabulous Sunset
Wailea, Maui&lt;/p&gt;&lt;p&gt;&lt;a href=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/5932407_ktZMKq#!i=391615904&amp;k=r5Z3s"" title=""Another fabulous Sunset
Wailea, Maui""&gt;&lt;img src=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Th.jpg"" width=""150"" height=""100"" alt=""Another fabulous Sunset
Wailea, Maui"" title=""Another fabulous Sunset
Wailea, Maui"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sat, 11 Oct 2008 15:51:30 -0700</pubDate>
      <author>nobody@smugmug.com (mikewilde)</author>
      <guid isPermaLink=""false"">http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-09-27 22:25:43</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Ti.jpg"" fileSize=""5440"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">67e29b5566c93b54a0fc1f2b82d9e512</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Th.jpg"" fileSize=""10198"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">c9f38392cf18a6cf341609850e68e272</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-S.jpg"" fileSize=""59824"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">34706144f925ee906b68bb5895d41ef7</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-M.jpg"" fileSize=""120769"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">6d6913aaa457454d575dd23294dff44d</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-L.jpg"" fileSize=""210696"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">af05422b429f8ead5b434f33982460ee</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-XL.jpg"" fileSize=""336413"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""682"">
          <media:hash algo=""md5"">b3569dfb5ffb63699b9666dfaf67ced4</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-X2.jpg"" fileSize=""488249"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""853"">
          <media:hash algo=""md5"">39c08b586c68a49989c64ed9526e2965</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-X3.jpg"" fileSize=""744561"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1066"">
          <media:hash algo=""md5"">6395e6128579331f67a4b8bbb0585af0</media:hash>
        </media:content>
        <media:content url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-O.jpg"" fileSize=""11781975"" type=""image/jpeg"" medium=""image"" width=""4666"" height=""3110"">
          <media:hash algo=""md5"">212a0ac832bbab53fb3cb55408e135e5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Another fabulous Sunset
Wailea, Maui</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://mikewilde.smugmug.com""&gt;mikewilde&lt;/a&gt;&lt;br /&gt;Another fabulous Sunset
Wailea, Maui&lt;/p&gt;&lt;p&gt;&lt;a href=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/5932407_ktZMKq#!i=391615904&amp;k=r5Z3s"" title=""Another fabulous Sunset
Wailea, Maui""&gt;&lt;img src=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Th.jpg"" width=""150"" height=""100"" alt=""Another fabulous Sunset
Wailea, Maui"" title=""Another fabulous Sunset
Wailea, Maui"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://mikewilde.smugmug.com/Portfolio/Photographers-Favorites/Maui0621/391615904_r5Z3s-Th.jpg"" width=""150"" height=""100""/>
      <media:category>Portfolio</media:category>
      <media:keywords>maui, hawaii, seascape, ocean, sunset</media:keywords>
      <media:copyright url=""http://www.mbwilde.com"">mikewilde</media:copyright>
      <media:credit role=""photographer"">mikewilde</media:credit>
    </item>
    <item>
      <title>Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net</title>
      <link>http://hershy.smugmug.com/Photography/Daily-photos-2008/4836391_fKP23d#!i=293717285&amp;k=XpLSv</link>
      <description>&lt;p&gt;&lt;a href=""http://hershy.smugmug.com""&gt;Hershy&lt;/a&gt;&lt;br /&gt;Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net&lt;/p&gt;&lt;p&gt;&lt;a href=""http://hershy.smugmug.com/Photography/Daily-photos-2008/4836391_fKP23d#!i=293717285&amp;k=XpLSv"" title=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net""&gt;&lt;img src=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Th-1.jpg"" width=""150"" height=""100"" alt=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net"" title=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Sun, 11 May 2008 20:51:14 -0700</pubDate>
      <author>nobody@smugmug.com (Hershy)</author>
      <guid isPermaLink=""false"">http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-02-25 11:48:26</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Ti-1.jpg"" fileSize=""5625"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">164d14ec165cad921fff2e400732fa5e</media:hash>
        </media:content>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Th-1.jpg"" fileSize=""9659"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">689e45f8d202ccb959327ebfb4b94919</media:hash>
        </media:content>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-S-1.jpg"" fileSize=""46733"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">dedd538f05ef1e5928c2a3ea24e4a016</media:hash>
        </media:content>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-M-1.jpg"" fileSize=""84679"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">e4cbec90cc2c3e5caf35ae6091df0fc2</media:hash>
        </media:content>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-L-1.jpg"" fileSize=""132673"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">b5e4adc196c54239ecb2ad2ae4ce2374</media:hash>
        </media:content>
        <media:content url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-XL-1.jpg"" fileSize=""190029"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">7eba9534566a5bd96d40da115aabfd2f</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://hershy.smugmug.com""&gt;Hershy&lt;/a&gt;&lt;br /&gt;Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net&lt;/p&gt;&lt;p&gt;&lt;a href=""http://hershy.smugmug.com/Photography/Daily-photos-2008/4836391_fKP23d#!i=293717285&amp;k=XpLSv"" title=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net""&gt;&lt;img src=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Th-1.jpg"" width=""150"" height=""100"" alt=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net"" title=""Soaking it in.&#13;
Sunflower shot at my cousin's garden in Ahmedabad. &#13;
&#13;
www.javeri.net"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://hershy.smugmug.com/Photography/Daily-photos-2008/DSC2897/293717285_XpLSv-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Photography</media:category>
      <media:keywords>flowers, garden, nature</media:keywords>
      <media:copyright url=""http://hershy.smugmug.com"">Hershy</media:copyright>
      <media:credit role=""photographer"">Hershy</media:credit>
    </item>
    <item>
      <title>Upper North Falls, Silver Falls State Park</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402485109&amp;k=nsK6E</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Upper North Falls, Silver Falls State Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402485109&amp;k=nsK6E"" title=""Upper North Falls, Silver Falls State Park""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Th-4.jpg"" width=""100"" height=""150"" alt=""Upper North Falls, Silver Falls State Park"" title=""Upper North Falls, Silver Falls State Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sat, 25 Oct 2008 22:19:20 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-10-23 18:13:10</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Ti-4.jpg"" fileSize=""3955"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">47c05f5ae1390e0986cfab390e0cecd5</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Th-4.jpg"" fileSize=""6726"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">bd6230d50771c741932d6d6feaebbc1d</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-S-4.jpg"" fileSize=""24688"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">b0e9408be00a18f31776192307c5edd8</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-M-4.jpg"" fileSize=""46562"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">df9e4fca086c56472b644c45607d637d</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-L-4.jpg"" fileSize=""74720"" type=""image/jpeg"" medium=""image"" width=""401"" height=""600"">
          <media:hash algo=""md5"">161e0686a1992051639cd371ddfbe7ec</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Upper North Falls, Silver Falls State Park</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Upper North Falls, Silver Falls State Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402485109&amp;k=nsK6E"" title=""Upper North Falls, Silver Falls State Park""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Th-4.jpg"" width=""100"" height=""150"" alt=""Upper North Falls, Silver Falls State Park"" title=""Upper North Falls, Silver Falls State Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1657/402485109_nsK6E-Th-4.jpg"" width=""100"" height=""150""/>
      <media:category>Other</media:category>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Baldy10.jpg</title>
      <link>http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=141742901&amp;k=t4fK5</link>
      <description>&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com""&gt;Jody Melanson (jmelanson)&lt;/a&gt;&lt;br /&gt;Baldy10.jpg&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=141742901&amp;k=t4fK5"" title=""Baldy10.jpg""&gt;&lt;img src=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Th-1.jpg"" width=""150"" height=""100"" alt=""Baldy10.jpg"" title=""Baldy10.jpg"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Newest Shots</category>
      <pubDate>Sat, 07 Apr 2007 05:03:59 -0700</pubDate>
      <author>nobody@smugmug.com (Jody Melanson (jmelanson))</author>
      <guid isPermaLink=""false"">http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2006-12-29 22:46:01</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Ti-1.jpg"" fileSize=""6918"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">9e19dc8e7002160e06820974d4d7ce19</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Th-1.jpg"" fileSize=""9571"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">581b1b5d274f222a897c83ec8d35290b</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-S-1.jpg"" fileSize=""33712"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">6be197270ec362ff0f8cdeded5b0eb42</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-M-1.jpg"" fileSize=""57041"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">181c2edc6a5e2990222c7d14199b5c0e</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-L-1.jpg"" fileSize=""81767"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">fcf547939b5795dd360710c1db13594c</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-XL-1.jpg"" type=""image/jpeg"" medium=""image"" width=""750"" height=""500""/>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-O-1.jpg"" fileSize=""117113"" type=""image/jpeg"" medium=""image"" width=""750"" height=""500"">
          <media:hash algo=""md5"">60e26a149ebd6a08ef3c25c0baa1e08c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Baldy10.jpg</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com""&gt;Jody Melanson (jmelanson)&lt;/a&gt;&lt;br /&gt;Baldy10.jpg&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=141742901&amp;k=t4fK5"" title=""Baldy10.jpg""&gt;&lt;img src=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Th-1.jpg"" width=""150"" height=""100"" alt=""Baldy10.jpg"" title=""Baldy10.jpg"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/Baldy10/141742901_t4fK5-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Newest Shots</media:category>
      <media:keywords>baldy</media:keywords>
      <media:copyright url=""http://jmelanson.smugmug.com"">Jody Melanson (jmelanson)</media:copyright>
      <media:credit role=""photographer"">Jody Melanson (jmelanson)</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Animals/9688385_Ts48xD#!i=799684755&amp;k=vpf45</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Animals/9688385_Ts48xD#!i=799684755&amp;k=vpf45"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Th-2.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sun, 28 Feb 2010 22:18:21 -0800</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2010-02-24 15:07:32</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Ti-2.jpg"" fileSize=""3920"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">b6290b8470f5071fe3d3a7ebaf727bdc</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Th-2.jpg"" fileSize=""6639"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">2fdffe5cad1e17a8f16b5cd10f2d526b</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-S-2.jpg"" fileSize=""36145"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">2542d764e0264f6e2a917a62b4372126</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-M-2.jpg"" fileSize=""69598"" type=""image/jpeg"" medium=""image"" width=""600"" height=""401"">
          <media:hash algo=""md5"">8aed5b690adb2a9454282610b01572d2</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-L-2.jpg"" fileSize=""111351"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">0a1e399710b9fca32636455108b2fd9a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Animals/9688385_Ts48xD#!i=799684755&amp;k=vpf45"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Th-2.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Animals/IMG1790b/799684755_vpf45-Th-2.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast</title>
      <link>http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/838223_vvtv2R#!i=38652136&amp;k=8RPjh</link>
      <description>&lt;p&gt;&lt;a href=""http://foto.smugmug.com""&gt;Daniela Caneschi (foto)&lt;/a&gt;&lt;br /&gt;My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast&lt;/p&gt;&lt;p&gt;&lt;a href=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/838223_vvtv2R#!i=38652136&amp;k=8RPjh"" title=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast""&gt;&lt;img src=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Th-1.jpg"" width=""150"" height=""113"" alt=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast"" title=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Flowers</category>
      <pubDate>Tue, 04 Oct 2005 07:01:48 -0700</pubDate>
      <author>nobody@smugmug.com (Daniela Caneschi (foto))</author>
      <guid isPermaLink=""false"">http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Th-1.jpg</guid>
      <media:group>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Ti-1.jpg"" fileSize=""4964"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">0f28dd8bf1a659c0adffdf097439f17d</media:hash>
        </media:content>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Th-1.jpg"" fileSize=""9664"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">3aac304973667beeb5db216fefe86f10</media:hash>
        </media:content>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-S-1.jpg"" fileSize=""55134"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">ee9dac8f2227f020a00e12aacd1d4b2c</media:hash>
        </media:content>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-M-1.jpg"" fileSize=""105083"" type=""image/jpeg"" medium=""image"" width=""600"" height=""450"">
          <media:hash algo=""md5"">a7508a6ac28ce6b3ac27817626f25403</media:hash>
        </media:content>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-L-1.jpg"" fileSize=""134842"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">e33e4fdb1e742af114895b95436681ef</media:hash>
        </media:content>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-XL-1.jpg"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600""/>
        <media:content url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-O-1.jpg"" fileSize=""110747"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">74074ab730b529a616607007ad319854</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://foto.smugmug.com""&gt;Daniela Caneschi (foto)&lt;/a&gt;&lt;br /&gt;My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast&lt;/p&gt;&lt;p&gt;&lt;a href=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/838223_vvtv2R#!i=38652136&amp;k=8RPjh"" title=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast""&gt;&lt;img src=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Th-1.jpg"" width=""150"" height=""113"" alt=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast"" title=""My blue dandellion - 
Note: any manipulation with photoshop except the regulation of the contrast"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://foto.smugmug.com/Flowers/In-my-Garden-other-flowers/In-my-Garden/soffione/38652136_8RPjh-Th-1.jpg"" width=""150"" height=""113""/>
      <media:category>Flowers</media:category>
      <media:keywords>nature, flowers, blue, wind</media:keywords>
      <media:copyright url=""http://foto.smugmug.com"">Daniela Caneschi (foto)</media:copyright>
      <media:credit role=""photographer"">Daniela Caneschi (foto)</media:credit>
    </item>
    <item>
      <title>Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.</title>
      <link>http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=121326255&amp;k=Ff76c</link>
      <description>&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=121326255&amp;k=Ff76c"" title=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Th-2.jpg"" width=""150"" height=""101"" alt=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning."" title=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Sat, 06 Jan 2007 00:42:16 -0800</pubDate>
      <author>nobody@smugmug.com (winchester)</author>
      <guid isPermaLink=""false"">http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2006-07-01 14:18:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Ti-2.jpg"" fileSize=""7190"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">21db63b3f90b912d23f5894173e20e6b</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Th-2.jpg"" fileSize=""10800"" type=""image/jpeg"" medium=""image"" width=""150"" height=""101"" isDefault=""true"">
          <media:hash algo=""md5"">217765c9f2d154efc4d9c3ed64b5e4ec</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-S-2.jpg"" fileSize=""45232"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">9b24fc7d6b53977fdc95e64b1a59165e</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-M-2.jpg"" fileSize=""83903"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">6b9105624640b5df7ed1df7911f62582</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-L-2.jpg"" fileSize=""133675"" type=""image/jpeg"" medium=""image"" width=""800"" height=""536"">
          <media:hash algo=""md5"">ac761179cc856e59ad0c05e4fde1c189</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-XL-2.jpg"" fileSize=""271337"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""677"">
          <media:hash algo=""md5"">9d9fc6eea2c63ccc26815258248d1bd3</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-O-2.jpg"" fileSize=""271337"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""677"">
          <media:hash algo=""md5"">9d9fc6eea2c63ccc26815258248d1bd3</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Alaska-2006/1618300_8MDZXh#!i=121326255&amp;k=Ff76c"" title=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Th-2.jpg"" width=""150"" height=""101"" alt=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning."" title=""Even six months after my visit, the Alaskan Range still fascinates me. I look forward to returning."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://winchester.smugmug.com/Landscapes/Alaska-2006/IMG1693/121326255_Ff76c-Th-2.jpg"" width=""150"" height=""101""/>
      <media:category>Landscapes</media:category>
      <media:copyright url=""http://winchester.smugmug.com"">winchester</media:copyright>
      <media:credit role=""photographer"">winchester</media:credit>
    </item>
    <item>
      <title>I LOVED the lighting with this one!  It was probably my favorite to play with.</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146824691&amp;k=ZfG3U</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;I LOVED the lighting with this one!  It was probably my favorite to play with.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146824691&amp;k=ZfG3U"" title=""I LOVED the lighting with this one!  It was probably my favorite to play with.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Th-3.jpg"" width=""150"" height=""100"" alt=""I LOVED the lighting with this one!  It was probably my favorite to play with."" title=""I LOVED the lighting with this one!  It was probably my favorite to play with."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Wed, 25 Apr 2007 08:14:35 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2007-04-04 20:46:03</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Ti-3.jpg"" fileSize=""3390"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">66ebce6f1931afc14dd91fb1e75c9c5f</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Th-3.jpg"" fileSize=""5223"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">cb70f6b6d6bfbf9f9b0cb075642e17fc</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-S-3.jpg"" fileSize=""27062"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">0807cf342c6a28d2211315560fff28dc</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-M-3.jpg"" fileSize=""52279"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">f93694e7c538eb60a6859b12e8741715</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-L-3.jpg"" fileSize=""90210"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">35feff4a0f4a3a4e574b59b6551ccc1e</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">I LOVED the lighting with this one!  It was probably my favorite to play with.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;I LOVED the lighting with this one!  It was probably my favorite to play with.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146824691&amp;k=ZfG3U"" title=""I LOVED the lighting with this one!  It was probably my favorite to play with.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Th-3.jpg"" width=""150"" height=""100"" alt=""I LOVED the lighting with this one!  It was probably my favorite to play with."" title=""I LOVED the lighting with this one!  It was probably my favorite to play with."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7711/146824691_ZfG3U-Th-3.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>dance, ballroom, stage, theater, byu ballroom team</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=336380069&amp;k=X4CaN</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=336380069&amp;k=X4CaN"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Th-4.jpg"" width=""150"" height=""96"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Mon, 21 Jul 2008 23:26:50 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-07-20 14:22:01</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Ti-4.jpg"" fileSize=""5500"" type=""image/jpeg"" medium=""image"" width=""100"" height=""64"">
          <media:hash algo=""md5"">dabba321e392cac5256ba7d90c11d72f</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Th-4.jpg"" fileSize=""10448"" type=""image/jpeg"" medium=""image"" width=""150"" height=""96"" isDefault=""true"">
          <media:hash algo=""md5"">4246be0fcd81dca69c117ba8cb3007ee</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-S-4.jpg"" fileSize=""56221"" type=""image/jpeg"" medium=""image"" width=""400"" height=""256"">
          <media:hash algo=""md5"">340aee7614d9ec7d313225e4d91812cb</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-M-4.jpg"" fileSize=""103946"" type=""image/jpeg"" medium=""image"" width=""600"" height=""384"">
          <media:hash algo=""md5"">67fbb5128af96f9bcdf58cc2a01086e7</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-L-4.jpg"" fileSize=""163407"" type=""image/jpeg"" medium=""image"" width=""800"" height=""513"">
          <media:hash algo=""md5"">de9161585c81b2df8514f3487b86ff95</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=336380069&amp;k=X4CaN"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Th-4.jpg"" width=""150"" height=""96"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7364/336380069_X4CaN-Th-4.jpg"" width=""150"" height=""96""/>
      <media:category>Other</media:category>
      <media:keywords>pearls, jewelry</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!</title>
      <link>http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/802241_4hx3jt#!i=35673995&amp;k=uxyZi</link>
      <description>&lt;p&gt;&lt;a href=""http://atomicfish.smugmug.com""&gt;Jeff Kleber (atomicfish)&lt;/a&gt;&lt;br /&gt;Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/802241_4hx3jt#!i=35673995&amp;k=uxyZi"" title=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!""&gt;&lt;img src=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Th-2.jpg"" width=""150"" height=""120"" alt=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!"" title=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Competitions</category>
      <pubDate>Sun, 11 Sep 2005 19:36:34 -0700</pubDate>
      <author>nobody@smugmug.com (Jeff Kleber (atomicfish))</author>
      <guid isPermaLink=""false"">http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Th-2.jpg</guid>
      <media:group>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Ti-2.jpg"" fileSize=""5548"" type=""image/jpeg"" medium=""image"" width=""100"" height=""80"">
          <media:hash algo=""md5"">355b48260691723c5c91a8ac79417f6c</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Th-2.jpg"" fileSize=""9869"" type=""image/jpeg"" medium=""image"" width=""150"" height=""120"" isDefault=""true"">
          <media:hash algo=""md5"">742cd3cef5ee3d15c2f9f3eb54cf8b1c</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-S-2.jpg"" fileSize=""44143"" type=""image/jpeg"" medium=""image"" width=""375"" height=""300"">
          <media:hash algo=""md5"">c1233f7018cfe66e1dfb1461bb7071cc</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-M-2.jpg"" fileSize=""84528"" type=""image/jpeg"" medium=""image"" width=""563"" height=""450"">
          <media:hash algo=""md5"">41215b04b6d247d77f0662d9047f7fe5</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-L-2.jpg"" fileSize=""136594"" type=""image/jpeg"" medium=""image"" width=""750"" height=""600"">
          <media:hash algo=""md5"">e7dad2029367d9fc84ce38a8d5a294a7</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-XL-2.jpg"" fileSize=""207228"" type=""image/jpeg"" medium=""image"" width=""960"" height=""768"">
          <media:hash algo=""md5"">1254a4b6f82432fd59668c9e2f351cdd</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-X2-2.jpg"" fileSize=""632951"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""819"">
          <media:hash algo=""md5"">fee61650ea63a7c999d0dcffc96f8587</media:hash>
        </media:content>
        <media:content url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-O-2.jpg"" fileSize=""632951"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""819"">
          <media:hash algo=""md5"">fee61650ea63a7c999d0dcffc96f8587</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://atomicfish.smugmug.com""&gt;Jeff Kleber (atomicfish)&lt;/a&gt;&lt;br /&gt;Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/802241_4hx3jt#!i=35673995&amp;k=uxyZi"" title=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!""&gt;&lt;img src=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Th-2.jpg"" width=""150"" height=""120"" alt=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!"" title=""Red eye Tree Frog was entered in the Animals and Birds category and was given honorable mention!"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://atomicfish.smugmug.com/Competitions/2005-Western-Fair-Photo-1/P1060695-1/35673995_uxyZi-Th-2.jpg"" width=""150"" height=""120""/>
      <media:category>Competitions</media:category>
      <media:keywords>slide, frog, redeye, treefrog, tree, fav, tropical, rain, rainforest, panasonic, fz10, fz20, fz30, photo contest, green, amphibian, reptile, pet, forest, red, eye, agalychnis, callidryas, leaf, fz50, costa, rica, jungle, redeyed</media:keywords>
      <media:copyright url=""http://atomicfish.smugmug.com"">Jeff Kleber (atomicfish)</media:copyright>
      <media:credit role=""photographer"">Jeff Kleber (atomicfish)</media:credit>
    </item>
    <item>
      <title>A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.</title>
      <link>http://ken.smugmug.com/Birds/Upload/1877107_tV6hh6#!i=171888915&amp;k=3hvHe</link>
      <description>&lt;p&gt;&lt;a href=""http://ken.smugmug.com""&gt;Ken&lt;/a&gt;&lt;br /&gt;A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ken.smugmug.com/Birds/Upload/1877107_tV6hh6#!i=171888915&amp;k=3hvHe"" title=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.""&gt;&lt;img src=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Th-1.jpg"" width=""150"" height=""115"" alt=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007."" title=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Birds</category>
      <pubDate>Wed, 11 Jul 2007 07:20:04 -0700</pubDate>
      <author>nobody@smugmug.com (Ken)</author>
      <guid isPermaLink=""false"">http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2007-05-06 15:37:47</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Ti-1.jpg"" fileSize=""6017"" type=""image/jpeg"" medium=""image"" width=""100"" height=""77"">
          <media:hash algo=""md5"">5335343c9aef85c648139b622fa3e538</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Th-1.jpg"" fileSize=""10718"" type=""image/jpeg"" medium=""image"" width=""150"" height=""115"" isDefault=""true"">
          <media:hash algo=""md5"">a2dc41bd90e1de53a3aab2b8afa127e0</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-S-1.jpg"" fileSize=""45399"" type=""image/jpeg"" medium=""image"" width=""391"" height=""300"">
          <media:hash algo=""md5"">12b7252e3a6b224c90b5045f7655cfe9</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-M-1.jpg"" fileSize=""87854"" type=""image/jpeg"" medium=""image"" width=""587"" height=""450"">
          <media:hash algo=""md5"">3ad2d6337da0864a60749f7543c81636</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-L-1.jpg"" fileSize=""147487"" type=""image/jpeg"" medium=""image"" width=""782"" height=""600"">
          <media:hash algo=""md5"">41f64841f9c97bf7b0879bea46609797</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-XL-1.jpg"" fileSize=""222649"" type=""image/jpeg"" medium=""image"" width=""1001"" height=""768"">
          <media:hash algo=""md5"">3b9030ede8a341099748438b05f02da4</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-X2-1.jpg"" fileSize=""334888"" type=""image/jpeg"" medium=""image"" width=""1251"" height=""960"">
          <media:hash algo=""md5"">e944c78896ff8528a856a5cd6ad93894</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-X3-1.jpg"" fileSize=""499769"" type=""image/jpeg"" medium=""image"" width=""1564"" height=""1200"">
          <media:hash algo=""md5"">c1470dbb9f494f082b4ad312d12af90d</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-O-1.jpg"" fileSize=""764352"" type=""image/jpeg"" medium=""image"" width=""1889"" height=""1449"">
          <media:hash algo=""md5"">a58bd704ab2621a52d0b4ce7410da8e5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ken.smugmug.com""&gt;Ken&lt;/a&gt;&lt;br /&gt;A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ken.smugmug.com/Birds/Upload/1877107_tV6hh6#!i=171888915&amp;k=3hvHe"" title=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007.""&gt;&lt;img src=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Th-1.jpg"" width=""150"" height=""115"" alt=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007."" title=""A Western Tanager taken in a Silky Oak tree on Catalina Island in the spring of 2007."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ken.smugmug.com/Birds/Upload/Western-Tanager-5/171888915_3hvHe-Th-1.jpg"" width=""150"" height=""115""/>
      <media:category>Birds</media:category>
      <media:keywords>western, tanager</media:keywords>
      <media:copyright url=""http://ken.smugmug.com"">Ken</media:copyright>
      <media:credit role=""photographer"">Ken</media:credit>
    </item>
    <item>
      <title>June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ...</title>
      <link>http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/8404819_6nwJJS#!i=553456729&amp;k=SLinc</link>
      <description>&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com""&gt;JenniferDiFranco&lt;/a&gt;&lt;br /&gt;June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both recitals - taken from the finale, which was a tribute to the Beatles.  This is Emily's jazz team. All of the jazz teams dance together during the finale.&#13;
&#13;
If you want to see the video's here is a link&#13;
&#13;
First half - http://www.youtube.com/watch?v=Hkew8MXB_tA&#13;
&#13;
Second half - http://www.youtube.com/watch?v=Ugq5xp33_1s&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/8404819_6nwJJS#!i=553456729&amp;k=SLinc"" title=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ...""&gt;&lt;img src=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Th-2.jpg"" width=""150"" height=""100"" alt=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ..."" title=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Through My Eyes - Daily Photo</category>
      <pubDate>Tue, 02 Jun 2009 20:59:56 -0700</pubDate>
      <author>nobody@smugmug.com (JenniferDiFranco)</author>
      <guid isPermaLink=""false"">http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2009-05-24 07:14:59</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Ti-2.jpg"" fileSize=""2742"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">85c8bb47111cb336433f52240780affb</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Th-2.jpg"" fileSize=""4423"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">8beb4b567cdc3195f20184c048ecaf82</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-S-2.jpg"" fileSize=""21783"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">8dbf69cad2da5605e7c0c6abff6d960c</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-M-2.jpg"" fileSize=""38053"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">1b50091a03c259f61b08ca920260662c</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-L-2.jpg"" fileSize=""57710"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">8fa9e0ee93e33b071f2e93241cfbc693</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-XL-2.jpg"" fileSize=""83827"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""682"">
          <media:hash algo=""md5"">b5f49e3d868beed6a4f216a8d184fc54</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com""&gt;JenniferDiFranco&lt;/a&gt;&lt;br /&gt;June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both recitals - taken from the finale, which was a tribute to the Beatles.  This is Emily's jazz team. All of the jazz teams dance together during the finale.&#13;
&#13;
If you want to see the video's here is a link&#13;
&#13;
First half - http://www.youtube.com/watch?v=Hkew8MXB_tA&#13;
&#13;
Second half - http://www.youtube.com/watch?v=Ugq5xp33_1s&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/8404819_6nwJJS#!i=553456729&amp;k=SLinc"" title=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ...""&gt;&lt;img src=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Th-2.jpg"" width=""150"" height=""100"" alt=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ..."" title=""June 3, 2009  The last dance recital photo - I promise. I just finished processing and editing the final ones this afternoon.&#13;
&#13;
 This is the very last pose of the very last dance in both re ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-June-2009/IMG9968/553456729_SLinc-Th-2.jpg"" width=""150"" height=""100""/>
      <media:category>Through My Eyes - Daily Photo</media:category>
      <media:copyright url=""http://jenniferdifranco.smugmug.com"">JenniferDiFranco</media:copyright>
      <media:credit role=""photographer"">JenniferDiFranco</media:credit>
    </item>
    <item>
      <title>Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ...</title>
      <link>http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/2941917_HGmGjk#!i=221205054&amp;k=8qiFt</link>
      <description>&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from November 2008 through May 2009&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/2941917_HGmGjk#!i=221205054&amp;k=8qiFt"" title=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ...""&gt;&lt;img src=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Th-1.jpg"" width=""150"" height=""115"" alt=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ..."" title=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscape and Nature Photography</category>
      <pubDate>Tue, 13 Nov 2007 17:56:33 -0800</pubDate>
      <author>nobody@smugmug.com (Daniel Ewert)</author>
      <guid isPermaLink=""false"">http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2007-09-02 22:57:18</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Ti-1.jpg"" fileSize=""5923"" type=""image/jpeg"" medium=""image"" width=""100"" height=""77"">
          <media:hash algo=""md5"">c897148e6c2b69e7874d9fba1658380b</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Th-1.jpg"" fileSize=""11635"" type=""image/jpeg"" medium=""image"" width=""150"" height=""115"" isDefault=""true"">
          <media:hash algo=""md5"">07d5eff91c6a5f0889c33c5d9de57a64</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-S-1.jpg"" fileSize=""69675"" type=""image/jpeg"" medium=""image"" width=""392"" height=""300"">
          <media:hash algo=""md5"">a82cad219b0c09658b1771a250454b9c</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-M-1.jpg"" fileSize=""149751"" type=""image/jpeg"" medium=""image"" width=""587"" height=""450"">
          <media:hash algo=""md5"">28a074ae9e5154f0c579b85668fd537a</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-L-1.jpg"" fileSize=""251774"" type=""image/jpeg"" medium=""image"" width=""783"" height=""600"">
          <media:hash algo=""md5"">e98293b4166b60b9d810bd070fff3c18</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-XL-1.jpg"" fileSize=""287671"" type=""image/jpeg"" medium=""image"" width=""800"" height=""613"">
          <media:hash algo=""md5"">7d2081d5020aabc77f4b8cd9ba16f005</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-X2-1.jpg"" fileSize=""369547"" type=""image/jpeg"" medium=""image"" width=""800"" height=""613"">
          <media:hash algo=""md5"">766718ab0dd7f06bbdaa16f9bfbcdd64</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-O-1.jpg"" fileSize=""369547"" type=""image/jpeg"" medium=""image"" width=""800"" height=""613"">
          <media:hash algo=""md5"">766718ab0dd7f06bbdaa16f9bfbcdd64</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from November 2008 through May 2009&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/2941917_HGmGjk#!i=221205054&amp;k=8qiFt"" title=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ...""&gt;&lt;img src=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Th-1.jpg"" width=""150"" height=""115"" alt=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ..."" title=""Waterfalls in Paradise Meadow&#13;
*Landscape Category Winner of 2008 Nature's Best Photography Competition&#13;
*Displayed in Smithsonian National Museum of Natural History in Washington D.C. from  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Mountains-Forests-and-Trees/Myrtle4/221205054_8qiFt-Th-1.jpg"" width=""150"" height=""115""/>
      <media:category>Landscape and Nature Photography</media:category>
      <media:keywords>waterfall, stream, meadow, mountain, mt., rainier, national, park, flowers, wild, wildflowers, river, brook, wilderness, alpine, snow, clouds, flowing, water, trees, landscape, nature, art, pacific, northwest, north, west, washington, state, plants</media:keywords>
      <media:copyright url=""http://www.ewertnaturephotography.com"">Daniel Ewert</media:copyright>
      <media:credit role=""photographer"">Daniel Ewert</media:credit>
    </item>
    <item>
      <title>Ramona Falls</title>
      <link>http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/5867841_7M8rTg#!i=364446235&amp;k=UHtgN</link>
      <description>&lt;p&gt;&lt;a href=""http://godfactor.smugmug.com""&gt;Robert Resnick&lt;/a&gt;&lt;br /&gt;Ramona Falls&lt;/p&gt;&lt;p&gt;&lt;a href=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/5867841_7M8rTg#!i=364446235&amp;k=UHtgN"" title=""Ramona Falls""&gt;&lt;img src=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Th-4.jpg"" width=""150"" height=""100"" alt=""Ramona Falls"" title=""Ramona Falls"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Tue, 02 Sep 2008 17:40:32 -0700</pubDate>
      <author>nobody@smugmug.com (Robert Resnick)</author>
      <guid isPermaLink=""false"">http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-08-10 12:58:46</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Ti-4.jpg"" fileSize=""8161"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">36b531b12344290696b6e1bd0c7dd0ac</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Th-4.jpg"" fileSize=""12649"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">e1a1c94af52a0b639456c7fe76abad63</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-S-4.jpg"" fileSize=""56871"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">5a4998f1e76ebd4000ae558be6b605a8</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-M-4.jpg"" fileSize=""111224"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">3ee534dda5dc44fb77bb6911d259d413</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-L-4.jpg"" fileSize=""184652"" type=""image/jpeg"" medium=""image"" width=""800"" height=""536"">
          <media:hash algo=""md5"">0fc830700734672bff43aed302388f28</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-XL-4.jpg"" fileSize=""290924"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""685"">
          <media:hash algo=""md5"">78f43b81bb51d8c9e162b26ad6929a7a</media:hash>
        </media:content>
        <media:content url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-X2-4.jpg"" fileSize=""437621"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""857"">
          <media:hash algo=""md5"">ca3a8d4cfb742ed9365242ed25dde231</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Ramona Falls</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://godfactor.smugmug.com""&gt;Robert Resnick&lt;/a&gt;&lt;br /&gt;Ramona Falls&lt;/p&gt;&lt;p&gt;&lt;a href=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/5867841_7M8rTg#!i=364446235&amp;k=UHtgN"" title=""Ramona Falls""&gt;&lt;img src=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Th-4.jpg"" width=""150"" height=""100"" alt=""Ramona Falls"" title=""Ramona Falls"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://godfactor.smugmug.com/Landscapes/The-Beauty-of-Oregon/RamonaFallsDSF4187/364446235_UHtgN-Th-4.jpg"" width=""150"" height=""100""/>
      <media:category>Landscapes</media:category>
      <media:keywords>ramona, falls, oregon, water, waterfall, waterfalls, moss</media:keywords>
      <media:copyright url=""http://www.revelationsofdesign.com"">Robert Resnick</media:copyright>
      <media:credit role=""photographer"">Robert Resnick</media:credit>
    </item>
    <item>
      <title>Shot with a 400mm Beautiful Butterfly Posing</title>
      <link>http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/1682479_4HtC9K#!i=82553315&amp;k=pouGk</link>
      <description>&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;Shot with a 400mm Beautiful Butterfly Posing&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/1682479_4HtC9K#!i=82553315&amp;k=pouGk"" title=""Shot with a 400mm Beautiful Butterfly Posing""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Th-5.jpg"" width=""150"" height=""116"" alt=""Shot with a 400mm Beautiful Butterfly Posing"" title=""Shot with a 400mm Beautiful Butterfly Posing"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Tue, 18 Jul 2006 18:32:33 -0700</pubDate>
      <author>nobody@smugmug.com (Eos-Muller)</author>
      <guid isPermaLink=""false"">http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Th-5.jpg</guid>
      <exif:DateTimeOriginal>2006-07-18 14:25:51</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Ti-5.jpg"" fileSize=""7104"" type=""image/jpeg"" medium=""image"" width=""100"" height=""77"">
          <media:hash algo=""md5"">cfd7bc5e513ce7e8d3049c8f6d5ab916</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Th-5.jpg"" fileSize=""10545"" type=""image/jpeg"" medium=""image"" width=""150"" height=""116"" isDefault=""true"">
          <media:hash algo=""md5"">2e2041da8a1182a8262d9fae814a6a57</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-S-5.jpg"" fileSize=""40236"" type=""image/jpeg"" medium=""image"" width=""388"" height=""300"">
          <media:hash algo=""md5"">bcb033ec49d80729190e580ae848c0b2</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-M-5.jpg"" fileSize=""74414"" type=""image/jpeg"" medium=""image"" width=""582"" height=""450"">
          <media:hash algo=""md5"">ce8415a0389c73eacfe696298512e762</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-L-5.jpg"" fileSize=""120119"" type=""image/jpeg"" medium=""image"" width=""777"" height=""600"">
          <media:hash algo=""md5"">ff9245022b9167848a0d29f8274074de</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-XL-5.jpg"" fileSize=""185831"" type=""image/jpeg"" medium=""image"" width=""994"" height=""768"">
          <media:hash algo=""md5"">953af9956e75c7de393bcbcf107f64bb</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-X2-5.jpg"" fileSize=""278055"" type=""image/jpeg"" medium=""image"" width=""1242"" height=""960"">
          <media:hash algo=""md5"">25fbd7496bc4f653b5933dfbc00f2eed</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-X3-5.jpg"" fileSize=""412682"" type=""image/jpeg"" medium=""image"" width=""1553"" height=""1200"">
          <media:hash algo=""md5"">b89445ac0d89f3964ce54ef0e124a447</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Shot with a 400mm Beautiful Butterfly Posing</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;Shot with a 400mm Beautiful Butterfly Posing&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/1682479_4HtC9K#!i=82553315&amp;k=pouGk"" title=""Shot with a 400mm Beautiful Butterfly Posing""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Th-5.jpg"" width=""150"" height=""116"" alt=""Shot with a 400mm Beautiful Butterfly Posing"" title=""Shot with a 400mm Beautiful Butterfly Posing"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://eos-muller.smugmug.com/Portfolio/Nature/Butterflies/IMG8170/82553315_pouGk-Th-5.jpg"" width=""150"" height=""116""/>
      <media:category>Portfolio</media:category>
      <media:keywords>butterflies, posing, freelance, portrait, flight, beautiful, 500mm lens, cincinnati, ohio</media:keywords>
      <media:copyright url=""http://eos-muller.smugmug.com"">Eos-Muller</media:copyright>
      <media:credit role=""photographer"">Eos-Muller</media:credit>
    </item>
    <item>
      <title>UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001</title>
      <link>http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/1765247_Cx46Kj#!i=165972321&amp;k=zv2D4</link>
      <description>&lt;p&gt;&lt;a href=""http://rickwillis.smugmug.com""&gt;Rick Willis&lt;/a&gt;&lt;br /&gt;UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001&lt;/p&gt;&lt;p&gt;&lt;a href=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/1765247_Cx46Kj#!i=165972321&amp;k=zv2D4"" title=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001""&gt;&lt;img src=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Th-35.jpg"" width=""150"" height=""100"" alt=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001"" title=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sat, 23 Jun 2007 22:54:03 -0700</pubDate>
      <author>nobody@smugmug.com (Rick Willis)</author>
      <guid isPermaLink=""false"">http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Th-35.jpg</guid>
      <media:group>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Ti-35.jpg"" fileSize=""5809"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">919f0d4ff95f2ce1f6aa74ed51ca69d5</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Th-35.jpg"" fileSize=""10984"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">c3832be4d8ceb5e2c57297ccbb04b49c</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-S-35.jpg"" fileSize=""65126"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">7c49ba5a1e065dbe95873cea4c7caf1a</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-M-35.jpg"" fileSize=""130610"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">e02d7fc98ef842659c01a33469010cb5</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-L-35.jpg"" fileSize=""219115"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">d7b0cec3cd5f6911ff94bece7c902df6</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-XL-35.jpg"" fileSize=""340534"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">9debb84bedc244a4ba1fbd5127109040</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-X2-35.jpg"" fileSize=""499364"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""854"">
          <media:hash algo=""md5"">a28adf997aa0e30d1ca632bba1ad535b</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-X3-35.jpg"" fileSize=""734950"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1067"">
          <media:hash algo=""md5"">efffaedeb0ce1926154f92d9abe3d3d6</media:hash>
        </media:content>
        <media:content url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-O-35.jpg"" fileSize=""6757685"" type=""image/jpeg"" medium=""image"" width=""3504"" height=""2336"">
          <media:hash algo=""md5"">380f356fe95ef345a71c73a0b7f4b8c8</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://rickwillis.smugmug.com""&gt;Rick Willis&lt;/a&gt;&lt;br /&gt;UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001&lt;/p&gt;&lt;p&gt;&lt;a href=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/1765247_Cx46Kj#!i=165972321&amp;k=zv2D4"" title=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001""&gt;&lt;img src=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Th-35.jpg"" width=""150"" height=""100"" alt=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001"" title=""UT-Bryce Canyon National Park-Sunset Point-2006-09-20-5001"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://rickwillis.smugmug.com/Portfolio/Best/5-Star-Colorful-Images/UT-Bryce-Canyon-National-Park/165972321_zv2D4-Th-35.jpg"" width=""150"" height=""100""/>
      <media:category>Portfolio</media:category>
      <media:keywords>scenic, usa, utah, horizontal, bryce canyon national park, sunset point</media:keywords>
      <media:copyright url=""http://www.rickwillis-photos.com"">Rick Willis</media:copyright>
      <media:credit role=""photographer"">Rick Willis</media:credit>
    </item>
    <item>
      <title>Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146857576&amp;k=8Sgcw</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146857576&amp;k=8Sgcw"" title=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Th-3.jpg"" width=""100"" height=""150"" alt=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot."" title=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Wed, 25 Apr 2007 11:03:12 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2007-04-07 13:22:37</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Ti-3.jpg"" fileSize=""3286"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">fdfeb3956d34a739c70cf70dfb502d28</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Th-3.jpg"" fileSize=""5047"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">6145be8c68c87e73dd4f7ae48fd31875</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-S-3.jpg"" fileSize=""18031"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">49a0712637d52001b8c2ef9508324d88</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-M-3.jpg"" fileSize=""31666"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">6f700b793482b40bc02a81380c8dd953</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-L-3.jpg"" fileSize=""50006"" type=""image/jpeg"" medium=""image"" width=""400"" height=""600"">
          <media:hash algo=""md5"">12dd1bccb1046987a32df3706fb0a5bd</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146857576&amp;k=8Sgcw"" title=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Th-3.jpg"" width=""100"" height=""150"" alt=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot."" title=""Third performance -- I really loved this shot of her, with the cloud beneath her and the angle of the shot."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG8599/146857576_8Sgcw-Th-3.jpg"" width=""100"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>dance, theater, stage, ballroom, byu ballroom team</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>The Pinnacles, Phillip Island, Victoria, Australia</title>
      <link>http://introversion.smugmug.com/Communities/HDR/2775500_k345dd#!i=240721076&amp;k=AENk3</link>
      <description>&lt;p&gt;&lt;a href=""http://introversion.smugmug.com""&gt;Introversion Photography&lt;/a&gt;&lt;br /&gt;The Pinnacles, Phillip Island, Victoria, Australia&lt;/p&gt;&lt;p&gt;&lt;a href=""http://introversion.smugmug.com/Communities/HDR/2775500_k345dd#!i=240721076&amp;k=AENk3"" title=""The Pinnacles, Phillip Island, Victoria, Australia""&gt;&lt;img src=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Th-1.jpg"" width=""150"" height=""100"" alt=""The Pinnacles, Phillip Island, Victoria, Australia"" title=""The Pinnacles, Phillip Island, Victoria, Australia"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Communities</category>
      <pubDate>Mon, 07 Jan 2008 02:57:47 -0800</pubDate>
      <author>nobody@smugmug.com (Introversion Photography)</author>
      <guid isPermaLink=""false"">http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-01-05 22:07:45</exif:DateTimeOriginal>
      <geo:lat>-38.55903915150000</geo:lat>
      <geo:long>145.34268379200000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Ti-1.jpg"" fileSize=""4435"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">a9a0fefe7f5e1d6dd6081e095a5d96cf</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Th-1.jpg"" fileSize=""7992"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">0a1c40ec1a5aa3641815131543cb12e0</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-S-1.jpg"" fileSize=""22919"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">ac6afe3f45f43d3ee69823343664aa34</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-M-1.jpg"" fileSize=""96401"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">25421ecd3bade784c017581ef12893e1</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-L-1.jpg"" fileSize=""74065"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">380b26ffc5f38956b39f540c42cfe318</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-XL-1.jpg"" fileSize=""116499"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">b7455d1529a61a15e1766e2d0e11cba1</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-X2-1.jpg"" fileSize=""176053"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""854"">
          <media:hash algo=""md5"">c7b01b6581af3c6ad16e59e5bcb61368</media:hash>
        </media:content>
        <media:content url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-X3-1.jpg"" fileSize=""270985"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1067"">
          <media:hash algo=""md5"">d64341d47552d6f9a4f1022e406f46bf</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">The Pinnacles, Phillip Island, Victoria, Australia</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://introversion.smugmug.com""&gt;Introversion Photography&lt;/a&gt;&lt;br /&gt;The Pinnacles, Phillip Island, Victoria, Australia&lt;/p&gt;&lt;p&gt;&lt;a href=""http://introversion.smugmug.com/Communities/HDR/2775500_k345dd#!i=240721076&amp;k=AENk3"" title=""The Pinnacles, Phillip Island, Victoria, Australia""&gt;&lt;img src=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Th-1.jpg"" width=""150"" height=""100"" alt=""The Pinnacles, Phillip Island, Victoria, Australia"" title=""The Pinnacles, Phillip Island, Victoria, Australia"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://introversion.smugmug.com/Communities/HDR/IMG0835And9moreprocessed/240721076_AENk3-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Communities</media:category>
      <media:keywords>hdr, high dynamic range, coastline, sunset, rocks, the pinnacles, phillip island, introversion photography, david parry</media:keywords>
      <media:copyright url=""http://www.introversion.com.au"">Introversion Photography</media:copyright>
      <media:credit role=""photographer"">Introversion Photography</media:credit>
    </item>
    <item>
      <title>winchester's photo</title>
      <link>http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=81845332&amp;k=Yhn3m</link>
      <description>&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=81845332&amp;k=Yhn3m"" title=""winchester's photo""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Th-2.jpg"" width=""101"" height=""150"" alt=""winchester's photo"" title=""winchester's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Sat, 15 Jul 2006 15:10:08 -0700</pubDate>
      <author>nobody@smugmug.com (winchester)</author>
      <guid isPermaLink=""false"">http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Th-2.jpg</guid>
      <media:group>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Ti-2.jpg"" fileSize=""5354"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">c08cd6eb00b7926b72b3d64ef6fb0d59</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Th-2.jpg"" fileSize=""10425"" type=""image/jpeg"" medium=""image"" width=""101"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">b5b6665327639769bac274c53bff95ad</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-S-2.jpg"" fileSize=""38396"" type=""image/jpeg"" medium=""image"" width=""201"" height=""300"">
          <media:hash algo=""md5"">df0bd2e7aacb29600cc6791160cc857b</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-M-2.jpg"" fileSize=""76878"" type=""image/jpeg"" medium=""image"" width=""302"" height=""450"">
          <media:hash algo=""md5"">163e832c6f609eebb3c56bf75a77180c</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-L-2.jpg"" fileSize=""126932"" type=""image/jpeg"" medium=""image"" width=""402"" height=""600"">
          <media:hash algo=""md5"">b8ea0947a2e2cba1b057839a70c2717c</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-XL-2.jpg"" fileSize=""194027"" type=""image/jpeg"" medium=""image"" width=""515"" height=""768"">
          <media:hash algo=""md5"">05481c5fb4797fc56023e32227174a3d</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-X2-2.jpg"" fileSize=""282245"" type=""image/jpeg"" medium=""image"" width=""643"" height=""960"">
          <media:hash algo=""md5"">0cf3ef68a05307d3d706ae1966e21cd6</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-X3-2.jpg"" fileSize=""230009"" type=""image/jpeg"" medium=""image"" width=""677"" height=""1010"">
          <media:hash algo=""md5"">f9707ea294994f0d5039807851c8e454</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-O-2.jpg"" fileSize=""230009"" type=""image/jpeg"" medium=""image"" width=""677"" height=""1010"">
          <media:hash algo=""md5"">f9707ea294994f0d5039807851c8e454</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">winchester's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=81845332&amp;k=Yhn3m"" title=""winchester's photo""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Th-2.jpg"" width=""101"" height=""150"" alt=""winchester's photo"" title=""winchester's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://winchester.smugmug.com/Landscapes/Favorites/pond-1/81845332_Yhn3m-Th-2.jpg"" width=""101"" height=""150""/>
      <media:category>Landscapes</media:category>
      <media:keywords>pond, connecticut</media:keywords>
      <media:copyright url=""http://winchester.smugmug.com"">winchester</media:copyright>
      <media:credit role=""photographer"">winchester</media:credit>
    </item>
    <item>
      <title>This is Lyla</title>
      <link>http://www.smugmug.com/gallery/5851497_NqSVtL#!i=386101404&amp;k=RQG87</link>
      <description>&lt;p&gt;&lt;a href=""http://www.smugmug.com""&gt;SmugMug&lt;/a&gt;&lt;br /&gt;This is Lyla&lt;/p&gt;&lt;p&gt;&lt;a href=""http://www.smugmug.com/gallery/5851497_NqSVtL#!i=386101404&amp;k=RQG87"" title=""This is Lyla""&gt;&lt;img src=""http://www.smugmug.com/photos/386101404_RQG87-Th-1.jpg"" width=""150"" height=""150"" alt=""This is Lyla"" title=""This is Lyla"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Sat, 04 Oct 2008 00:44:36 -0700</pubDate>
      <author>nobody@smugmug.com (SmugMug)</author>
      <guid isPermaLink=""false"">http://www.smugmug.com/photos/386101404_RQG87-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-10-03 11:05:40</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://www.smugmug.com/photos/386101404_RQG87-Ti-1.jpg"" fileSize=""7277"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">369063837f22318394d09555fadd6b2d</media:hash>
        </media:content>
        <media:content url=""http://www.smugmug.com/photos/386101404_RQG87-Th-1.jpg"" fileSize=""10685"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">85d25c8368578e54e90b9ed349fa6002</media:hash>
        </media:content>
        <media:content url=""http://www.smugmug.com/photos/386101404_RQG87-S-1.jpg"" fileSize=""24357"" type=""image/jpeg"" medium=""image"" width=""400"" height=""227"">
          <media:hash algo=""md5"">5bd5fecc6d81a088762e248fa00c50bc</media:hash>
        </media:content>
        <media:content url=""http://www.smugmug.com/photos/386101404_RQG87-M-1.jpg"" fileSize=""42148"" type=""image/jpeg"" medium=""image"" width=""600"" height=""340"">
          <media:hash algo=""md5"">0273323ebe7dfe9b2aced26b084dffe0</media:hash>
        </media:content>
        <media:content url=""http://www.smugmug.com/photos/386101404_RQG87-L-1.jpg"" fileSize=""67723"" type=""image/jpeg"" medium=""image"" width=""800"" height=""453"">
          <media:hash algo=""md5"">74338c677956a71beee10308defcfb2d</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">This is Lyla</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://www.smugmug.com""&gt;SmugMug&lt;/a&gt;&lt;br /&gt;This is Lyla&lt;/p&gt;&lt;p&gt;&lt;a href=""http://www.smugmug.com/gallery/5851497_NqSVtL#!i=386101404&amp;k=RQG87"" title=""This is Lyla""&gt;&lt;img src=""http://www.smugmug.com/photos/386101404_RQG87-Th-1.jpg"" width=""150"" height=""150"" alt=""This is Lyla"" title=""This is Lyla"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://www.smugmug.com/photos/386101404_RQG87-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Daily Album</media:category>
    </item>
    <item>
      <title>Golden Tree, Glen affric</title>
      <link>http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/1922337_kT33hJ#!i=97477702&amp;k=bEA7d</link>
      <description>&lt;p&gt;&lt;a href=""http://beatsonphotography.smugmug.com""&gt;David Beatson&lt;/a&gt;&lt;br /&gt;Golden Tree, Glen affric&lt;/p&gt;&lt;p&gt;&lt;a href=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/1922337_kT33hJ#!i=97477702&amp;k=bEA7d"" title=""Golden Tree, Glen affric""&gt;&lt;img src=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Th-6.jpg"" width=""150"" height=""100"" alt=""Golden Tree, Glen affric"" title=""Golden Tree, Glen affric"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Sun, 24 Sep 2006 00:52:30 -0700</pubDate>
      <author>nobody@smugmug.com (David Beatson)</author>
      <guid isPermaLink=""false"">http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Th-6.jpg</guid>
      <exif:DateTimeOriginal>2004-10-14 11:34:36</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Ti-6.jpg"" fileSize=""6391"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">8b2e9ea6671b32a57e7e22a79fb2361d</media:hash>
        </media:content>
        <media:content url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Th-6.jpg"" fileSize=""9047"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">7a6c5336a01b2d591af58973e0302457</media:hash>
        </media:content>
        <media:content url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-S-6.jpg"" fileSize=""36277"" type=""image/jpeg"" medium=""image"" width=""400"" height=""266"">
          <media:hash algo=""md5"">0c26dc8519bf405431b8738cc0e5833c</media:hash>
        </media:content>
        <media:content url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-M-6.jpg"" fileSize=""67428"" type=""image/jpeg"" medium=""image"" width=""600"" height=""399"">
          <media:hash algo=""md5"">619049dbdf5c82aad25849d82e07a5f3</media:hash>
        </media:content>
        <media:content url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-L-6.jpg"" fileSize=""111105"" type=""image/jpeg"" medium=""image"" width=""800"" height=""532"">
          <media:hash algo=""md5"">f2636314c934a406e149b9e22fba1b96</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Golden Tree, Glen affric</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://beatsonphotography.smugmug.com""&gt;David Beatson&lt;/a&gt;&lt;br /&gt;Golden Tree, Glen affric&lt;/p&gt;&lt;p&gt;&lt;a href=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/1922337_kT33hJ#!i=97477702&amp;k=bEA7d"" title=""Golden Tree, Glen affric""&gt;&lt;img src=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Th-6.jpg"" width=""150"" height=""100"" alt=""Golden Tree, Glen affric"" title=""Golden Tree, Glen affric"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://beatsonphotography.smugmug.com/Landscapes/Scottish-landscapes/Glen-Affric/97477702_bEA7d-Th-6.jpg"" width=""150"" height=""100""/>
      <media:category>Landscapes</media:category>
      <media:keywords>glen, affric</media:keywords>
      <media:copyright url=""http://www.beatsonphotography.co.uk"">David Beatson</media:copyright>
      <media:credit role=""photographer"">David Beatson</media:credit>
    </item>
    <item>
      <title>Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ...</title>
      <link>http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/4093660_Bh3TR8#!i=253970665&amp;k=A6irz</link>
      <description>&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to win the LPS photo contest&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/4093660_Bh3TR8#!i=253970665&amp;k=A6irz"" title=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ...""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Th-8.jpg"" width=""150"" height=""86"" alt=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ..."" title=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Tue, 12 Feb 2008 20:26:51 -0800</pubDate>
      <author>nobody@smugmug.com (Vandana)</author>
      <guid isPermaLink=""false"">http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Th-8.jpg</guid>
      <exif:DateTimeOriginal>2008-02-10 17:26:43</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Ti-8.jpg"" fileSize=""7219"" type=""image/jpeg"" medium=""image"" width=""100"" height=""57"">
          <media:hash algo=""md5"">05f9d5f767581d2862237302733b2c83</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Th-8.jpg"" fileSize=""10687"" type=""image/jpeg"" medium=""image"" width=""150"" height=""86"" isDefault=""true"">
          <media:hash algo=""md5"">24167952c1c56f4d4ccdd6f3a47f062e</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-S-8.jpg"" fileSize=""40800"" type=""image/jpeg"" medium=""image"" width=""400"" height=""229"">
          <media:hash algo=""md5"">7e1ce024193a6dd45cebf56c838ee9d4</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-M-8.jpg"" fileSize=""70419"" type=""image/jpeg"" medium=""image"" width=""600"" height=""343"">
          <media:hash algo=""md5"">02987daf7e21e77cffcac25198145e46</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-L-8.jpg"" fileSize=""108664"" type=""image/jpeg"" medium=""image"" width=""800"" height=""458"">
          <media:hash algo=""md5"">6fc3f0bcc6097cb14e255330b0d6f950</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-XL-8.jpg"" fileSize=""158710"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""586"">
          <media:hash algo=""md5"">82ea723238f65a8842571754e5e6d57d</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-X2-8.jpg"" fileSize=""229106"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""732"">
          <media:hash algo=""md5"">82e994660ad7d8261e75b25798289299</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to win the LPS photo contest&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/4093660_Bh3TR8#!i=253970665&amp;k=A6irz"" title=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ...""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Th-8.jpg"" width=""150"" height=""86"" alt=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ..."" title=""Feb 12, 2008 - The Lone Ranger

LPS or blah ? 
I have been trying to shoot something that would be southwestern/new mexicanly unique..

late edit: this photo or a version of it, went on to w ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://vandana.smugmug.com/Photography/Photo-a-day/Daily-Photos-2008/the-lone-ranger/253970665_A6irz-Th-8.jpg"" width=""150"" height=""86""/>
      <media:category>Photography</media:category>
      <media:keywords>sunset, silhouette, horse, old west, southwest, reflections, barn, new mexico, clovis, lps</media:keywords>
      <media:copyright url=""http://www.vandanaphotography.com"">Vandana</media:copyright>
      <media:credit role=""photographer"">Vandana</media:credit>
    </item>
    <item>
      <title>10/20/06&#13;
&#13;
Follow the Yellow Brick Road.</title>
      <link>http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=104582020&amp;k=eviMN</link>
      <description>&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;10/20/06&#13;
&#13;
Follow the Yellow Brick Road.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=104582020&amp;k=eviMN"" title=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road.""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Th-7.jpg"" width=""150"" height=""116"" alt=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road."" title=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sun, 22 Oct 2006 17:22:39 -0700</pubDate>
      <author>nobody@smugmug.com (Eos-Muller)</author>
      <guid isPermaLink=""false"">http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Th-7.jpg</guid>
      <exif:DateTimeOriginal>2006-10-20 11:56:32</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Ti-7.jpg"" fileSize=""6795"" type=""image/jpeg"" medium=""image"" width=""100"" height=""77"">
          <media:hash algo=""md5"">d2c8fd301a39f1ce49e4196943af020e</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Th-7.jpg"" fileSize=""13519"" type=""image/jpeg"" medium=""image"" width=""150"" height=""116"" isDefault=""true"">
          <media:hash algo=""md5"">da6a6d56552069817ad81d77f3dce3e0</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-S-7.jpg"" fileSize=""84242"" type=""image/jpeg"" medium=""image"" width=""388"" height=""300"">
          <media:hash algo=""md5"">e4689eb221bb24dc320b26d6e1fd086f</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-M-7.jpg"" fileSize=""180032"" type=""image/jpeg"" medium=""image"" width=""582"" height=""450"">
          <media:hash algo=""md5"">a5314ba0bfe3f16781843a2e50c2607b</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-L-7.jpg"" fileSize=""311907"" type=""image/jpeg"" medium=""image"" width=""777"" height=""600"">
          <media:hash algo=""md5"">81941f86f5ac27d71b31a45b0c802b01</media:hash>
        </media:content>
        <media:content url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-XL-7.jpg"" fileSize=""501312"" type=""image/jpeg"" medium=""image"" width=""994"" height=""768"">
          <media:hash algo=""md5"">bbd06d642350afb5a48525ca925c4196</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">10/20/06&#13;
&#13;
Follow the Yellow Brick Road.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com""&gt;Eos-Muller&lt;/a&gt;&lt;br /&gt;10/20/06&#13;
&#13;
Follow the Yellow Brick Road.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/1739091_Pgswxt#!i=104582020&amp;k=eviMN"" title=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road.""&gt;&lt;img src=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Th-7.jpg"" width=""150"" height=""116"" alt=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road."" title=""10/20/06&#13;
&#13;
Follow the Yellow Brick Road."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://eos-muller.smugmug.com/Portfolio/Other/Daily-Photos/QF1C8535-01s/104582020_eviMN-Th-7.jpg"" width=""150"" height=""116""/>
      <media:category>Portfolio</media:category>
      <media:keywords>path, golden, fall, park, trail, scenic, mitchell memorial forest, hamilton county, follow the yellow brick road, bike</media:keywords>
      <media:copyright url=""http://eos-muller.smugmug.com"">Eos-Muller</media:copyright>
      <media:credit role=""photographer"">Eos-Muller</media:credit>
    </item>
    <item>
      <title>My black labrador, Shane</title>
      <link>http://ourobouros.smugmug.com/Now-Showing/Nature/682488_Br2Wf8#!i=153878464&amp;k=dNSZJ</link>
      <description>&lt;p&gt;&lt;a href=""http://ourobouros.smugmug.com""&gt;Jeff Arthur&lt;/a&gt;&lt;br /&gt;My black labrador, Shane&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ourobouros.smugmug.com/Now-Showing/Nature/682488_Br2Wf8#!i=153878464&amp;k=dNSZJ"" title=""My black labrador, Shane""&gt;&lt;img src=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Th-5.jpg"" width=""150"" height=""150"" alt=""My black labrador, Shane"" title=""My black labrador, Shane"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Now Showing</category>
      <pubDate>Fri, 18 May 2007 13:33:33 -0700</pubDate>
      <author>nobody@smugmug.com (Jeff Arthur)</author>
      <guid isPermaLink=""false"">http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Th-5.jpg</guid>
      <exif:DateTimeOriginal>2007-05-18 19:36:55</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Ti-5.jpg"" fileSize=""2334"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">54d13801040ab2d16de712e0d89b6726</media:hash>
        </media:content>
        <media:content url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Th-5.jpg"" fileSize=""3908"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">248980f2408788e11e0015ab38d08d07</media:hash>
        </media:content>
        <media:content url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-S-5.jpg"" fileSize=""14433"" type=""image/jpeg"" medium=""image"" width=""300"" height=""300"">
          <media:hash algo=""md5"">62950e9c1ae8103d3ff0841fb218b045</media:hash>
        </media:content>
        <media:content url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-M-5.jpg"" fileSize=""25647"" type=""image/jpeg"" medium=""image"" width=""450"" height=""450"">
          <media:hash algo=""md5"">cdc383fbbed3678bba9f9e5c109c040b</media:hash>
        </media:content>
        <media:content url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-L-5.jpg"" fileSize=""40570"" type=""image/jpeg"" medium=""image"" width=""600"" height=""600"">
          <media:hash algo=""md5"">5100ac9271f4878a3693af34a5f579cc</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">My black labrador, Shane</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ourobouros.smugmug.com""&gt;Jeff Arthur&lt;/a&gt;&lt;br /&gt;My black labrador, Shane&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ourobouros.smugmug.com/Now-Showing/Nature/682488_Br2Wf8#!i=153878464&amp;k=dNSZJ"" title=""My black labrador, Shane""&gt;&lt;img src=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Th-5.jpg"" width=""150"" height=""150"" alt=""My black labrador, Shane"" title=""My black labrador, Shane"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ourobouros.smugmug.com/Now-Showing/Nature/shanehead3/153878464_dNSZJ-Th-5.jpg"" width=""150"" height=""150""/>
      <media:category>Now Showing</media:category>
      <media:keywords>labrador, dark, dog, face, black</media:keywords>
      <media:copyright url=""http://www.snappyphotos.co.uk"">Jeff Arthur</media:copyright>
      <media:credit role=""photographer"">Jeff Arthur</media:credit>
    </item>
    <item>
      <title>I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ...</title>
      <link>http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/4814887_R8JFdf#!i=436706333&amp;k=YnkV9</link>
      <description>&lt;p&gt;&lt;a href=""http://aaron1013.smugmug.com""&gt;Aaron Ellingsen&lt;/a&gt;&lt;br /&gt;I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and the sea spray was worse. I tried for quite some time to get a clean shot with a dry lens, but it just wasn't happening. So I settled on heading back to Shore Acres to get it from a distance. I still was only able to keep just a handfull of shots due to water on my lens. I had a blast though.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/4814887_R8JFdf#!i=436706333&amp;k=YnkV9"" title=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ...""&gt;&lt;img src=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Th-1.jpg"" width=""150"" height=""100"" alt=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ..."" title=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sun, 14 Dec 2008 18:28:47 -0800</pubDate>
      <author>nobody@smugmug.com (Aaron Ellingsen)</author>
      <guid isPermaLink=""false"">http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Th-1.jpg</guid>
      <media:group>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Ti-1.jpg"" fileSize=""2849"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">cb2e97b7660c9d266da5a5e1a727a3db</media:hash>
        </media:content>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Th-1.jpg"" fileSize=""5127"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">8681d73db01fa5a85e6477f041e62b53</media:hash>
        </media:content>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-S-1.jpg"" fileSize=""30037"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">f12359245c4f2691016f141b7b0eb461</media:hash>
        </media:content>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-M-1.jpg"" fileSize=""59996"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">327065ce91189d83f6b45a9ab46fd1db</media:hash>
        </media:content>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-L-1.jpg"" fileSize=""77342"" type=""image/jpeg"" medium=""image"" width=""720"" height=""480"">
          <media:hash algo=""md5"">c69c6fb88262ad08d6d890c3fd3448f3</media:hash>
        </media:content>
        <media:content url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-XL-1.jpg"" fileSize=""67931"" type=""image/jpeg"" medium=""image"" width=""720"" height=""480"">
          <media:hash algo=""md5"">c3cab775536a76240099eed32a7bba9a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://aaron1013.smugmug.com""&gt;Aaron Ellingsen&lt;/a&gt;&lt;br /&gt;I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and the sea spray was worse. I tried for quite some time to get a clean shot with a dry lens, but it just wasn't happening. So I settled on heading back to Shore Acres to get it from a distance. I still was only able to keep just a handfull of shots due to water on my lens. I had a blast though.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/4814887_R8JFdf#!i=436706333&amp;k=YnkV9"" title=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ...""&gt;&lt;img src=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Th-1.jpg"" width=""150"" height=""100"" alt=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ..."" title=""I had been standing where the man is out on the cliff just a few minutes earlier, hoping to catch some wave shots up close and personal. It was nearly impossible. The wind was brutal, and th ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://aaron1013.smugmug.com/Portfolio/Oregon-Coast-2008/Surf-explosion-copy/436706333_YnkV9-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Portfolio</media:category>
      <media:keywords>surf, explosion</media:keywords>
      <media:copyright url=""http://aaron1013.smugmug.com"">Aaron Ellingsen</media:copyright>
      <media:credit role=""photographer"">Aaron Ellingsen</media:credit>
    </item>
    <item>
      <title>I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.</title>
      <link>http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/8766998_NWJgWT#!i=588580945&amp;k=GxcKL</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/8766998_NWJgWT#!i=588580945&amp;k=GxcKL"" title=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Th.jpg"" width=""150"" height=""150"" alt=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day."" title=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Sat, 11 Jul 2009 23:54:16 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Th.jpg</guid>
      <exif:DateTimeOriginal>2009-07-09 11:57:56</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Ti.jpg"" fileSize=""4375"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">1287c7d51869217edb47ddc138e96599</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Th.jpg"" fileSize=""7469"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">cefe72a82d1ad27ab676bf40387881c8</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-S.jpg"" fileSize=""15082"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">ff01f0c5394bdb9ba2181aa87cd24e5c</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-M.jpg"" fileSize=""28218"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">11ede47785887b2fab75e913e74cd113</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-L.jpg"" fileSize=""43319"" type=""image/jpeg"" medium=""image"" width=""400"" height=""600"">
          <media:hash algo=""md5"">543ad7dc3009d117647aa1466426073a</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-XL.jpg"" fileSize=""70336"" type=""image/jpeg"" medium=""image"" width=""512"" height=""768"">
          <media:hash algo=""md5"">268d41369bbe3199830185c9839a0707</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/8766998_NWJgWT#!i=588580945&amp;k=GxcKL"" title=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Th.jpg"" width=""150"" height=""150"" alt=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day."" title=""I just wanted to say thanks so much for all the lovely comments yesterday.&#13;
&#13;
Have a wonderful day."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Daily-Album/July-2009/IMG2379/588580945_GxcKL-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Daily Album</media:category>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>Mike Juvet's photo</title>
      <link>http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/5407237_bjSqZg#!i=367061408&amp;k=9byzw</link>
      <description>&lt;p&gt;&lt;a href=""http://groovyjoovy.smugmug.com""&gt;Mike Juvet&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/5407237_bjSqZg#!i=367061408&amp;k=9byzw"" title=""Mike Juvet's photo""&gt;&lt;img src=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Th.jpg"" width=""150"" height=""150"" alt=""Mike Juvet's photo"" title=""Mike Juvet's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Travel</category>
      <pubDate>Sat, 06 Sep 2008 18:01:57 -0700</pubDate>
      <author>nobody@smugmug.com (Mike Juvet)</author>
      <guid isPermaLink=""false"">http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-07-12 19:43:28</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Ti.jpg"" fileSize=""4019"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">4c47a797f5b47145dc5d77b523b3c508</media:hash>
        </media:content>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Th.jpg"" fileSize=""7119"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">eda2874ef351dca2b34ed511e401d2b3</media:hash>
        </media:content>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-S.jpg"" fileSize=""24823"" type=""image/jpeg"" medium=""image"" width=""295"" height=""300"">
          <media:hash algo=""md5"">4c12367f08f4b4c0a492781297955a1b</media:hash>
        </media:content>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-M.jpg"" fileSize=""47024"" type=""image/jpeg"" medium=""image"" width=""443"" height=""450"">
          <media:hash algo=""md5"">29644c19f1bdf8166ce3b79282514cb5</media:hash>
        </media:content>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-L.jpg"" fileSize=""75433"" type=""image/jpeg"" medium=""image"" width=""590"" height=""600"">
          <media:hash algo=""md5"">82bf6ea429e2f04599ed76ba4c24bfed</media:hash>
        </media:content>
        <media:content url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-XL.jpg"" fileSize=""115251"" type=""image/jpeg"" medium=""image"" width=""756"" height=""768"">
          <media:hash algo=""md5"">9db4e7bb58b50cdf0f7ffcb7ad758d59</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Mike Juvet's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://groovyjoovy.smugmug.com""&gt;Mike Juvet&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/5407237_bjSqZg#!i=367061408&amp;k=9byzw"" title=""Mike Juvet's photo""&gt;&lt;img src=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Th.jpg"" width=""150"" height=""150"" alt=""Mike Juvet's photo"" title=""Mike Juvet's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://groovyjoovy.smugmug.com/Travel/Halibut-Point-State-Park/MG8976/367061408_9byzw-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Travel</media:category>
      <media:keywords>beach, halibut point state park, massachusetts, sunset, 8976</media:keywords>
      <media:copyright url=""http://groovyjoovy.smugmug.com"">Mike Juvet</media:copyright>
      <media:credit role=""photographer"">Mike Juvet</media:credit>
    </item>
    <item>
      <title>A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ...</title>
      <link>http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2434326_h3Jj6B#!i=130555500&amp;k=mLzG5</link>
      <description>&lt;p&gt;&lt;a href=""http://garchambault.smugmug.com""&gt;Gilles Archambault&lt;/a&gt;&lt;br /&gt;A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
&lt;h2&gt;First place winner of the Biodome 2007 photo contest. &#13;
Gagnant du premier prix du concours de photo du Biodôme de Montréal 2007.&#13;
&lt;/h2&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2434326_h3Jj6B#!i=130555500&amp;k=mLzG5"" title=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ...""&gt;&lt;img src=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Th-12.jpg"" width=""150"" height=""150"" alt=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ..."" title=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Birds</category>
      <pubDate>Sun, 18 Feb 2007 18:48:52 -0800</pubDate>
      <author>nobody@smugmug.com (Gilles Archambault)</author>
      <guid isPermaLink=""false"">http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Th-12.jpg</guid>
      <exif:DateTimeOriginal>2007-01-27 12:58:36</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Ti-12.jpg"" fileSize=""4726"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">6cc90b8f58cbe09f3d702cdcf631f52b</media:hash>
        </media:content>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Th-12.jpg"" fileSize=""8774"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">311f573f4067ffc9452cec018d18e5dd</media:hash>
        </media:content>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-S-12.jpg"" fileSize=""36112"" type=""image/jpeg"" medium=""image"" width=""382"" height=""300"">
          <media:hash algo=""md5"">4a385b55edecffa344edc8174364fa2b</media:hash>
        </media:content>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-M-12.jpg"" fileSize=""69948"" type=""image/jpeg"" medium=""image"" width=""573"" height=""450"">
          <media:hash algo=""md5"">e72ed258638c8cae055692ac2cbffcc3</media:hash>
        </media:content>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-L-12.jpg"" fileSize=""114713"" type=""image/jpeg"" medium=""image"" width=""764"" height=""600"">
          <media:hash algo=""md5"">d446b44995c8c4926454bc443364efb5</media:hash>
        </media:content>
        <media:content url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-XL-12.jpg"" fileSize=""183515"" type=""image/jpeg"" medium=""image"" width=""977"" height=""768"">
          <media:hash algo=""md5"">f55fcfcb515c7ad129ad0cff4cc949b5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://garchambault.smugmug.com""&gt;Gilles Archambault&lt;/a&gt;&lt;br /&gt;A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
&lt;h2&gt;First place winner of the Biodome 2007 photo contest. &#13;
Gagnant du premier prix du concours de photo du Biodôme de Montréal 2007.&#13;
&lt;/h2&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2434326_h3Jj6B#!i=130555500&amp;k=mLzG5"" title=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ...""&gt;&lt;img src=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Th-12.jpg"" width=""150"" height=""150"" alt=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ..."" title=""A snowy owl eye over the photographer. &#13;
Le regard d'un harfang des neiges sur le photographe. &#13;
Somewhere in Quebec, Canada&#13;
First place winner of the Biodome 2007 photo contest. &#13;
Gagnant  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://garchambault.smugmug.com/Birds/Owls-Snowy-Owls/2987/130555500_mLzG5-Th-12.jpg"" width=""150"" height=""150""/>
      <media:category>Birds</media:category>
      <media:keywords>myfavorites</media:keywords>
      <media:copyright url=""http://www.garchambault.com"">Gilles Archambault</media:copyright>
      <media:credit role=""photographer"">Gilles Archambault</media:credit>
    </item>
    <item>
      <title>I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ...</title>
      <link>http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/4465852_3R7MzP#!i=480829564&amp;k=7repN</link>
      <description>&lt;p&gt;&lt;a href=""http://kurtpreston.smugmug.com""&gt;KurtPreston&lt;/a&gt;&lt;br /&gt;I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicted with the result :)&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/4465852_3R7MzP#!i=480829564&amp;k=7repN"" title=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ...""&gt;&lt;img src=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Th-1.jpg"" width=""150"" height=""120"" alt=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ..."" title=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Wed, 25 Feb 2009 16:55:59 -0800</pubDate>
      <author>nobody@smugmug.com (KurtPreston)</author>
      <guid isPermaLink=""false"">http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-06-27 19:33:16</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Ti-1.jpg"" fileSize=""7541"" type=""image/jpeg"" medium=""image"" width=""100"" height=""80"">
          <media:hash algo=""md5"">4dda5025f378a13437e0833ccc6b14d6</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Th-1.jpg"" fileSize=""11242"" type=""image/jpeg"" medium=""image"" width=""150"" height=""120"" isDefault=""true"">
          <media:hash algo=""md5"">53c32ba1d3bedd0f71ba935237fdaebf</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-S-1.jpg"" fileSize=""40164"" type=""image/jpeg"" medium=""image"" width=""375"" height=""300"">
          <media:hash algo=""md5"">bcf34204368a80001e242d094fba80e6</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-M-1.jpg"" fileSize=""70282"" type=""image/jpeg"" medium=""image"" width=""563"" height=""450"">
          <media:hash algo=""md5"">4799fc77f4a8ac079bcd85801594109d</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-L-1.jpg"" fileSize=""106736"" type=""image/jpeg"" medium=""image"" width=""750"" height=""600"">
          <media:hash algo=""md5"">1f93376ad67be32bb10bf7a1616b56d5</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-XL-1.jpg"" fileSize=""156124"" type=""image/jpeg"" medium=""image"" width=""960"" height=""768"">
          <media:hash algo=""md5"">94721affe137437636543a8bcbf498f6</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-X2-1.jpg"" fileSize=""224117"" type=""image/jpeg"" medium=""image"" width=""1200"" height=""960"">
          <media:hash algo=""md5"">d2bf955d68a73ae9faa1de6b6471aa9d</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-X3-1.jpg"" fileSize=""324641"" type=""image/jpeg"" medium=""image"" width=""1500"" height=""1200"">
          <media:hash algo=""md5"">0dbc7f61e14a47aca25664a8a1da783f</media:hash>
        </media:content>
        <media:content url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-O-1.jpg"" fileSize=""2452893"" type=""image/jpeg"" medium=""image"" width=""3116"" height=""2493"">
          <media:hash algo=""md5"">264a656685b34e88216ed818b373c449</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://kurtpreston.smugmug.com""&gt;KurtPreston&lt;/a&gt;&lt;br /&gt;I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicted with the result :)&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/4465852_3R7MzP#!i=480829564&amp;k=7repN"" title=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ...""&gt;&lt;img src=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Th-1.jpg"" width=""150"" height=""120"" alt=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ..."" title=""I don't know what the heck I am doing in CS3 enough to really make this concept shine. But I've been dying to use this sky as a background in a composite image for awhile so you get inflicte ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://kurtpreston.smugmug.com/Nature/Birds/Bald-Eagles-in-Maryland/DailyEagleSky/480829564_7repN-Th-1.jpg"" width=""150"" height=""120""/>
      <media:category>Nature</media:category>
      <media:keywords>daily, eaglesky</media:keywords>
      <media:copyright url=""http://kurtpreston.smugmug.com"">KurtPreston</media:copyright>
      <media:credit role=""photographer"">KurtPreston</media:credit>
    </item>
    <item>
      <title>unnamed stream in Cottonwood Canyon, Utah</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=391950085&amp;k=H5BGg</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;unnamed stream in Cottonwood Canyon, Utah&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=391950085&amp;k=H5BGg"" title=""unnamed stream in Cottonwood Canyon, Utah""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Th-4.jpg"" width=""150"" height=""100"" alt=""unnamed stream in Cottonwood Canyon, Utah"" title=""unnamed stream in Cottonwood Canyon, Utah"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sat, 11 Oct 2008 23:43:53 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-10-02 14:44:32</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Ti-4.jpg"" fileSize=""5046"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">5614b1b5c91adfcf38978f5def086e3e</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Th-4.jpg"" fileSize=""8977"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">6a7be4e814c5c57253d2b66ff90bd83e</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-S-4.jpg"" fileSize=""50165"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">40dccd1a6869abd23f2e4926c486bbe5</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-M-4.jpg"" fileSize=""98375"" type=""image/jpeg"" medium=""image"" width=""600"" height=""401"">
          <media:hash algo=""md5"">bc54a94cc09ef0989562da54d1d2de57</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-L-4.jpg"" fileSize=""158554"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">1e456678f19d913101efd979e18c1955</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">unnamed stream in Cottonwood Canyon, Utah</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;unnamed stream in Cottonwood Canyon, Utah&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=391950085&amp;k=H5BGg"" title=""unnamed stream in Cottonwood Canyon, Utah""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Th-4.jpg"" width=""150"" height=""100"" alt=""unnamed stream in Cottonwood Canyon, Utah"" title=""unnamed stream in Cottonwood Canyon, Utah"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0286/391950085_H5BGg-Th-4.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>river, cottonwood canyon, utah, water, nature, landscape</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>This is one of my favorite shots --- and it was just recently published.</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146865736&amp;k=7MpUX</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;This is one of my favorite shots --- and it was just recently published.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146865736&amp;k=7MpUX"" title=""This is one of my favorite shots --- and it was just recently published.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Th-4.jpg"" width=""150"" height=""100"" alt=""This is one of my favorite shots --- and it was just recently published."" title=""This is one of my favorite shots --- and it was just recently published."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Wed, 25 Apr 2007 11:40:05 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2007-04-05 18:25:17</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Ti-4.jpg"" fileSize=""6125"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">47c13cc8760d70b5749f933a5f765a15</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Th-4.jpg"" fileSize=""10543"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">b516bbfbb80a08072548782c3b359b2b</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-S-4.jpg"" fileSize=""49554"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">98447dd1c9a5dc9d23e340ea591cea13</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-M-4.jpg"" fileSize=""89358"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">9eae40d063e32e5ebdb8fd59efee885e</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-L-4.jpg"" fileSize=""141455"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">e8c60d3c0cb73933dcfa657b688910e5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">This is one of my favorite shots --- and it was just recently published.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;This is one of my favorite shots --- and it was just recently published.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=146865736&amp;k=7MpUX"" title=""This is one of my favorite shots --- and it was just recently published.""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Th-4.jpg"" width=""150"" height=""100"" alt=""This is one of my favorite shots --- and it was just recently published."" title=""This is one of my favorite shots --- and it was just recently published."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7871/146865736_7MpUX-Th-4.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>dance, ballroom, theater, stage, byu ballroom team</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ...</title>
      <link>http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=260985283&amp;k=haRCe</link>
      <description>&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com""&gt;John Bennett&lt;/a&gt;&lt;br /&gt;2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been working out the best vantage points and sunset times eversince I moved to my current home last summer. Mid-afternoon today it was very grey and trying to rain, then suddenly it all blew through and the air cleared. Sensing a nice sunset I grabbed my gear and walked a few minutes to a local hill crest and waited as this golden lightshow developed. It took much longer to select the DP than I spent taking photos, and I will upload other contenders to my Lincoln Sunset Gallery later this evening (see here  http://johnloguk.smugmug.com/gallery/4053411_yY5Qg#260994902 ).&#13;
&#13;
I hope you like this shot as much as I do, and I am so glad that I live so close to this view!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=260985283&amp;k=haRCe"" title=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ...""&gt;&lt;img src=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Th-4.jpg"" width=""150"" height=""150"" alt=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ..."" title=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Sun, 02 Mar 2008 10:40:01 -0800</pubDate>
      <author>nobody@smugmug.com (John Bennett)</author>
      <guid isPermaLink=""false"">http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2007-03-21 06:06:07</exif:DateTimeOriginal>
      <geo:lat>53.24033323510000</geo:lat>
      <geo:long>-0.49404144287100</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Ti-4.jpg"" fileSize=""4896"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">83e74a0700543f69f9ccf9fc6ccfb5d0</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Th-4.jpg"" fileSize=""8239"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">c7cf4a9410594d42031bde9d069befa0</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-S-4.jpg"" fileSize=""20739"" type=""image/jpeg"" medium=""image"" width=""247"" height=""300"">
          <media:hash algo=""md5"">a4552afee92dfaec5b9d6def34ae38b2</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-M-4.jpg"" fileSize=""35281"" type=""image/jpeg"" medium=""image"" width=""371"" height=""450"">
          <media:hash algo=""md5"">4b6ab148e9658acae78be0d356431bf6</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-L-4.jpg"" fileSize=""53103"" type=""image/jpeg"" medium=""image"" width=""495"" height=""600"">
          <media:hash algo=""md5"">c39336b079ad95a6328c9fb5f1a6a338</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-XL-4.jpg"" fileSize=""79533"" type=""image/jpeg"" medium=""image"" width=""633"" height=""768"">
          <media:hash algo=""md5"">08613869b68440bd4b02932f817b36ec</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-X2-4.jpg"" fileSize=""116911"" type=""image/jpeg"" medium=""image"" width=""791"" height=""960"">
          <media:hash algo=""md5"">da6f53ffa4f63943bb0749fa186f4d54</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com""&gt;John Bennett&lt;/a&gt;&lt;br /&gt;2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been working out the best vantage points and sunset times eversince I moved to my current home last summer. Mid-afternoon today it was very grey and trying to rain, then suddenly it all blew through and the air cleared. Sensing a nice sunset I grabbed my gear and walked a few minutes to a local hill crest and waited as this golden lightshow developed. It took much longer to select the DP than I spent taking photos, and I will upload other contenders to my Lincoln Sunset Gallery later this evening (see here  http://johnloguk.smugmug.com/gallery/4053411_yY5Qg#260994902 ).&#13;
&#13;
I hope you like this shot as much as I do, and I am so glad that I live so close to this view!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=260985283&amp;k=haRCe"" title=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ...""&gt;&lt;img src=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Th-4.jpg"" width=""150"" height=""150"" alt=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ..."" title=""2.3.08 - Golden Years. Some photos are moments of inspiration, pieces of technical brilliance or snatched opportunities. This one had been meticulously planned for sometime. I have been work ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0102/260985283_haRCe-Th-4.jpg"" width=""150"" height=""150""/>
      <media:category>Photography</media:category>
      <media:keywords>lincoln cathedral, sunset, golden</media:keywords>
      <media:copyright url=""http://johnloguk.smugmug.com"">John Bennett</media:copyright>
      <media:credit role=""photographer"">John Bennett</media:credit>
    </item>
    <item>
      <title>Harry's photo</title>
      <link>http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/2472264_rSDtrF#!i=129676790&amp;k=kmVV4</link>
      <description>&lt;p&gt;&lt;a href=""http://behret.smugmug.com""&gt;Harry&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/2472264_rSDtrF#!i=129676790&amp;k=kmVV4"" title=""Harry's photo""&gt;&lt;img src=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Th-2.jpg"" width=""150"" height=""131"" alt=""Harry's photo"" title=""Harry's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Wed, 14 Feb 2007 12:38:40 -0800</pubDate>
      <author>nobody@smugmug.com (Harry)</author>
      <guid isPermaLink=""false"">http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2007-02-07 11:51:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Ti-2.jpg"" fileSize=""7354"" type=""image/jpeg"" medium=""image"" width=""100"" height=""88"">
          <media:hash algo=""md5"">c04d8e85bee003b4f212e1c5aed6fe19</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Th-2.jpg"" fileSize=""14708"" type=""image/jpeg"" medium=""image"" width=""150"" height=""131"" isDefault=""true"">
          <media:hash algo=""md5"">6883ca2d65accb13a191438b641dc6ec</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-S-2.jpg"" fileSize=""69885"" type=""image/jpeg"" medium=""image"" width=""342"" height=""300"">
          <media:hash algo=""md5"">8ddcd01e5a7f0869365e1cc70dcf124e</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-M-2.jpg"" fileSize=""147330"" type=""image/jpeg"" medium=""image"" width=""514"" height=""450"">
          <media:hash algo=""md5"">a2f958939ab53d9e1f9fd327e19c22eb</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-L-2.jpg"" fileSize=""240210"" type=""image/jpeg"" medium=""image"" width=""685"" height=""600"">
          <media:hash algo=""md5"">d60a9bdd7e4851c2faedc8cab1760c3e</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-XL-2.jpg"" fileSize=""323539"" type=""image/jpeg"" medium=""image"" width=""876"" height=""768"">
          <media:hash algo=""md5"">c4b47ac5803ff3c4b415016c0dfef312</media:hash>
        </media:content>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-X2-2.jpg"" type=""image/jpeg"" medium=""image"" width=""800"" height=""701""/>
        <media:content url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-O-2.jpg"" fileSize=""352266"" type=""image/jpeg"" medium=""image"" width=""800"" height=""701"">
          <media:hash algo=""md5"">4491305de2d59b57fbb6d94b70b22208</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Harry's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://behret.smugmug.com""&gt;Harry&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/2472264_rSDtrF#!i=129676790&amp;k=kmVV4"" title=""Harry's photo""&gt;&lt;img src=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Th-2.jpg"" width=""150"" height=""131"" alt=""Harry's photo"" title=""Harry's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://behret.smugmug.com/Nature/Audubon-of-Floridas-Center-for/pic32210c/129676790_kmVV4-Th-2.jpg"" width=""150"" height=""131""/>
      <media:category>Nature</media:category>
      <media:copyright url=""http://behret.smugmug.com"">Harry</media:copyright>
      <media:credit role=""photographer"">Harry</media:credit>
    </item>
    <item>
      <title>Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ...</title>
      <link>http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=421797582&amp;k=aBuAG</link>
      <description>&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of months ago when we had our first bout of hoarfrost.  I dashed out the take photos, but the sun was melting the frost faster than I could capture it.  I ended up spending about two hours taking photos of water drops all over the garden and yard.  I remember being so excited that I forgot to get breakfast for my poor long-suffering hubby.  You all posted a lot of great images today.  I really enjoyed looking through them all.  It is amazing how much talent is here on smugmug.  I am constantly being inspired to try new things by all the creativity I see here.  So, thanks, everyone for being SUCH an inspiration!  Hope your day is lovely.  I know Dave is getting buried in snow again in Seattle.  I think we're all ready for winter to end.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=421797582&amp;k=aBuAG"" title=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ...""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Th-3.jpg"" width=""150"" height=""149"" alt=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ..."" title=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily shots for the dailies forum on Dgrin</category>
      <pubDate>Fri, 21 Nov 2008 15:26:23 -0800</pubDate>
      <author>nobody@smugmug.com (fotoeffects)</author>
      <guid isPermaLink=""false"">http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2008-11-21 09:51:02</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Ti-3.jpg"" fileSize=""9667"" type=""image/jpeg"" medium=""image"" width=""100"" height=""99"">
          <media:hash algo=""md5"">282a7a73f6ed85940830a9c26ff626bd</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Th-3.jpg"" fileSize=""15675"" type=""image/jpeg"" medium=""image"" width=""150"" height=""149"" isDefault=""true"">
          <media:hash algo=""md5"">bcbd3ea9ad0e01f5a4db27786a4c3aba</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-S-3.jpg"" fileSize=""45778"" type=""image/jpeg"" medium=""image"" width=""302"" height=""300"">
          <media:hash algo=""md5"">eeaee6d5625102b58571a17952a8f0bd</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-M-3.jpg"" fileSize=""81865"" type=""image/jpeg"" medium=""image"" width=""453"" height=""450"">
          <media:hash algo=""md5"">2da89b6e3d34e02accaaab27408f4c16</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-L-3.jpg"" fileSize=""124994"" type=""image/jpeg"" medium=""image"" width=""604"" height=""600"">
          <media:hash algo=""md5"">2777a6aa16d046f35fed4b86a94f8733</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-XL-3.jpg"" fileSize=""181257"" type=""image/jpeg"" medium=""image"" width=""773"" height=""768"">
          <media:hash algo=""md5"">cc9c5ce32e0810f1474ee513e8d0c970</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-X2-3.jpg"" fileSize=""253590"" type=""image/jpeg"" medium=""image"" width=""966"" height=""960"">
          <media:hash algo=""md5"">7579ed99a61003ab1bde493b0fca5c7f</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-X3-3.jpg"" fileSize=""360670"" type=""image/jpeg"" medium=""image"" width=""1208"" height=""1200"">
          <media:hash algo=""md5"">b78d53e1f5cc578547ce77d886920f9c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of months ago when we had our first bout of hoarfrost.  I dashed out the take photos, but the sun was melting the frost faster than I could capture it.  I ended up spending about two hours taking photos of water drops all over the garden and yard.  I remember being so excited that I forgot to get breakfast for my poor long-suffering hubby.  You all posted a lot of great images today.  I really enjoyed looking through them all.  It is amazing how much talent is here on smugmug.  I am constantly being inspired to try new things by all the creativity I see here.  So, thanks, everyone for being SUCH an inspiration!  Hope your day is lovely.  I know Dave is getting buried in snow again in Seattle.  I think we're all ready for winter to end.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=421797582&amp;k=aBuAG"" title=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ...""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Th-3.jpg"" width=""150"" height=""149"" alt=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ..."" title=""Day 10 - The sun is reflected in water droplets on fir tree needles; please look at this in the large size and you'll see the starbursts in all the water drops.  I took this shot a couple of ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC2009/421797582_aBuAG-Th-3.jpg"" width=""150"" height=""149""/>
      <media:category>Daily shots for the dailies forum on Dgrin</media:category>
      <media:keywords>droplets, reflections, tree, water, sun, needles, macro</media:keywords>
      <media:copyright url=""http://fotoeffects.smugmug.com"">fotoeffects</media:copyright>
      <media:credit role=""photographer"">fotoeffects</media:credit>
    </item>
    <item>
      <title>Great Horned Owl</title>
      <link>http://dennismullen.smugmug.com/Animals/Animals/3112001_tpnfJp#!i=292921725&amp;k=q6s2G</link>
      <description>&lt;p&gt;&lt;a href=""http://dennismullen.smugmug.com""&gt;Dennis Mullen&lt;/a&gt;&lt;br /&gt;Great Horned Owl&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennismullen.smugmug.com/Animals/Animals/3112001_tpnfJp#!i=292921725&amp;k=q6s2G"" title=""Great Horned Owl""&gt;&lt;img src=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Th-3.jpg"" width=""150"" height=""100"" alt=""Great Horned Owl"" title=""Great Horned Owl"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Animals</category>
      <pubDate>Sat, 10 May 2008 15:51:59 -0700</pubDate>
      <author>nobody@smugmug.com (Dennis Mullen)</author>
      <guid isPermaLink=""false"">http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2008-05-10 13:29:11</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Ti-3.jpg"" fileSize=""4359"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">ee8dec017702d4fec912badf6ad51038</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Th-3.jpg"" fileSize=""7916"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">48a7bb06df2f6d940761fc4eda4e43e9</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-S-3.jpg"" fileSize=""44572"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">500198808568146a17d5b0d3efe17e8c</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-M-3.jpg"" fileSize=""86712"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">213ec966cb147dde55861732293435e6</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-L-3.jpg"" fileSize=""143904"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">b1f9ffd20d131ab4c259e090cfaf482d</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-XL-3.jpg"" fileSize=""222695"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">fac26d380ed837e9d892b47202047d17</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-X2-3.jpg"" fileSize=""327233"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""854"">
          <media:hash algo=""md5"">c3df74849112d2dd3e74f2919e5c683e</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-X3-3.jpg"" fileSize=""472962"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1067"">
          <media:hash algo=""md5"">f8caa41e55d8fcc49890aaeaedeb0d83</media:hash>
        </media:content>
        <media:content url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-O-3.jpg"" fileSize=""1259406"" type=""image/jpeg"" medium=""image"" width=""1620"" height=""1080"">
          <media:hash algo=""md5"">eae07121c1ebb904d50b60a23c00ba8a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Great Horned Owl</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://dennismullen.smugmug.com""&gt;Dennis Mullen&lt;/a&gt;&lt;br /&gt;Great Horned Owl&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennismullen.smugmug.com/Animals/Animals/3112001_tpnfJp#!i=292921725&amp;k=q6s2G"" title=""Great Horned Owl""&gt;&lt;img src=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Th-3.jpg"" width=""150"" height=""100"" alt=""Great Horned Owl"" title=""Great Horned Owl"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://dennismullen.smugmug.com/Animals/Animals/DSC09718b/292921725_q6s2G-Th-3.jpg"" width=""150"" height=""100""/>
      <media:category>Animals</media:category>
      <media:keywords>favorite, owl, _all_pictures, animal, bird, animals, birds, wildlife</media:keywords>
      <media:copyright url=""http://www.dennismullen.com"">Dennis Mullen</media:copyright>
      <media:credit role=""photographer"">Dennis Mullen</media:credit>
    </item>
    <item>
      <title>Forest Sun</title>
      <link>http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/518373_ct8Nds#!i=21343772&amp;k=eBoV3</link>
      <description>&lt;p&gt;&lt;a href=""http://palusmus.smugmug.com""&gt;Jerry Snelling&lt;/a&gt;&lt;br /&gt;Forest Sun&lt;/p&gt;&lt;p&gt;&lt;a href=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/518373_ct8Nds#!i=21343772&amp;k=eBoV3"" title=""Forest Sun""&gt;&lt;img src=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Th-8.jpg"" width=""150"" height=""113"" alt=""Forest Sun"" title=""Forest Sun"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Thu, 05 May 2005 15:09:29 -0700</pubDate>
      <author>nobody@smugmug.com (Jerry Snelling)</author>
      <guid isPermaLink=""false"">http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Th-8.jpg</guid>
      <exif:DateTimeOriginal>2003-01-12 05:47:46</exif:DateTimeOriginal>
      <geo:lat>57.04600606200000</geo:lat>
      <geo:long>-135.31620025600000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Ti-8.jpg"" fileSize=""5701"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">c5c5511e80b88b692e60b64b81a103cf</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Th-8.jpg"" fileSize=""11029"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">2fffdc0b457bb8de1c7368fbff90fb38</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-S-8.jpg"" fileSize=""74963"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">75aa83a8a98d62e19911971dea075253</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-M-8.jpg"" fileSize=""175371"" type=""image/jpeg"" medium=""image"" width=""600"" height=""450"">
          <media:hash algo=""md5"">9dca765dc57b26facc8d07e38169694f</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-L-8.jpg"" fileSize=""326209"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">fd95197540cf8c75ea6564e1a4710de2</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-XL-8.jpg"" fileSize=""551885"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""768"">
          <media:hash algo=""md5"">0c59468c42980690bdf22166a6771963</media:hash>
        </media:content>
        <media:content url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-X2-8.jpg"" fileSize=""2251548"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""960"">
          <media:hash algo=""md5"">fad2f316597fcf849f1bbbb9b9040b45</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Forest Sun</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://palusmus.smugmug.com""&gt;Jerry Snelling&lt;/a&gt;&lt;br /&gt;Forest Sun&lt;/p&gt;&lt;p&gt;&lt;a href=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/518373_ct8Nds#!i=21343772&amp;k=eBoV3"" title=""Forest Sun""&gt;&lt;img src=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Th-8.jpg"" width=""150"" height=""113"" alt=""Forest Sun"" title=""Forest Sun"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://palusmus.smugmug.com/Nature/TREES-and-TRAILS/ForestSun3/21343772_eBoV3-Th-8.jpg"" width=""150"" height=""113""/>
      <media:category>Nature</media:category>
      <media:keywords>foggy, trail, path, trees, forest, sitka, alaska, sitka national historic park</media:keywords>
      <media:copyright url=""http://www.sitkapics.com"">Jerry Snelling</media:copyright>
      <media:credit role=""photographer"">Jerry Snelling</media:credit>
    </item>
    <item>
      <title>18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ...</title>
      <link>http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=256096057&amp;k=cPWZ7</link>
      <description>&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com""&gt;John Bennett&lt;/a&gt;&lt;br /&gt;18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish things off. I often go for a run around sunset, my favourite time of day, and although carrying a camera tends to break up the run it is often worth it. Today was more photos than running, and I might even have a whole gallery for the rest of the shots (here it is  http://johnloguk.smugmug.com/gallery/4363156_BFbUq#256100460 ). But here is today's offering, another take on ""fire and ice"", reflected sun and reeds in the icy river.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=256096057&amp;k=cPWZ7"" title=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ...""&gt;&lt;img src=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Th-1.jpg"" width=""150"" height=""150"" alt=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ..."" title=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Mon, 18 Feb 2008 14:48:58 -0800</pubDate>
      <author>nobody@smugmug.com (John Bennett)</author>
      <guid isPermaLink=""false"">http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2007-03-08 05:29:40</exif:DateTimeOriginal>
      <geo:lat>53.22561428440000</geo:lat>
      <geo:long>-0.48717498779300</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Ti-1.jpg"" fileSize=""5581"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">ac7efa7ce3cabf702116bd5b69d32234</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Th-1.jpg"" fileSize=""11532"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">baabded67e38e77e8cf731c33aab94d1</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-S-1.jpg"" fileSize=""50444"" type=""image/jpeg"" medium=""image"" width=""400"" height=""258"">
          <media:hash algo=""md5"">775164626d8eab7bac88666afd1a8019</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-M-1.jpg"" fileSize=""106428"" type=""image/jpeg"" medium=""image"" width=""600"" height=""386"">
          <media:hash algo=""md5"">8bf9659afbbc2c9d32629158884fb8a1</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-L-1.jpg"" fileSize=""180926"" type=""image/jpeg"" medium=""image"" width=""800"" height=""515"">
          <media:hash algo=""md5"">ad945e52a4ce831f3d48c2bce07c291f</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-XL-1.jpg"" fileSize=""281801"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""659"">
          <media:hash algo=""md5"">b80b1f428f6f23ce83be0d4de89858b8</media:hash>
        </media:content>
        <media:content url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-X2-1.jpg"" fileSize=""414066"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""824"">
          <media:hash algo=""md5"">73b0945742271c6cd410ed0eaaf03a41</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com""&gt;John Bennett&lt;/a&gt;&lt;br /&gt;18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish things off. I often go for a run around sunset, my favourite time of day, and although carrying a camera tends to break up the run it is often worth it. Today was more photos than running, and I might even have a whole gallery for the rest of the shots (here it is  http://johnloguk.smugmug.com/gallery/4363156_BFbUq#256100460 ). But here is today's offering, another take on ""fire and ice"", reflected sun and reeds in the icy river.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/4267920_CStH6N#!i=256096057&amp;k=cPWZ7"" title=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ...""&gt;&lt;img src=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Th-1.jpg"" width=""150"" height=""150"" alt=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ..."" title=""18.2.08 - Today has been another beautiful day. Hard frost to start, not a cloud in the sky all day, that low mist that characterises clear winter days, and a stunning sunset to finish thing ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://johnloguk.smugmug.com/Photography/DAILY-PHOTOS-FIRST-YEAR/DSC0114/256096057_cPWZ7-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Photography</media:category>
      <media:keywords>sunset, river, ice, reeds, winter, reflection</media:keywords>
      <media:copyright url=""http://johnloguk.smugmug.com"">John Bennett</media:copyright>
      <media:credit role=""photographer"">John Bennett</media:credit>
    </item>
    <item>
      <title>Day 17 - A hummingbird exhibits amazing acrobatic flying skills</title>
      <link>http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=424495512&amp;k=hbkLn</link>
      <description>&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 17 - A hummingbird exhibits amazing acrobatic flying skills&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=424495512&amp;k=hbkLn"" title=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Th-2.jpg"" width=""150"" height=""146"" alt=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills"" title=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily shots for the dailies forum on Dgrin</category>
      <pubDate>Tue, 25 Nov 2008 13:11:25 -0800</pubDate>
      <author>nobody@smugmug.com (fotoeffects)</author>
      <guid isPermaLink=""false"">http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2008-08-22 08:53:19</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Ti-2.jpg"" fileSize=""3856"" type=""image/jpeg"" medium=""image"" width=""100"" height=""98"">
          <media:hash algo=""md5"">b997f19a166ac2ad722e318f7f9f46f5</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Th-2.jpg"" fileSize=""6294"" type=""image/jpeg"" medium=""image"" width=""150"" height=""146"" isDefault=""true"">
          <media:hash algo=""md5"">9dbe15007ac79079e597e36314c281bb</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-S-2.jpg"" fileSize=""20451"" type=""image/jpeg"" medium=""image"" width=""307"" height=""300"">
          <media:hash algo=""md5"">7e4bb31f22d8c76d5acde6d41ba387cb</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-M-2.jpg"" fileSize=""33859"" type=""image/jpeg"" medium=""image"" width=""461"" height=""450"">
          <media:hash algo=""md5"">97b54fdcffacc9d944392499b860a918</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-L-2.jpg"" fileSize=""52727"" type=""image/jpeg"" medium=""image"" width=""615"" height=""600"">
          <media:hash algo=""md5"">f8f0e4ab676813715cf6bd8ff36d149e</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-XL-2.jpg"" fileSize=""83356"" type=""image/jpeg"" medium=""image"" width=""787"" height=""768"">
          <media:hash algo=""md5"">2d4e6732cb92929e3046647957ee81fe</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-X2-2.jpg"" fileSize=""112470"" type=""image/jpeg"" medium=""image"" width=""983"" height=""960"">
          <media:hash algo=""md5"">d449525b222f9d95acfbd6f71f77bab2</media:hash>
        </media:content>
        <media:content url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-X3-2.jpg"" fileSize=""173219"" type=""image/jpeg"" medium=""image"" width=""1229"" height=""1200"">
          <media:hash algo=""md5"">108aac1e6c8afde9cfa5518b1bb2776d</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Day 17 - A hummingbird exhibits amazing acrobatic flying skills</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com""&gt;fotoeffects&lt;/a&gt;&lt;br /&gt;Day 17 - A hummingbird exhibits amazing acrobatic flying skills&lt;/p&gt;&lt;p&gt;&lt;a href=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/6928550_9gMRmv#!i=424495512&amp;k=hbkLn"" title=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills""&gt;&lt;img src=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Th-2.jpg"" width=""150"" height=""146"" alt=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills"" title=""Day 17 - A hummingbird exhibits amazing acrobatic flying skills"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://fotoeffects.smugmug.com/Daily-shots-for-the-dailies/Dailies/DSC6486/424495512_hbkLn-Th-2.jpg"" width=""150"" height=""146""/>
      <media:category>Daily shots for the dailies forum on Dgrin</media:category>
      <media:keywords>bird, hummingbird, animal, nature, rufous</media:keywords>
      <media:copyright url=""http://fotoeffects.smugmug.com"">fotoeffects</media:copyright>
      <media:credit role=""photographer"">fotoeffects</media:credit>
    </item>
    <item>
      <title>Model :  Kharu
Makeup by Kitten Capili</title>
      <link>http://elr.smugmug.com/Models/Portfolio-Female-Models/2727047_pm26ws#!i=83526137&amp;k=PLVPq</link>
      <description>&lt;p&gt;&lt;a href=""http://elr.smugmug.com""&gt;Edlin Roguel&lt;/a&gt;&lt;br /&gt;Model :  Kharu
Makeup by Kitten Capili&lt;/p&gt;&lt;p&gt;&lt;a href=""http://elr.smugmug.com/Models/Portfolio-Female-Models/2727047_pm26ws#!i=83526137&amp;k=PLVPq"" title=""Model :  Kharu
Makeup by Kitten Capili""&gt;&lt;img src=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Th-1.jpg"" width=""150"" height=""105"" alt=""Model :  Kharu
Makeup by Kitten Capili"" title=""Model :  Kharu
Makeup by Kitten Capili"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Models</category>
      <pubDate>Sun, 23 Jul 2006 18:25:06 -0700</pubDate>
      <author>nobody@smugmug.com (Edlin Roguel)</author>
      <guid isPermaLink=""false"">http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2006-07-22 09:32:49</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Ti-1.jpg"" fileSize=""4235"" type=""image/jpeg"" medium=""image"" width=""100"" height=""70"">
          <media:hash algo=""md5"">e171d5a4fb6cf9076e4c6bfab1fb5f31</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Th-1.jpg"" fileSize=""7252"" type=""image/jpeg"" medium=""image"" width=""150"" height=""105"" isDefault=""true"">
          <media:hash algo=""md5"">01567c94ebe68dee3416cddcd9233857</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-S-1.jpg"" fileSize=""29402"" type=""image/jpeg"" medium=""image"" width=""400"" height=""279"">
          <media:hash algo=""md5"">f7a2b8c74c7050ed1496e9e6decd76bf</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-M-1.jpg"" fileSize=""60136"" type=""image/jpeg"" medium=""image"" width=""600"" height=""418"">
          <media:hash algo=""md5"">d35f2e425ff12f6de5f84a1c7a72f436</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-L-1.jpg"" fileSize=""111364"" type=""image/jpeg"" medium=""image"" width=""800"" height=""558"">
          <media:hash algo=""md5"">fc18f29d738535bb7ec8ae66dddfbbb6</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-XL-1.jpg"" fileSize=""174775"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""714"">
          <media:hash algo=""md5"">d9e455527e34145753575bbe36f3a093</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-X2-1.jpg"" fileSize=""273161"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""892"">
          <media:hash algo=""md5"">c25651239561fe0c1b5b57caee92bba6</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-X3-1.jpg"" fileSize=""441481"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1115"">
          <media:hash algo=""md5"">bfa0e9907d2f8193251b050fe365a82f</media:hash>
        </media:content>
        <media:content url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-O-1.jpg"" fileSize=""6206279"" type=""image/jpeg"" medium=""image"" width=""3268"" height=""2278"">
          <media:hash algo=""md5"">d84c2ea29ec512187d8074789b24fce6</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Model :  Kharu
Makeup by Kitten Capili</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://elr.smugmug.com""&gt;Edlin Roguel&lt;/a&gt;&lt;br /&gt;Model :  Kharu
Makeup by Kitten Capili&lt;/p&gt;&lt;p&gt;&lt;a href=""http://elr.smugmug.com/Models/Portfolio-Female-Models/2727047_pm26ws#!i=83526137&amp;k=PLVPq"" title=""Model :  Kharu
Makeup by Kitten Capili""&gt;&lt;img src=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Th-1.jpg"" width=""150"" height=""105"" alt=""Model :  Kharu
Makeup by Kitten Capili"" title=""Model :  Kharu
Makeup by Kitten Capili"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://elr.smugmug.com/Models/Portfolio-Female-Models/Kharu001/83526137_PLVPq-Th-1.jpg"" width=""150"" height=""105""/>
      <media:category>Models</media:category>
      <media:keywords>kharu, model, beauty, portrait, glamour</media:keywords>
      <media:copyright url=""http://photo.roguel.com"">Edlin Roguel</media:copyright>
      <media:credit role=""photographer"">Edlin Roguel</media:credit>
    </item>
    <item>
      <title>POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ...</title>
      <link>http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145308&amp;k=BmaMx</link>
      <description>&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com""&gt;Dennis Fast&lt;/a&gt;&lt;br /&gt;POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of early sun and frost in the air took my breath away.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145308&amp;k=BmaMx"" title=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ...""&gt;&lt;img src=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Th-5.jpg"" width=""150"" height=""113"" alt=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ..."" title=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Animals</category>
      <pubDate>Sat, 08 Sep 2007 12:46:18 -0700</pubDate>
      <author>nobody@smugmug.com (Dennis Fast)</author>
      <guid isPermaLink=""false"">http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Th-5.jpg</guid>
      <media:group>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Ti-5.jpg"" fileSize=""5309"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">ba2a035c58f5cb4bbb4087dc77ec2fe0</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Th-5.jpg"" fileSize=""10481"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">4510c7856a1ee0575e4a51164319dc01</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-S-5.jpg"" fileSize=""65204"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">e7043c567ef348d6b231a2a559f1a3b6</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-M-5.jpg"" fileSize=""130251"" type=""image/jpeg"" medium=""image"" width=""600"" height=""450"">
          <media:hash algo=""md5"">6dc66d0b4ad4e365e08daeb48c4ea7a1</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-L-5.jpg"" fileSize=""216264"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">b7a92fc77de44861dcd707e267edb685</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com""&gt;Dennis Fast&lt;/a&gt;&lt;br /&gt;POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of early sun and frost in the air took my breath away.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145308&amp;k=BmaMx"" title=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ...""&gt;&lt;img src=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Th-5.jpg"" width=""150"" height=""113"" alt=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ..."" title=""POLAR BEAR IN MORNING SUN AND FOG.

This curious polar bear showed up one morning near Dymond Lake Lodge where I was staying at not far from Churchill, Manitoba, Canada. The combination of e ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF0114-Edit/193145308_BmaMx-Th-5.jpg"" width=""150"" height=""113""/>
      <media:category>Animals</media:category>
      <media:keywords>polar bear, bear, frost, morning, sunrise, boreal, tundra, arctic, hoarfrost, power, bears, carnivores, canada, churchill, hubbart point, hudson bay, ice bear, nanook, nanuk, snow bear, ursus maritimus, wapusk, white bear</media:keywords>
      <media:copyright url=""http://www.dennisfast.com"">Dennis Fast</media:copyright>
      <media:credit role=""photographer"">Dennis Fast</media:credit>
    </item>
    <item>
      <title>Palmtrees at sunset - Palmwag. Namibia</title>
      <link>http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/5016477_dRfr8K#!i=475670182&amp;k=j2wTe</link>
      <description>&lt;p&gt;&lt;a href=""http://prayingmantisphotography.smugmug.com""&gt;Christina (PrayingMantisPhotography)&lt;/a&gt;&lt;br /&gt;Palmtrees at sunset - Palmwag. Namibia&lt;/p&gt;&lt;p&gt;&lt;a href=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/5016477_dRfr8K#!i=475670182&amp;k=j2wTe"" title=""Palmtrees at sunset - Palmwag. Namibia""&gt;&lt;img src=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Th.jpg"" width=""150"" height=""150"" alt=""Palmtrees at sunset - Palmwag. Namibia"" title=""Palmtrees at sunset - Palmwag. Namibia"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Tue, 17 Feb 2009 12:18:03 -0800</pubDate>
      <author>nobody@smugmug.com (Christina (PrayingMantisPhotography))</author>
      <guid isPermaLink=""false"">http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Th.jpg</guid>
      <exif:DateTimeOriginal>2009-02-12 19:45:23</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Ti.jpg"" fileSize=""5665"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">b04c13a316be7a322487606f2987736a</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Th.jpg"" fileSize=""10180"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">c52dac2a1e21fb1f576cfe7ebb4a228b</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-S.jpg"" fileSize=""35155"" type=""image/jpeg"" medium=""image"" width=""400"" height=""269"">
          <media:hash algo=""md5"">e4de4df9c288b224a167b39e97250ca6</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-M.jpg"" fileSize=""65173"" type=""image/jpeg"" medium=""image"" width=""600"" height=""404"">
          <media:hash algo=""md5"">49404ee93efacfd10617674a4cddf53b</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-L.jpg"" fileSize=""103052"" type=""image/jpeg"" medium=""image"" width=""800"" height=""539"">
          <media:hash algo=""md5"">dd5119b6e6097c4b24f95598eb17af91</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-XL.jpg"" fileSize=""152978"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""689"">
          <media:hash algo=""md5"">39abd9c21971c5ee04865c07798c5995</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-X2.jpg"" fileSize=""217670"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""862"">
          <media:hash algo=""md5"">b74460a4dfd6cee45c82fb71ce0e6b54</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-X3.jpg"" fileSize=""303687"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1077"">
          <media:hash algo=""md5"">15056205ba9046fa71af7cfb11fed29d</media:hash>
        </media:content>
        <media:content url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-O.jpg"" fileSize=""192833"" type=""image/jpeg"" medium=""image"" width=""3508"" height=""2362"">
          <media:hash algo=""md5"">e63da6f1c01de4db53ef90e1828bed4d</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Palmtrees at sunset - Palmwag. Namibia</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://prayingmantisphotography.smugmug.com""&gt;Christina (PrayingMantisPhotography)&lt;/a&gt;&lt;br /&gt;Palmtrees at sunset - Palmwag. Namibia&lt;/p&gt;&lt;p&gt;&lt;a href=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/5016477_dRfr8K#!i=475670182&amp;k=j2wTe"" title=""Palmtrees at sunset - Palmwag. Namibia""&gt;&lt;img src=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Th.jpg"" width=""150"" height=""150"" alt=""Palmtrees at sunset - Palmwag. Namibia"" title=""Palmtrees at sunset - Palmwag. Namibia"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://prayingmantisphotography.smugmug.com/Other/Sunsets-Sunrise/IMG989867-copy/475670182_j2wTe-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>palm trees, sunset, namibia</media:keywords>
      <media:copyright url=""http://prayingmantisphotography.smugmug.com"">Christina (PrayingMantisPhotography)</media:copyright>
      <media:credit role=""photographer"">Christina (PrayingMantisPhotography)</media:credit>
    </item>
    <item>
      <title>Chimera from Notre-Dame cathedral at night, Paris, France.</title>
      <link>http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/1349345_SNCrnr#!i=63668958&amp;k=vUyG6</link>
      <description>&lt;p&gt;&lt;a href=""http://stevenlevourch.smugmug.com""&gt;Steven Le Vourch&lt;/a&gt;&lt;br /&gt;Chimera from Notre-Dame cathedral at night, Paris, France.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/1349345_SNCrnr#!i=63668958&amp;k=vUyG6"" title=""Chimera from Notre-Dame cathedral at night, Paris, France.""&gt;&lt;img src=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Th-2.jpg"" width=""150"" height=""113"" alt=""Chimera from Notre-Dame cathedral at night, Paris, France."" title=""Chimera from Notre-Dame cathedral at night, Paris, France."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Urban landscapes at night</category>
      <pubDate>Sun, 09 Apr 2006 14:56:22 -0700</pubDate>
      <author>nobody@smugmug.com (Steven Le Vourch)</author>
      <guid isPermaLink=""false"">http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2003-07-19 22:16:09</exif:DateTimeOriginal>
      <geo:lat>48.85306089660000</geo:lat>
      <geo:long>2.34919667244000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Ti-2.jpg"" fileSize=""6141"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">229e7969d7c17cd09576416d1667a912</media:hash>
        </media:content>
        <media:content url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Th-2.jpg"" fileSize=""10888"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">753683dd71dfae2e91e86706f3c638ea</media:hash>
        </media:content>
        <media:content url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-S-2.jpg"" fileSize=""57615"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">a4f5350bb00ea3697300d9a691398832</media:hash>
        </media:content>
        <media:content url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-M-2.jpg"" fileSize=""115611"" type=""image/jpeg"" medium=""image"" width=""600"" height=""450"">
          <media:hash algo=""md5"">43925decb6fd4302257e6bc0fc0a6c30</media:hash>
        </media:content>
        <media:content url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-L-2.jpg"" fileSize=""193773"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">2885e60ea3ab200c531b1e114a3b6238</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Chimera from Notre-Dame cathedral at night, Paris, France.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://stevenlevourch.smugmug.com""&gt;Steven Le Vourch&lt;/a&gt;&lt;br /&gt;Chimera from Notre-Dame cathedral at night, Paris, France.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/1349345_SNCrnr#!i=63668958&amp;k=vUyG6"" title=""Chimera from Notre-Dame cathedral at night, Paris, France.""&gt;&lt;img src=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Th-2.jpg"" width=""150"" height=""113"" alt=""Chimera from Notre-Dame cathedral at night, Paris, France."" title=""Chimera from Notre-Dame cathedral at night, Paris, France."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://stevenlevourch.smugmug.com/Urban-landscapes-at-night/Paris-at-night/DSCN2170-copy/63668958_vUyG6-Th-2.jpg"" width=""150"" height=""113""/>
      <media:category>Urban landscapes at night</media:category>
      <media:keywords>notredame, paris, eiffel, tower, chimera, gargouille, cathedral, france, night, blue, eiffel tower</media:keywords>
      <media:copyright url=""http://stevenlevourch.smugmug.com"">Steven Le Vourch</media:copyright>
      <media:credit role=""photographer"">Steven Le Vourch</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=473699159&amp;k=3Y8LF</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=473699159&amp;k=3Y8LF"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Th-5.jpg"" width=""150"" height=""99"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sat, 14 Feb 2009 17:25:55 -0800</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Th-5.jpg</guid>
      <exif:DateTimeOriginal>2009-02-14 13:23:47</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Ti-5.jpg"" fileSize=""5117"" type=""image/jpeg"" medium=""image"" width=""100"" height=""66"">
          <media:hash algo=""md5"">624157525e12062071f68843660bbfdc</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Th-5.jpg"" fileSize=""9342"" type=""image/jpeg"" medium=""image"" width=""150"" height=""99"" isDefault=""true"">
          <media:hash algo=""md5"">b144f3650b59043efa4015ba8e822183</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-S-5.jpg"" fileSize=""51201"" type=""image/jpeg"" medium=""image"" width=""400"" height=""264"">
          <media:hash algo=""md5"">e7a72771412eb74b5429198c9640f2e6</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-M-5.jpg"" fileSize=""98543"" type=""image/jpeg"" medium=""image"" width=""600"" height=""396"">
          <media:hash algo=""md5"">2b35e8c30347118ec57f8fab6bc5d2a0</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-L-5.jpg"" fileSize=""157767"" type=""image/jpeg"" medium=""image"" width=""800"" height=""529"">
          <media:hash algo=""md5"">7f9f124da80695646755263cff746eef</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=473699159&amp;k=3Y8LF"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Th-5.jpg"" width=""150"" height=""99"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG7306/473699159_3Y8LF-Th-5.jpg"" width=""150"" height=""99""/>
      <media:category>Other</media:category>
      <media:keywords>chopin, rose, music, sheet music, piano music, still life</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Mara Giraffe Sunset</title>
      <link>http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/6556994_fwDgbT#!i=427709767&amp;k=ga2To</link>
      <description>&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com""&gt;Cindy Cone&lt;/a&gt;&lt;br /&gt;Mara Giraffe Sunset&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/6556994_fwDgbT#!i=427709767&amp;k=ga2To"" title=""Mara Giraffe Sunset""&gt;&lt;img src=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Th-3.jpg"" width=""150"" height=""150"" alt=""Mara Giraffe Sunset"" title=""Mara Giraffe Sunset"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Travel</category>
      <pubDate>Sun, 30 Nov 2008 16:46:42 -0800</pubDate>
      <author>nobody@smugmug.com (Cindy Cone)</author>
      <guid isPermaLink=""false"">http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2007-10-16 18:14:09</exif:DateTimeOriginal>
      <geo:lat>-1.36114666925000</geo:lat>
      <geo:long>34.98046875000000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Ti-3.jpg"" fileSize=""4934"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">530c5ee684a52ee5046cc5645a9651e2</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Th-3.jpg"" fileSize=""9255"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">eed2a47f69abcfd48ea23e3a843693d6</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-S-3.jpg"" fileSize=""35944"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">025f8e2e8cc84ef56fc9577f5c5d6c01</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-M-3.jpg"" fileSize=""68679"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">137bce8e7588a1df7fdb981c6ea75e8c</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-L-3.jpg"" fileSize=""112382"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">6a63a082c7c0f825b5df7f373a45ea70</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-XL-3.jpg"" fileSize=""172230"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">c11e156327e0122058391e0d94e62505</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-X2-3.jpg"" fileSize=""264042"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""854"">
          <media:hash algo=""md5"">d319e6c480374a0a2bc5d7a7534fba85</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Mara Giraffe Sunset</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com""&gt;Cindy Cone&lt;/a&gt;&lt;br /&gt;Mara Giraffe Sunset&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/6556994_fwDgbT#!i=427709767&amp;k=ga2To"" title=""Mara Giraffe Sunset""&gt;&lt;img src=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Th-3.jpg"" width=""150"" height=""150"" alt=""Mara Giraffe Sunset"" title=""Mara Giraffe Sunset"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://cindycone.smugmug.com/Travel/Kenya-Photo-Safari/giraffe3257/427709767_ga2To-Th-3.jpg"" width=""150"" height=""150""/>
      <media:category>Travel</media:category>
      <media:keywords>giraffes, masai mara, sunset, kenya, safari, savannah, africa, wildlife, giraffe</media:keywords>
      <media:copyright url=""http://www.cindyconephotography.com"">Cindy Cone</media:copyright>
      <media:credit role=""photographer"">Cindy Cone</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Landscapes/Waterfalls/9187927_7TXQpW#!i=999939017&amp;k=kHBy8</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/9187927_7TXQpW#!i=999939017&amp;k=kHBy8"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Th-2.jpg"" width=""150"" height=""85"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Wed, 08 Sep 2010 13:09:32 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2010-06-11 18:45:38</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Ti-2.jpg"" fileSize=""2904"" type=""image/jpeg"" medium=""image"" width=""100"" height=""57"">
          <media:hash algo=""md5"">71476790b769c9829e2d4c9695b504db</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Th-2.jpg"" fileSize=""4331"" type=""image/jpeg"" medium=""image"" width=""150"" height=""85"" isDefault=""true"">
          <media:hash algo=""md5"">9a38441f6e6c965fb7f9a82a3c0e5719</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-S-2.jpg"" fileSize=""21799"" type=""image/jpeg"" medium=""image"" width=""400"" height=""227"">
          <media:hash algo=""md5"">4c1b908e603b0142a86555757a4cbae1</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-M-2.jpg"" fileSize=""38472"" type=""image/jpeg"" medium=""image"" width=""600"" height=""341"">
          <media:hash algo=""md5"">a94d41eddfb6fc2dbc1be35881a15e79</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-L-2.jpg"" fileSize=""60151"" type=""image/jpeg"" medium=""image"" width=""800"" height=""454"">
          <media:hash algo=""md5"">7ad253559920592f6467562cd1f77d8f</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/9187927_7TXQpW#!i=999939017&amp;k=kHBy8"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Th-2.jpg"" width=""150"" height=""85"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Landscapes/Waterfalls/IMG3236pan2/999939017_kHBy8-Th-2.jpg"" width=""150"" height=""85""/>
      <media:category>Landscapes</media:category>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>TheCuriousCamel's photo</title>
      <link>http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/1554529_n8MZHZ#!i=72551566&amp;k=w5NTR</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/1554529_n8MZHZ#!i=72551566&amp;k=w5NTR"" title=""TheCuriousCamel's photo""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Th-2.jpg"" width=""150"" height=""113"" alt=""TheCuriousCamel's photo"" title=""TheCuriousCamel's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Flowers</category>
      <pubDate>Wed, 31 May 2006 03:31:55 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2005-06-20 10:33:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Ti-2.jpg"" fileSize=""4530"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">732af38988bd9d3c90339abd4641f8ec</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Th-2.jpg"" fileSize=""7979"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">d3da179f8e93d818bce9904ac2e8ead3</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-S-2.jpg"" fileSize=""37474"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">f6558227c958dd7a2fe60b09f3df3842</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-M-2.jpg"" fileSize=""71126"" type=""image/jpeg"" medium=""image"" width=""599"" height=""450"">
          <media:hash algo=""md5"">885dc6b5a3815af2e1cdf75f592b9f42</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-L-2.jpg"" fileSize=""122620"" type=""image/jpeg"" medium=""image"" width=""799"" height=""600"">
          <media:hash algo=""md5"">a80c2078c04044de908dbc87e0525d6d</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">TheCuriousCamel's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/1554529_n8MZHZ#!i=72551566&amp;k=w5NTR"" title=""TheCuriousCamel's photo""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Th-2.jpg"" width=""150"" height=""113"" alt=""TheCuriousCamel's photo"" title=""TheCuriousCamel's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Flowers/Roses/Roses-roses-and-more-roses/DSCF3511-copy-2/72551566_w5NTR-Th-2.jpg"" width=""150"" height=""113""/>
      <media:category>Flowers</media:category>
      <media:keywords>rose, roses, flower, flowers, pink, petals pretty</media:keywords>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>Upper North Falls, Silver Falls State Park</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402484758&amp;k=XFLjH</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Upper North Falls, Silver Falls State Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402484758&amp;k=XFLjH"" title=""Upper North Falls, Silver Falls State Park""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Th-6.jpg"" width=""150"" height=""100"" alt=""Upper North Falls, Silver Falls State Park"" title=""Upper North Falls, Silver Falls State Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sat, 25 Oct 2008 22:18:50 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Th-6.jpg</guid>
      <exif:DateTimeOriginal>2008-10-23 18:10:58</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Ti-6.jpg"" fileSize=""5105"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">156ff98c469e0fdb132aa7aa61cc8468</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Th-6.jpg"" fileSize=""9571"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">906b07f26fa5d2d348ff9f9d79184a79</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-S-6.jpg"" fileSize=""58650"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">050991463d71144cafb0fdc8d280fd87</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-M-6.jpg"" fileSize=""119043"" type=""image/jpeg"" medium=""image"" width=""600"" height=""401"">
          <media:hash algo=""md5"">1562c76a8a4271abbba5c2bcd9ba3a09</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-L-6.jpg"" fileSize=""195417"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">6288eacc8312bfb3eba027c3f35d3e05</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Upper North Falls, Silver Falls State Park</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Upper North Falls, Silver Falls State Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402484758&amp;k=XFLjH"" title=""Upper North Falls, Silver Falls State Park""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Th-6.jpg"" width=""150"" height=""100"" alt=""Upper North Falls, Silver Falls State Park"" title=""Upper North Falls, Silver Falls State Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1652/402484758_XFLjH-Th-6.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>upper north falls, silver falls, oregon, waterfall, autumn, landscape, nature</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=542073237&amp;k=mhMvV</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=542073237&amp;k=mhMvV"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Th-3.jpg"" width=""100"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Wed, 20 May 2009 23:08:23 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Th-3.jpg</guid>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Ti-3.jpg"" fileSize=""4997"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">a2f684e1e98460905df87ace1f2a07e3</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Th-3.jpg"" fileSize=""9447"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">23f9bafee2da502203c97aab38b9e5b0</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-S-3.jpg"" fileSize=""35664"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">d4a16090d742e508aa9aa44b94020fbe</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-M-3.jpg"" fileSize=""71451"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">0bd8ba86cec6619f87cb66bd825046a5</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-L-3.jpg"" fileSize=""118672"" type=""image/jpeg"" medium=""image"" width=""401"" height=""600"">
          <media:hash algo=""md5"">4698b2ca9fdd1183856d8ef1a089fdc3</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=542073237&amp;k=mhMvV"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Th-3.jpg"" width=""100"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG2127HDR/542073237_mhMvV-Th-3.jpg"" width=""100"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>hdr, epcot, italy pavilion, disney world, arch, architecture</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ...</title>
      <link>http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/6268620_MbLVJN#!i=460820987&amp;k=M9wsk</link>
      <description>&lt;p&gt;&lt;a href=""http://agilitypics.smugmug.com""&gt;agilitypics&lt;/a&gt;&lt;br /&gt;Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. This little gal stole my heart instantly, good job she's already taken ;-) So I have to settle with some pictures of her.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/6268620_MbLVJN#!i=460820987&amp;k=M9wsk"" title=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ...""&gt;&lt;img src=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Th.jpg"" width=""150"" height=""100"" alt=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ..."" title=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily shot</category>
      <pubDate>Sat, 24 Jan 2009 12:29:59 -0800</pubDate>
      <author>nobody@smugmug.com (agilitypics)</author>
      <guid isPermaLink=""false"">http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Th.jpg</guid>
      <exif:DateTimeOriginal>2009-01-24 15:20:31</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Ti.jpg"" fileSize=""3010"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">8b65b8fee63ed73f1c1e70be75ce6db8</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Th.jpg"" fileSize=""5156"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">f927d18d8b443e6903183f5b8814b492</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-S.jpg"" fileSize=""27775"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">832172fcd4d87caa553dd4643f148870</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-M.jpg"" fileSize=""52581"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">759dbdd89f71d93870b6c1f8fc525db7</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-L.jpg"" fileSize=""84407"" type=""image/jpeg"" medium=""image"" width=""800"" height=""536"">
          <media:hash algo=""md5"">6d73da3304267b44cfd4e767be3e8e36</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-XL.jpg"" fileSize=""128337"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""685"">
          <media:hash algo=""md5"">ef4fd367a4b203b38ac82b097b671e2c</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-X2.jpg"" fileSize=""188057"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""857"">
          <media:hash algo=""md5"">0fe9f81e2717a5b501cc491b155cd9c1</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-X3.jpg"" fileSize=""268631"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1071"">
          <media:hash algo=""md5"">f816f6308e62acce43d7e4298cadbf40</media:hash>
        </media:content>
        <media:content url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-O.jpg"" fileSize=""590905"" type=""image/jpeg"" medium=""image"" width=""1936"" height=""1296"">
          <media:hash algo=""md5"">46e794805d4171de9457677d36ef682b</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://agilitypics.smugmug.com""&gt;agilitypics&lt;/a&gt;&lt;br /&gt;Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. This little gal stole my heart instantly, good job she's already taken ;-) So I have to settle with some pictures of her.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/6268620_MbLVJN#!i=460820987&amp;k=M9wsk"" title=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ...""&gt;&lt;img src=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Th.jpg"" width=""150"" height=""100"" alt=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ..."" title=""Nana, a very cute puppy - Jan 24 2009

Today my Caroline had to do her license test agility with Blue (which they passed easily), time to see our agility friend again and some new puppies. T ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://agilitypics.smugmug.com/Daily-shot/A-shot-a-day/2009-01-24/460820987_M9wsk-Th.jpg"" width=""150"" height=""100""/>
      <media:category>Daily shot</media:category>
      <media:keywords>puppy, border_collie</media:keywords>
      <media:copyright url=""http://agilitypics.smugmug.com"">agilitypics</media:copyright>
      <media:credit role=""photographer"">agilitypics</media:credit>
    </item>
    <item>
      <title>Saw Whet Owl</title>
      <link>http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/10094038_7VfRNX#!i=692835783&amp;k=3C5nA</link>
      <description>&lt;p&gt;&lt;a href=""http://georgino.smugmug.com""&gt;Georgino&lt;/a&gt;&lt;br /&gt;Saw Whet Owl&lt;/p&gt;&lt;p&gt;&lt;a href=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/10094038_7VfRNX#!i=692835783&amp;k=3C5nA"" title=""Saw Whet Owl""&gt;&lt;img src=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Th-1.jpg"" width=""150"" height=""150"" alt=""Saw Whet Owl"" title=""Saw Whet Owl"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature &amp; Wildlife</category>
      <pubDate>Sun, 25 Oct 2009 20:39:57 -0700</pubDate>
      <author>nobody@smugmug.com (Georgino)</author>
      <guid isPermaLink=""false"">http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2009-10-23 12:58:03</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Ti-1.jpg"" fileSize=""9160"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">5589da2ffa2d5d5c6fa1e26cce81ec8e</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Th-1.jpg"" fileSize=""14683"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">2bc53a0752dede1bc22c6cb5f4194688</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-S-1.jpg"" fileSize=""47092"" type=""image/jpeg"" medium=""image"" width=""400"" height=""266"">
          <media:hash algo=""md5"">d543168bd4d3513ae8c7afa61ba55574</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-M-1.jpg"" fileSize=""88203"" type=""image/jpeg"" medium=""image"" width=""600"" height=""399"">
          <media:hash algo=""md5"">29a7f7866e6911c9c65cdb5cf05e2536</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-L-1.jpg"" fileSize=""141824"" type=""image/jpeg"" medium=""image"" width=""800"" height=""532"">
          <media:hash algo=""md5"">b4d6ceefeea21f788c0a23e9b4ecf340</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-XL-1.jpg"" fileSize=""217554"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""681"">
          <media:hash algo=""md5"">296e52e0f68bc90b5fffcdf63eb34139</media:hash>
        </media:content>
        <media:content url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-X2-1.jpg"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""681""/>
      </media:group>
      <media:title type=""html"">Saw Whet Owl</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://georgino.smugmug.com""&gt;Georgino&lt;/a&gt;&lt;br /&gt;Saw Whet Owl&lt;/p&gt;&lt;p&gt;&lt;a href=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/10094038_7VfRNX#!i=692835783&amp;k=3C5nA"" title=""Saw Whet Owl""&gt;&lt;img src=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Th-1.jpg"" width=""150"" height=""150"" alt=""Saw Whet Owl"" title=""Saw Whet Owl"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://georgino.smugmug.com/Nature-1/Birds/Mountsberg/GEO2391WEB/692835783_3C5nA-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Nature &amp; Wildlife</media:category>
      <media:keywords>bird, mountsberg, raptor, saw whet owl, ray barlow workshop</media:keywords>
      <media:copyright url=""http://www.georgenagy.ca"">Georgino</media:copyright>
      <media:credit role=""photographer"">Georgino</media:credit>
    </item>
    <item>
      <title>Evening at Laguna Beach</title>
      <link>http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=434470314&amp;k=qGEsM</link>
      <description>&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com""&gt;Michael Weitzman (micalngelo)&lt;/a&gt;&lt;br /&gt;Evening at Laguna Beach&lt;/p&gt;&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=434470314&amp;k=qGEsM"" title=""Evening at Laguna Beach""&gt;&lt;img src=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Th-1.jpg"" width=""150"" height=""150"" alt=""Evening at Laguna Beach"" title=""Evening at Laguna Beach"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Wed, 10 Dec 2008 21:04:13 -0800</pubDate>
      <author>nobody@smugmug.com (Michael Weitzman (micalngelo))</author>
      <guid isPermaLink=""false"">http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-12-10 17:57:50</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Ti-1.jpg"" fileSize=""5013"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">7b1ad51d4646acf2de941f7e2471766a</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Th-1.jpg"" fileSize=""9031"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">13e99a75fa602dfa036760a042433fa5</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-S-1.jpg"" fileSize=""33422"" type=""image/jpeg"" medium=""image"" width=""400"" height=""266"">
          <media:hash algo=""md5"">0a4674c9f0e81397d7f7a6aae282cb25</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-M-1.jpg"" fileSize=""61148"" type=""image/jpeg"" medium=""image"" width=""600"" height=""399"">
          <media:hash algo=""md5"">05857eab83a766a79619fda432379779</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-L-1.jpg"" fileSize=""97711"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">e3002ef3ebac5c91108447259144e348</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-XL-1.jpg"" fileSize=""145662"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""682"">
          <media:hash algo=""md5"">7891a35386fffaeac26e0175646abf54</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-X2-1.jpg"" fileSize=""207974"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""852"">
          <media:hash algo=""md5"">2eaf34888ef1527304260d80893bcc8e</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-X3-1.jpg"" fileSize=""295210"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1065"">
          <media:hash algo=""md5"">d4201b5df7060b29b412934455e80ccc</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-O-1.jpg"" fileSize=""4371121"" type=""image/jpeg"" medium=""image"" width=""4256"" height=""2832"">
          <media:hash algo=""md5"">103cd7360f32628565897cb92634a068</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Evening at Laguna Beach</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com""&gt;Michael Weitzman (micalngelo)&lt;/a&gt;&lt;br /&gt;Evening at Laguna Beach&lt;/p&gt;&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=434470314&amp;k=qGEsM"" title=""Evening at Laguna Beach""&gt;&lt;img src=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Th-1.jpg"" width=""150"" height=""150"" alt=""Evening at Laguna Beach"" title=""Evening at Laguna Beach"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/MWA6670-1/434470314_qGEsM-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>lagunabeach, ocean</media:keywords>
      <media:copyright url=""http://www.studiomwphotography.com"">Michael Weitzman (micalngelo)</media:copyright>
      <media:credit role=""photographer"">Michael Weitzman (micalngelo)</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=314623154&amp;k=8SDWD</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=314623154&amp;k=8SDWD"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Th-4.jpg"" width=""100"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Mon, 16 Jun 2008 22:11:18 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-06-15 19:39:45</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Ti-4.jpg"" fileSize=""4958"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">13ca5e87d245f9ef43cb3ef4da829c43</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Th-4.jpg"" fileSize=""9723"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">4982ef8f682520638f9e37905794f2c6</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-S-4.jpg"" fileSize=""40289"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">242378ba1f9cf237b0afd5eb01049e19</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-M-4.jpg"" fileSize=""85360"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">c9f64f5ecdbeeb3c3f2a4c3e227d3d22</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-L-4.jpg"" fileSize=""144084"" type=""image/jpeg"" medium=""image"" width=""401"" height=""600"">
          <media:hash algo=""md5"">c836477bef702c92d00b558ed3c4d3b3</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=314623154&amp;k=8SDWD"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Th-4.jpg"" width=""100"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG6735/314623154_8SDWD-Th-4.jpg"" width=""100"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>trees, utah, woods, forest, nature, landscape</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Ricketts Glen</title>
      <link>http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/5759864_8tsrkP#!i=356170724&amp;k=a2ZMA</link>
      <description>&lt;p&gt;&lt;a href=""http://chuckrobinson.smugmug.com""&gt;Chuck Robinson&lt;/a&gt;&lt;br /&gt;Ricketts Glen&lt;/p&gt;&lt;p&gt;&lt;a href=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/5759864_8tsrkP#!i=356170724&amp;k=a2ZMA"" title=""Ricketts Glen""&gt;&lt;img src=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Th.jpg"" width=""150"" height=""150"" alt=""Ricketts Glen"" title=""Ricketts Glen"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Thu, 21 Aug 2008 07:20:13 -0700</pubDate>
      <author>nobody@smugmug.com (Chuck Robinson)</author>
      <guid isPermaLink=""false"">http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-05-22 10:57:03</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Ti.jpg"" fileSize=""6956"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">f1bdf712446994483d39583d3485f53e</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Th.jpg"" fileSize=""13254"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">1b3ea9c833093db84f538e34ad02450f</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-S.jpg"" fileSize=""61589"" type=""image/jpeg"" medium=""image"" width=""400"" height=""269"">
          <media:hash algo=""md5"">e1d523d7c7bb956ae9f0de9c1445dd2b</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-M.jpg"" fileSize=""122875"" type=""image/jpeg"" medium=""image"" width=""600"" height=""403"">
          <media:hash algo=""md5"">48a20848d08196a096b98b06d17278dc</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-L.jpg"" fileSize=""200941"" type=""image/jpeg"" medium=""image"" width=""800"" height=""538"">
          <media:hash algo=""md5"">910a08764165992211595d807c4b2d70</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-XL.jpg"" fileSize=""313412"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""688"">
          <media:hash algo=""md5"">4d8c82bf89ca661ac2fde9ac5adb401e</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-X2.jpg"" fileSize=""444111"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""860"">
          <media:hash algo=""md5"">fc407bed53366f3b9d8e372e8fe6cc8e</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-X3.jpg"" fileSize=""646055"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1075"">
          <media:hash algo=""md5"">b27647626f05620cc659d4ea79f8216b</media:hash>
        </media:content>
        <media:content url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-O.jpg"" fileSize=""8961156"" type=""image/jpeg"" medium=""image"" width=""3880"" height=""2607"">
          <media:hash algo=""md5"">991f53da7a7bfb44f41c738b4e4306e2</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Ricketts Glen</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://chuckrobinson.smugmug.com""&gt;Chuck Robinson&lt;/a&gt;&lt;br /&gt;Ricketts Glen&lt;/p&gt;&lt;p&gt;&lt;a href=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/5759864_8tsrkP#!i=356170724&amp;k=a2ZMA"" title=""Ricketts Glen""&gt;&lt;img src=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Th.jpg"" width=""150"" height=""150"" alt=""Ricketts Glen"" title=""Ricketts Glen"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://chuckrobinson.smugmug.com/Photography/High-Dynamic-Range/HDR-25/356170724_a2ZMA-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Photography</media:category>
      <media:keywords>waterfalls, ricketts glenn, hdr, pennsylvania</media:keywords>
      <media:copyright url=""http://www.chuckrobinsonphoto.com"">Chuck Robinson</media:copyright>
      <media:credit role=""photographer"">Chuck Robinson</media:credit>
    </item>
    <item>
      <title>Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ...</title>
      <link>http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=264788734&amp;k=YZg3a</link>
      <description>&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=264788734&amp;k=YZg3a"" title=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Th-4.jpg"" width=""150"" height=""120"" alt=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ..."" title=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Tue, 11 Mar 2008 18:36:13 -0700</pubDate>
      <author>nobody@smugmug.com (Ken Conger)</author>
      <guid isPermaLink=""false"">http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-02-29 03:04:07</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Ti-4.jpg"" fileSize=""8461"" type=""image/jpeg"" medium=""image"" width=""100"" height=""80"">
          <media:hash algo=""md5"">56c693b6f5b5c5007344f776111e62d6</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Th-4.jpg"" fileSize=""13428"" type=""image/jpeg"" medium=""image"" width=""150"" height=""120"" isDefault=""true"">
          <media:hash algo=""md5"">24edde53166e2c794fcb2ec64b4af7c3</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-S-4.jpg"" fileSize=""51561"" type=""image/jpeg"" medium=""image"" width=""375"" height=""300"">
          <media:hash algo=""md5"">3209e25a240d0175adec206882184cbf</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-M-4.jpg"" fileSize=""92942"" type=""image/jpeg"" medium=""image"" width=""563"" height=""450"">
          <media:hash algo=""md5"">7c4a17092caed0a6a1aebdc43b99775a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=264788734&amp;k=YZg3a"" title=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Th-4.jpg"" width=""150"" height=""120"" alt=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ..."" title=""Fierce, bold, proud!  This immature Bald Eagle photograph was captured in Homer, Alaska (3/2008). &#13;
&#13;
This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded o ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Im-Mouth/264788734_YZg3a-Th-4.jpg"" width=""150"" height=""120""/>
      <media:category>Nature</media:category>
      <media:keywords>eagle, eagles, bald eagle, alaska</media:keywords>
      <media:copyright url=""http://www.kencongerphotography.com"">Ken Conger</media:copyright>
      <media:credit role=""photographer"">Ken Conger</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=250885375&amp;k=LBjdn</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=250885375&amp;k=LBjdn"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Th-5.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Mon, 04 Feb 2008 11:29:21 -0800</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Th-5.jpg</guid>
      <exif:DateTimeOriginal>2008-02-02 01:54:43</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Ti-5.jpg"" fileSize=""4397"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">db70c5611934464d7468db0379196489</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Th-5.jpg"" fileSize=""7567"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">b870db864bf89503415af90f2538548c</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-S-5.jpg"" fileSize=""41672"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">e1b034ac5332323ffbf2cee096bdadd2</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-M-5.jpg"" fileSize=""83237"" type=""image/jpeg"" medium=""image"" width=""600"" height=""401"">
          <media:hash algo=""md5"">7786049fc07392baa0423f749f54b3bd</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-L-5.jpg"" fileSize=""138808"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">cde4255ff7e83557e37faef4bc85246e</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=250885375&amp;k=LBjdn"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Th-5.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG49021/250885375_LBjdn-Th-5.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>utah, mountains, winter</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Now I lay me down to sleep....</title>
      <link>http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=350291688&amp;k=M3JPS</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;Now I lay me down to sleep....&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=350291688&amp;k=M3JPS"" title=""Now I lay me down to sleep....""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Th-3.jpg"" width=""150"" height=""150"" alt=""Now I lay me down to sleep...."" title=""Now I lay me down to sleep...."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Tue, 12 Aug 2008 03:53:42 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2008-08-07 07:54:35</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Ti-3.jpg"" fileSize=""4460"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">be1afa1b0ab800390a81458cadb39fd5</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Th-3.jpg"" fileSize=""8464"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">e7434be1a31967d1b75583f4fb197d95</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-S-3.jpg"" fileSize=""37000"" type=""image/jpeg"" medium=""image"" width=""384"" height=""300"">
          <media:hash algo=""md5"">56d0eef807489ab3444388c6943cba52</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-M-3.jpg"" fileSize=""77887"" type=""image/jpeg"" medium=""image"" width=""576"" height=""450"">
          <media:hash algo=""md5"">68931307d856998f674bd0a80992de88</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-L-3.jpg"" fileSize=""132894"" type=""image/jpeg"" medium=""image"" width=""769"" height=""600"">
          <media:hash algo=""md5"">a4b42ccda154a5ef03f93e9cb01a338e</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Now I lay me down to sleep....</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;Now I lay me down to sleep....&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=350291688&amp;k=M3JPS"" title=""Now I lay me down to sleep....""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Th-3.jpg"" width=""150"" height=""150"" alt=""Now I lay me down to sleep...."" title=""Now I lay me down to sleep...."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG9839/350291688_M3JPS-Th-3.jpg"" width=""150"" height=""150""/>
      <media:category>Daily Album</media:category>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>Horseshoe ablaze</title>
      <link>http://aaronmacomber.smugmug.com/Portfolio/Southwest/7535277_45ksQW#!i=486663335&amp;k=JMoXL</link>
      <description>&lt;p&gt;&lt;a href=""http://aaronmacomber.smugmug.com""&gt;Aaron Macomber&lt;/a&gt;&lt;br /&gt;Horseshoe ablaze&lt;/p&gt;&lt;p&gt;&lt;a href=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/7535277_45ksQW#!i=486663335&amp;k=JMoXL"" title=""Horseshoe ablaze""&gt;&lt;img src=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Th.jpg"" width=""150"" height=""150"" alt=""Horseshoe ablaze"" title=""Horseshoe ablaze"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portfolio</category>
      <pubDate>Sat, 07 Mar 2009 10:51:23 -0800</pubDate>
      <author>nobody@smugmug.com (Aaron Macomber)</author>
      <guid isPermaLink=""false"">http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Th.jpg</guid>
      <exif:DateTimeOriginal>2009-03-05 18:28:13</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Ti.jpg"" fileSize=""6162"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">9ff705e130d85e76efc123942c03222c</media:hash>
        </media:content>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Th.jpg"" fileSize=""11487"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">273c25462d7502de5a551557f64caf4f</media:hash>
        </media:content>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-S.jpg"" fileSize=""50495"" type=""image/jpeg"" medium=""image"" width=""400"" height=""266"">
          <media:hash algo=""md5"">51af3363276d1d5e713253a5fcf819aa</media:hash>
        </media:content>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-M.jpg"" fileSize=""101404"" type=""image/jpeg"" medium=""image"" width=""600"" height=""399"">
          <media:hash algo=""md5"">0fd1cb886f10524b61c5bdb455846b99</media:hash>
        </media:content>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-L.jpg"" fileSize=""162065"" type=""image/jpeg"" medium=""image"" width=""800"" height=""531"">
          <media:hash algo=""md5"">fb6b7c4ab327c052884168ba3f566362</media:hash>
        </media:content>
        <media:content url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-XL.jpg"" fileSize=""264135"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""680"">
          <media:hash algo=""md5"">1182a7b8f84c6bba0f5cd315d6bd9b58</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Horseshoe ablaze</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://aaronmacomber.smugmug.com""&gt;Aaron Macomber&lt;/a&gt;&lt;br /&gt;Horseshoe ablaze&lt;/p&gt;&lt;p&gt;&lt;a href=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/7535277_45ksQW#!i=486663335&amp;k=JMoXL"" title=""Horseshoe ablaze""&gt;&lt;img src=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Th.jpg"" width=""150"" height=""150"" alt=""Horseshoe ablaze"" title=""Horseshoe ablaze"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://aaronmacomber.smugmug.com/Portfolio/Southwest/HorseshoeFireSMUG/486663335_JMoXL-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Portfolio</media:category>
      <media:keywords>horseshoe bend, page arizona, southwest, colorado river, grand canyon, 9 mile camp</media:keywords>
      <media:copyright url=""http://www.aaronmacomber.com"">Aaron Macomber</media:copyright>
      <media:credit role=""photographer"">Aaron Macomber</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402450561&amp;k=96PwQ</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402450561&amp;k=96PwQ"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Th-6.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Sat, 25 Oct 2008 21:33:18 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Th-6.jpg</guid>
      <exif:DateTimeOriginal>2008-10-22 15:43:01</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Ti-6.jpg"" fileSize=""5872"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">629f1ca5b89b011fe35ec09b9d724c70</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Th-6.jpg"" fileSize=""11395"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">59e5134d54d083f5833f1d0bffde763a</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-S-6.jpg"" fileSize=""73806"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">57bfc787a7b734fb2260a749c62824eb</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-M-6.jpg"" fileSize=""153378"" type=""image/jpeg"" medium=""image"" width=""600"" height=""401"">
          <media:hash algo=""md5"">fc2846237279053990c099e95348ce09</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-L-6.jpg"" fileSize=""252229"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">6690ca09aa5a020688f205a5fb5e7c91</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=402450561&amp;k=96PwQ"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Th-6.jpg"" width=""150"" height=""100"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG1325/402450561_96PwQ-Th-6.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada</title>
      <link>http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145524&amp;k=3HpHz</link>
      <description>&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com""&gt;Dennis Fast&lt;/a&gt;&lt;br /&gt;SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145524&amp;k=3HpHz"" title=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada""&gt;&lt;img src=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Th-5.jpg"" width=""150"" height=""100"" alt=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada"" title=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Animals</category>
      <pubDate>Sat, 08 Sep 2007 12:47:00 -0700</pubDate>
      <author>nobody@smugmug.com (Dennis Fast)</author>
      <guid isPermaLink=""false"">http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Th-5.jpg</guid>
      <media:group>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Ti-5.jpg"" fileSize=""5050"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">aece212cd8d3323c35fd419ba76d9c14</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Th-5.jpg"" fileSize=""8936"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">c5fced659fdc1c7f34421e356d996cec</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-S-5.jpg"" fileSize=""50746"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">4c29a3357c13c39b2dfe649278057345</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-M-5.jpg"" fileSize=""96326"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">a37bf6ed344dd093b3fa9b99ab5ef9b3</media:hash>
        </media:content>
        <media:content url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-L-5.jpg"" fileSize=""156829"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">ef5b1efb0dbd459b585156b8b1b2fdc2</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com""&gt;Dennis Fast&lt;/a&gt;&lt;br /&gt;SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada&lt;/p&gt;&lt;p&gt;&lt;a href=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/3429627_h7jdNS#!i=193145524&amp;k=3HpHz"" title=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada""&gt;&lt;img src=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Th-5.jpg"" width=""150"" height=""100"" alt=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada"" title=""SLEEPY YOUNG RED FOX KIT.

A wary young red fox begins to nap outside its boreal den near Churchill, Manitoba, Canada"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://dennisfast.smugmug.com/Animals/ANIMALS-ANIMALS/DRF-11069/193145524_3HpHz-Th-5.jpg"" width=""150"" height=""100""/>
      <media:category>Animals</media:category>
      <media:keywords>red fox, kit, young fox, wary, vulpes vulpes, nap</media:keywords>
      <media:copyright url=""http://www.dennisfast.com"">Dennis Fast</media:copyright>
      <media:credit role=""photographer"">Dennis Fast</media:credit>
    </item>
    <item>
      <title>Short Eared Owl heard my shutter going off and took a look at me.</title>
      <link>http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=148870104&amp;k=SvsSf</link>
      <description>&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com""&gt;Jody Melanson (jmelanson)&lt;/a&gt;&lt;br /&gt;Short Eared Owl heard my shutter going off and took a look at me.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=148870104&amp;k=SvsSf"" title=""Short Eared Owl heard my shutter going off and took a look at me.""&gt;&lt;img src=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Th.jpg"" width=""150"" height=""94"" alt=""Short Eared Owl heard my shutter going off and took a look at me."" title=""Short Eared Owl heard my shutter going off and took a look at me."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Newest Shots</category>
      <pubDate>Wed, 02 May 2007 04:31:45 -0700</pubDate>
      <author>nobody@smugmug.com (Jody Melanson (jmelanson))</author>
      <guid isPermaLink=""false"">http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Th.jpg</guid>
      <exif:DateTimeOriginal>2007-03-12 01:34:49</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Ti.jpg"" fileSize=""3202"" type=""image/jpeg"" medium=""image"" width=""100"" height=""63"">
          <media:hash algo=""md5"">45d4608ddaaadeb9416aec8b3e89e8af</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Th.jpg"" fileSize=""5795"" type=""image/jpeg"" medium=""image"" width=""150"" height=""94"" isDefault=""true"">
          <media:hash algo=""md5"">7daa05c0b027b3bee71964d9bf55880b</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-S.jpg"" fileSize=""37106"" type=""image/jpeg"" medium=""image"" width=""400"" height=""250"">
          <media:hash algo=""md5"">350b0dbb780c768a2d42976aaf2c6e83</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-M.jpg"" fileSize=""70328"" type=""image/jpeg"" medium=""image"" width=""600"" height=""375"">
          <media:hash algo=""md5"">dd30eb4b867e28e27a74396f4f92de8b</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-L.jpg"" fileSize=""110503"" type=""image/jpeg"" medium=""image"" width=""750"" height=""469"">
          <media:hash algo=""md5"">f7ceb537103d7e7f7f47b14d8d949964</media:hash>
        </media:content>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-XL.jpg"" type=""image/jpeg"" medium=""image"" width=""750"" height=""469""/>
        <media:content url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-O.jpg"" fileSize=""161776"" type=""image/jpeg"" medium=""image"" width=""750"" height=""469"">
          <media:hash algo=""md5"">a96cd3080c3f0e6e56c03b747f00c177</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Short Eared Owl heard my shutter going off and took a look at me.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com""&gt;Jody Melanson (jmelanson)&lt;/a&gt;&lt;br /&gt;Short Eared Owl heard my shutter going off and took a look at me.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/3012712_xxd89h#!i=148870104&amp;k=SvsSf"" title=""Short Eared Owl heard my shutter going off and took a look at me.""&gt;&lt;img src=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Th.jpg"" width=""150"" height=""94"" alt=""Short Eared Owl heard my shutter going off and took a look at me."" title=""Short Eared Owl heard my shutter going off and took a look at me."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://jmelanson.smugmug.com/Newest-Shots/Newest-Shots/JODY2139/148870104_SvsSf-Th.jpg"" width=""150"" height=""94""/>
      <media:category>Newest Shots</media:category>
      <media:keywords>jody</media:keywords>
      <media:copyright url=""http://jmelanson.smugmug.com"">Jody Melanson (jmelanson)</media:copyright>
      <media:credit role=""photographer"">Jody Melanson (jmelanson)</media:credit>
    </item>
    <item>
      <title>Autumn_Glow.jpg</title>
      <link>http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/6819136_LsFCwK#!i=418999027&amp;k=SnsSB</link>
      <description>&lt;p&gt;&lt;a href=""http://timellenburg.smugmug.com""&gt;Tim Ellenburg&lt;/a&gt;&lt;br /&gt;Autumn_Glow.jpg&lt;/p&gt;&lt;p&gt;&lt;a href=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/6819136_LsFCwK#!i=418999027&amp;k=SnsSB"" title=""Autumn_Glow.jpg""&gt;&lt;img src=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Th.jpg"" width=""150"" height=""150"" alt=""Autumn_Glow.jpg"" title=""Autumn_Glow.jpg"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Mon, 17 Nov 2008 07:12:23 -0800</pubDate>
      <author>nobody@smugmug.com (Tim Ellenburg)</author>
      <guid isPermaLink=""false"">http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Th.jpg</guid>
      <exif:DateTimeOriginal>2006-11-04 10:25:57</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Ti.jpg"" fileSize=""4689"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">fb1c8b3ed4b85d494af84697af999765</media:hash>
        </media:content>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Th.jpg"" fileSize=""8064"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">53732b13ee182c08177bba69089c12ab</media:hash>
        </media:content>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-S.jpg"" fileSize=""21104"" type=""image/jpeg"" medium=""image"" width=""199"" height=""300"">
          <media:hash algo=""md5"">bfe2ee87f613649cd33485c56b02429c</media:hash>
        </media:content>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-M.jpg"" fileSize=""35927"" type=""image/jpeg"" medium=""image"" width=""299"" height=""450"">
          <media:hash algo=""md5"">815b7b654feb358cafda82da46b640e4</media:hash>
        </media:content>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-L.jpg"" fileSize=""52335"" type=""image/jpeg"" medium=""image"" width=""399"" height=""600"">
          <media:hash algo=""md5"">dcc9ee4c2974a9c8e5e4e8ce26370f6b</media:hash>
        </media:content>
        <media:content url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-XL.jpg"" fileSize=""74113"" type=""image/jpeg"" medium=""image"" width=""510"" height=""768"">
          <media:hash algo=""md5"">3714cb4f699520c3fcdb0d1fd6980467</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Autumn_Glow.jpg</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://timellenburg.smugmug.com""&gt;Tim Ellenburg&lt;/a&gt;&lt;br /&gt;Autumn_Glow.jpg&lt;/p&gt;&lt;p&gt;&lt;a href=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/6819136_LsFCwK#!i=418999027&amp;k=SnsSB"" title=""Autumn_Glow.jpg""&gt;&lt;img src=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Th.jpg"" width=""150"" height=""150"" alt=""Autumn_Glow.jpg"" title=""Autumn_Glow.jpg"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://timellenburg.smugmug.com/Nature/Flora-and-Fauna/AutumnGlow/418999027_SnsSB-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Nature</media:category>
      <media:keywords>autumn, glow</media:keywords>
      <media:copyright url=""http://www.timellenburg.com"">Tim Ellenburg</media:copyright>
      <media:credit role=""photographer"">Tim Ellenburg</media:credit>
    </item>
    <item>
      <title>Little Bee Eater</title>
      <link>http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/2162490_RNmS5v#!i=112670133&amp;k=vnwfB</link>
      <description>&lt;p&gt;&lt;a href=""http://johnchapmanphotographer.smugmug.com""&gt;johnchapmanphotographer&lt;/a&gt;&lt;br /&gt;Little Bee Eater&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/2162490_RNmS5v#!i=112670133&amp;k=vnwfB"" title=""Little Bee Eater""&gt;&lt;img src=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Th.jpg"" width=""106"" height=""150"" alt=""Little Bee Eater"" title=""Little Bee Eater"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Galleries</category>
      <pubDate>Sat, 25 Nov 2006 05:13:41 -0800</pubDate>
      <author>nobody@smugmug.com (johnchapmanphotographer)</author>
      <guid isPermaLink=""false"">http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Th.jpg</guid>
      <media:group>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Ti.jpg"" fileSize=""2170"" type=""image/jpeg"" medium=""image"" width=""71"" height=""100"">
          <media:hash algo=""md5"">5c5d835b009ba1eeeeefbc289110aec2</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Th.jpg"" fileSize=""3412"" type=""image/jpeg"" medium=""image"" width=""106"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">6a9f4ea335df18cec5144880c735bae7</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-S.jpg"" fileSize=""11422"" type=""image/jpeg"" medium=""image"" width=""213"" height=""300"">
          <media:hash algo=""md5"">14ee0d875849ce2829f34cb97dd9b4b9</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-M.jpg"" fileSize=""24095"" type=""image/jpeg"" medium=""image"" width=""319"" height=""450"">
          <media:hash algo=""md5"">3fb33cd83a829a11685f93b0c274c4a1</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-L.jpg"" fileSize=""40300"" type=""image/jpeg"" medium=""image"" width=""425"" height=""600"">
          <media:hash algo=""md5"">e80a4328d6dd899c78884b7ac2271ace</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-XL.jpg"" fileSize=""78706"" type=""image/jpeg"" medium=""image"" width=""496"" height=""700"">
          <media:hash algo=""md5"">818fcdf812886670b9a17813fb509c26</media:hash>
        </media:content>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-X2.jpg"" type=""image/jpeg"" medium=""image"" width=""496"" height=""700""/>
        <media:content url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-O.jpg"" fileSize=""162820"" type=""image/jpeg"" medium=""image"" width=""496"" height=""700"">
          <media:hash algo=""md5"">f2a69f89de519683a042462b9b61868a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Little Bee Eater</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://johnchapmanphotographer.smugmug.com""&gt;johnchapmanphotographer&lt;/a&gt;&lt;br /&gt;Little Bee Eater&lt;/p&gt;&lt;p&gt;&lt;a href=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/2162490_RNmS5v#!i=112670133&amp;k=vnwfB"" title=""Little Bee Eater""&gt;&lt;img src=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Th.jpg"" width=""106"" height=""150"" alt=""Little Bee Eater"" title=""Little Bee Eater"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://johnchapmanphotographer.smugmug.com/Galleries/Namibia-Aug-2002/Untitled-23/112670133_vnwfB-Th.jpg"" width=""106"" height=""150""/>
      <media:category>Galleries</media:category>
      <media:copyright url=""http://www.johnchapmanphotographer.co.uk"">johnchapmanphotographer</media:copyright>
      <media:credit role=""photographer"">johnchapmanphotographer</media:credit>
    </item>
    <item>
      <title>Amboseli Procession at Dawn</title>
      <link>http://cindycone.smugmug.com/Nature/My-Favorites/9254324_VC6NwX#!i=418674230&amp;k=k6T9R</link>
      <description>&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com""&gt;Cindy Cone&lt;/a&gt;&lt;br /&gt;Amboseli Procession at Dawn&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com/Nature/My-Favorites/9254324_VC6NwX#!i=418674230&amp;k=k6T9R"" title=""Amboseli Procession at Dawn""&gt;&lt;img src=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Th-3.jpg"" width=""150"" height=""94"" alt=""Amboseli Procession at Dawn"" title=""Amboseli Procession at Dawn"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Sun, 16 Nov 2008 19:29:03 -0800</pubDate>
      <author>nobody@smugmug.com (Cindy Cone)</author>
      <guid isPermaLink=""false"">http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2005-10-03 23:40:32</exif:DateTimeOriginal>
      <geo:lat>-2.67145585212000</geo:lat>
      <geo:long>37.27111816410000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Ti-3.jpg"" fileSize=""3353"" type=""image/jpeg"" medium=""image"" width=""100"" height=""63"">
          <media:hash algo=""md5"">3cffa13541f66b408eb1d731c58bcad2</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Th-3.jpg"" fileSize=""5653"" type=""image/jpeg"" medium=""image"" width=""150"" height=""94"" isDefault=""true"">
          <media:hash algo=""md5"">8eca93346a5e623f6c0f5408ba2e5ece</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-S-3.jpg"" fileSize=""30205"" type=""image/jpeg"" medium=""image"" width=""400"" height=""250"">
          <media:hash algo=""md5"">7a907be981a52ecad8adb3351bbede6d</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-M-3.jpg"" fileSize=""55315"" type=""image/jpeg"" medium=""image"" width=""600"" height=""375"">
          <media:hash algo=""md5"">8aefab53b3cfb4d17100e386a5795f56</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-L-3.jpg"" fileSize=""87931"" type=""image/jpeg"" medium=""image"" width=""800"" height=""500"">
          <media:hash algo=""md5"">c4ccf93f30cc93a6b2ef9284c1c2e5bf</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-XL-3.jpg"" fileSize=""130659"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""640"">
          <media:hash algo=""md5"">259c729e9ec694c79f4e1635a3a3b69a</media:hash>
        </media:content>
        <media:content url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-X2-3.jpg"" fileSize=""193494"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""800"">
          <media:hash algo=""md5"">a11ade3d02c8015965026dfa574b6a99</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Amboseli Procession at Dawn</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com""&gt;Cindy Cone&lt;/a&gt;&lt;br /&gt;Amboseli Procession at Dawn&lt;/p&gt;&lt;p&gt;&lt;a href=""http://cindycone.smugmug.com/Nature/My-Favorites/9254324_VC6NwX#!i=418674230&amp;k=k6T9R"" title=""Amboseli Procession at Dawn""&gt;&lt;img src=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Th-3.jpg"" width=""150"" height=""94"" alt=""Amboseli Procession at Dawn"" title=""Amboseli Procession at Dawn"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://cindycone.smugmug.com/Nature/My-Favorites/Wildebeests/418674230_k6T9R-Th-3.jpg"" width=""150"" height=""94""/>
      <media:category>Nature</media:category>
      <media:keywords>wildebeests, amboseli national park, kenya, africa, safari, dawn, dust</media:keywords>
      <media:copyright url=""http://www.cindyconephotography.com"">Cindy Cone</media:copyright>
      <media:credit role=""photographer"">Cindy Cone</media:credit>
    </item>
    <item>
      <title>Tessa as photographed by Christopher R. Cote, 2008.</title>
      <link>http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/6891226_rPKVQS#!i=440843226&amp;k=du7GC</link>
      <description>&lt;p&gt;&lt;a href=""http://christophercote.smugmug.com""&gt;Christopher&lt;/a&gt;&lt;br /&gt;Tessa as photographed by Christopher R. Cote, 2008.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/6891226_rPKVQS#!i=440843226&amp;k=du7GC"" title=""Tessa as photographed by Christopher R. Cote, 2008.""&gt;&lt;img src=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Th-3.jpg"" width=""150"" height=""150"" alt=""Tessa as photographed by Christopher R. Cote, 2008."" title=""Tessa as photographed by Christopher R. Cote, 2008."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portraits</category>
      <pubDate>Sun, 21 Dec 2008 21:39:11 -0800</pubDate>
      <author>nobody@smugmug.com (Christopher)</author>
      <guid isPermaLink=""false"">http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2008-06-01 19:55:37</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Ti-3.jpg"" fileSize=""6632"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">896fefcc4dc102993663d66ddd4f29cf</media:hash>
        </media:content>
        <media:content url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Th-3.jpg"" fileSize=""12901"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">802905214327d5a7f0cf8cc7cb372520</media:hash>
        </media:content>
        <media:content url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-S-3.jpg"" fileSize=""59672"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">3d70e9915e61458a1718b4d2aab1870b</media:hash>
        </media:content>
        <media:content url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-M-3.jpg"" fileSize=""121374"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">acbdc4d52fe3b1b9383cde204f7c7b56</media:hash>
        </media:content>
        <media:content url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-L-3.jpg"" fileSize=""210660"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">69b4fc767eaae4565e3319c169024042</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Tessa as photographed by Christopher R. Cote, 2008.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://christophercote.smugmug.com""&gt;Christopher&lt;/a&gt;&lt;br /&gt;Tessa as photographed by Christopher R. Cote, 2008.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/6891226_rPKVQS#!i=440843226&amp;k=du7GC"" title=""Tessa as photographed by Christopher R. Cote, 2008.""&gt;&lt;img src=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Th-3.jpg"" width=""150"" height=""150"" alt=""Tessa as photographed by Christopher R. Cote, 2008."" title=""Tessa as photographed by Christopher R. Cote, 2008."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://christophercote.smugmug.com/Portraits/Model-Portfolios/Various-Models/tessalaying-down1/440843226_du7GC-Th-3.jpg"" width=""150"" height=""150""/>
      <media:category>Portraits</media:category>
      <media:keywords>christopher, cote, photograph, tessa, portrait, ting, implied, nude, plant, laying</media:keywords>
      <media:copyright url=""http://christophercote.smugmug.com"">Christopher</media:copyright>
      <media:credit role=""photographer"">Christopher</media:credit>
    </item>
    <item>
      <title>March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ...</title>
      <link>http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/8349045_CRsnKw#!i=488698057&amp;k=WGp2d</link>
      <description>&lt;p&gt;&lt;a href=""http://tewmom.smugmug.com""&gt;Dianne M. Ward&lt;/a&gt;&lt;br /&gt;March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for me as I try to wrap up lose ends in work and prepare for the trip.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/8349045_CRsnKw#!i=488698057&amp;k=WGp2d"" title=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ...""&gt;&lt;img src=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Th-1.jpg"" width=""150"" height=""150"" alt=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ..."" title=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>POTD Gallery</category>
      <pubDate>Tue, 10 Mar 2009 04:06:25 -0700</pubDate>
      <author>nobody@smugmug.com (Dianne M. Ward)</author>
      <guid isPermaLink=""false"">http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2009-03-07 13:52:40</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Ti-1.jpg"" fileSize=""5510"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">36cefa856cfbae64347f99a0630ca21b</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Th-1.jpg"" fileSize=""9990"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">c1607987bb06ab51539d4187ea8110cd</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-S-1.jpg"" fileSize=""38946"" type=""image/jpeg"" medium=""image"" width=""400"" height=""282"">
          <media:hash algo=""md5"">c672fffc2ec3ae7b62ccfb1eb39278ee</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-M-1.jpg"" fileSize=""79553"" type=""image/jpeg"" medium=""image"" width=""600"" height=""423"">
          <media:hash algo=""md5"">0bcbf3cf4f5e562437598a2c59de952a</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-L-1.jpg"" fileSize=""132035"" type=""image/jpeg"" medium=""image"" width=""800"" height=""565"">
          <media:hash algo=""md5"">6b158db0e4fd67a5b03bb789d8dbb878</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-XL-1.jpg"" fileSize=""199986"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""723"">
          <media:hash algo=""md5"">31cbb459f495425931005ca86a31c240</media:hash>
        </media:content>
        <media:content url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-X2-1.jpg"" fileSize=""288676"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""903"">
          <media:hash algo=""md5"">e9aaaf362a442b3ae4db442bf1346fb5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://tewmom.smugmug.com""&gt;Dianne M. Ward&lt;/a&gt;&lt;br /&gt;March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for me as I try to wrap up lose ends in work and prepare for the trip.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/8349045_CRsnKw#!i=488698057&amp;k=WGp2d"" title=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ...""&gt;&lt;img src=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Th-1.jpg"" width=""150"" height=""150"" alt=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ..."" title=""March 10, 2009

Another shot from my visit to the zoo on Saturday.  I didn't have any time yesterday to take any pictures.  I am leaving for Ireland on Friday so this is a very busy week for ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://tewmom.smugmug.com/POTD-Gallery/Project-365-in-2009/DSC0896framed/488698057_WGp2d-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>POTD Gallery</media:category>
      <media:keywords>cougar, zoo, animal, cat, stone zoo, daily photo</media:keywords>
      <media:copyright url=""http://www.dmwardphotography.com"">Dianne M. Ward</media:copyright>
      <media:credit role=""photographer"">Dianne M. Ward</media:credit>
    </item>
    <item>
      <title>Basilica Notre Dame, Montreal Canada</title>
      <link>http://joecan.smugmug.com/Architecture/Architecture/2203095_mkp27z#!i=3693358&amp;k=p3z3D</link>
      <description>&lt;p&gt;&lt;a href=""http://joecan.smugmug.com""&gt;Joe Lipniczky&lt;/a&gt;&lt;br /&gt;Basilica Notre Dame, Montreal Canada&lt;/p&gt;&lt;p&gt;&lt;a href=""http://joecan.smugmug.com/Architecture/Architecture/2203095_mkp27z#!i=3693358&amp;k=p3z3D"" title=""Basilica Notre Dame, Montreal Canada""&gt;&lt;img src=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Th.jpg"" width=""150"" height=""113"" alt=""Basilica Notre Dame, Montreal Canada"" title=""Basilica Notre Dame, Montreal Canada"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Architecture</category>
      <pubDate>Sat, 24 Apr 2004 10:31:34 -0700</pubDate>
      <author>nobody@smugmug.com (Joe Lipniczky)</author>
      <guid isPermaLink=""false"">http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Th.jpg</guid>
      <media:group>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Ti.jpg"" fileSize=""3449"" type=""image/jpeg"" medium=""image"" width=""100"" height=""75"">
          <media:hash algo=""md5"">28cc07791727cee055371e53a49ffb15</media:hash>
        </media:content>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Th.jpg"" fileSize=""6524"" type=""image/jpeg"" medium=""image"" width=""150"" height=""113"" isDefault=""true"">
          <media:hash algo=""md5"">9f315d46ce4236e9cb36af5434295eec</media:hash>
        </media:content>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-S.jpg"" fileSize=""74905"" type=""image/jpeg"" medium=""image"" width=""400"" height=""300"">
          <media:hash algo=""md5"">b3c2bd20ed0c9904e55403b5c5cae7b5</media:hash>
        </media:content>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-M.jpg"" fileSize=""150479"" type=""image/jpeg"" medium=""image"" width=""600"" height=""450"">
          <media:hash algo=""md5"">753947218296596ed55713ba74d69e63</media:hash>
        </media:content>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-L.jpg"" fileSize=""138689"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">df4501c6c547b570aefdfdeb7c3e1a8b</media:hash>
        </media:content>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-XL.jpg"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600""/>
        <media:content url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-O.jpg"" fileSize=""179999"" type=""image/jpeg"" medium=""image"" width=""800"" height=""600"">
          <media:hash algo=""md5"">55daa60fcd718fa5543c7bbcc4f94e1e</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Basilica Notre Dame, Montreal Canada</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://joecan.smugmug.com""&gt;Joe Lipniczky&lt;/a&gt;&lt;br /&gt;Basilica Notre Dame, Montreal Canada&lt;/p&gt;&lt;p&gt;&lt;a href=""http://joecan.smugmug.com/Architecture/Architecture/2203095_mkp27z#!i=3693358&amp;k=p3z3D"" title=""Basilica Notre Dame, Montreal Canada""&gt;&lt;img src=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Th.jpg"" width=""150"" height=""113"" alt=""Basilica Notre Dame, Montreal Canada"" title=""Basilica Notre Dame, Montreal Canada"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://joecan.smugmug.com/Architecture/Architecture/DSC03174/3693358_p3z3D-Th.jpg"" width=""150"" height=""113""/>
      <media:category>Architecture</media:category>
      <media:copyright url=""http://joecan.smugmug.com"">Joe Lipniczky</media:copyright>
      <media:credit role=""photographer"">Joe Lipniczky</media:credit>
    </item>
    <item>
      <title>Chondro</title>
      <link>http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/11415172_ZrNxPJ#!i=815971990&amp;k=LMcM2</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;Chondro&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/11415172_ZrNxPJ#!i=815971990&amp;k=LMcM2"" title=""Chondro""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Th.jpg"" width=""150"" height=""100"" alt=""Chondro"" title=""Chondro"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Sun, 21 Mar 2010 17:52:57 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Th.jpg</guid>
      <exif:DateTimeOriginal>2010-03-21 12:20:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Ti.jpg"" fileSize=""4355"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">affa88fb28996320c71af2c973dc9025</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Th.jpg"" fileSize=""8250"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">d3f7d420bd7cfd7f5fdcdfd26de71534</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-S.jpg"" fileSize=""44152"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">7bf05c687ceecac88ece4fbb681ba449</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-M.jpg"" fileSize=""82156"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">64d069a2b33b889eff3dbc3f170eadcb</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-L.jpg"" fileSize=""131487"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">9c815859d8274d7602d6e37efbbacf5b</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-XL.jpg"" fileSize=""194554"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">205bfa278de578d94a44c32de7b7d6ac</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Chondro</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;Chondro&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/11415172_ZrNxPJ#!i=815971990&amp;k=LMcM2"" title=""Chondro""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Th.jpg"" width=""150"" height=""100"" alt=""Chondro"" title=""Chondro"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Daily-Album/March-2010/IMG1391chondro/815971990_LMcM2-Th.jpg"" width=""150"" height=""100""/>
      <media:category>Daily Album</media:category>
      <media:keywords>1391chondro</media:keywords>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>Fairy Falls, Columbia River Gorge, OR</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=520293125&amp;k=puVdB</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Fairy Falls, Columbia River Gorge, OR&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=520293125&amp;k=puVdB"" title=""Fairy Falls, Columbia River Gorge, OR""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Th-3.jpg"" width=""100"" height=""150"" alt=""Fairy Falls, Columbia River Gorge, OR"" title=""Fairy Falls, Columbia River Gorge, OR"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Fri, 24 Apr 2009 22:51:42 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2009-04-19 12:17:21</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Ti-3.jpg"" fileSize=""5158"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">0575c6f71e642d264eec4fec199c33a2</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Th-3.jpg"" fileSize=""9107"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">4a725d050d92da22351688ddeb814d61</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-S-3.jpg"" fileSize=""32509"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">3796777de00cf44ec0e5d90f864ea2b6</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-M-3.jpg"" fileSize=""64070"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">0e8cb04d9be126e95454247be8e9ff13</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-L-3.jpg"" fileSize=""104357"" type=""image/jpeg"" medium=""image"" width=""400"" height=""600"">
          <media:hash algo=""md5"">175b0e9cae7d351873b261065b8b7cfd</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Fairy Falls, Columbia River Gorge, OR</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Fairy Falls, Columbia River Gorge, OR&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=520293125&amp;k=puVdB"" title=""Fairy Falls, Columbia River Gorge, OR""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Th-3.jpg"" width=""100"" height=""150"" alt=""Fairy Falls, Columbia River Gorge, OR"" title=""Fairy Falls, Columbia River Gorge, OR"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0866/520293125_puVdB-Th-3.jpg"" width=""100"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>fairy falls, columbia river gorge, waterfall, nature, landscape, oregon, water</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ...</title>
      <link>http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=265479346&amp;k=eNsfa</link>
      <description>&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
&lt;FONT COLOR=""GREEN""&gt;&lt;h5&gt;&lt;strong&gt; Top ten finalist in the &lt;a href=""http://www.worldphotographyawards.org/2009shortlist-am.asp""&gt;2009 Sony World Photography Awards&lt;/a&gt; competition in the ""Natural History"" category.&lt;/h5&gt;&lt;/strong&gt;&lt;/FONT COLOR=""GREEEN""&gt;&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=265479346&amp;k=eNsfa"" title=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Th-3.jpg"" width=""150"" height=""120"" alt=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ..."" title=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Thu, 13 Mar 2008 18:53:19 -0700</pubDate>
      <author>nobody@smugmug.com (Ken Conger)</author>
      <guid isPermaLink=""false"">http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2008-03-07 02:36:43</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Ti-3.jpg"" fileSize=""7597"" type=""image/jpeg"" medium=""image"" width=""100"" height=""80"">
          <media:hash algo=""md5"">7f53c86fa1cca3a61db8b9ed394ecc39</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Th-3.jpg"" fileSize=""11291"" type=""image/jpeg"" medium=""image"" width=""150"" height=""120"" isDefault=""true"">
          <media:hash algo=""md5"">26ed97363ea906dea2d041d28677b0f0</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-S-3.jpg"" fileSize=""41818"" type=""image/jpeg"" medium=""image"" width=""375"" height=""300"">
          <media:hash algo=""md5"">3ef1b33b7b849108a2fa513fadd0efb9</media:hash>
        </media:content>
        <media:content url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-M-3.jpg"" fileSize=""76629"" type=""image/jpeg"" medium=""image"" width=""563"" height=""450"">
          <media:hash algo=""md5"">f102e78b1be28c6ac867f10c5b13afa2</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com""&gt;Ken Conger&lt;/a&gt;&lt;br /&gt;This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
&lt;FONT COLOR=""GREEN""&gt;&lt;h5&gt;&lt;strong&gt; Top ten finalist in the &lt;a href=""http://www.worldphotographyawards.org/2009shortlist-am.asp""&gt;2009 Sony World Photography Awards&lt;/a&gt; competition in the ""Natural History"" category.&lt;/h5&gt;&lt;/strong&gt;&lt;/FONT COLOR=""GREEEN""&gt;&lt;FONT COLOR=""RED""&gt;&lt;h5&gt;This photograph is protected by the U.S. Copyright Laws and shall not to be downloaded or reproduced by any means without the formal written permission of Ken Conger Photography.&lt;FONT COLOR=""RED""&gt;&lt;/h5&gt;&lt;/p&gt;&lt;p&gt;&lt;a href=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/1269226_wJntf9#!i=265479346&amp;k=eNsfa"" title=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ...""&gt;&lt;img src=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Th-3.jpg"" width=""150"" height=""120"" alt=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ..."" title=""This Bald Eagle group photograph was captured in Homer, Alaska (3/2008). &#13;
 Top ten finalist in the 2009 Sony World Photography Awards competition in the ""Natural History"" category.This phot ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://kenconger.smugmug.com/Nature/Eagle-Gallery/Group-Tree/265479346_eNsfa-Th-3.jpg"" width=""150"" height=""120""/>
      <media:category>Nature</media:category>
      <media:keywords>eagle, eagles, bald eagle, alaska</media:keywords>
      <media:copyright url=""http://www.kencongerphotography.com"">Ken Conger</media:copyright>
      <media:credit role=""photographer"">Ken Conger</media:credit>
    </item>
    <item>
      <title>Cora Lynn waterfalls in Camberville (past Marysville)</title>
      <link>http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/14689507_GvJth5#!i=72747906&amp;k=QFtiZ</link>
      <description>&lt;p&gt;&lt;a href=""http://mudder.smugmug.com""&gt;Andrew Falcke&lt;/a&gt;&lt;br /&gt;Cora Lynn waterfalls in Camberville (past Marysville)&lt;/p&gt;&lt;p&gt;&lt;a href=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/14689507_GvJth5#!i=72747906&amp;k=QFtiZ"" title=""Cora Lynn waterfalls in Camberville (past Marysville)""&gt;&lt;img src=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Th.jpg"" width=""150"" height=""100"" alt=""Cora Lynn waterfalls in Camberville (past Marysville)"" title=""Cora Lynn waterfalls in Camberville (past Marysville)"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Places</category>
      <pubDate>Thu, 01 Jun 2006 01:24:44 -0700</pubDate>
      <author>nobody@smugmug.com (Andrew Falcke)</author>
      <guid isPermaLink=""false"">http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Th.jpg</guid>
      <exif:DateTimeOriginal>2006-06-01 09:32:42</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Ti.jpg"" fileSize=""3083"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">47c1afb048b3f7c6e1c75cc3a2712d1c</media:hash>
        </media:content>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Th.jpg"" fileSize=""5668"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">adc44165180c4f609d892f69c82845ce</media:hash>
        </media:content>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-S.jpg"" fileSize=""43468"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">0692c215105622fd1c1c41a38a2bc9a7</media:hash>
        </media:content>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-M.jpg"" fileSize=""113588"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">db07047389d35933b1dd494c355f04c4</media:hash>
        </media:content>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-L.jpg"" fileSize=""141387"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">2f84445cd77a72305a9a010b5bb0bef3</media:hash>
        </media:content>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-XL.jpg"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533""/>
        <media:content url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-O.jpg"" fileSize=""184749"" type=""image/jpeg"" medium=""image"" width=""800"" height=""533"">
          <media:hash algo=""md5"">43b5ec40872961b4ea49001e3642ca18</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Cora Lynn waterfalls in Camberville (past Marysville)</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://mudder.smugmug.com""&gt;Andrew Falcke&lt;/a&gt;&lt;br /&gt;Cora Lynn waterfalls in Camberville (past Marysville)&lt;/p&gt;&lt;p&gt;&lt;a href=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/14689507_GvJth5#!i=72747906&amp;k=QFtiZ"" title=""Cora Lynn waterfalls in Camberville (past Marysville)""&gt;&lt;img src=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Th.jpg"" width=""150"" height=""100"" alt=""Cora Lynn waterfalls in Camberville (past Marysville)"" title=""Cora Lynn waterfalls in Camberville (past Marysville)"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://mudder.smugmug.com/Places/Forest-and-coastal-scenes/MG0362/72747906_QFtiZ-Th.jpg"" width=""150"" height=""100""/>
      <media:category>Places</media:category>
      <media:keywords>falcke, cora lynn, waterfall, marysville, cambarville</media:keywords>
      <media:copyright url=""http://mudder.smugmug.com"">Andrew Falcke</media:copyright>
      <media:credit role=""photographer"">Andrew Falcke</media:credit>
    </item>
    <item>
      <title>May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!</title>
      <link>http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/2790130_rR7DQX#!i=152411815&amp;k=hBQzc</link>
      <description>&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun &lt;a href=""http://vandana.smugmug.com/gallery/2843700""&gt;here&lt;a/&gt;&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/2790130_rR7DQX#!i=152411815&amp;k=hBQzc"" title=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Th-2.jpg"" width=""150"" height=""110"" alt=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!"" title=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Photography</category>
      <pubDate>Sun, 13 May 2007 20:21:48 -0700</pubDate>
      <author>nobody@smugmug.com (Vandana)</author>
      <guid isPermaLink=""false"">http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2007-05-13 15:36:09</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Ti-2.jpg"" fileSize=""9824"" type=""image/jpeg"" medium=""image"" width=""100"" height=""73"">
          <media:hash algo=""md5"">114062c1f36b61f3c5f1e42387b1840c</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Th-2.jpg"" fileSize=""17033"" type=""image/jpeg"" medium=""image"" width=""150"" height=""110"" isDefault=""true"">
          <media:hash algo=""md5"">b44499d3277f332e8f727cb60544699b</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-S-2.jpg"" fileSize=""97572"" type=""image/jpeg"" medium=""image"" width=""400"" height=""294"">
          <media:hash algo=""md5"">f01c8eeb8a477af614cf00ee4665d2a5</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-M-2.jpg"" fileSize=""206242"" type=""image/jpeg"" medium=""image"" width=""600"" height=""441"">
          <media:hash algo=""md5"">ea9254c1d1440e10329beffb4de6737e</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-L-2.jpg"" fileSize=""348815"" type=""image/jpeg"" medium=""image"" width=""800"" height=""588"">
          <media:hash algo=""md5"">c667d3ec54ea3ab2feddde6046af76da</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-XL-2.jpg"" fileSize=""539296"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""752"">
          <media:hash algo=""md5"">8958cc432780f1bed51c35955bba4e39</media:hash>
        </media:content>
        <media:content url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-X2-2.jpg"" fileSize=""791802"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""940"">
          <media:hash algo=""md5"">1444aeec020cd4efc390c1b51198633c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://vandana.smugmug.com""&gt;Vandana&lt;/a&gt;&lt;br /&gt;May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun &lt;a href=""http://vandana.smugmug.com/gallery/2843700""&gt;here&lt;a/&gt;&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!&lt;/p&gt;&lt;p&gt;&lt;a href=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/2790130_rR7DQX#!i=152411815&amp;k=hBQzc"" title=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!""&gt;&lt;img src=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Th-2.jpg"" width=""150"" height=""110"" alt=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!"" title=""May 13, 2007 - Peacock show-off !&#13;
&#13;
More Peacock fun here&#13;
&#13;
They put on quite a show today..&#13;
&#13;
Happy Mother's day to all!"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://vandana.smugmug.com/Photography/Photo-a-day/May-2007/DSC4795/152411815_hBQzc-Th-2.jpg"" width=""150"" height=""110""/>
      <media:category>Photography</media:category>
      <media:keywords>peacock, display, colors, new mexico, clovis, bird, birds</media:keywords>
      <media:copyright url=""http://www.vandanaphotography.com"">Vandana</media:copyright>
      <media:credit role=""photographer"">Vandana</media:credit>
    </item>
    <item>
      <title>I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.</title>
      <link>http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=333499004&amp;k=H57zE</link>
      <description>&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=333499004&amp;k=H57zE"" title=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Th.jpg"" width=""150"" height=""106"" alt=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates."" title=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Daily Album</category>
      <pubDate>Thu, 17 Jul 2008 06:37:59 -0700</pubDate>
      <author>nobody@smugmug.com (TheCuriousCamel)</author>
      <guid isPermaLink=""false"">http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-07-14 10:08:40</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Ti.jpg"" fileSize=""2944"" type=""image/jpeg"" medium=""image"" width=""100"" height=""71"">
          <media:hash algo=""md5"">742ed37fef84798227a361f46ea1f8a8</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Th.jpg"" fileSize=""5279"" type=""image/jpeg"" medium=""image"" width=""150"" height=""106"" isDefault=""true"">
          <media:hash algo=""md5"">4f75c848b26b8e20d88a90dc012553d1</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-S.jpg"" fileSize=""25822"" type=""image/jpeg"" medium=""image"" width=""400"" height=""283"">
          <media:hash algo=""md5"">06910532f2da80479537876a15dd677a</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-M.jpg"" fileSize=""47532"" type=""image/jpeg"" medium=""image"" width=""600"" height=""425"">
          <media:hash algo=""md5"">1257b40c534ef565ed98f14314968edc</media:hash>
        </media:content>
        <media:content url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-L.jpg"" fileSize=""78396"" type=""image/jpeg"" medium=""image"" width=""800"" height=""566"">
          <media:hash algo=""md5"">045a0977f0db218c7601ee605de3df87</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com""&gt;TheCuriousCamel&lt;/a&gt;&lt;br /&gt;I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/4635239_rR5xfR#!i=333499004&amp;k=H57zE"" title=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates.""&gt;&lt;img src=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Th.jpg"" width=""150"" height=""106"" alt=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates."" title=""I think this might be a self portrait as this is was how I looked most of the time.&#13;
&#13;
I'll catch up shortly and give updates."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://thecuriouscamel.smugmug.com/Daily-Album/2008-June-thru-August/IMG8634/333499004_H57zE-Th.jpg"" width=""150"" height=""106""/>
      <media:category>Daily Album</media:category>
      <media:copyright url=""http://thecuriouscamel.smugmug.com"">TheCuriousCamel</media:copyright>
      <media:credit role=""photographer"">TheCuriousCamel</media:credit>
    </item>
    <item>
      <title>Glistening Rock at Dusk, Olympic National Park</title>
      <link>http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/2910910_5VQQgk#!i=452631803&amp;k=S2QNq</link>
      <description>&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Glistening Rock at Dusk, Olympic National Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/2910910_5VQQgk#!i=452631803&amp;k=S2QNq"" title=""Glistening Rock at Dusk, Olympic National Park""&gt;&lt;img src=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Th-1.jpg"" width=""150"" height=""93"" alt=""Glistening Rock at Dusk, Olympic National Park"" title=""Glistening Rock at Dusk, Olympic National Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscape and Nature Photography</category>
      <pubDate>Sat, 10 Jan 2009 19:25:32 -0800</pubDate>
      <author>nobody@smugmug.com (Daniel Ewert)</author>
      <guid isPermaLink=""false"">http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Th-1.jpg</guid>
      <media:group>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Ti-1.jpg"" fileSize=""2956"" type=""image/jpeg"" medium=""image"" width=""100"" height=""62"">
          <media:hash algo=""md5"">da5be08aa3d78b7b348912c1b8816c06</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Th-1.jpg"" fileSize=""5240"" type=""image/jpeg"" medium=""image"" width=""150"" height=""93"" isDefault=""true"">
          <media:hash algo=""md5"">eae17e2106d838467ecbd9f49c2af710</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-S-1.jpg"" fileSize=""27039"" type=""image/jpeg"" medium=""image"" width=""400"" height=""247"">
          <media:hash algo=""md5"">5c3cb2e912055080601f605fc7678240</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-M-1.jpg"" fileSize=""50508"" type=""image/jpeg"" medium=""image"" width=""600"" height=""371"">
          <media:hash algo=""md5"">09b45784a10697dd3966571d20e1ebfb</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-L-1.jpg"" fileSize=""81070"" type=""image/jpeg"" medium=""image"" width=""800"" height=""495"">
          <media:hash algo=""md5"">4167e35260ef38c9892ccf3cd8671684</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-XL-1.jpg"" fileSize=""82864"" type=""image/jpeg"" medium=""image"" width=""815"" height=""504"">
          <media:hash algo=""md5"">5a461c6c5b1fb6148e40a6fa94c50b5e</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-X2-1.jpg"" fileSize=""235535"" type=""image/jpeg"" medium=""image"" width=""815"" height=""504"">
          <media:hash algo=""md5"">28883921ddc4789760b599f98ae833f8</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-O-1.jpg"" fileSize=""235535"" type=""image/jpeg"" medium=""image"" width=""815"" height=""504"">
          <media:hash algo=""md5"">28883921ddc4789760b599f98ae833f8</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Glistening Rock at Dusk, Olympic National Park</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Glistening Rock at Dusk, Olympic National Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/2910910_5VQQgk#!i=452631803&amp;k=S2QNq"" title=""Glistening Rock at Dusk, Olympic National Park""&gt;&lt;img src=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Th-1.jpg"" width=""150"" height=""93"" alt=""Glistening Rock at Dusk, Olympic National Park"" title=""Glistening Rock at Dusk, Olympic National Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ewert.smugmug.com/Landscape-and-Nature-1/Oceans-and-Coasts-Photography/Glistenrock/452631803_S2QNq-Th-1.jpg"" width=""150"" height=""93""/>
      <media:category>Landscape and Nature Photography</media:category>
      <media:keywords>glistening, rock, dusk, olympic, national, park, sunset, color, photography, beach, remote, coastal, coast, sand, waves, quality, professional</media:keywords>
      <media:copyright url=""http://www.ewertnaturephotography.com"">Daniel Ewert</media:copyright>
      <media:credit role=""photographer"">Daniel Ewert</media:credit>
    </item>
    <item>
      <title>A Sandhill Crane strutting along.</title>
      <link>http://ken.smugmug.com/Birds/Diaily-Photos/6620360_4zJqMk#!i=422049336&amp;k=SVFgW</link>
      <description>&lt;p&gt;&lt;a href=""http://ken.smugmug.com""&gt;Ken&lt;/a&gt;&lt;br /&gt;A Sandhill Crane strutting along.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ken.smugmug.com/Birds/Diaily-Photos/6620360_4zJqMk#!i=422049336&amp;k=SVFgW"" title=""A Sandhill Crane strutting along.""&gt;&lt;img src=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Th.jpg"" width=""150"" height=""150"" alt=""A Sandhill Crane strutting along."" title=""A Sandhill Crane strutting along."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Birds</category>
      <pubDate>Sat, 22 Nov 2008 05:35:39 -0800</pubDate>
      <author>nobody@smugmug.com (Ken)</author>
      <guid isPermaLink=""false"">http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Th.jpg</guid>
      <exif:DateTimeOriginal>2008-11-05 09:16:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Ti.jpg"" fileSize=""4467"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">dd368d13b9b149c76efd5f67792e8449</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Th.jpg"" fileSize=""7829"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">8c1f5746e915a963876e82a22ac6fd7f</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-S.jpg"" fileSize=""27062"" type=""image/jpeg"" medium=""image"" width=""400"" height=""255"">
          <media:hash algo=""md5"">38d6ce0be8a4f1b46a609a791c176cd2</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-M.jpg"" fileSize=""50685"" type=""image/jpeg"" medium=""image"" width=""600"" height=""383"">
          <media:hash algo=""md5"">e5efd26dae1d1418b352a39f4cc159d4</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-L.jpg"" fileSize=""82685"" type=""image/jpeg"" medium=""image"" width=""800"" height=""510"">
          <media:hash algo=""md5"">2a83df7fb9741755c078187d5f6c525b</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-XL.jpg"" fileSize=""127660"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""653"">
          <media:hash algo=""md5"">6dc69166db7771d76969419c6aa357ef</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-X2.jpg"" fileSize=""189152"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""816"">
          <media:hash algo=""md5"">fac398e134c97ebb9b831567d52c47d3</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-X3.jpg"" fileSize=""286362"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1020"">
          <media:hash algo=""md5"">8c17f7d70eb8a317c2e5573be9a8c7e9</media:hash>
        </media:content>
        <media:content url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-O.jpg"" fileSize=""3720608"" type=""image/jpeg"" medium=""image"" width=""2716"" height=""1731"">
          <media:hash algo=""md5"">27eaacf64fa70182c065ef68363c0c7f</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">A Sandhill Crane strutting along.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ken.smugmug.com""&gt;Ken&lt;/a&gt;&lt;br /&gt;A Sandhill Crane strutting along.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ken.smugmug.com/Birds/Diaily-Photos/6620360_4zJqMk#!i=422049336&amp;k=SVFgW"" title=""A Sandhill Crane strutting along.""&gt;&lt;img src=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Th.jpg"" width=""150"" height=""150"" alt=""A Sandhill Crane strutting along."" title=""A Sandhill Crane strutting along."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ken.smugmug.com/Birds/Diaily-Photos/Crane-Tipper/422049336_SVFgW-Th.jpg"" width=""150"" height=""150""/>
      <media:category>Birds</media:category>
      <media:keywords>crane, tipper</media:keywords>
      <media:copyright url=""http://ken.smugmug.com"">Ken</media:copyright>
      <media:credit role=""photographer"">Ken</media:credit>
    </item>
    <item>
      <title>Ruby Crowned Kinglet at Clyde Shepherd NP</title>
      <link>http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/2432303_Vc6jjr#!i=132036379&amp;k=uw5Hp</link>
      <description>&lt;p&gt;&lt;a href=""http://davidhodgson.smugmug.com""&gt;David Hodgson&lt;/a&gt;&lt;br /&gt;Ruby Crowned Kinglet at Clyde Shepherd NP&lt;/p&gt;&lt;p&gt;&lt;a href=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/2432303_Vc6jjr#!i=132036379&amp;k=uw5Hp"" title=""Ruby Crowned Kinglet at Clyde Shepherd NP""&gt;&lt;img src=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Th-1.jpg"" width=""150"" height=""111"" alt=""Ruby Crowned Kinglet at Clyde Shepherd NP"" title=""Ruby Crowned Kinglet at Clyde Shepherd NP"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Birds</category>
      <pubDate>Sun, 25 Feb 2007 12:43:04 -0800</pubDate>
      <author>nobody@smugmug.com (David Hodgson)</author>
      <guid isPermaLink=""false"">http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2007-02-24 09:26:27</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Ti-1.jpg"" fileSize=""6770"" type=""image/jpeg"" medium=""image"" width=""100"" height=""74"">
          <media:hash algo=""md5"">30607adde856b52c4c27ec893ce9efcb</media:hash>
        </media:content>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Th-1.jpg"" fileSize=""9723"" type=""image/jpeg"" medium=""image"" width=""150"" height=""111"" isDefault=""true"">
          <media:hash algo=""md5"">b872f7018792e3418b1785996a4c17bd</media:hash>
        </media:content>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-S-1.jpg"" fileSize=""39012"" type=""image/jpeg"" medium=""image"" width=""400"" height=""295"">
          <media:hash algo=""md5"">6f5fb3f73646d43fba594dc510f93afa</media:hash>
        </media:content>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-M-1.jpg"" fileSize=""81817"" type=""image/jpeg"" medium=""image"" width=""600"" height=""442"">
          <media:hash algo=""md5"">3988bbd04c65418a71a130c200622e37</media:hash>
        </media:content>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-L-1.jpg"" fileSize=""347326"" type=""image/jpeg"" medium=""image"" width=""700"" height=""516"">
          <media:hash algo=""md5"">eab6c00a1cd1737a7cb41888b2d0975c</media:hash>
        </media:content>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-XL-1.jpg"" type=""image/jpeg"" medium=""image"" width=""700"" height=""516""/>
        <media:content url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-O-1.jpg"" fileSize=""347326"" type=""image/jpeg"" medium=""image"" width=""700"" height=""516"">
          <media:hash algo=""md5"">eab6c00a1cd1737a7cb41888b2d0975c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Ruby Crowned Kinglet at Clyde Shepherd NP</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://davidhodgson.smugmug.com""&gt;David Hodgson&lt;/a&gt;&lt;br /&gt;Ruby Crowned Kinglet at Clyde Shepherd NP&lt;/p&gt;&lt;p&gt;&lt;a href=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/2432303_Vc6jjr#!i=132036379&amp;k=uw5Hp"" title=""Ruby Crowned Kinglet at Clyde Shepherd NP""&gt;&lt;img src=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Th-1.jpg"" width=""150"" height=""111"" alt=""Ruby Crowned Kinglet at Clyde Shepherd NP"" title=""Ruby Crowned Kinglet at Clyde Shepherd NP"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://davidhodgson.smugmug.com/Birds/Atlanta-Birds/RC-Kinglet3/132036379_uw5Hp-Th-1.jpg"" width=""150"" height=""111""/>
      <media:category>Birds</media:category>
      <media:keywords>clyde shepherd np, david hodgson, kinglet</media:keywords>
      <media:copyright url=""http://davidhodgson.smugmug.com"">David Hodgson</media:copyright>
      <media:credit role=""photographer"">David Hodgson</media:credit>
    </item>
    <item>
      <title>Photo-Artistry's photo</title>
      <link>http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/10017397_nFkDQJ#!i=567162752&amp;k=J5tzG</link>
      <description>&lt;p&gt;&lt;a href=""http://photo-artistry.smugmug.com""&gt;Photo-Artistry&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/10017397_nFkDQJ#!i=567162752&amp;k=J5tzG"" title=""Photo-Artistry's photo""&gt;&lt;img src=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Th-1.jpg"" width=""150"" height=""150"" alt=""Photo-Artistry's photo"" title=""Photo-Artistry's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Portraits</category>
      <pubDate>Wed, 17 Jun 2009 23:20:11 -0700</pubDate>
      <author>nobody@smugmug.com (Photo-Artistry)</author>
      <guid isPermaLink=""false"">http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2009-06-18 09:09:22</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Ti-1.jpg"" fileSize=""3730"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">847135806851581a0166ba66015f5809</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Th-1.jpg"" fileSize=""6200"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">7b488f9e6946acb807a6b39cc2bbf8da</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-S-1.jpg"" fileSize=""20333"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">2d1838c32243ea7a75e517597a39e376</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-M-1.jpg"" fileSize=""34597"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">f61f1329fae2d21439e84dc982cb1d93</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-L-1.jpg"" fileSize=""53678"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">55731a1b465f8e809393c3b08cf479e0</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-XL-1.jpg"" fileSize=""81849"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""683"">
          <media:hash algo=""md5"">437870e01e509868b6ef7b54ab852343</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-X2-1.jpg"" fileSize=""123384"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""854"">
          <media:hash algo=""md5"">3b204196b7c5656cb2bc7f8289fa9c09</media:hash>
        </media:content>
        <media:content url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-X3-1.jpg"" fileSize=""191316"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""1067"">
          <media:hash algo=""md5"">abfb5bc769a8a923db9af9cc8ffbc163</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Photo-Artistry's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://photo-artistry.smugmug.com""&gt;Photo-Artistry&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/10017397_nFkDQJ#!i=567162752&amp;k=J5tzG"" title=""Photo-Artistry's photo""&gt;&lt;img src=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Th-1.jpg"" width=""150"" height=""150"" alt=""Photo-Artistry's photo"" title=""Photo-Artistry's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://photo-artistry.smugmug.com/Portraits/Photoartistrys-Favorites/BeckaMeetAudreyBW/567162752_J5tzG-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Portraits</media:category>
      <media:keywords>beckameetaudreybw</media:keywords>
      <media:copyright url=""http://www.customphotoartistry.com"">Photo-Artistry</media:copyright>
      <media:credit role=""photographer"">Photo-Artistry</media:credit>
    </item>
    <item>
      <title>Sunset Splash.....</title>
      <link>http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=462596314&amp;k=D9ANK</link>
      <description>&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com""&gt;Michael Weitzman (micalngelo)&lt;/a&gt;&lt;br /&gt;Sunset Splash.....&lt;/p&gt;&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=462596314&amp;k=D9ANK"" title=""Sunset Splash.....""&gt;&lt;img src=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Th-1.jpg"" width=""150"" height=""150"" alt=""Sunset Splash....."" title=""Sunset Splash....."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Mon, 26 Jan 2009 20:51:12 -0800</pubDate>
      <author>nobody@smugmug.com (Michael Weitzman (micalngelo))</author>
      <guid isPermaLink=""false"">http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2009-01-26 17:57:28</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Ti-1.jpg"" fileSize=""5194"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">e2d70c7edd9fc40840ad8853ea987dc3</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Th-1.jpg"" fileSize=""9954"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">f5d808085d4fc54aeb48f0ca9dff4431</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-S-1.jpg"" fileSize=""32802"" type=""image/jpeg"" medium=""image"" width=""400"" height=""215"">
          <media:hash algo=""md5"">f29e9d9c7cb6d35d28a1e4b6d8b1647c</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-M-1.jpg"" fileSize=""66991"" type=""image/jpeg"" medium=""image"" width=""600"" height=""323"">
          <media:hash algo=""md5"">cb740a5ddc40dc3d657db6252b939bf4</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-L-1.jpg"" fileSize=""114392"" type=""image/jpeg"" medium=""image"" width=""800"" height=""431"">
          <media:hash algo=""md5"">a9c9cb86f74e5c05a4d9e26296892468</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-XL-1.jpg"" fileSize=""175659"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""551"">
          <media:hash algo=""md5"">959cb7cf0db61a551aed36aa1ebc4ba3</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-X2-1.jpg"" fileSize=""264483"" type=""image/jpeg"" medium=""image"" width=""1280"" height=""689"">
          <media:hash algo=""md5"">fb11859d833db04c24eea5a4bbb17773</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-X3-1.jpg"" fileSize=""395750"" type=""image/jpeg"" medium=""image"" width=""1600"" height=""861"">
          <media:hash algo=""md5"">4ad1dea0db60c23ea16e265b26bf52f4</media:hash>
        </media:content>
        <media:content url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-O-1.jpg"" fileSize=""4600922"" type=""image/jpeg"" medium=""image"" width=""3978"" height=""2140"">
          <media:hash algo=""md5"">fbb524026a92a0dc7475f800b69d53b1</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Sunset Splash.....</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com""&gt;Michael Weitzman (micalngelo)&lt;/a&gt;&lt;br /&gt;Sunset Splash.....&lt;/p&gt;&lt;p&gt;&lt;a href=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/4964095_VN6j44#!i=462596314&amp;k=D9ANK"" title=""Sunset Splash.....""&gt;&lt;img src=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Th-1.jpg"" width=""150"" height=""150"" alt=""Sunset Splash....."" title=""Sunset Splash....."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://micalngelo.smugmug.com/Other/Daily-almost-Photos/DSC9548-1/462596314_D9ANK-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Other</media:category>
      <media:keywords>sunset, ocean, beach, seashore</media:keywords>
      <media:copyright url=""http://www.studiomwphotography.com"">Michael Weitzman (micalngelo)</media:copyright>
      <media:credit role=""photographer"">Michael Weitzman (micalngelo)</media:credit>
    </item>
    <item>
      <title>Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ...</title>
      <link>http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/2411818_pJXDwQ#!i=126456308&amp;k=SBzz4</link>
      <description>&lt;p&gt;&lt;a href=""http://autumn-ethereal.smugmug.com""&gt;Alexandre Deschaumes (Autumn-Ethereal)&lt;/a&gt;&lt;br /&gt;Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extremement jaunes du coté ensoleillé du lac avoisinant.

hum mhm&lt;/p&gt;&lt;p&gt;&lt;a href=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/2411818_pJXDwQ#!i=126456308&amp;k=SBzz4"" title=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ...""&gt;&lt;img src=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Th-6.jpg"" width=""150"" height=""150"" alt=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ..."" title=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Ethereal Nature</category>
      <pubDate>Tue, 30 Jan 2007 06:18:11 -0800</pubDate>
      <author>nobody@smugmug.com (Alexandre Deschaumes (Autumn-Ethereal))</author>
      <guid isPermaLink=""false"">http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Th-6.jpg</guid>
      <exif:DateTimeOriginal>2005-10-16 11:38:19</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Ti-6.jpg"" fileSize=""7435"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">117b3d779cd97508a58264a05668eac9</media:hash>
        </media:content>
        <media:content url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Th-6.jpg"" fileSize=""14403"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">c5aab4cf4890437d6f70e583708786d7</media:hash>
        </media:content>
        <media:content url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-S-6.jpg"" fileSize=""59667"" type=""image/jpeg"" medium=""image"" width=""372"" height=""300"">
          <media:hash algo=""md5"">eb0fa8c608a02f7a2d7fd1f7d10bdc01</media:hash>
        </media:content>
        <media:content url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-M-6.jpg"" fileSize=""127403"" type=""image/jpeg"" medium=""image"" width=""558"" height=""450"">
          <media:hash algo=""md5"">6878afb7054b0467b3e36db1656e30d5</media:hash>
        </media:content>
        <media:content url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-L-6.jpg"" fileSize=""397940"" type=""image/jpeg"" medium=""image"" width=""600"" height=""484"">
          <media:hash algo=""md5"">c3a42ed866a0af085bb4836dc5f5000b</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://autumn-ethereal.smugmug.com""&gt;Alexandre Deschaumes (Autumn-Ethereal)&lt;/a&gt;&lt;br /&gt;Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extremement jaunes du coté ensoleillé du lac avoisinant.

hum mhm&lt;/p&gt;&lt;p&gt;&lt;a href=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/2411818_pJXDwQ#!i=126456308&amp;k=SBzz4"" title=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ...""&gt;&lt;img src=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Th-6.jpg"" width=""150"" height=""150"" alt=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ..."" title=""Autumn Ethereal
Golden reflections

***

""L'automne éthéré""
vision tres particuliere d'une feuille avec la rosée, dans laquelle se refletent, avec une certaine puissance, les arbes extre ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://autumn-ethereal.smugmug.com/EtherealNature/Quintessence/Autumnethereal/126456308_SBzz4-Th-6.jpg"" width=""150"" height=""150""/>
      <media:category>Ethereal Nature</media:category>
      <media:keywords>autumn, ethereal, leaf, macro, water, drop, gold, alexandre deschaumes, workshop, photographe, haute savoie, france, rhone alpe, cours photo, stage photo</media:keywords>
      <media:copyright url=""http://www.alexandredeschaumes.com"">Alexandre Deschaumes (Autumn-Ethereal)</media:copyright>
      <media:credit role=""photographer"">Alexandre Deschaumes (Autumn-Ethereal)</media:credit>
    </item>
    <item>
      <title>April 11, 2009   Those eyes - say it all.</title>
      <link>http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/7778665_KMgD9T#!i=509858620&amp;k=avq3J</link>
      <description>&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com""&gt;JenniferDiFranco&lt;/a&gt;&lt;br /&gt;April 11, 2009   Those eyes - say it all.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/7778665_KMgD9T#!i=509858620&amp;k=avq3J"" title=""April 11, 2009   Those eyes - say it all.""&gt;&lt;img src=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Th.jpg"" width=""100"" height=""150"" alt=""April 11, 2009   Those eyes - say it all."" title=""April 11, 2009   Those eyes - say it all."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Through My Eyes - Daily Photo</category>
      <pubDate>Fri, 10 Apr 2009 21:33:38 -0700</pubDate>
      <author>nobody@smugmug.com (JenniferDiFranco)</author>
      <guid isPermaLink=""false"">http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Th.jpg</guid>
      <exif:DateTimeOriginal>2009-04-10 01:51:24</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Ti.jpg"" fileSize=""2948"" type=""image/jpeg"" medium=""image"" width=""67"" height=""100"">
          <media:hash algo=""md5"">10d47fd7f317d9019112636e110fb5e7</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Th.jpg"" fileSize=""5267"" type=""image/jpeg"" medium=""image"" width=""100"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">37aa8125f617a7d574b30e7181a26d36</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-S.jpg"" fileSize=""21191"" type=""image/jpeg"" medium=""image"" width=""200"" height=""300"">
          <media:hash algo=""md5"">7e544a420e4bca2cc75e6f098e63a211</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-M.jpg"" fileSize=""42131"" type=""image/jpeg"" medium=""image"" width=""300"" height=""450"">
          <media:hash algo=""md5"">c0859d14f0fe1642eed952096bd58e04</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-L.jpg"" fileSize=""70280"" type=""image/jpeg"" medium=""image"" width=""400"" height=""600"">
          <media:hash algo=""md5"">9a8d8874a92b638d8a0af3e0b2f083d0</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-XL.jpg"" fileSize=""110458"" type=""image/jpeg"" medium=""image"" width=""512"" height=""768"">
          <media:hash algo=""md5"">89ae346b67273835153b2b0f1180ee4e</media:hash>
        </media:content>
        <media:content url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-X2.jpg"" fileSize=""165518"" type=""image/jpeg"" medium=""image"" width=""640"" height=""960"">
          <media:hash algo=""md5"">20c4e57a1be35963882444a6dcd4cc34</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">April 11, 2009   Those eyes - say it all.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com""&gt;JenniferDiFranco&lt;/a&gt;&lt;br /&gt;April 11, 2009   Those eyes - say it all.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/7778665_KMgD9T#!i=509858620&amp;k=avq3J"" title=""April 11, 2009   Those eyes - say it all.""&gt;&lt;img src=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Th.jpg"" width=""100"" height=""150"" alt=""April 11, 2009   Those eyes - say it all."" title=""April 11, 2009   Those eyes - say it all."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://jenniferdifranco.smugmug.com/Through-My-Eyes-Daily-Photo/Daily-Photo-April-2009/IMG6153/509858620_avq3J-Th.jpg"" width=""100"" height=""150""/>
      <media:category>Through My Eyes - Daily Photo</media:category>
      <media:copyright url=""http://jenniferdifranco.smugmug.com"">JenniferDiFranco</media:copyright>
      <media:credit role=""photographer"">JenniferDiFranco</media:credit>
    </item>
    <item>
      <title>lordv's photo</title>
      <link>http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120517663&amp;k=5NCN4</link>
      <description>&lt;p&gt;&lt;a href=""http://lordv.smugmug.com""&gt;lordv&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120517663&amp;k=5NCN4"" title=""lordv's photo""&gt;&lt;img src=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Th.jpg"" width=""150"" height=""100"" alt=""lordv's photo"" title=""lordv's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Macrophotography</category>
      <pubDate>Tue, 02 Jan 2007 08:45:55 -0800</pubDate>
      <author>nobody@smugmug.com (lordv)</author>
      <guid isPermaLink=""false"">http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Th.jpg</guid>
      <media:group>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Ti.jpg"" fileSize=""2072"" type=""image/jpeg"" medium=""image"" width=""100"" height=""66"">
          <media:hash algo=""md5"">4277ff5202d52328b3ffdaa29b5d18dc</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Th.jpg"" fileSize=""3151"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">74f90c4fde6dae26f4f8ab240322875c</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-S.jpg"" fileSize=""19773"" type=""image/jpeg"" medium=""image"" width=""400"" height=""266"">
          <media:hash algo=""md5"">32f7b10df38c3a70b379ab66a42c9f7b</media:hash>
        </media:content>
        <media:content url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-M.jpg"" fileSize=""34300"" type=""image/jpeg"" medium=""image"" width=""600"" height=""398"">
          <media:hash algo=""md5"">37f3b4c9f7efaa01292e1cb35242e55a</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">lordv's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://lordv.smugmug.com""&gt;lordv&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/2305526_mh3cDD#!i=120517663&amp;k=5NCN4"" title=""lordv's photo""&gt;&lt;img src=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Th.jpg"" width=""150"" height=""100"" alt=""lordv's photo"" title=""lordv's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://lordv.smugmug.com/Macrophotography/Printable-Macros/IMG7507sa/120517663_5NCN4-Th.jpg"" width=""150"" height=""100""/>
      <media:category>Macrophotography</media:category>
      <media:copyright url=""http://lordv.smugmug.com"">lordv</media:copyright>
      <media:credit role=""photographer"">lordv</media:credit>
    </item>
    <item>
      <title>Smooth Rock at Sunset, Olympic National Park</title>
      <link>http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/3832028_hPw2gs#!i=452656933&amp;k=LfWug</link>
      <description>&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Smooth Rock at Sunset, Olympic National Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/3832028_hPw2gs#!i=452656933&amp;k=LfWug"" title=""Smooth Rock at Sunset, Olympic National Park""&gt;&lt;img src=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Th.jpg"" width=""150"" height=""99"" alt=""Smooth Rock at Sunset, Olympic National Park"" title=""Smooth Rock at Sunset, Olympic National Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Pacific Northwest Nature Photography</category>
      <pubDate>Sat, 10 Jan 2009 20:07:22 -0800</pubDate>
      <author>nobody@smugmug.com (Daniel Ewert)</author>
      <guid isPermaLink=""false"">http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Th.jpg</guid>
      <media:group>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Ti.jpg"" fileSize=""3771"" type=""image/jpeg"" medium=""image"" width=""100"" height=""66"">
          <media:hash algo=""md5"">1e64967eb5fa9443d88f5d1d5fcf90bb</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Th.jpg"" fileSize=""7045"" type=""image/jpeg"" medium=""image"" width=""150"" height=""99"" isDefault=""true"">
          <media:hash algo=""md5"">b5d314a0df06eed36c22ccc1499f90a5</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-S.jpg"" fileSize=""39646"" type=""image/jpeg"" medium=""image"" width=""400"" height=""263"">
          <media:hash algo=""md5"">a57743624ffa6a18a1ac7fe0e8330b9c</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-M.jpg"" fileSize=""78566"" type=""image/jpeg"" medium=""image"" width=""600"" height=""395"">
          <media:hash algo=""md5"">85633b912c493a5b936d545cb22118f3</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-L.jpg"" fileSize=""128356"" type=""image/jpeg"" medium=""image"" width=""800"" height=""526"">
          <media:hash algo=""md5"">57312794302a30683403f1ffc7c53b05</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-XL.jpg"" fileSize=""131701"" type=""image/jpeg"" medium=""image"" width=""815"" height=""536"">
          <media:hash algo=""md5"">bc9c7e0f72e1c6ecb2997277d3286d47</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-X2.jpg"" fileSize=""361467"" type=""image/jpeg"" medium=""image"" width=""815"" height=""536"">
          <media:hash algo=""md5"">f991d043471a64f411af1af64b2bb89c</media:hash>
        </media:content>
        <media:content url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-O.jpg"" fileSize=""361467"" type=""image/jpeg"" medium=""image"" width=""815"" height=""536"">
          <media:hash algo=""md5"">f991d043471a64f411af1af64b2bb89c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Smooth Rock at Sunset, Olympic National Park</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ewert.smugmug.com""&gt;Daniel Ewert&lt;/a&gt;&lt;br /&gt;Smooth Rock at Sunset, Olympic National Park&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/3832028_hPw2gs#!i=452656933&amp;k=LfWug"" title=""Smooth Rock at Sunset, Olympic National Park""&gt;&lt;img src=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Th.jpg"" width=""150"" height=""99"" alt=""Smooth Rock at Sunset, Olympic National Park"" title=""Smooth Rock at Sunset, Olympic National Park"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ewert.smugmug.com/Pacific-Northwest-Nature/Pacific-Northwest-Nature/ShiTanRock/452656933_LfWug-Th.jpg"" width=""150"" height=""99""/>
      <media:category>Pacific Northwest Nature Photography</media:category>
      <media:keywords>stormy, sky, olympic, national, park, shi, beach, washington, neah, bay, cape, flattery</media:keywords>
      <media:copyright url=""http://www.ewertnaturephotography.com"">Daniel Ewert</media:copyright>
      <media:credit role=""photographer"">Daniel Ewert</media:credit>
    </item>
    <item>
      <title>Middle North Falls, Silver Falls State Park, OR</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=518244957&amp;k=wHoyY</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Middle North Falls, Silver Falls State Park, OR&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=518244957&amp;k=wHoyY"" title=""Middle North Falls, Silver Falls State Park, OR""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Th-3.jpg"" width=""150"" height=""100"" alt=""Middle North Falls, Silver Falls State Park, OR"" title=""Middle North Falls, Silver Falls State Park, OR"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Tue, 21 Apr 2009 22:58:32 -0700</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Th-3.jpg</guid>
      <exif:DateTimeOriginal>2009-04-16 14:47:07</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Ti-3.jpg"" fileSize=""4539"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">bc99bc6be8d55bd62d2f9fe43bbefa47</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Th-3.jpg"" fileSize=""7859"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">5fe60cb9499b3100f2186e7834f4a422</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-S-3.jpg"" fileSize=""39959"" type=""image/jpeg"" medium=""image"" width=""400"" height=""267"">
          <media:hash algo=""md5"">079f68ba588ba2b6f2916e02ef1ceae3</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-M-3.jpg"" fileSize=""72637"" type=""image/jpeg"" medium=""image"" width=""600"" height=""400"">
          <media:hash algo=""md5"">d9d97d076e935cf9433aaf16c7e7a900</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-L-3.jpg"" fileSize=""114286"" type=""image/jpeg"" medium=""image"" width=""800"" height=""534"">
          <media:hash algo=""md5"">a5a5858fe91bcf6a7feca348b98efdb8</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Middle North Falls, Silver Falls State Park, OR</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt;&lt;br /&gt;Middle North Falls, Silver Falls State Park, OR&lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=518244957&amp;k=wHoyY"" title=""Middle North Falls, Silver Falls State Park, OR""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Th-3.jpg"" width=""150"" height=""100"" alt=""Middle North Falls, Silver Falls State Park, OR"" title=""Middle North Falls, Silver Falls State Park, OR"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/IMG0319/518244957_wHoyY-Th-3.jpg"" width=""150"" height=""100""/>
      <media:category>Other</media:category>
      <media:keywords>middle north falls, silver falls, waterfall, nature, landscape, blue, water</media:keywords>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>Crab-eater seals</title>
      <link>http://ptutty.smugmug.com/Nature/Antarctica/4085897_DkCj7M#!i=238099054&amp;k=RcgCK</link>
      <description>&lt;p&gt;&lt;a href=""http://ptutty.smugmug.com""&gt;Paul Tuttle (ptutty)&lt;/a&gt;&lt;br /&gt;Crab-eater seals&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ptutty.smugmug.com/Nature/Antarctica/4085897_DkCj7M#!i=238099054&amp;k=RcgCK"" title=""Crab-eater seals""&gt;&lt;img src=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Th-2.jpg"" width=""150"" height=""114"" alt=""Crab-eater seals"" title=""Crab-eater seals"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Nature</category>
      <pubDate>Mon, 31 Dec 2007 16:11:37 -0800</pubDate>
      <author>nobody@smugmug.com (Paul Tuttle (ptutty))</author>
      <guid isPermaLink=""false"">http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Th-2.jpg</guid>
      <exif:DateTimeOriginal>2007-12-23 09:37:56</exif:DateTimeOriginal>
      <geo:lat>-63.59028100000000</geo:lat>
      <geo:long>-58.18359400000000</geo:long>
      <geo:alt>0</geo:alt>
      <media:group>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Ti-2.jpg"" fileSize=""4352"" type=""image/jpeg"" medium=""image"" width=""100"" height=""76"">
          <media:hash algo=""md5"">921b61fc269ea8b3aca92f0ce0826a3d</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Th-2.jpg"" fileSize=""7646"" type=""image/jpeg"" medium=""image"" width=""150"" height=""114"" isDefault=""true"">
          <media:hash algo=""md5"">788cbd624fa43a4152b5125b02509fa7</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-S-2.jpg"" fileSize=""37225"" type=""image/jpeg"" medium=""image"" width=""396"" height=""300"">
          <media:hash algo=""md5"">8e9d3aa075d91da4e5cb98fad88e5a6b</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-M-2.jpg"" fileSize=""69316"" type=""image/jpeg"" medium=""image"" width=""594"" height=""450"">
          <media:hash algo=""md5"">da52a6e4b4d4d5df1624ff2a1a3b16a4</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-L-2.jpg"" fileSize=""110491"" type=""image/jpeg"" medium=""image"" width=""792"" height=""600"">
          <media:hash algo=""md5"">84416268ceee34bb4fb839ce2cdb89f5</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-XL-2.jpg"" fileSize=""171575"" type=""image/jpeg"" medium=""image"" width=""1013"" height=""768"">
          <media:hash algo=""md5"">21792fb6a3056caa95593e72951d681f</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-X2-2.jpg"" fileSize=""259758"" type=""image/jpeg"" medium=""image"" width=""1266"" height=""960"">
          <media:hash algo=""md5"">7306e12e2844a55a8cb1d19e7c722656</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-X3-2.jpg"" fileSize=""397040"" type=""image/jpeg"" medium=""image"" width=""1583"" height=""1200"">
          <media:hash algo=""md5"">6423825fe2611dde083a48916e6f334d</media:hash>
        </media:content>
        <media:content url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-O-2.jpg"" fileSize=""1423123"" type=""image/jpeg"" medium=""image"" width=""2006"" height=""1521"">
          <media:hash algo=""md5"">7e6ceb85a593de3b37e8fd0ed6e869ac</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Crab-eater seals</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://ptutty.smugmug.com""&gt;Paul Tuttle (ptutty)&lt;/a&gt;&lt;br /&gt;Crab-eater seals&lt;/p&gt;&lt;p&gt;&lt;a href=""http://ptutty.smugmug.com/Nature/Antarctica/4085897_DkCj7M#!i=238099054&amp;k=RcgCK"" title=""Crab-eater seals""&gt;&lt;img src=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Th-2.jpg"" width=""150"" height=""114"" alt=""Crab-eater seals"" title=""Crab-eater seals"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://ptutty.smugmug.com/Nature/Antarctica/DSC3970/238099054_RcgCK-Th-2.jpg"" width=""150"" height=""114""/>
      <media:category>Nature</media:category>
      <media:keywords>seals, crabeater, antarctica</media:keywords>
      <media:copyright url=""http://ptutty.smugmug.com"">Paul Tuttle (ptutty)</media:copyright>
      <media:credit role=""photographer"">Paul Tuttle (ptutty)</media:credit>
    </item>
    <item>
      <title>laurajohnson's photo</title>
      <link>http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=476021832&amp;k=Z8fS8</link>
      <description>&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=476021832&amp;k=Z8fS8"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Th-4.jpg"" width=""139"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Other</category>
      <pubDate>Tue, 17 Feb 2009 22:47:26 -0800</pubDate>
      <author>nobody@smugmug.com (laurajohnson)</author>
      <guid isPermaLink=""false"">http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Th-4.jpg</guid>
      <exif:DateTimeOriginal>2008-05-17 20:09:58</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Ti-4.jpg"" fileSize=""6831"" type=""image/jpeg"" medium=""image"" width=""93"" height=""100"">
          <media:hash algo=""md5"">d6ed7f040db246ce2afe0d15d05c4915</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Th-4.jpg"" fileSize=""13412"" type=""image/jpeg"" medium=""image"" width=""139"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">fe224fb6353fbe8ce179da8bce492842</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-S-4.jpg"" fileSize=""48489"" type=""image/jpeg"" medium=""image"" width=""279"" height=""300"">
          <media:hash algo=""md5"">4249e203e8eb70aa8070b6d951b4a5b5</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-M-4.jpg"" fileSize=""93932"" type=""image/jpeg"" medium=""image"" width=""418"" height=""450"">
          <media:hash algo=""md5"">75b5bbf3f3ff886374373cf7317444a1</media:hash>
        </media:content>
        <media:content url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-L-4.jpg"" fileSize=""149585"" type=""image/jpeg"" medium=""image"" width=""557"" height=""600"">
          <media:hash algo=""md5"">a639176e7748909e6aaa77ecd24b3dc5</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">laurajohnson's photo</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com""&gt;laurajohnson&lt;/a&gt; &lt;/p&gt;&lt;p&gt;&lt;a href=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/13499095_KQnRjV#!i=476021832&amp;k=Z8fS8"" title=""laurajohnson's photo""&gt;&lt;img src=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Th-4.jpg"" width=""139"" height=""150"" alt=""laurajohnson's photo"" title=""laurajohnson's photo"" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://laurajohnson.smugmug.com/Other/Most-Popular-Photos/302631866img5582/476021832_Z8fS8-Th-4.jpg"" width=""139"" height=""150""/>
      <media:category>Other</media:category>
      <media:copyright url=""http://laurajohnson.smugmug.com"">laurajohnson</media:copyright>
      <media:credit role=""photographer"">laurajohnson</media:credit>
    </item>
    <item>
      <title>""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ...</title>
      <link>http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/6113322_mRJbSh#!i=395670934&amp;k=ZKsBK</link>
      <description>&lt;p&gt;&lt;a href=""http://billpador.smugmug.com""&gt;Bill Pador&lt;/a&gt;&lt;br /&gt;&lt;center&gt;&lt;em&gt;&lt;h2&gt;&lt;u&gt;""The Road to Color""&lt;/u&gt;&lt;/h2&gt;&lt;/em&gt;&lt;/center&gt;&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small fire road and was shocked by the colors in this tunnel like ride. it was raining pretty good all day today so there was no sun and nasty puddles everywhere but I was able to snap this by opening my sun roof and sticking the D300 out on constant focus and shot it while doing about 10 mph. I was amazed by the composition I was able to get since I was in the center of the road and being above the car it gave me some extra height then the usual eye view, I think it worked out pretty well LOL.&#13;
&#13;
In post I did a minor contrast adjustment and small crop off the bottom, that's it! I'm enjoying the colors here now but I see them disappearing very rapidly so I'm going to try and get some shots everyday now until their gone. It's supposed to get very cold here starting tonight in the 50's as a high during the day and low 40's to high 30's at night Brrrrrrrrrr. I got some other shots from today to process yet but I will be putting them in my Fall Colors 2008 gallery when there finished.&#13;
&#13;
Hope you all enjoy your day,&#13;
Bill Pador&lt;/p&gt;&lt;p&gt;&lt;a href=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/6113322_mRJbSh#!i=395670934&amp;k=ZKsBK"" title=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ...""&gt;&lt;img src=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Th-1.jpg"" width=""150"" height=""150"" alt=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ..."" title=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Galleries</category>
      <pubDate>Thu, 16 Oct 2008 17:04:46 -0700</pubDate>
      <author>nobody@smugmug.com (Bill Pador)</author>
      <guid isPermaLink=""false"">http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Th-1.jpg</guid>
      <exif:DateTimeOriginal>2008-10-16 12:14:06</exif:DateTimeOriginal>
      <media:group>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Ti-1.jpg"" fileSize=""9977"" type=""image/jpeg"" medium=""image"" width=""100"" height=""100"">
          <media:hash algo=""md5"">c484c72610f2d0ea5426cea412f3ea90</media:hash>
        </media:content>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Th-1.jpg"" fileSize=""21746"" type=""image/jpeg"" medium=""image"" width=""150"" height=""150"" isDefault=""true"">
          <media:hash algo=""md5"">405f10c70e674e781d81877de6a40174</media:hash>
        </media:content>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-S-1.jpg"" fileSize=""106488"" type=""image/jpeg"" medium=""image"" width=""400"" height=""257"">
          <media:hash algo=""md5"">1b96a18fce7033df62543c4c7419c4a4</media:hash>
        </media:content>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-M-1.jpg"" fileSize=""232451"" type=""image/jpeg"" medium=""image"" width=""600"" height=""385"">
          <media:hash algo=""md5"">1fcee59cd30f30d912647168138f33e1</media:hash>
        </media:content>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-L-1.jpg"" fileSize=""402842"" type=""image/jpeg"" medium=""image"" width=""800"" height=""513"">
          <media:hash algo=""md5"">6b9e8252dd23e0863f650ee2fb55e9e8</media:hash>
        </media:content>
        <media:content url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-XL-1.jpg"" fileSize=""688339"" type=""image/jpeg"" medium=""image"" width=""1024"" height=""657"">
          <media:hash algo=""md5"">91b107adcef1fa8b0a1928daf01e6f48</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ...</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://billpador.smugmug.com""&gt;Bill Pador&lt;/a&gt;&lt;br /&gt;&lt;center&gt;&lt;em&gt;&lt;h2&gt;&lt;u&gt;""The Road to Color""&lt;/u&gt;&lt;/h2&gt;&lt;/em&gt;&lt;/center&gt;&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small fire road and was shocked by the colors in this tunnel like ride. it was raining pretty good all day today so there was no sun and nasty puddles everywhere but I was able to snap this by opening my sun roof and sticking the D300 out on constant focus and shot it while doing about 10 mph. I was amazed by the composition I was able to get since I was in the center of the road and being above the car it gave me some extra height then the usual eye view, I think it worked out pretty well LOL.&#13;
&#13;
In post I did a minor contrast adjustment and small crop off the bottom, that's it! I'm enjoying the colors here now but I see them disappearing very rapidly so I'm going to try and get some shots everyday now until their gone. It's supposed to get very cold here starting tonight in the 50's as a high during the day and low 40's to high 30's at night Brrrrrrrrrr. I got some other shots from today to process yet but I will be putting them in my Fall Colors 2008 gallery when there finished.&#13;
&#13;
Hope you all enjoy your day,&#13;
Bill Pador&lt;/p&gt;&lt;p&gt;&lt;a href=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/6113322_mRJbSh#!i=395670934&amp;k=ZKsBK"" title=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ...""&gt;&lt;img src=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Th-1.jpg"" width=""150"" height=""150"" alt=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ..."" title=""""The Road to Color""&#13;
10/16/08&#13;
While Driving today I got stuck behind a SLOW POKE on the only single lane road leading out of town and was getting frustrated so I jumped off onto this small  ..."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://billpador.smugmug.com/Galleries/Photo-of-The-Day-2008/October-08-Photo-of-The-Day/FallColorsRoad/395670934_ZKsBK-Th-1.jpg"" width=""150"" height=""150""/>
      <media:category>Galleries</media:category>
      <media:keywords>trees fall autumn 2008 bill pador photography autumn sky colors, fall, colors, road</media:keywords>
      <media:copyright url=""http://www.billpador.com"">Bill Pador</media:copyright>
      <media:credit role=""photographer"">Bill Pador</media:credit>
    </item>
    <item>
      <title>Northwest Connecticut. May 2006.</title>
      <link>http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=71106934&amp;k=V7gNk</link>
      <description>&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Northwest Connecticut. May 2006.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=71106934&amp;k=V7gNk"" title=""Northwest Connecticut. May 2006.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Th-1.jpg"" width=""150"" height=""100"" alt=""Northwest Connecticut. May 2006."" title=""Northwest Connecticut. May 2006."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</description>
      <category>Landscapes</category>
      <pubDate>Tue, 23 May 2006 00:50:58 -0700</pubDate>
      <author>nobody@smugmug.com (winchester)</author>
      <guid isPermaLink=""false"">http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Th-1.jpg</guid>
      <media:group>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Ti-1.jpg"" fileSize=""4229"" type=""image/jpeg"" medium=""image"" width=""100"" height=""67"">
          <media:hash algo=""md5"">9a28736c19b901b17f6a7206142952b9</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Th-1.jpg"" fileSize=""7590"" type=""image/jpeg"" medium=""image"" width=""150"" height=""100"" isDefault=""true"">
          <media:hash algo=""md5"">1586be7a44888c6262dec0edaff60a15</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-S-1.jpg"" fileSize=""36679"" type=""image/jpeg"" medium=""image"" width=""400"" height=""268"">
          <media:hash algo=""md5"">4308636e4fa83767a9ffe449733ab6bb</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-M-1.jpg"" fileSize=""71549"" type=""image/jpeg"" medium=""image"" width=""600"" height=""402"">
          <media:hash algo=""md5"">7a51f34a659bc09b3d704c01ad7e99fd</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-L-1.jpg"" fileSize=""117485"" type=""image/jpeg"" medium=""image"" width=""800"" height=""535"">
          <media:hash algo=""md5"">c0e2eedaddd03f938c9f2e95a1a13c18</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-XL-1.jpg"" fileSize=""234461"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""676"">
          <media:hash algo=""md5"">feda331dbe9e1ec411d887460af4836c</media:hash>
        </media:content>
        <media:content url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-O-1.jpg"" fileSize=""234461"" type=""image/jpeg"" medium=""image"" width=""1010"" height=""676"">
          <media:hash algo=""md5"">feda331dbe9e1ec411d887460af4836c</media:hash>
        </media:content>
      </media:group>
      <media:title type=""html"">Northwest Connecticut. May 2006.</media:title>
      <media:text type=""html"">&lt;p&gt;&lt;a href=""http://winchester.smugmug.com""&gt;winchester&lt;/a&gt;&lt;br /&gt;Northwest Connecticut. May 2006.&lt;/p&gt;&lt;p&gt;&lt;a href=""http://winchester.smugmug.com/Landscapes/Favorites/1259043_cHZrKr#!i=71106934&amp;k=V7gNk"" title=""Northwest Connecticut. May 2006.""&gt;&lt;img src=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Th-1.jpg"" width=""150"" height=""100"" alt=""Northwest Connecticut. May 2006."" title=""Northwest Connecticut. May 2006."" style=""border: 1px solid #000000;"" /&gt;&lt;/a&gt;&lt;/p&gt;</media:text>
      <media:thumbnail url=""http://winchester.smugmug.com/Landscapes/Favorites/three/71106934_V7gNk-Th-1.jpg"" width=""150"" height=""100""/>
      <media:category>Landscapes</media:category>
      <media:keywords>connecticut, sunset, lake, water, rocks, reflection, blue, surreal, winchester lake</media:keywords>
      <media:copyright url=""http://winchester.smugmug.com"">winchester</media:copyright>
      <media:credit role=""photographer"">winchester</media:credit>
    </item>
  </channel>
</rss>

";
    }
}
