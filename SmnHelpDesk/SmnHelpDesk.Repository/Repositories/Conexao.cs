using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SmnHelpDesk.Repository.Repositories
{
    public class Conexao
    {
        public SqlConnection SqlConnection { get; }
        public SqlTransaction SqlTransaction { get; set; }
        public SqlCommand SqlCommand { get; set; }

        public Conexao()
        {
            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionSql"].ToString());
        }

        public void OpenTransaction()
        {
            if (SqlConnection.State == ConnectionState.Broken)
            {
                SqlConnection.Close();
                SqlConnection.Open();
            }

            if (SqlConnection.State == ConnectionState.Closed)
                SqlConnection.Open();

            SqlTransaction = SqlConnection.BeginTransaction();
        }

        public void Commit()
        {
            SqlTransaction.Commit();
            SqlTransaction.Dispose();
        }

        public void Rollback()
        {
            SqlTransaction.Rollback();
            SqlTransaction.Dispose();
        }

        public void Dispose()
        {
            SqlConnection.Close();
        }

        public void OpenConnection()
        {
            try
            {
                if (SqlCommand.Connection.State == ConnectionState.Broken)
                {
                    SqlCommand.Connection.Close();
                    SqlCommand.Connection.Open();
                }

                if (SqlCommand.Connection.State == ConnectionState.Closed)
                    SqlCommand.Connection.Open();
            }
            catch (SqlException ex)
            {
                throw ex.Number == 53 ? new Exception("Falha ao efetuar conexão com o Banco de Dados") : ex;
            }
        }

        public void CloseConnection() => SqlConnection.Close();

        public void ExecuteProcedure(object procedureName)
        {
            SqlCommand = new SqlCommand(procedureName.ToString(), SqlConnection, SqlTransaction)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 99999,
            };
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            SqlCommand.Parameters.AddWithValue(parameterName, parameterValue);
        }

        public void AddParameterOutput(string parameterName, object parameterValue, DbType parameterType)
        {
            SqlCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.Output,
                Value = parameterValue,
                DbType = parameterType
            });
        }

        public void AddParameterReturn(string parameterName = "@RETURN_VALUE", DbType parameterType = DbType.Int16)
        {
            SqlCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.ReturnValue,
                DbType = parameterType
            });
        }

        public string GetParameterOutput(string parameter) => SqlCommand.Parameters[parameter].Value.ToString();

        public int ExecuteNonQuery()
        {
            OpenConnection();
            return SqlCommand.ExecuteNonQuery();
        }

        public int ExecuteNonQueryWithReturn()
        {
            AddParameterReturn();
            OpenConnection();
            SqlCommand.ExecuteNonQuery();
            return int.Parse(SqlCommand.Parameters["@RETURN_VALUE"].Value.ToString());
        }

        public T ExecuteNonQueryWithReturn<T>()
        {
            AddParameterReturn();
            OpenConnection();
            SqlCommand.ExecuteNonQuery();
            var value = SqlCommand.Parameters["@RETURN_VALUE"].Value;
            if (value == DBNull.Value)
                return default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public IDataReader ExecuteReader()
        {
            OpenConnection();
            return SqlCommand.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior cb)
        {
            return SqlCommand.ExecuteReader(cb);
        }
    }
}
