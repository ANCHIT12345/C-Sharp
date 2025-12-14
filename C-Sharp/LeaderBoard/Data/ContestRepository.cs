using Leaderboard.Models;
using LeaderBoard.Data;
using System;
using System.Collections.Generic;

namespace Leaderboard.Data
{
    public class ContestRepository
    {
        private readonly DatabaseHelper _db;
        public ContestRepository() {
            _db = new DatabaseHelper();
        }

        public int Insert(Contest contest)
        {
            const string sql = @"
                INSERT INTO Contest (CtID, Winner, MVP_OF_Contest, Runner_UP, TotalNumberOfMatches, Best_Time, LtID, ContestStartDate, ContestEndDate)
                VALUES (@CtID, @Winner, @MVP, @RunnerUp, @TotalMatches, @BestTime, @LtID, @StartDate, @EndDate);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
            return _db.ExecuteScalar<int>(sql, new
            {
                CtID = contest.CtID,
                Winner = contest.Winner <= 0 ? (int?)null : contest.Winner,
                MVP = contest.MVP_OF_Contest <= 0 ? (int?)null : contest.MVP_OF_Contest,
                RunnerUp = contest.Runner_UP <= 0 ? (int?)null : contest.Runner_UP,
                TotalMatches = contest.TotalNumberOfMatches,
                BestTime = contest.Best_Time,
                LtID = contest.LtID,
                StartDate = contest.ContestStartDate,
                EndDate = contest.ContestEndDate
            });
        }
        public bool Update(Contest contest)
        {
            const string sql = @"
                UPDATE Contest SET CtID=@CtID, Winner=@Winner, MVP_OF_Contest=@MVP, Runner_UP=@RunnerUp,
                  TotalNumberOfMatches=@TotalMatches, Best_Time=@BestTime, LtID=@LtID, ContestStartDate=@StartDate, ContestEndDate=@EndDate
                WHERE Contest_ID = @ContestId;
            ";
            return _db.ExecuteNonQuery(sql, new
            {
                CtID = contest.CtID,
                Winner = contest.Winner <= 0 ? (int?)null : contest.Winner,
                MVP = contest.MVP_OF_Contest <= 0 ? (int?)null : contest.MVP_OF_Contest,
                RunnerUp = contest.Runner_UP <= 0 ? (int?)null : contest.Runner_UP,
                TotalMatches = contest.TotalNumberOfMatches,
                BestTime = contest.Best_Time,
                LtID = contest.LtID,
                StartDate = contest.ContestStartDate,
                EndDate = contest.ContestEndDate,
                ContestId = contest.ContestId
            }) > 0;
        }
        public bool Delete(int contestId)
        {
            const string sql = "DELETE FROM Contest WHERE Contest_ID = @ContestId;";
            return _db.ExecuteNonQuery(sql, new { ContestId = contestId }) > 0;
        }

        public Contest GetById(int id)
        {
            const string sql = @"
                SELECT Contest_ID, CtID, Winner, MVP_OF_Contest, Runner_UP, TotalNumberOfMatches, Best_Time, LtID, ContestStartDate, ContestEndDate
                FROM Contest WHERE Contest_ID = @ContestId;
            ";
            using var rdr = _db.ExecuteReader(sql, new { ContestId = id });
            if (rdr.Read())
            {
                return new Contest
                {
                    ContestId = rdr.GetInt32(0),
                    CtID = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                    Winner = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                    MVP_OF_Contest = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3),
                    Runner_UP = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                    TotalNumberOfMatches = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                    Best_Time = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                    LtID = rdr.IsDBNull(7) ? 0 : rdr.GetInt32(7),
                    ContestStartDate = rdr.IsDBNull(8) ? (DateTime?)null : rdr.GetDateTime(8),
                    ContestEndDate = rdr.IsDBNull(9) ? (DateTime?)null : rdr.GetDateTime(9)
                };
            }
            return null;
        }

        public List<Contest> GetAll()
        {
            const string sql = @"
                SELECT Contest_ID, CtID, Winner, MVP_OF_Contest, Runner_UP, TotalNumberOfMatches, Best_Time, LtID, ContestStartDate, ContestEndDate
                FROM Contest ORDER BY ContestStartDate DESC;
            ";
            var list = new List<Contest>();
            using var rdr = _db.ExecuteReader(sql);
            while (rdr.Read())
            {
                list.Add(new Contest
                {
                    ContestId = rdr.GetInt32(0),
                    CtID = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                    Winner = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                    MVP_OF_Contest = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3),
                    Runner_UP = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                    TotalNumberOfMatches = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                    Best_Time = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                    LtID = rdr.IsDBNull(7) ? 0 : rdr.GetInt32(7),
                    ContestStartDate = rdr.IsDBNull(8) ? (DateTime?)null : rdr.GetDateTime(8),
                    ContestEndDate = rdr.IsDBNull(9) ? (DateTime?)null : rdr.GetDateTime(9)
                });
            }
            return list;
        }

        public List<Contest> GetActiveContests(DateTime now)
        {
            const string sql = @"
                SELECT Contest_ID, CtID, Winner, MVP_OF_Contest, Runner_UP, TotalNumberOfMatches, Best_Time, LtID, ContestStartDate, ContestEndDate
                FROM Contest
                WHERE ContestStartDate <= @Now AND ContestEndDate >= @Now;
            ";
            var list = new List<Contest>();
            using var rdr = _db.ExecuteReader(sql, new { Now = now.Date }); // adjust if time-of-day needed
            {
                while (rdr.Read())
                {
                    list.Add(new Contest
                    {
                        ContestId = rdr.GetInt32(0),
                        CtID = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                        Winner = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                        MVP_OF_Contest = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3),
                        Runner_UP = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                        TotalNumberOfMatches = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
                        Best_Time = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                        LtID = rdr.IsDBNull(7) ? 0 : rdr.GetInt32(7),
                        ContestStartDate = rdr.IsDBNull(8) ? (DateTime?)null : rdr.GetDateTime(8),
                        ContestEndDate = rdr.IsDBNull(9) ? (DateTime?)null : rdr.GetDateTime(9)
                    });
                }
            }
            return list;
        }
    }
}