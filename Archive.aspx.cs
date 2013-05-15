using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Archive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = Page.Header.Title + " | Archived Articles";
        Master.ActivePage = "Archives";
    }
    protected void lvArticles_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "OpenArticle")
        {
            Response.Redirect("~/Article.aspx?ArticleID=" + e.CommandArgument.ToString());
        }
    }
}