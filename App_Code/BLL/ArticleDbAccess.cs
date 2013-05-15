using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDM.LucasD;
using CPDM.LucasD.Midterm.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CPDM.LucasD.Midterm.BLL
{
    /// <summary>
    /// Summary description for ArticleDbAccess
    /// </summary>
    public class ArticleDbAccess
    {
        private DataAccessLayer articleDb;

        public ArticleDbAccess()
            : this(ConfigurationManager.ConnectionStrings[1].Name)
        {
        }

        public ArticleDbAccess(string connectionString)
        {
            articleDb = new DataAccessLayer(connectionString);
        }

        public Article GetByID(int articleID)
        {
            Article article = null;
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@articleid",articleID)
			};

            using (DataTable table = articleDb.ExecuteStoredProcedure("GetArticles", parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    article = new Article();
                    article = ParseArticle(article, row);
                }
            }

            return article;
        }
        public Article GetByID(int articleID, string filter)
        {
            List<Article> articles = new List<Article>(){GetByID(articleID)};
            return ArticleFilter(articles, filter).FirstOrDefault();
        }

        public List<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            using (DataTable table = articleDb.ExecuteStoredProcedure("GetArticles"))
            {
                if (table.Rows.Count > 0)
                {                    
                    foreach (DataRow row in table.Rows)
                    {
                        Article article = new Article();
                        article = ParseArticle(article, row);
                        articles.Add(article);
                    }
                }
            }
            return articles;
        }

        public List<Article> GetAll(string filter)
        {
            return ArticleFilter(GetAll(),filter).ToList();
        }

        public List<Article> GetAll(string filter, string userrole)
        {
            return ArticleFilter(GetAll(),filter, userrole).ToList();
        }

        public bool Save(Article article)
        {
            return articleDb.ExecuteNonQueryStoredProcedure("SaveArticle", ExtractParameters(article));
        }

        public void Publish(int articleID)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@articleid",articleID)
			};
            articleDb.ExecuteStoredProcedure("PublishArticle", parameters);
        }
        public void Unpublish(int articleID)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@articleid",articleID)
			};
            articleDb.ExecuteStoredProcedure("UnpublishArticle", parameters);
        }
        public void Delete(int articleid)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@articleid",articleid)
			};
            articleDb.ExecuteStoredProcedure("DeleteArticle", parameters);
        }
        //public Status GetAllStatuses()
        //{
        //    throw new NotImplementedException();
        //}

        private SqlParameter[] ExtractParameters(Article article)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@title",article.Title??(object)DBNull.Value),
				new SqlParameter("@summary",article.Summary??(object)DBNull.Value),
				new SqlParameter("@copy",article.Copy??(object)DBNull.Value),
				new SqlParameter("@thumbnailURL",article.ThumbnailImageURL??(object)DBNull.Value),
				new SqlParameter("@imageURL",article.ArticleImageURL??(object)DBNull.Value),
				new SqlParameter("@startdate",article.StartDate??(object)DBNull.Value),
				new SqlParameter("@enddate",article.EndDate??(object)DBNull.Value),
				new SqlParameter("@published",article.IsPublished),
				new SqlParameter("@publisheddate",article.PublishedDate??(object)DBNull.Value),
				new SqlParameter("@userid",article.UserID??(object)DBNull.Value)
			};

            if (article.ArticleID != null)
            {
                parameters.Add(new SqlParameter("@articleid", article.ArticleID));
            }
            return parameters.ToArray();
        }

        private Article ParseArticle(Article article, DataRow row)
        {
            DateTime startdate;
            DateTime enddate;
            DateTime publisheddate;
            article.ArticleID = (int)row["articleid"];
            article.ArticleImageURL = row["imageURL"].ToString();
            article.Copy = row["copy"].ToString();
            article.EndDate = DateTime.TryParse(row["enddate"].ToString(), out enddate) ? enddate as DateTime? : null;
            article.IsPublished = (bool)row["published"];
            article.PublishedDate = DateTime.TryParse(row["publisheddate"].ToString(), out publisheddate) ? publisheddate as DateTime? : null;
            article.StartDate = DateTime.TryParse(row["startdate"].ToString(), out startdate) ? startdate as DateTime? : null;
            article.StatusID = (int)row["statusid"];
            article.Status = row["status"].ToString();
            article.Summary = row["summary"].ToString();
            article.ThumbnailImageURL = row["thumbnailURL"].ToString();
            article.Title = row["title"].ToString();
            article.UserID = (int)row["userid"];
            return article;
        }

        private IQueryable<Article> ArticleFilter(List<Article> articles, string filter)
        {
            var query = from a in articles select a;

            switch (filter)
            {
                case "Expired":
                    query = query.Where(a => a.Status.Equals("Expired"));
                    break;
                case "Archived":
                    query = query.OrderByDescending(a => a.PublishedDate).Where(a => a.IsPublished.Equals(true) && a.Status.Equals("Expired"));
                    break;
                case "Active":
                    query = query.Where(a => a.Status.Equals("Active"));
                    break;
                case "Top":
                    query = query.OrderByDescending(a => a.PublishedDate).Where(a => a.IsPublished.Equals(true) && a.Status.Equals("Active")).Take(4);
                    break;
                case "NotPublished":
                    query = query.Where(a => a.IsPublished.Equals(false));
                    break;
                case "Published":
                    query = query.Where(a => a.IsPublished.Equals(true));
                    break;
                case "SortedPublished":
                    query = query.OrderByDescending(a => a.PublishedDate).Where(a => a.IsPublished.Equals(true) && a.Status.Equals("Active"));
                    break;
                default:
                    break;
            }
            return query.AsQueryable();
        }

        private IQueryable<Article> ArticleFilter(List<Article> articles, string filter, string userrole)
        {
            var query = ArticleFilter(articles,filter);
            switch (userrole)
            {
                case "Administrator":
                    break;
                case "Publisher":
                    //query = query.Where(a => a.Status.Equals("Active"));
                    break;
                case "Author":
                    query = query.Where(a => a.UserID.Equals(int.Parse(HttpContext.Current.User.Identity.Name.ToString())));
                    break;
                default:
                    break;
            }
            return query;
        }
    }
}