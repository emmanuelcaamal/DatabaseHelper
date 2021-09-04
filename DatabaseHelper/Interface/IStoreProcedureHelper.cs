using System.Collections.Generic;

namespace DatabaseHelper.Interface
{
    public interface IStoreProcedureHelper
    {
        string ConnectionString { get; set; }
        IList<T> ExecuteRead<T>(string procName,object parameters = null);

        int ExecuteCommand(string procName, object parameters = null);

        object ExecuteScalar(string procName, object parameters = null);
    }
}
