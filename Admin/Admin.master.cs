using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPDM.LucasD.Midterm.BLL;
using CPDM.LucasD.Midterm.Models;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    public bool PermissionAdd { get; private set; }
    public bool PermissionEdit { get; private set; }
    public bool PermissionLimitedEdit { get; private set; }
    public bool PermissionDelete { get; private set; }
    public bool PermissionPublish { get; private set; }
    public string userID { get { return Context.User.Identity.Name.ToString(); } }

    
    protected void Page_Init(object sender, EventArgs e)
    {
        User currentUser = new UserDbAccess().GetByID(int.Parse(Context.User.Identity.Name.ToString()));
        if (Session["CurrentUserRole"] == null)
        {            
            Session.Add("CurrentUserRole", currentUser.Role);
        }
        switch (Session["CurrentUserRole"].ToString())
        {
            case "Administrator":
                PermissionAdd = true;
                PermissionEdit = true;
                PermissionLimitedEdit = false;
                PermissionDelete = true;
                PermissionPublish = true;
                break;
            case "Publisher":
                PermissionAdd = false;
                PermissionEdit = false;
                PermissionLimitedEdit = false;
                PermissionDelete = false;
                PermissionPublish = true;
                break;
            case "Author":
                PermissionAdd = true;
                PermissionEdit = false;
                PermissionLimitedEdit = true;
                PermissionDelete = false;
                PermissionPublish = false;
                break;
            default:
                break;
        }
        logout.Text = "Logout " + currentUser.FirstName + " " + currentUser.LastName;
        lnkAddArticle.Visible = PermissionAdd;

    }
}
