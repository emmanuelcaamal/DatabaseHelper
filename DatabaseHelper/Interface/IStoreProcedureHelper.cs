using System.Collections.Generic;

namespace DatabaseHelper.Interface
{
    public interface IStoreProcedureHelper
    {
        IList<T> ExecReadProc<T>(string procName,object parameters = null);

        int ExecCommandProc(string procName, object parameters = null);

        object ExecScalarProc(string procName, object parameters = null);
    }
}
