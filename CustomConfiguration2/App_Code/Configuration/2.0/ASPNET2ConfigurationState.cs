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
/// Summary description for ASPNET2ConfigurationState
/// </summary>
public class ASPNET2ConfigurationState : ConfigurationElement
{
    [ConfigurationProperty("name", IsRequired=true)]
    public string Name
    {
        get
        {
            return this["name"] as string;
        }
    }


    [ConfigurationProperty("abbreviation", IsRequired = false)]
    public string Abbreviation
    {
        get
        {
            return this["abbreviation"] as string;
        }
    }
}
