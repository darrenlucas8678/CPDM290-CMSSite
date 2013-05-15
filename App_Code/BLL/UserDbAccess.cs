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
    /// Summary description for UserDbAccess
    /// </summary>
    public class UserDbAccess
    {
        private DataAccessLayer userDb;

        public UserDbAccess()
            : this(ConfigurationManager.ConnectionStrings[1].Name)
        {
        }

        public UserDbAccess(string connectionString)
        {
            userDb = new DataAccessLayer(connectionString);
        }

        public int? Authenticate(string username, string password)
        {
            int userid;
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@username",username),
				new SqlParameter("@password",password)
			};

            try
            {
                if (!int.TryParse(userDb.ExecuteScalarStoredProcedure("User_Authenticate", parameters).ToString(), out userid))
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            return userid;
        }

        public User GetByID(int userID)
        {
            User user = null;
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@userid",userID)
			};

            using (DataTable table = userDb.ExecuteStoredProcedure("GetUsers", parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    user = new User();
                    user = ParseUser(user, row);
                }
            }

            return user;
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(string filter)
        {
            throw new NotImplementedException();
        }

        //public void Save(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(int userID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Role GetAllRoles()
        //{
        //    throw new NotImplementedException();
        //}

        //public SecurityQuestion GetAllSecurityQuestions()
        //{
        //    throw new NotImplementedException();
        //}

        private User ParseUser(User user, DataRow row)
        {
            user.UserID = (int)row["userid"];
            user.Username = row["username"].ToString();
            user.Password = row["password"].ToString();
            user.RoleID = (int?)row["userid"];
            user.FirstName = row["firstname"].ToString();
            user.LastName = row["lastname"].ToString();
            user.Email = row["email"].ToString();
            user.Role = row["role"].ToString();
            return user;
        }
        //private Profile ParseProfile(Profile profile, DataRow row)
        //{
        //    DateTime datevalue;
        //    profile.ProfileID = (int)row["profileid"];
        //    profile.EmailAddress = row["email"].ToString();
        //    profile.FirstName = row["firstname"].ToString();
        //    profile.LastName = row["lastname"].ToString();
        //    profile.Address = row["address"].ToString();
        //    profile.City = row["city"].ToString();
        //    profile.State.StateID = row["stateid"] as int?;
        //    profile.Zip = row["zip"].ToString();
        //    profile.BirthDate = DateTime.TryParse(row["birthdate"].ToString(), out datevalue) ? datevalue as DateTime? : null;
        //    profile.Age = row["age"] as int?;
        //    profile.Gender.GenderID = row["genderid"] as int?;
        //    return profile;
        //}
        //private SecurityQuestion ParseQuestion(SecurityQuestion question, DataRow row)
        //{
        //    question.SecurityQuestionID = (int)row["securityquestionid"];
        //    question.SecurityQuestionText = row["securityquestion"].ToString();
        //    return question;
        //}
        //private State ParseState(State state, DataRow row)
        //{
        //    state.StateID = row["stateid"] as int?;
        //    state.StateCode = row["statecode"].ToString();
        //    state.StateName = row["statename"].ToString();
        //    return state;
        //}
        //private Gender ParseGender(Gender gender, DataRow row)
        //{
        //    gender.GenderID = row["genderid"] as int?;
        //    gender.GenderCode = row["gendercode"].ToString();
        //    gender.GenderName = row["gender"].ToString();
        //    return gender;
        //}
        //private SqlParameter[] ExtractParameters(User user)
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@username",user.UserName??(object)DBNull.Value),
        //        new SqlParameter("@password",user.Password??(object)DBNull.Value),
        //        new SqlParameter("@serurityquestionid",user.SecurityQuestion.SecurityQuestionID??(object)DBNull.Value),
        //        new SqlParameter("@securityquestionanswer",user.SecurityQuestionAnswer??(object)DBNull.Value),
        //        new SqlParameter("@profileid",user.Profile.ProfileID??(object)DBNull.Value)
        //    };

        //    if (user.UserID != null)
        //    {
        //        parameters.Add(new SqlParameter("@userid", user.UserID));
        //    }
        //    return parameters.ToArray();
        //}
        //private SqlParameter[] ExtractParameters(Profile profile)
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@profileid", profile.ProfileID),
        //        new SqlParameter("@email", profile.EmailAddress??(object)DBNull.Value),
        //        new SqlParameter("@firstname",profile.FirstName??(object)DBNull.Value),
        //        new SqlParameter("@lastname", profile.LastName??(object)DBNull.Value),
        //        new SqlParameter("@address",profile.Address??(object)DBNull.Value),
        //        new SqlParameter("@city",profile.City??(object)DBNull.Value),
        //        new SqlParameter("@stateid",profile.State.StateID??(object)DBNull.Value),
        //        new SqlParameter("@zip",profile.Zip??(object)DBNull.Value),
        //        new SqlParameter("@genderid",profile.Gender.GenderID??(object)DBNull.Value),
        //        new SqlParameter("@birthdate",profile.BirthDate??(object)DBNull.Value)
        //    };
        //    return parameters.ToArray();
        //}

    }
}