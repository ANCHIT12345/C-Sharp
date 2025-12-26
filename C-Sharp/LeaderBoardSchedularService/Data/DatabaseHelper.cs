using System;
using System.Data;
using System.Data.SqlClient;

namespace Leaderboard.Data
{
    public class DatabaseHelper : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DatabaseHelper()
        {
            _connectionString =
                "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=LeaderBoardSystem;Trusted_Connection=True;";
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;
            }
        }

        public int ExecuteNonQuery(string sql, object parameters = null)
        {
            using var cmd = CreateCommand(sql, parameters);
            return cmd.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            using var cmd = CreateCommand(sql, parameters);
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public IDataReader ExecuteReader(string sql, object parameters = null)
        {
            var cmd = CreateCommand(sql, parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        private SqlCommand CreateCommand(string sql, object parameters)
        {
            var cmd = (SqlCommand)Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    cmd.Parameters.AddWithValue(
                        "@" + prop.Name,
                        prop.GetValue(parameters) ?? DBNull.Value
                    );
                }
            }

            return cmd;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
