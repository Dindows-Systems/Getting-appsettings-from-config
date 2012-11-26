using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Summary description for ASPNET2ConfigurationSection
/// </summary>
public class ASPNET2Configuration : ConfigurationSection
{
    /// <summary>
    /// Returns an ASPNET2Configuration instance
    /// </summary>
    public static ASPNET2Configuration GetConfig()
    {
        return ConfigurationManager.GetSection("customConfigDemo/aspnet2ConfigurationDemo") as ASPNET2Configuration;
    }


    [ConfigurationProperty("quoteOfTheDay", DefaultValue="It is what it is.", IsRequired=false)]
    public string QuoteOfTheDay
    {
        get
        {
            return this["quoteOfTheDay"] as string;
        }
    }

    [ConfigurationProperty("yourAge", IsRequired=true)]
    public int YourAge
    {
        get
        {
            return (int) this["yourAge"];
        }
    }

    [ConfigurationProperty("favoriteStates")]
    public ASPNET2ConfigurationStateCollection FavoriteStates
    {
        get 
        { 
            return this["favoriteStates"] as ASPNET2ConfigurationStateCollection; 
        }
    } 
}
