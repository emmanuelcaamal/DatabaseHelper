using Dapper;
using DatabaseHelper.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DatabaseHelper
{
	public class CommandTransactionHelper : ICommandTransactionHelper
	{
		private IDbConnection Connection { get; set; }
		private IDbTransaction Transaction = null;

		public CommandTransactionHelper()
		{
		}

		public CommandTransactionHelper(IDbConnection connection)
		{
			Connection = connection;
		}

		public void CreateConnection(string connectionString)
		{
			Connection = new SqlConnection(connectionString);
		}

		public void CreateConnection(SqlConnection connection)
		{
			Connection = connection;
		}

		public void OpenTransaction(IsolationLevel isolation = IsolationLevel.Unspecified)
		{
			if (Connection == null)
				throw new Exception("There is no connection to the database");

			Connection.Open();
			if (isolation == IsolationLevel.Unspecified)
				Transaction = Connection.BeginTransaction();
			else
				Transaction = Connection.BeginTransaction(isolation);
		}

		public int ExecuteCommand(string sqlCommand, object parameters = null)
		{
			try
			{
				if (Connection == null)
					throw new Exception("There is no connection to the database");

				if(Connection.State != ConnectionState.Open)
					Connection.Open();
				
				int rows = Connection.Execute(sqlCommand, parameters, Transaction, commandType: CommandType.Text);
				return rows;
			}
			catch (Exception ex)
			{
				RollbackTransaction();
				throw ex;
			}
		}

		public object ExecuteScalar(string sqlCommand, object parameters = null)
		{
			try
			{
				if (Connection == null)
					throw new Exception("There is no connection to the database");

				if (Connection.State != ConnectionState.Open)
					Connection.Open();

				var scalarValue = Connection.Query<object>(sqlCommand, parameters, Transaction, commandType: CommandType.Text);
				return scalarValue;
			}
			catch (Exception ex)
			{
				RollbackTransaction();
				throw ex;
			}
		}

		public IList<T> ExecuteRead<T>(string sqlCommand, object parameters = null)
		{
			try
			{
				if (Connection == null)
					throw new Exception("There is no connection to the database");
				
				var data = Connection.Query<T>(sqlCommand, parameters, Transaction, commandType: CommandType.Text);
				return data.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void CommitTransaction()
		{
			if(Transaction != null)
			{
				Transaction.Commit();
				Transaction = null;
			}	
		}

		private void RollbackTransaction()
		{
			if (Transaction != null)
			{
				Transaction.Rollback();
				Connection.Close();

				Transaction = null;
			}
		}
	}
}
