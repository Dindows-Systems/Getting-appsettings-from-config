using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FeedRetriever.Configuration
{
    public class FeedElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true, DefaultValue="http://localhost")]
        [RegexStringValidator(@"https?\://\S+")]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("cache", IsRequired = false, DefaultValue = true)]
        public bool Cache
        {
            get { return (bool)this["cache"]; }
            set { this["cache"] = value; }
        }
    }
}
