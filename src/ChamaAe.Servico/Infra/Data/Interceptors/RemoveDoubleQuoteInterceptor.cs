using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ChamaAe.Servico.Infra.Data.Interceptors
{
    public class RemoveDoubleQuoteInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.CommandText = command.CommandText.Replace("\"", "");
            return result;
        }
    }
}