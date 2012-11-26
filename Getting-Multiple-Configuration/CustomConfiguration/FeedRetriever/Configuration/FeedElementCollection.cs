using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FeedRetriever.Configuration
{
    [ConfigurationCollection(typeof(FeedElement))]
    public class FeedElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FeedElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeedElement)element).Name;
        }
    }
}
