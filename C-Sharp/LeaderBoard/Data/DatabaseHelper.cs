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
        private IDbTransaction _currentTransaction;


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

        //IDbConnection IRepository.Connection => throw new NotImplementedException();

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
        public IDataReader ExecuteReader(string sql, object parameters = null)
        {
            var cmd = CreateCommand(sql, parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private SqlCommand CreateCommand(string sql, object parameters)
        {
            var cmd = (SqlCommand)Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (_currentTransaction != null)
                cmd.Transaction = (SqlTransaction)_currentTransaction;

            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    cmd.Parameters.AddWithValue("@" + prop.Name,
                        prop.GetValue(parameters) ?? DBNull.Value);
                }
            }

            return cmd;
        }


        public IDbTransaction BeginTransaction()
        {
            if (_currentTransaction != null)
                return _currentTransaction;

            _currentTransaction = Connection.BeginTransaction();
            return _currentTransaction;
        }


        public void CommitTransaction(IDbTransaction tx)
        {
            if (tx == null) return;

            if (tx.Connection != null)
            {
                tx.Commit();
            }

            _currentTransaction = null;
        }

        public void RollbackTransaction(IDbTransaction tx)
        {
            try
            {
                if (tx?.Connection != null)
                    tx.Rollback();
            }
            catch
            {
            }
            finally
            {
                _currentTransaction = null;
            }
        }



        IDbConnection IRepository.Connection => this.Connection;

        bool IRepository.ExecuteInsert(string sql, object parameters)
        {
            return ExecuteInsert(sql, parameters);
        }

        int IRepository.ExecuteNonQuery(string sql, object parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        T IRepository.ExecuteScalar<T>(string sql, object parameters)
        {
            return ExecuteScalar<T>(sql, parameters);
        }

        IDataReader IRepository.ExecuteReader(string sql, object parameters)
        {
            return ExecuteReader(sql, parameters);
        }

        IDbTransaction IRepository.BeginTransaction()
        {
            return BeginTransaction();
        }

        void IRepository.CommitTransaction(IDbTransaction tx)
        {
            CommitTransaction(tx);
        }

        void IRepository.RollbackTransaction(IDbTransaction tx)
        {
            RollbackTransaction(tx);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

    }
}
