using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPDM.LucasD.Midterm.BLL;
using CPDM.LucasD.Midterm.Models;

public partial class Articles : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Add("ListMode", null);
        }
        lnkFilterAll.Click += new EventHandler(ArticleFilter_Click);
        lnkFilterActive.Click += new EventHandler(ArticleFilter_Click);
        lnkFilterExpired.Click += new EventHandler(ArticleFilter_Click);
    }
    protected void lnkFilterAll_Click(object sender, EventArgs e)
    {
        Session["ListMode"] = String.Empty;
    }
    protected void lnkFilterActive_Click(object sender, EventArgs e)
    {
        Session["ListMode"] = "Active";
    }
    protected void lnkFilterExpired_Click(object sender, EventArgs e)
    {
        Session["ListMode"] = "Expired";
    }
    protected void grdArticleList_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit":
                Response.Redirect("~/Admin/ArticleEditor.aspx?ArticleID=" + e.CommandArgument);
                break;
            case "Delete":
                new ArticleDbAccess().Delete(int.Parse(e.CommandArgument.ToString()));
                break;
            case "Publish":
                new ArticleDbAccess().Publish(int.Parse(e.CommandArgument.ToString()));
                grdArticleList.DataBind();
                break;
            case "Unpublish":
                new ArticleDbAccess().Unpublish(int.Parse(e.CommandArgument.ToString()));
                grdArticleList.DataBind();
                break;
            default:
                break;
        }

    }
    protected void grdArticleList_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        grdArticleList.DataBind();
        uplArticleList.Update();
    }
    protected void ArticleFilter_Click(object sender, EventArgs e)
    {
        ControlCollection controlcollection = uplArticleList.ContentTemplateContainer.Controls;
        foreach (Control ctrl in controlcollection)
        {
            if (ctrl.GetType() == typeof(LinkButton))
            {
                ((LinkButton)ctrl).Enabled = true;
                ((LinkButton)ctrl).CssClass = "enabled";
            }
        }
        ((LinkButton)sender).Enabled = false;
        ((LinkButton)sender).CssClass = "disabled";
    }
}