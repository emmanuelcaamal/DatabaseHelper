using System.Collections.Generic;

namespace DatabaseHelper.Interface
{
    public interface ICommandHelper
    {
        string ConnectionString { get; set; }
        IList<T> ExecuteRead<T>(string sqlCommand, object parameters = null);

        int ExecuteCommand(string sqlCommand, object parameters = null);

        object ExecuteScalar(string sqlCommand, object parameters = null);
    }
}
