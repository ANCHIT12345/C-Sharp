using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Data
{
    public interface IRepository : IDisposable
    {
        IDbConnection Connection { get; }
        bool ExecuteInsert(string sql, object parameters = null);
        int ExecuteNonQuery(string sql, object parameters = null);
        T ExecuteScalar<T>(string sql, object parameters = null);
        IDataReader ExecuteReader(string sql, object parameters = null);

        IDbTransaction BeginTransaction();
        void CommitTransaction(IDbTransaction tx);
        void RollbackTransaction(IDbTransaction tx);
    }
}
