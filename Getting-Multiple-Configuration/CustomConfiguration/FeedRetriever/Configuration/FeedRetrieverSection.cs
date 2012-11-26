using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FeedRetriever.Configuration
{
    public class FeedRetrieverSection : ConfigurationSection
    {
        [ConfigurationProperty("feeds", IsDefaultCollection = true)]
        public FeedElementCollection Feeds
        {
            get { return (FeedElementCollection)this["feeds"]; }
            set { this["feeds"] = value; }
        }
    }
}
