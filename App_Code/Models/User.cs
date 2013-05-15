using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPDM.LucasD;

namespace CPDM.LucasD.Midterm.Models
{
    /// <summary>
    /// Summary description for Users
    /// </summary>
    public class User
    {
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? SecurityQuestionID { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public int? RoleID;
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class Role
    {
        public int? RoleID { get; set; }
        public string UserRole { get; set; }
    }

    public class SecurityQuestion
    {
        public int? SecurityQuestionID { get; set; }
        public string SecurityQuestionText { get; set; }
    }
}