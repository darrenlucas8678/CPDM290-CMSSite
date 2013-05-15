using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPDM.LucasD.Midterm.BLL;
using CPDM.LucasD.Midterm.Models;

public partial class Article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = Page.Header.Title + " | Article";
        if (!IsPostBack)
        {
            if (Request.QueryString["ArticleID"] != null)
            {
                int articleid;
                if (int.TryParse(Request.QueryString["ArticleID"], out articleid))
                {
                    var article = new ArticleDbAccess().GetByID(articleid,"Published");
                    if (article != null)
                    {
                        ltlTitle.Text = article.Title;
                        ltlCopy.Text = article.Copy;
                        imgArticleImage.ImageUrl = article.ArticleImageURL;
                    }
                    else
                    {
                        Response.Redirect("~/Articles.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Articles.aspx");
                }
            }
        }
    }
}
