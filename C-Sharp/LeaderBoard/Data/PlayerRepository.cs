using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderBoard.Models;

namespace LeaderBoard.Data
{
    public class PlayerRepository
    {
        private readonly DatabaseHelper _db;
        public PlayerRepository(DatabaseHelper db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public int Insert(User user)
        {
            const string sql = @"
                INSERT INTO Players (UserName, Email, PhoneNo, UtID)
                VALUES (@UserName, @Email, @PhoneNo, @UtID);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
            return _db.ExecuteScalar<int>(sql, new
            {
                user.UserName,
                user.Email,
                user.PhoneNo,
                user.UtID
            });
        }
        public User GetById(int userId)
        {
            const string sql = @"
                SELECT UserID, UserName, Email, PhoneNo, UtID
                FROM Players
                WHERE UserID = @UserID;
            ";
            using var rdr = _db.ExecuteReader(sql, new { UserID = userId });
            if (rdr.Read())
            {
                return new User
                {
                    UserID = rdr.GetInt32(0),
                    UserName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                    Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    PhoneNo = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                    UtID = rdr.IsDBNull(4) ? null : rdr.GetString(4)
                };
            }
            return null;
        }
        public List<User> GetAll()
        {
            const string sql = @"
                SELECT UserID, UserName, Email, PhoneNo, UtID
                FROM Players
                ORDER BY UserName;
            ";
            var list = new List<User>();
            using var rdr = _db.ExecuteReader(sql);
            while (rdr.Read())
            {
                list.Add(new User
                {
                    UserID = rdr.GetInt32(0),
                    UserName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                    Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    PhoneNo = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                    UtID = rdr.IsDBNull(4) ? null : rdr.GetString(4)
                });
            }
            return list;
        }
        public bool Update(User user)
        {
            const string sql = @"
                UPDATE Players
                SET UserName = @UserName,
                    Email = @Email,
                    PhoneNo = @PhoneNo,
                    UtID = @UtID
                WHERE UserID = @UserID;
            ";
            return _db.ExecuteNonQuery(sql, new
            {
                user.UserName,
                user.Email,
                user.PhoneNo,
                user.UtID,
                user.UserID
            }) > 0;
        }
        public bool Delete(int userId)
        {
            const string sql = @"
                DELETE FROM Players
                WHERE UserID = @UserID;
            ";
            return _db.ExecuteNonQuery(sql, new { UserID = userId }) > 0;
        }
        public bool EmailExists(string email, int? excludeId = null)
        {
            string sql;
            object param;
            if (excludeId.HasValue)
            {
                sql = @"
                    SELECT COUNT(1)
                    FROM Players
                    WHERE Email = @Email AND UserID <> @ExcludeID;
                ";
                param = new { Email = email, ExcludeID = excludeId.Value };
            }
            else
            {
                sql = @"
                    SELECT COUNT(1)
                    FROM Players
                    WHERE Email = @Email;
                ";
                param = new { Email = email };
            }
            return _db.ExecuteScalar<int>(sql, param) > 0;
        }
        public bool UpdateRating(int userId, decimal newRating)
        {
            const string sql = "UPDATE [User] SET Rating = @Rating WHERE UserID = @UserId;";
            return _db.ExecuteNonQuery(sql, new { Rating = newRating, UserId = userId }) > 0;
        }

    }
}
