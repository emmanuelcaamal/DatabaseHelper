using Dapper;
using DatabaseHelper.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DatabaseHelper
{
    public class CommandHelper : ICommandHelper
    {
        public string ConnectionString { get; set; }

        public CommandHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public CommandHelper()
        {
            ConnectionString = "";
        }

        public IList<T> ExecuteRead<T>(string sqlCommand, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    var data = cnn.Query<T>(sqlCommand, parameters, commandType: CommandType.Text);

                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteCommand(string sqlCommand, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    int rows = cnn.Execute(sqlCommand, parameters, commandType: CommandType.Text);

                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteScalar(string sqlCommand, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    var scalarValue = cnn.Query<object>(sqlCommand, parameters, commandType: CommandType.Text);

                    return scalarValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
