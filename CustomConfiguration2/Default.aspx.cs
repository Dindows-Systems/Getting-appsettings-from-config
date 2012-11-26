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
            // Get the ASP.NET 2.0-style configuration data...
            string quote = ASPNET2Configuration.GetConfig().QuoteOfTheDay;
            int age = ASPNET2Configuration.GetConfig().YourAge;
            List<string> favStates = new List<string>();
            
            string states = string.Empty;

            if (ASPNET2Configuration.GetConfig().FavoriteStates.Count == 0)
                states = "<i>You have not selected any states!</i>";
            else
            {
                foreach (ASPNET2ConfigurationState state in ASPNET2Configuration.GetConfig().FavoriteStates)
                    favStates.Add(string.Format("{0} ({1})", state.Name, state.Abbreviation));
                states = string.Join(", ", favStates.ToArray());
            }

            // Display the configuration settings
            aspnet2Values.Text = string.Format("You are {0} years old, and the quote of the day is: {1}. Your favorite states include: {2}.", age, quote, states);
        }
    }
}
