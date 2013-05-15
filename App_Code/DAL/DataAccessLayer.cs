using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CPDM.LucasD
{
    public class DataAccessLayer
    {
        private string dbConnectionString;
        private SqlConnection dbConnection;
        public DataAccessLayer(string dbConnectionName)
        {
            dbConnectionString = ConfigurationManager.ConnectionStrings[dbConnectionName].ConnectionString;
            dbConnection = new SqlConnection(dbConnectionString);
        }
        private void OpenDatabaseConnection()
        {
            switch (this.dbConnection.State)
            {
                case ConnectionState.Broken:
                    this.dbConnection.Close();
                    this.dbConnection.Open();
                    break;
                case ConnectionState.Closed:
                    this.dbConnection.Open();
                    break;
                case ConnectionState.Open:
                    this.dbConnection.Close();
                    this.dbConnection.Open();
                    break;
                default:
                    this.dbConnection.Open();
                    break;
            }
        }
        private void CloseDatabaseConnection()
        {
            this.dbConnection.Close();
        }
        public Boolean ExecuteNonQueryCommand(string commandString, CommandType commandType, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlCommand command = this.dbConnection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandString;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    this.OpenDatabaseConnection();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
            this.CloseDatabaseConnection();
            return (rowsAffected > 0);
        }
        public Boolean ExecuteNonQueryStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            return ExecuteNonQueryCommand(procedureName, CommandType.StoredProcedure, parameters);
        }
        public DataTable ExecuteCommand(string commandString, CommandType commandType, SqlParameter[] parameters = null)
        {
            DataTable dbDataTable = new DataTable();
            using (SqlCommand command = this.dbConnection.CreateCommand())
            {
                command.CommandType = commandType;
                command.CommandText = commandString;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    this.OpenDatabaseConnection();
                    using (SqlDataAdapter dbDataAdapter = new SqlDataAdapter(command))
                    {
                        dbDataAdapter.Fill(dbDataTable);
                    }
                }
                catch
                {
                    throw;
                }
            }
            this.CloseDatabaseConnection();
            return dbDataTable;
        }
        public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            return ExecuteCommand(procedureName, CommandType.StoredProcedure, parameters);
        }
        public DataTable ExecuteView(string viewName)
        {
            return ExecuteCommand(viewName, CommandType.Text);
        }
        public Object ExecuteScalar(string commandString, CommandType commandType, SqlParameter[] parameters = null)
        {
            Object dbObject = new Object();
            using (SqlCommand command = this.dbConnection.CreateCommand())
            {

                command.CommandType = commandType;
                command.CommandText = commandString;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                try
                {
                    this.OpenDatabaseConnection();
                    dbObject = command.ExecuteScalar();
                }
                catch
                {
                    throw;
                }
            }
            this.CloseDatabaseConnection();
            return dbObject;
        }
        public Object ExecuteScalarStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            return ExecuteScalar(procedureName, CommandType.StoredProcedure, parameters);
        }
    }
}