using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Get the ASP.NET 1.x-style configuration data...
            string messageHtml = ASPNET1Configuration.GetConfig().Message;
            
            if (ASPNET1Configuration.GetConfig().ShowMessageInBold)
                messageHtml = "<b>" + messageHtml + "</b>";
            
            int favNumber = ASPNET1Configuration.GetConfig().FavoriteNumber;
            
            string favColors = string.Empty;
            if (ASPNET1Configuration.GetConfig().FavoriteColors.Count == 0)
                favColors = "<i>None</i>";
            else
            {
                foreach(string color in ASPNET1Configuration.GetConfig().FavoriteColors)
                    favColors += color + ", ";

                // Remove the trailing ", "
                favColors = favColors.Substring(0, favColors.Length - 2);
            }
                

            // Display the configuration settings
            aspnet1Values.Text = string.Format("The message is {0} and your favorite number is {1}. Your favorite colors are {2}...", messageHtml, favNumber, favColors);
        }
    }
}
