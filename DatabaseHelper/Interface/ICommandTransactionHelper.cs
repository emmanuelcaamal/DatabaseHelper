using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseHelper.Interface
{
	public interface ICommandTransactionHelper
	{
		void CommitTransaction();
		void SetConnection(string connectionString);
		void SetConnection(SqlConnection connection);
		object ExecuteScalar(string sqlCommand, object parameters = null);
        T ExecuteScalar<T>(string sqlCommand, object parameters = null);
        int ExecuteCommand(string sqlCommand, object parameters = null);
		void OpenTransaction(IsolationLevel isolation = IsolationLevel.Unspecified);
		IList<T> ExecuteRead<T>(string sqlCommand, object parameters = null);
		void RollbackTransaction();
	}
}
