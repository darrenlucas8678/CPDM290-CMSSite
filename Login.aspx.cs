using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CPDM.LucasD.Midterm.BLL;
using CPDM.LucasD.Midterm.Models;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = Page.Header.Title + " | Login";
        Master.ActivePage = "Login";
        if (Request.QueryString["Logout"] != null)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string userName = UserName.Text.ToString();
        string password = Password.Text.ToString();

        int? userid = new UserDbAccess().Authenticate(userName, password);

        if (userid != null)
        {
            FormsAuthentication.RedirectFromLoginPage(userid.ToString(), RememberMe.Checked);
        }
        else
        {
            LoginFailed.IsValid = false;
        }

    }
}