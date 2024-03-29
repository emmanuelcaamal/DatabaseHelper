﻿using Dapper;
using DatabaseHelper.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DatabaseHelper
{
    public class StoreProcedureHelper : IStoreProcedureHelper
    {
        public string ConnectionString { get; set; }

        public StoreProcedureHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public StoreProcedureHelper()
        {
            ConnectionString = "";
        }

        public int ExecuteCommand(string procName, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    int rows = cnn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);

                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<T> ExecuteRead<T>(string procName, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    var data = cnn.Query<T>(procName, parameters, commandType: CommandType.StoredProcedure);

                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteScalar(string procName, object parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("The connection string has not been specified");

                using (IDbConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    var scalarValue = cnn.Query<object>(procName, parameters, commandType: CommandType.StoredProcedure);

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
