using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using FeedRetriever.Configuration;

namespace FeedRetriever
{
public class FeedRetriever
{
    public static FeedRetrieverSection _Config = ConfigurationManager.GetSection("feedRetriever") as FeedRetrieverSection;
    public static void GetFeeds()
    {
        foreach (FeedElement feedEl in _Config.Feeds)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(feedEl.Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string feedData = String.Empty;

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    feedData = reader.ReadToEnd();
                }

                if (feedEl.Cache)
                {
                    // filename of cache file
                    string filename = String.Format("{0}_{1}.xml", feedEl.Name, DateTime.Now.Ticks);

                    // cache file
                    using (StreamWriter writer = new StreamWriter(@"C:\" + filename))
                    {
                        writer.Write(feedData);
                    }
                }
            }
        }
    }
}
}
