using Dapper;
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
        private readonly string _connectionString;

        public StoreProcedureHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ExecCommandProc(string procName, object parameters = null)
        {
            try
            {
                using (IDbConnection cnn = new SqlConnection(_connectionString))
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

        public IList<T> ExecReadProc<T>(string procName, object parameters = null)
        {
            try
            {
                using (IDbConnection cnn = new SqlConnection(_connectionString))
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

        public object ExecScalarProc(string procName, object parameters = null)
        {
            try
            {
                using (IDbConnection cnn = new SqlConnection(_connectionString))
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
