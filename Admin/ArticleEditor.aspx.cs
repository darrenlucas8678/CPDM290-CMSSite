using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using CPDM.LucasD.Midterm;
using CPDM.LucasD.Midterm.Models;
using CPDM.LucasD.Midterm.BLL;
using System.Web.Security;

public partial class Admin_ArticleEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ArticleID"] != null)
            {
                int articleid;
                if (int.TryParse(Request.QueryString["ArticleID"], out articleid))
                {
                    Article article = new ArticleDbAccess().GetByID(articleid);
                    if (article != null)
                    {
                        if (((Master.PermissionLimitedEdit && article.UserID == int.Parse(Master.userID)) || Master.PermissionEdit))
                        {
                            txtTitle.Text = article.Title;
                            txtSummary.Text = article.Summary;
                            txtCopy.Text = article.Copy;
                            imgArticleImage.ImageUrl = article.ArticleImageURL;
                            imgArticleImage.Visible = true;
                            imgArticleImage.Width = 100;
                            imgThumbnailImage.ImageUrl = article.ThumbnailImageURL;
                            imgThumbnailImage.Visible = true;
                            imgThumbnailImage.Width = 100;
                            txtStartDate.Text = String.Format("{0:d}", article.StartDate);
                            txtEndDate.Text = String.Format("{0:d}", article.EndDate);
                            hidIsPublished.Value = article.IsPublished.ToString();
                            hidPublishedDate.Value = article.PublishedDate.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/Admin/Articles.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Admin/Articles.aspx");
                    }
                }
            }
        }
    }
    protected void btnSaveArticle_Click(object sender, EventArgs e)
    {

        Article newArticle = new Article();
        string articleImageFileName;
        string thumbnailImageFileName;
        int userID;
        int articleID;
        bool ispublished;
        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        DateTime publishedDate = new DateTime();

        if (int.TryParse(Request.QueryString["ArticleID"], out articleID))
        {
            newArticle.ArticleID = articleID;
        }
        newArticle.Title = txtTitle.Text.ToString();
        newArticle.Summary = txtSummary.Text.ToString();
        newArticle.Copy = txtCopy.Text.ToString();

        if (bool.TryParse(hidIsPublished.Value.ToString(),out ispublished))
        {
        newArticle.IsPublished = ispublished;
        }
        
        if (DateTime.TryParse(hidPublishedDate.Value.ToString(), out publishedDate))
        {
            newArticle.PublishedDate = publishedDate;
        }
        if (int.TryParse(Master.userID, out userID))
        {
            newArticle.UserID = userID;
        }

        if (DateTime.TryParse(txtStartDate.Text.ToString(), out startDate))
        {
            newArticle.StartDate = startDate;
        }

        if (DateTime.TryParse(txtEndDate.Text.ToString(), out endDate))
        {
            newArticle.EndDate = endDate;
        }

        if (uploadThumbnailImage.HasFile)
        {
            thumbnailImageFileName = new ThumbnailImageProcessor().ProcessImage(uploadThumbnailImage.FileName, uploadThumbnailImage.PostedFile.InputStream);

            if (thumbnailImageFileName == null)
            {
                ThumbnailImageFile.IsValid = false;
            }
            else
            {
                if (!Page.IsValid)
                {
                    File.Delete(Server.MapPath(thumbnailImageFileName));
                    ThumbnailImagePosted.IsValid = false;
                }
                else
                {
                    newArticle.ThumbnailImageURL = thumbnailImageFileName;
                }
            }
        }
        else
        {
            if (imgThumbnailImage.ImageUrl == null)
            {
                ThumbnailRequired.IsValid = false;
            }
            else
            {
                newArticle.ThumbnailImageURL = imgThumbnailImage.ImageUrl;
            }

        }
        if (uploadArticleImage.HasFile)
        {
            articleImageFileName = new ArticleImageProcessor().ProcessImage(uploadArticleImage.FileName, uploadArticleImage.PostedFile.InputStream);
            if (articleImageFileName == null)
            {
                ArticleImageFile.IsValid = false;
            }
            else
            {
                if (!Page.IsValid)
                {
                    File.Delete(Server.MapPath(articleImageFileName));
                    ArticleImagePosted.IsValid = false;
                }
                else
                {
                    newArticle.ArticleImageURL = articleImageFileName;
                }
            }
        }
        else
        {
            if (imgArticleImage.ImageUrl == null)
            {
                ArticleImageRequired.IsValid = false;
            }
            else
            {
                newArticle.ArticleImageURL = imgArticleImage.ImageUrl;
            }

        }
        if (Page.IsValid)
        {
            if (new ArticleDbAccess().Save(newArticle))
            {
                Response.Redirect("~/Admin/Articles.aspx");
            }
        }

    }

}

