using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDM.LucasD;

namespace CPDM.LucasD.Midterm.Models
{
    /// <summary>
    /// Summary description for Articles
    /// </summary>
    public class Article
    {
        public int? ArticleID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ThumbnailImageURL { get; set; }
        public string ArticleImageURL { get; set; }
        public string Copy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? UserID { get; set; }
    }

    public class Status
    {
        public int? StatusID { get; set; }
        public string ArticleStatus { get; set; }
    }
}