using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Represents the configuration information for the blog engine specified in Web.config
/// </summary>
public class ASPNET1Configuration
{
    /// <summary>
    /// Returns an ASPNET1Configuration instance
    /// </summary>
    public static ASPNET1Configuration GetConfig()
    {
        return ConfigurationSettings.GetConfig("aspnet1Configuration") as ASPNET1Configuration;
    }

    // The configuration member variables (and default values) and corresponding properties
    private string _message = "Hello, World!";
    private int _favoriteNumber = 2;
    private bool _showMessageInBold = true;
    private StringCollection _favColors = new StringCollection();

    public string Message { get { return _message; } }
    public int FavoriteNumber { get { return _favoriteNumber; } }
    public bool ShowMessageInBold { get { return _showMessageInBold; } }
    public StringCollection FavoriteColors { get { return _favColors; } }


    /// <summary>
    /// Loads in the configuration information from the passed-in XmlNode
    /// </summary>
    internal void LoadValuesFromXml(XmlNode section)
    {
        XmlAttributeCollection attrs = section.Attributes;

        if (attrs["message"] != null)
        {
            _message = attrs["message"].Value;
            attrs.RemoveNamedItem("message");
        }

        if (attrs["favoriteNumber"] != null)
        {
            _favoriteNumber = Convert.ToInt32(attrs["favoriteNumber"].Value);
            attrs.RemoveNamedItem("favoriteNumber");
        }

        if (attrs["showMessageInBold"] != null)
        {
            _showMessageInBold = XmlConvert.ToBoolean(attrs["showMessageInBold"].Value);
            attrs.RemoveNamedItem("showMessageInBold");
        }

        // If there are any further attributes, there's an error!
        if (attrs.Count > 0)
            throw new ConfigurationException("There are illegal attributes provided in the <aspnet1Configuration> section");


        // Now, get the <favoriteColors> child node
        bool processedFavColors = false;
        foreach (XmlNode childNode in section.ChildNodes)
        {
            if (!processedFavColors && childNode.Name == "favoriteColors")
            {
                processedFavColors = true;

                foreach (XmlNode favColorNode in childNode.ChildNodes)
                {
                    if (favColorNode.Name == "color")
                    {
                        // Get the text node value
                        XmlNode textNode = favColorNode.ChildNodes[0];
                        if (textNode == null)
                            throw new ConfigurationException("You must provide a text value for each <color> element, like <color>Red</color>");
                        else
                            _favColors.Add(textNode.Value);
                    }
                    else
                        throw new ConfigurationException("<favoriteColors> can only contain child elements named <color>");
                }
            }
            else
                throw new ConfigurationException("<aspnet1Configuration> may only contain one child element and it must be named <favoriteColors>");
        }
    }
}
