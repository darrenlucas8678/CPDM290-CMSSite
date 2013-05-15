using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Articles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = Page.Header.Title + " | Article Listing";
        Master.ActivePage = "Articles";
    }
    protected void lvArticles_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "OpenArticle")
        {
            Response.Redirect("~/Article.aspx?ArticleID=" + e.CommandArgument.ToString());
        }
    }
    protected void lvArticles_DataBound(object sender, EventArgs e)
    {
        dpArticles.Visible = (  dpArticles.PageSize < dpArticles.TotalRowCount);
    }
}