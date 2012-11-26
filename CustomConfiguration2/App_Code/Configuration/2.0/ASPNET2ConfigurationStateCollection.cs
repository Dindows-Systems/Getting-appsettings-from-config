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
/// Summary description for ASPNET2ConfigurationStateCollection
/// </summary>
public class ASPNET2ConfigurationStateCollection : ConfigurationElementCollection
{
    public ASPNET2ConfigurationState this[int index]
    {
        get
        {
            return base.BaseGet(index) as ASPNET2ConfigurationState;
        }
        set
        {
            if (base.BaseGet(index) != null)
            {
                base.BaseRemoveAt(index);
            }
            this.BaseAdd(index, value);
        }
    }


    protected override ConfigurationElement CreateNewElement()
    {
        return new ASPNET2ConfigurationState();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
        return ((ASPNET2ConfigurationState)element).Name;
    } 
}
