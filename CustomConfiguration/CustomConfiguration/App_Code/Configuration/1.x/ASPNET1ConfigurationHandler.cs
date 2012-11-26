using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Class used to load configuration information in BlogConfiguration from Web.config
/// </summary>
public class ASPNET1ConfigurationHandler : IConfigurationSectionHandler
{
    #region IConfigurationSectionHandler Members

    public object Create(object parent, object configContext, System.Xml.XmlNode section)
    {
        ASPNET1Configuration config = new ASPNET1Configuration();
        config.LoadValuesFromXml(section);

        return config;
    }

    #endregion
}
