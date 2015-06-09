using System;
using System.Configuration;
using TestWCFProxy;
using System.Data;
using System.Data.SqlClient;

namespace TestMessageConsumer
{
    public interface IDataSink
    {
        void Save(ClientData message);
    }

    public class SqlDataSink : IDataSink
    {
        private readonly ConnectionStringSettings _connectionString;

        public SqlDataSink(ConnectionStringSettings connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(ClientData message)
        {
            using (var connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                try
                {


                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText =
                            "insert into inputData(value1, value2, value3, value4, value5)values(@value1, @value2, @value3, @value4, @value5)";
                        command.Parameters.AddWithValue("@value1", message.Value1);
                        command.Parameters.AddWithValue("@value2", message.Value2);
                        command.Parameters.AddWithValue("@value3", message.Value3);
                        command.Parameters.AddWithValue("@value4", message.Value4);
                        command.Parameters.AddWithValue("@value5", message.Value5);

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void AddParameter(SqlCommand command, string parameterName, string parameterValue)
        {
            var sqlParameter = new SqlParameter(parameterName, SqlDbType.NVarChar) { Value = parameterValue };
            command.Parameters.Add(sqlParameter);
        }
    }
}

