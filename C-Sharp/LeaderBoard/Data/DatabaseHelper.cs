using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LeaderBoard.Data
{
    public class DatabaseHelper : IRepository
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                return _connection;
            }
        }

        IDbConnection IRepository.Connection => throw new NotImplementedException();

        public DatabaseHelper()
        {
            _connectionString = "Data Source=LAPTOP-TH0TP9P1\\SQLEXPRESS;Initial Catalog=LeaderBoardSystem;Trusted_Connection=True;";
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                try { _connection.Close(); } catch { }
                _connection.Dispose();
                _connection = null;
            }
        }
        public bool ExecuteInsert(string sql, object parameters = null)
        {
            return ExecuteNonQuery(sql, parameters) > 0;
        }
        public int ExecuteNonQuery(string sql, object parameters = null)
        {
            using (var cmd = CreateCommand(sql, parameters))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            using (var cmd = CreateCommand(sql, parameters))
            {
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value) return default;
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }
        public IDataReader ExecuteReader(string sql, object parameters = null, Func<object, Models.User> value = null)
        {
            var cmd = CreateCommand(sql, parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private SqlCommand CreateCommand(string sql, object parameters)
        {
            var cmd = (SqlCommand)Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 60;
            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    var name = "@" + prop.Name;
                    var value = prop.GetValue(parameters) ?? DBNull.Value;
                    cmd.Parameters.AddWithValue(name, value);
                }
            }
            return cmd;
        }
        public IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }
        public void CommitTransaction(IDbTransaction tx)
        {
            tx?.Commit();
            tx?.Dispose();
        }
        public void RollbackTransaction(IDbTransaction tx)
        {
            try { tx?.Rollback(); } catch { }
            tx?.Dispose();
        }

        bool IRepository.ExecuteInsert(string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        int IRepository.ExecuteNonQuery(string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        T IRepository.ExecuteScalar<T>(string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        IDataReader IRepository.ExecuteReader(string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        IDbTransaction IRepository.BeginTransaction()
        {
            throw new NotImplementedException();
        }

        void IRepository.CommitTransaction(IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        void IRepository.RollbackTransaction(IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
