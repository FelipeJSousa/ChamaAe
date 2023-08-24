using System.Data;

namespace ChamaAe.Servico.Infra.Data.Extensions;

public static class DbConnectionExtensions
{
    public static object ReturnSequenceNextVal(this IDbConnection dbConnection, string sequence)
    {
        if (dbConnection.State == ConnectionState.Closed)
        {
            dbConnection.Open();
        }

        var sql = $"SELECT {sequence}.NEXTVAL FROM DUAL";

        var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteScalar();
    }

    public static object ReturnMaxVal(this IDbConnection dbConnection, string tableName, string campoName = "HANDLE")
    {
        if (dbConnection.State == ConnectionState.Closed)
        {
            dbConnection.Open();
        }

        var sql = $"SELECT MAX({campoName}) FROM {tableName}";

        var cmd = dbConnection.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;

        return cmd.ExecuteScalar();
    }

    private static void AddParameter(this IDbCommand command, string name, object value, ParameterDirection direction = ParameterDirection.Input)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        parameter.Direction = direction;
        command.Parameters.Add(parameter);
    }
}