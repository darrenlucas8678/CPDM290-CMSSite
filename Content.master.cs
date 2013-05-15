using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Content : System.Web.UI.MasterPage
{
    public string ActivePage
    {
        set
        {
            lnkHome.CssClass = "";
            lnkArticles.CssClass = "";
            lnkArchives.CssClass = "";
            lnkLogin.CssClass = "";

            switch (value)
            {
                case "Home":
                    lnkHome.CssClass = "active";
                    break;
                case "Articles":
                    lnkArticles.CssClass = "active";
                    break;
                case "Archives":
                    lnkArchives.CssClass = "active";
                    break;
                case "Login":
                    lnkLogin.CssClass = "active";
                    break;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
