// Services/LeaderboardService.cs
using LeaderBoard.Data;
using LeaderBoard.Models;
using LeaderboardApp.Data;
using LeaderboardApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaderboardApp.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IRepository _repo;

        public LeaderboardService(IRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>
        /// Generate leaderboard by aggregating PlayerScore rows for a contest.
        /// Persist results to ContestLeaderBoard and GlobalLeaderBoard (upsert).
        /// Returns ordered list of ContestLeaderBoard rows (rank assigned).
        /// </summary>
        public List<ContestLeaderrBoard> GenerateLeaderboard(int contestId)
        {
            if (contestId <= 0) throw new ArgumentException(nameof(contestId));

            // Step 1: aggregate scores per player for this contest
            const string aggregateSql = @"
                SELECT PlayerID, SUM(Score) AS TotalPoints
                FROM PlayerScore
                WHERE GameID IS NULL -- if your schema stores contest->game differently adjust
                  AND ContestID = @ContestId
                GROUP BY PlayerID
                ORDER BY TotalPoints DESC;
            ";
            // Note: If PlayerScore table doesn't have ContestID column in your actual schema,
            // switch WHERE clause to use ContestId mapping column you use.

            var rows = new List<(int PlayerID, decimal TotalPoints)>();
            using (var rdr = _repo.ExecuteReader(aggregateSql, new { ContestId = contestId }))
            {
                while (rdr.Read())
                {
                    var pid = rdr.GetInt32(rdr.GetOrdinal("PlayerID"));
                    var tp = rdr.GetDecimal(rdr.GetOrdinal("TotalPoints"));
                    rows.Add((pid, tp));
                }
                rdr.Close();
            }

            // Step 2: assign ranks (dense rank ordering) and upsert into ContestLeaderBoard
            var leaderboardRows = new List<ContestLeaderrBoard>();

            int rank = 0;
            decimal? lastPoints = null;
            int denseRank = 0;

            using (var tx = _repo.BeginTransaction())
            {
                try
                {
                    foreach (var r in rows)
                    {
                        rank++;
                        if (lastPoints == null || r.TotalPoints != lastPoints.Value)
                        {
                            denseRank++;
                            lastPoints = r.TotalPoints;
                        }

                        var clb = new ContestLeaderrBoard
                        {
                            PlayerID = r.PlayerID,
                            ContestID = contestId,
                            TotalPoints = r.TotalPoints,
                            Rank = denseRank
                        };

                        UpsertContestLeaderBoard(clb, tx);
                        leaderboardRows.Add(clb);
                    }

                    foreach (var r in rows)
                    {
                        UpsertGlobalLeaderboardForPlayer(r.PlayerID, tx);
                    }

                    _repo.CommitTransaction(tx);
                }
                catch
                {
                    _repo.RollbackTransaction(tx);
                    throw;
                }
            }

            return leaderboardRows;
        }

        private void UpsertContestLeaderBoard(ContestLeaderrBoard clb, IDbTransaction tx)
        {
            const string checkSql = "SELECT COUNT(1) FROM ContestLeaderBoard WHERE PlayerID = @PlayerId AND ContestID = @ContestId;";
            int exists = ExecuteScalarWithTransaction<int>(checkSql, new { PlayerId = clb.PlayerID, ContestId = clb.ContestID }, tx);

            if (exists > 0)
            {
                const string upd = @"
                    UPDATE ContestLeaderBoard
                    SET TotalPoints = @TotalPoints, Rank = @Rank
                    WHERE PlayerID = @PlayerId AND ContestID = @ContestId;
                ";
                ExecuteNonQueryWithTransaction(upd, new { TotalPoints = clb.TotalPoints, Rank = clb.Rank, PlayerId = clb.PlayerID, ContestId = clb.ContestID }, tx);
            }
            else
            {
                const string ins = @"
                    INSERT INTO ContestLeaderBoard (PlayerID, ContestID, TotalPoints, Rank)
                    VALUES (@PlayerId, @ContestId, @TotalPoints, @Rank);
                ";
                ExecuteNonQueryWithTransaction(ins, new { PlayerId = clb.PlayerID, ContestId = clb.ContestID, TotalPoints = clb.TotalPoints, Rank = clb.Rank }, tx);
            }
        }

        private void UpsertGlobalLeaderboardForPlayer(int playerId, IDbTransaction tx)
        {
            const string totalSql = @"
                SELECT ISNULL(SUM(Score),0) AS TotalPoints
                FROM PlayerScore
                WHERE PlayerID = @PlayerId;
            ";
            decimal totalPoints = ExecuteScalarWithTransaction<decimal>(totalSql, new { PlayerId = playerId }, tx);

            const string chk = "SELECT COUNT(1) FROM GlobalLeaderBoard WHERE PlayerID = @PlayerId;";
            int exists = ExecuteScalarWithTransaction<int>(chk, new { PlayerId = playerId }, tx);

            if (exists > 0)
            {
                const string upd = "UPDATE GlobalLeaderBoard SET TotalPoints = @TotalPoints WHERE PlayerID = @PlayerId;";
                ExecuteNonQueryWithTransaction(upd, new { TotalPoints = totalPoints, PlayerId = playerId }, tx);
            }
            else
            {
                const string ins = "INSERT INTO GlobalLeaderBoard (PlayerID, TotalPoints, Rank) VALUES (@PlayerId, @TotalPoints, 0);";
                ExecuteNonQueryWithTransaction(ins, new { PlayerId = playerId, TotalPoints = totalPoints }, tx);
            }

        }

        public void RecalculateGlobalRanks()
        {
            const string sql = @"
                SELECT GLB_ID, PlayerID, TotalPoints
                FROM GlobalLeaderBoard
                ORDER BY TotalPoints DESC;
            ";

            var list = new List<(int GlbId, int PlayerId, decimal TotalPoints)>();
            using (var rdr = _repo.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    list.Add((rdr.GetInt32(rdr.GetOrdinal("GLB_ID")), rdr.GetInt32(rdr.GetOrdinal("PlayerID")), rdr.GetDecimal(rdr.GetOrdinal("TotalPoints"))));
                }
                rdr.Close();
            }

            int denseRank = 0;
            decimal? lastPoints = null;
            using (var tx = _repo.BeginTransaction())
            {
                try
                {
                    foreach (var row in list)
                    {
                        if (lastPoints == null || row.TotalPoints != lastPoints.Value)
                        {
                            denseRank++;
                            lastPoints = row.TotalPoints;
                        }
                        const string updateSql = "UPDATE GlobalLeaderBoard SET Rank = @Rank WHERE GLB_ID = @GlbId;";
                        ExecuteNonQueryWithTransaction(updateSql, new { Rank = denseRank, GlbId = row.GlbId }, tx);
                    }

                    _repo.CommitTransaction(tx);
                }
                catch
                {
                    _repo.RollbackTransaction(tx);
                    throw;
                }
            }
        }

        public void DisplayLeaderboard(int contestId, List<ContestLeaderrBoard> rows = null)
        {
            if (rows == null) rows = GenerateLeaderboard(contestId);

            Console.WriteLine($"Leaderboard for Contest {contestId} (Top {rows.Count}):");
            Console.WriteLine("Rank | PlayerID | TotalPoints");
            foreach (var r in rows)
            {
                Console.WriteLine($"{r.Rank,4} | {r.PlayerID,8} | {r.TotalPoints,12:N2}");
            }
        }

        public string ExportLeaderboardToCsv(int contestId, string directoryPath)
        {
            var rows = GenerateLeaderboard(contestId);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filename = $"Leaderboard_Contest_{contestId}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
            var path = Path.Combine(directoryPath, filename);

            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine("Rank,PlayerID,TotalPoints");
                foreach (var r in rows)
                {
                    sw.WriteLine($"{r.Rank},{r.PlayerID},{r.TotalPoints.ToString(CultureInfo.InvariantCulture)}");
                }
            }

            using (var tx = _repo.BeginTransaction())
            {
                try
                {
                    const string ins = @"
                        INSERT INTO LeaderboardExportHistory (GameID, ContestID, FileLocation, ExportedAt)
                        VALUES (@GameId, @ContestId, @FileLocation, @ExportedAt);
                    ";
                    ExecuteNonQueryWithTransaction(ins, new { GameId = (int?)null, ContestId = contestId, FileLocation = path, ExportedAt = DateTime.UtcNow }, tx);

                    _repo.CommitTransaction(tx);
                }
                catch
                {
                    _repo.RollbackTransaction(tx);
                    throw;
                }
            }

            return path;
        }

        public async Task PeriodicRefreshAsync(int intervalSeconds, CancellationToken cancellationToken)
        {
            if (intervalSeconds <= 0) throw new ArgumentException(nameof(intervalSeconds));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    const string activeSql = @"
                        SELECT Contest_ID FROM Contest
                        WHERE ContestStartDate <= @Now AND ContestEndDate >= @Now;
                    ";

                    var contests = new List<int>();
                    using (var rdr = _repo.ExecuteReader(activeSql, new { Now = DateTime.UtcNow.Date })) 
                    {
                        while (rdr.Read()) contests.Add(rdr.GetInt32(0));
                        rdr.Close();
                    }

                    foreach (var c in contests)
                    {
                        try
                        {
                            GenerateLeaderboard(c);
                        }
                        catch (Exception gex)
                        {
                            Console.Error.WriteLine($"Failed to generate leaderboard for contest {c}: {gex.Message}");
                        }
                    }
                    RecalculateGlobalRanks();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"PeriodicRefresh error: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken).ConfigureAwait(false);
            }
        }

        #region Helper DB wrappers with transaction support

        private int ExecuteNonQueryWithTransaction(string sql, object parameters, IDbTransaction tx)
        {
            var conn = _repo.Connection;
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = tx;

                if (parameters != null)
                {
                    foreach (var prop in parameters.GetType().GetProperties())
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = "@" + prop.Name;
                        var v = prop.GetValue(parameters) ?? DBNull.Value;
                        p.Value = v;
                        cmd.Parameters.Add(p);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }

        private T ExecuteScalarWithTransaction<T>(string sql, object parameters, IDbTransaction tx)
        {
            var conn = _repo.Connection;
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = tx;

                if (parameters != null)
                {
                    foreach (var prop in parameters.GetType().GetProperties())
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = "@" + prop.Name;
                        var v = prop.GetValue(parameters) ?? DBNull.Value;
                        p.Value = v;
                        cmd.Parameters.Add(p);
                    }
                }

                var res = cmd.ExecuteScalar();
                if (res == null || res == DBNull.Value) return default;
                return (T)Convert.ChangeType(res, typeof(T));
            }
        }
        public List<ContestLeaderrBoard> GenerateLeaderboardUsingSql(int contestId)
        {
            const string sqlAgg = @"
        ;WITH Agg AS (
            SELECT ps.PlayerID, SUM(ps.Score) AS TotalPoints
            FROM PlayerScore ps
            WHERE ps.GameID IN (SELECT Game_ID FROM GameDetails WHERE /* some mapping to contest */ 1=1) -- adjust
            GROUP BY ps.PlayerID
        ),
        Ranked AS (
            SELECT PlayerID, TotalPoints,
                   DENSE_RANK() OVER (ORDER BY TotalPoints DESC) AS RankVal
            FROM Agg
        )
        MERGE ContestLeaderBoard AS target
        USING Ranked AS src
          ON target.PlayerID = src.PlayerID AND target.ContestID = @ContestId
        WHEN MATCHED THEN
          UPDATE SET TotalPoints = src.TotalPoints, Rank = src.RankVal
        WHEN NOT MATCHED THEN
          INSERT (PlayerID, ContestID, TotalPoints, Rank)
          VALUES (src.PlayerID, @ContestId, src.TotalPoints, src.RankVal);
    ";
            _repo.ExecuteNonQuery(sqlAgg, new { ContestId = contestId });
            return GetContestLeaderboardRows(contestId);
        }

        public List<ContestLeaderrBoard> GetContestLeaderboardRows(int contestId)
        {
            const string sql = "SELECT CLB_ID, PlayerID, ContestID, TotalPoints, Rank FROM ContestLeaderBoard WHERE ContestID = @ContestId ORDER BY Rank;";
            var outList = new List<ContestLeaderrBoard>();
            using (var rdr = _repo.ExecuteReader(sql, new { ContestId = contestId }))
            {
                while (rdr.Read())
                {
                    outList.Add(new ContestLeaderrBoard
                    {
                        CLB_ID = rdr.GetInt32(0),
                        PlayerID = rdr.GetInt32(1),
                        ContestID = rdr.GetInt32(2),
                        TotalPoints = rdr.GetDecimal(3),
                        Rank = rdr.GetInt32(4)
                    });
                }
            }
            return outList;
        }
        #endregion
    }
}
